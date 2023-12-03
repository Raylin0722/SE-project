from flask import Flask, request
import mysql.connector
import secrets
import hashlib
from datetime import datetime, timedelta
import json
import sys

app = Flask(__name__)

config = {
    'user': 'root',        
    'password': '114SE_project',        
    'database': 'software_engineering',        
    'host': 'localhost',        
    'port': '3306'        
}

# for local debugging with docker
# config = {
#     'user': 'root',        
#     'password': 'password',        
#     'database': 'mysql',        
#     'host': 'localhost',        
#     'port': '3306'        
# }

@app.route('/')
def index():
    return 'index'

@app.route('/register', methods=['GET', 'POST'])
def register():
    username = request.form.get("username")
    password = request.form.get("password")

    msg = ''

    pass_len = len(password)
    if pass_len < 8:
        msg = "1 Password too shord"

    name_check_query = "SELECT username FROM users WHERE username=%s;"

    cnx = mysql.connector.connect(**config)
    cur = cnx.cursor(buffered=True)

    cur.execute(name_check_query, (username,))
    result_num = cur.rowcount

    if result_num != 0:
        msg = '2 User already existed'
        return msg

    token = secrets.token_hex(32) # 256 bits token

    hashed_password = hashlib.sha256(password.encode('utf-8')).hexdigest()

    # user_insert_query = f"INSERT INTO users(username, email, token, hash) VALUES ('{username}', '{email}', '{token}', '{hashed_password}');"
    user_insert_query = "INSERT INTO users(username, token, hash) VALUES (%s, %s, %s);"
    cur.execute(user_insert_query, (username, token, hashed_password))
    cnx.commit()
    msg = '0 User register success\t' + token

    cur.execute("insert into usersdata(updateTime, playerName, token, money, expLevel, expTotal, `character`, lineup, tear, castleLevel, slingshotLevel, clearance, energy, remainTime, volume, backVolume, shock, remind, chestTime, props, faction)"
                "value(now(), %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, now(), %s, %s);", (username, token, 0, 1, 0,'{"1": 1, "2": 1, "3": 1, "4": 1, "5": 1, "6": 0, "7": 0}', '[1, 2, 3, 4, 5, 1]', 0, 1, 1, 
                                                                                                                 '{"1-1": 0, "1-2": 0, "1-3": 0, "1-4": 0, "1-5": 0, "1-6": 0, "2-1": 0, "2-2": 0, "2-3": 0, "2-4": 0, "2-5": 0, "2-6": 0}', 
                                                                                                                 30, 0, 100, 100, True, True, '{"1" : -1, "2" : 0}', "[0, 1, 1, 0, 0, 0]"))
    cnx.commit()

    cur.execute("insert into `rank`(playerName, chapter, level) value(%s, 1, 0)", username)
    cnx.commit()

    cur.close()
    cnx.close()

    return msg

@app.route('/login', methods=['GET', 'POST'])
def login():
    username = request.form.get('username')
    password = request.form.get('password')

    msg = ''

    pass_len = len(password)
    if pass_len < 8:
        msg = "1 Password too shord"

    hashed_password = hashlib.sha256(password.encode('utf-8')).hexdigest()

    name_check_query = "SELECT username, token, hash FROM users WHERE username=%s;"

    cnx = mysql.connector.connect(**config)
    cur = cnx.cursor(dictionary=True)

    cur.execute(name_check_query, (username,))

    result = cur.fetchall()
    result_num = len(result)

    if result_num != 1:
        msg = '3 Either no user with name, or more than one'
        return msg

    queried_hash = result[0]['hash']
    token = secrets.token_hex(32)

    if hashed_password != queried_hash:
        msg = '4 Incorrect password'
    else:
        msg = '0 User login success\t' + token
    
        updateQuery = "UPDATE users SET token=%s WHERE username=%s;"
        cur.execute(updateQuery, (token, username))
        cnx.commit()
        
        updateQuery = "UPDATE usersdata SET token=%s WHERE playerName=%s;"
        cur.execute(updateQuery, (token, username))
        cnx.commit()
    
    cur.close()
    cnx.close()

    return msg

@app.route("/openChest", methods=['GET', 'POST'])
def checkLegal():#判斷是否可開啟寶箱
    #openType = request.args.get('openType')
    #token = request.args.get('token')
    openType = request.form.get('openType')
    token = request.form.get('token')

    openType = True if openType == "True" else False

    cnx = mysql.connector.connect(**config)
    cur = cnx.cursor()

    checkquery = "SELECT token FROM users WHERE token=%s"
    cur.execute(checkquery, (token,))

    result = cur.fetchall()
    returnResult = {"success" : False, "situation" : 0, "get" : False, "character" : -1, "result" : -1}

    if len(result) == 1 and openType: #normal
        timequery = "SELECT money, expLevel, expTotal, tear, props, `character` , chestTime FROM usersdata WHERE token=%s"
        # money 0 expLevel 1 expTotal 2 tear 3 props 4 `character` 5 chestTime 6
        cur.execute(timequery, (token,))
        timeresult = cur.fetchall()

        if len(timeresult) == 1 :
            currentDatetime = datetime.now()
            if timeresult[0][6] <= currentDatetime:
                data = openChest(openType)
                if data['result'] == 0:
                    money = timeresult[0][0] + data["num"]["money"]
                    returnResult['result'] = data['result']
                    cur.execute("update usersdata set money=%s where token=%s;", (money, token,))
                    cnx.commit()

                elif data['result'] == 1:
                    expTotal = timeresult[0][2] + data['num']["exp"]
                    expLevel = timeresult[0][1]
                    if expTotal > 500 * (2.5 ** (expLevel - 1)):
                        expTotal -= 500 * (2.5 ** (expLevel - 1))
                        expLevel += 1
                    cur.execute("update usersdata set expLevel=%s, expTotal=%s where token=%s;", (expLevel, expTotal, token))
                    cnx.commit()

                    returnResult['result'] = data['result']
                elif data['result'] == 2: # OK
                    tear = timeresult[0][3] + data['num']["tear"]
                    returnResult['result'] = data['result']
                    cur.execute("update usersdata set tear=%s where token=%s;", (tear, token))
                    cnx.commit()

                elif data['result'] == 3:
                    props = json.loads(timeresult[0][4])
                    props[str(data["props"])] += 1
                    returnResult['result'] = data['result']
                    props = str(props).replace("'", "\"")
                    cur.execute("update usersdata set props=%s where token=%s;", (str(props), token,))
                    cnx.commit()

                elif data['result'] == 4:
                    print(timeresult[0][5])
                    character = json.loads(timeresult[0][5])
                    if character[str(data["normalCharacter"])] == 0:
                        character[str(data["normalCharacter"])] = 1
                        returnResult['get'] = True
                        character = str(character).replace("'", "\"")
                        cur.execute("update usersdata set `character`=%s where token=%s;", (character, token,))
                        cnx.commit()
                    else:
                        money = timeresult[0][0] + 500
                        returnResult['get'] = False
                        cur.execute("update usersdata set money=%s where token=%s;", (money, token,))
                        cnx.commit()
                    returnResult['result'] = data['result']
                    returnResult['character'] = data["normalCharacter"]
                returnResult["success"] = True

                #currentDatetime += timedelta(hours=72)

                cur.execute("update usersdata set chestTime=%s where token=%s", (currentDatetime, token))
                cnx.commit()

            else:
                returnResult["situation"] = -1
        else:
            returnResult["situation"] = -2
    elif len(result) == 1 and (not openType): # rare
        tearcheck = "SELECT money, expLevel, expTotal, tear, props, `character` FROM usersdata WHERE token='{0}'".format(token) # 要調整
        # money : 0 expLevel : 1 expTotal : 2 tear : 3 props : 4 `character` : 5
        cur.execute(tearcheck)
        tearresult = cur.fetchall()

        if len(tearresult) == 1 and tearresult[0][3] >= 10:# 這裡要調整
            data = openChest(openType)
            tearFinal = tearresult[0][3]

            if data['result'] == 0:
                    money = tearresult[0][0] + data["num"]["money"]
                    returnResult['result'] = data['result']
                    cur.execute("update usersdata set money=%s where token=%s;", (money, token,))
                    cnx.commit()
            elif data['result'] == 1:
                expTotal = tearresult[0][2] + data['num']["exp"]
                expLevel = tearresult[0][1]
                if expTotal > 500 * (2.5 ** (expLevel - 1)):
                    expTotal -= 500 * (2.5 ** (expLevel - 1))
                    expLevel += 1
                returnResult['result'] = data['result']
                cur.execute("update usersdata set expLevel=%s, expTotal=%s where token=%s;", (expLevel, expTotal, token))
                cnx.commit()
            elif data['result'] == 2: # OK
                tear = tearresult[0][3] + data['num']["tear"]
                returnResult['result'] = data['result']
                tearFinal = tear
                cur.execute("update usersdata set tear=%s where token=%s;", (tear, token))
                cnx.commit()
            elif data['result'] == 3:
                props = json.loads(tearresult[0][4])
                props[str(data["props"])] += 3
                returnResult['result'] = data['result']
                props = str(props).replace("'", "\"")
                cur.execute("update usersdata set props=%s where token=%s;", (str(props), token,))
                cnx.commit()
            elif data['result'] == 4:
                character = json.loads(tearresult[0][5])
                if character[str(data["normalCharacter"])] == 0:
                    character[str(data["normalCharacter"])] = 1
                    returnResult['get'] = True
                    character = str(character).replace("'", "\"")
                    cur.execute("update usersdata set `character`=%s where token=%s;", (str(character), token,))
                    cnx.commit()
                else:
                    money = tearresult[0][0] + 500
                    returnResult['get'] = False
                    cur.execute("update usersdata set money=%s where token=%s;", (money, token,))
                    cnx.commit()
                returnResult['result'] = data['result']
                returnResult['character'] = data["normalCharacter"]

            elif data['result'] == 5:
                character = json.loads(tearresult[0][5])
                if character[str(data["rareCharacter"])] == 0:
                    character[str(data["rareCharacter"])] = 1
                    returnResult['get'] = True
                    character = str(character).replace("'", "\"")
                    cur.execute("update usersdata set `character`=%s where token=%s;", (str(character), token,))
                    cnx.commit()
                else:
                    money = tearresult[0][0] + 500
                    returnResult['get'] = False
                    cur.execute("update usersdata set money=%s where token=%s;", (money, token,))
                    cnx.commit()
                returnResult['result'] = data['result']
                returnResult['character'] = data["rareCharacter"]
            returnResult["success"] = True
            tearFinal -= 10
            cur.execute("update usersdata set tear=%s where token=%s", (tearFinal, token))
            cnx.commit()
        else:
            returnResult["situation"] = -1

    else:
        returnResult["situation"] = -2

    cur.close()
    cnx.close()

    return returnResult

def openChest(openType:bool): # openType true : normal openType : false rare
    normalChoice = [50, 85, 90, 95, 100] # 錢 經驗 淚水 道具 普通 50, 85, 90, 95, 100
    normalItemofChest = {
        0 : {"money" : 250},
        1 : {"exp" : 450},
        2 : {"tear" : 2},
        3 : {"props" : [2]},
        4 : {"normalCharacter" : [1, 4, 5]}
    }

    rareChoice = [40, 70, 80, 85, 90, 100] # 錢 經驗 淚水 道具 普通 稀有 40, 70, 80, 85, 90, 100
    rareItemofChest = {
        0 : {"money" : 350},
        1 : {"exp" : 900},
        2 : {"tear" : 5},
        3 : {"props" : [2]}, # 1 冷風 2 炸彈
        4 : {"normalCharacter" : [1, 4, 5]}, # 1 天使 2 小小人 3 肌肉男 4 沒穿衣服 5 小女孩 6 蝸哞 7 工程師 未決定哪個是稀有哪個是普通
        5 : {"rareCharacter" : [2, 3, 6, 7]}
    }

    choice = secrets.randbelow(100)

    for i in range(5 if openType else 6):
        if choice < (normalChoice[i] if openType else rareChoice[i]):
            choice = i
            resultofOpen = (normalItemofChest[i] if openType else rareItemofChest[i])
            break

    if choice < 3: #錢 經驗 淚水 直接回傳
        return {"result" : choice, "num" : resultofOpen}
    elif choice == 3: #道具
        return {"result" : choice, "props" : secrets.choice(resultofOpen["props"])}
    elif choice == 4: #角色
        return {"result" : choice, "normalCharacter" : secrets.choice(resultofOpen["normalCharacter"])}
    elif (not openType) and choice == 5:
        return {"result" : choice, "rareCharacter" :  secrets.choice(resultofOpen["rareCharacter"])}

@app.route("/updateData", methods=['GET', 'POST'])
def updateData():
    token = request.form.get("token")
    #token = request.args.get("token")

    #print(token)

    data = {
            "success": False,
            "token": None,
            "money": None,
            "exp" : None, # 前為等級 後為total
            "character": None,
            "lineup": None,
            "tear": None,
            "castleLevel": None,
            "slingshotLevel": None,
            "clearance": None,
            "energy": None,
            "remainTime": None,
            "updateTime": None,
            "volume": None,
            "backVolume": None,
            "shock": None,
            "remind": None,
            "chestTime": None,
            "faction": None,
            "props": None,
            "updateTime": None
    }

    cnx = mysql.connector.connect(**config)
    cur = cnx.cursor()

    checkquery = "SELECT token FROM users WHERE token=%s;"
    searchquery = "SELECT * FROM usersdata WHERE token=%s;"
    updateTimequery = "UPDATE usersdata SET updateTime=NOW(), energy=%s, remainTime=%s WHERE token=%s;"
    cur.execute(checkquery, (token,))

    result = cur.fetchall()
    result_num = len(result)

    if(result_num == 1):
        cur.execute(searchquery, (token,))
        result = cur.fetchall()
        if(len(result) == 1):
            timeDiff = int((datetime.now() - result[0][0]).total_seconds())
            if  timeDiff >= 0:
                energy = result[0][12]
                remainTime = result[0][13]
                if energy < 30:
                    energy += timeDiff // 1200
                    remainTime += timeDiff % 1200
                    energy += remainTime // 1200
                    remainTime %= 1200
                    if energy >= 30:
                        energy = 30
                        remainTime = 0
                print(result)
                data["success"]  = True
                data["updateTime"] = datetime.now().strftime('%Y-%m-%d %H:%M:%S')
                data["token"] = result[0][2]
                data["money"] = result[0][3]
                data["exp"] = [result[0][4], result[0][5]]
                data["character"] =  [value for key, value in json.loads(result[0][6]).items()]
                data["lineup"] = json.loads(result[0][7])
                data["tear"] = result[0][8]
                data["castleLevel"] = result[0][9]
                data["slingshotLevel"] = result[0][10]
                data["clearance"] = [value for key, value in json.loads(result[0][11]).items()]
                data["energy"] = energy
                data["remainTime"] = remainTime
                data["volume"] = result[0][14]
                data["backVolume"] = result[0][15]
                data["shock"] = result[0][16]
                data["remind"] = result[0][17]
                data["chestTime"] = result[0][18].strftime('%Y-%m-%d %H:%M:%S')
                data["props"] = [value for key, value in json.loads(result[0][19]).items()]
                data["faction"] = json.loads(result[0][20])
                cur.execute(updateTimequery, (energy, remainTime, token,))
                cnx.commit()

    cur.close()
    cnx.close()

    return data

@app.route("/updateCard", methods=['GET', 'POST'])
def updateCard():
    token = request.form.get('token')
    target = int(request.form.get("target"))
    mode = int(request.form.get("mode"))

    returnResult = False

    cnx = mysql.connector.connect(**config)
    cur = cnx.cursor()

    checkquery = "SELECT token FROM users WHERE token=%s"
    cur.execute(checkquery, (token,))
    result = cur.fetchall()

    updateMoney = [[400, 300, 700, 600, 500, 650, 750], 1200]

    if len(result) == 1:
        moneyquery = "SELECT money, `character`, castleLevel, slingshotLevel FROM usersdata WHERE token=%s"
        cur.execute(moneyquery, (token,))
        moneyResult = cur.fetchall()
        if len(moneyResult) == 1:
            # 這邊需要升級時所需的經驗值 還要區分是角色(應該有id 1-7 mode 1)  還是彈弓主堡 (mode 2) 存在陣列 target 0 mode 1 目標
            if mode == 0:
                character = json.loads(moneyResult[0][1])
                orginLevel = character[str(target)]
                needMoney = updateMoney[mode][target - 1] * (1.5 ** (orginLevel -1))
                if moneyResult[0][0] >= needMoney and orginLevel < 5 and orginLevel >= 1:
                    money = moneyResult[0][0] - needMoney
                    character[str(target)] += 1
                    cur.execute("update usersdata set money=%s, `character`=%s where token=%s;", (money, str(character).replace("'", "\""), token))
                    cnx.commit()
                    returnResult = True
            elif mode == 1:
                orginLevel = moneyResult[0][2]
                print(updateMoney[mode])
                needMoney = updateMoney[mode] + (500 * (orginLevel - 1))
                castleLevel = moneyResult[0][2]
                slingshotLevel = moneyResult[0][3]
                if moneyResult[0][0] >= needMoney and (slingshotLevel < 15 and castleLevel < 15):
                    money = moneyResult[0][0] - needMoney
                    castleLevel += 1
                    slingshotLevel += 1
                    cur.execute("update usersdata set money=%s, castleLevel=%s, slingshotLevel=%s where token=%s;", (money, castleLevel, slingshotLevel, token))
                    cnx.commit()
                    returnResult = True

    cur.close()
    cnx.close()

    return str(returnResult)

@app.route("/updateLineup", methods=['GET', 'POST'])
def updateLineup():
    token = request.form.get('token')
    lineup = request.form.get('lineup')

    lineupArr = json.loads(lineup.replace("'", "\""))

    returnResult = False

    cnx = mysql.connector.connect(**config)
    cur = cnx.cursor()

    checkquery = "SELECT token FROM users WHERE token=%s"
    cur.execute(checkquery, (token,))
    result = cur.fetchall()

    if len(result) == 1:
        cur.execute("select `character`, props, lineup where token=%s", (token,))
        lineupResult = cur.fetchall()

        if len(lineupArr) == 6 and len(lineupResult) == 1:
            character = json.loads(lineupResult[0][0])
            props = json.loads(lineupResult[0][1])
            for i in range(5):
                if character[str(lineupArr[i])] != 1:
                    cur.close()
                    cnx.close()
                    return returnResult

            if lineupArr[5] == 2 and props['2'] <= 0 :
                cur.close()
                cnx.close()
                return returnResult

            cur.execute("update usersdata set lineup=%s where token=%s", (lineupArr, token))
            cnx.commit()

            returnResult = True

    cur.close()
    cnx.close()

    return str(returnResult)

@app.route("/beforeGame", methods=['GET', 'POST'])
def beforeGame():
    token = request.form.get('token')
    cnx = mysql.connector.connect(**config)
    cur = cnx.cursor()

    #token = request.args.get('token')

    success = False
    checkQuery = "select token from users where token=%s;"
    energyQuery = "select energy, updateTime, remainTime from usersdata where token=%s;"
    updateQuery = "update usersdata set updateTime=%s, energy=%s, remaindTime=%s where token=%s;"

    cur.execute(checkQuery, (token,))
    result = cur.fetchall()
    if len(result) == 1:
        cur.execute(energyQuery, (token,))
        energyResult = cur.fetchall()
        print(energyResult)
        if len(energyResult) == 1:
            if energyResult[0][0] >= 5:
                timeDiff = int((datetime.now() - energyResult[0][1]).total_seconds())
                energy = energyResult[0][0]
                remainTime = energyResult[0][2]
                if energy < 30:
                    energy += timeDiff // 1200
                    remainTime += timeDiff % 1200
                    energy += remainTime // 1200
                    remainTime %= 1200
                    if energy >= 30:
                        energy = 30
                        remainTime = 0

                energy -= 5

                cur.execute(updateQuery, (datetime.now(), energy, remainTime, token))
                cnx.commit()
                success = True

    cur.close()
    cnx.close()

    return str(success)

@app.route("/afterGame", methods=['GET', 'POST'])
def afterGame():
    leveltoMoneyExp = [[500, 500, 500, 500, 500, 600, 600, 600, 600, 600, 600, 700], [300, 320, 340, 360, 380, 400, 400, 420, 440, 460, 480, 500]]
    success = False
    token = request.form.get('token')
    clear = request.form.get('clear')
    target = request.form.get('target')

    chapter, level = target.split('-')
    chapter = int(chapter)
    level = int(level)

    cnx = mysql.connector.connect(**config)
    cur = cnx.cursor()
    checkQuery = "select token from users where token=%s;"
    clearQuery = "select money, expLevel, expTotal, clearance, tear, playerName, faction from usersdata where token=%s"
    updateQuery = "update usersdata set money=%s, expLevel=%s, expTotal=%s, clearance=%s, updateTime=%s, tear=%s, faction=%s where token=%s;"
    rankQuery = "update `rank` set chapter=%s, level=%s where playerName=%s"

    if clear == 'True':
        cur.execute(checkQuery, (token,))
        result = cur.fetchall()

        if len(result) == 1:
            cur.execute(clearQuery, (token,))
            clearResult = cur.fetchall()
            if len(clearResult) == 1:
                clearance = json.loads(clearResult[0][3])
                tear = clearResult[0][4]
                faction = json.loads(clearResult[0][6])
                if clearance[target] == 0:
                    tear += 2 if level >= 1 and level <= 5 else 3
                    if level == 6:
                        faction[chapter + 3] = 1
                
                
                clearance[target] += 1

                money = clearResult[0][0] + (leveltoMoneyExp[0][-1 + level if chapter == 1 else 5 + level]) * (2 ** -(clearance[target] - 1))
                expTotal = clearResult[0][2] + (leveltoMoneyExp[1][-1 + level if chapter == 1 else 5 + level]) * (2 ** -(clearance[target] - 1))
                expLevel = clearResult[0][1]

                if expTotal > 500 * (2.5 ** (expLevel - 1)):
                        expTotal -= 500 * (2.5 ** (expLevel - 1))
                        expLevel += 1

                clearance = str(clearance).replace("'", "\"")
                faction = str(faction).replace("'", "\"")
                #print(updateQuery %(money, expLevel, expTotal, clearance, datetime.now(), tear, token))
                cur.execute(updateQuery, (money, expLevel, expTotal, clearance, datetime.now(), tear, faction, token))
                cnx.commit()

                cur.execute(rankQuery, (chapter, level, result[0][5]))
                cnx.commit()

                success = True

    cur.close()
    cnx.close() 
    return str(success)

@app.route("/updateRank", methods=['get', 'post'])
def updateRank():
    cnx = mysql.connector.connect(**config)
    cur = cnx.cursor()

    cur.execute("select * from `rank` order by chapter desc, `level` desc;")
    result = cur.fetchall()

    print(result)

    RankName = []
    RankClear = []
    for i in range(len(result)):
        RankName.append(result[i][0])
        RankClear.append("%s-%s" %result[i][1], result[i][2])


    returnRank = {"RankName": RankName, "RankClear" : RankClear}

    cur.close()
    cnx.close() 
    return returnRank

@app.route("/updateFaction", methods=['get', 'post'])
def updateFaction():
    cnx = mysql.connector.connect(**config)
    cur = cnx.cursor()

    target = int(request.form.get('target'))
    token = request.form.get("token")

    resultReturn = False

    cur.execute("selcet faction from usersdata where token=%s", (token,))
    result = cur.fetchall()
    if len(result) == 1:
        faction = json.loads(result[0][0])
        faction[1] = target
        faction[0] = 0

        cur.execute("update usersdata set faction=%s where token=%s", (faction, token))
        cnx.commit()
        resultReturn = True
    
    cur.close()
    cnx.close() 
    return str(resultReturn)

@app.route("/initFaction", methods=['get', 'post'])
def initFaction():
    target = int(request.form.get('target'))
    token = request.form.get("token")

    cnx = mysql.connector.connect(**config)
    cur = cnx.cursor()

    resultReturn = False

    cur.execute("selcet faction from usersdata where token=%s", (token,))
    result = cur.fetchall()
    if len(result) == 1:
        faction = json.loads(result[0][0])
        if faction[0] != 0:
            faction[0] = 1
            faction[1] = target
            faction[target] = 1

            cur.execute("update usersdata set faction=%s where token=%s", (faction, token))
            cnx.commit()
            resultReturn = True
    
    cur.close()
    cnx.close() 
    return str(resultReturn)

@app.route("/addFriend", methods=['get', 'post']) #加到申請名單
def addFriend():
    friendName = request.form.get("friendName")
    self = request.form.get("self")
    insertCheckQuery = "insert into needcheckfriend(owner, friend) value(%s, %s);"
    checkUserExist = "select username from users where username=%s;"

    cnx = mysql.connector.connect(**config)
    cur = cnx.cursor()

    cur.execute(checkUserExist, (self,)) #檢查申請人是否存在
    checkSelf = cur.fetchall()

    cur.execute(checkUserExist, (friendName,)) #檢查被申請人是否存在
    checkFriend = cur.fetchall()
    
    resultReturn = False

    if len(checkSelf) == 1 and len(checkFriend) == 1:
        cur.execute(insertCheckQuery, (self, friendName))
        cnx.commit()

        resultReturn = True

    cur.close()
    cnx.close() 
    
    return str(resultReturn)

@app.route("/deletFriend", methods=['get', 'post']) #從好友名單刪除
def deleteFriend():
    friendName = request.form.get("friendName")
    self = request.form.get("self")
    deletedQuery = "delete from friends where owner=%s and friend=%s;"
    checkExistQuery = "select * from friends where owner=%s and friend=%s;"
    
    cnx = mysql.connector.connect(**config)
    cur = cnx.cursor()
    
    cur.execute(checkExistQuery, (self, friendName))
    checkSelf = cur.fetchall()
    
    cur.execute(checkExistQuery, (friendName, self))
    checkFriend = cur.fetchall()
    
    resultReturn = False
    
    if len(checkSelf) == 1 and len(checkFriend) == 1:
        cur.execute(deletedQuery, (self, friendName))
        cur.execute(deletedQuery, (friendName, self))
        
        cnx.commit()
        resultReturn = True
    
    cur.close()
    cnx.close() 
    
    return str(resultReturn)

@app.route("/acceptFriend", methods=['get', 'post']) #接受好友
def acceptFriend():
    friendName = request.form.get("friendName")
    self = request.form.get("self")
    friendQuery = "select * from needcheckfriend where owner=%s and friend=%s;"
    deletedCheck = "delete from needcheckfriend where owner=%s and friend=%s;"
    insertFriend = "insert into friends(owner, friend) value(%s, %s);"

    cnx = mysql.connector.connect(**config)
    cur = cnx.cursor()

    cur.execute(friendQuery, (friendName, self))
    friendResult = cur.fetchall()

    resultReturn = False

    if len(friendResult) == 1:
        cur.execute(deletedCheck, (friendName, self))
        cur.execute(insertFriend, (self, friendName))
        cur.execute(insertFriend, (friendName, self))
        cnx.commit()

        resultReturn = True


    cur.close()
    cnx.close() 

    return str(resultReturn)

@app.route("/rejectFriend", methods=['get', 'post']) #拒絕好友
def rejectFriend():
    friendName = request.form.get("friendName")
    self = request.form.get("self")
    friendQuery = "select * from needcheckfriend where owner=%s and friend=%s;"
    deletedCheck = "delete from needcheckfriend where owner=%s and friend=%s;"

    cnx = mysql.connector.connect(**config)
    cur = cnx.cursor()

    cur.execute(friendQuery, (friendName, self))
    friendResult = cur.fetchall()

    resultReturn = False

    if len(friendResult) == 1:
        cur.execute(deletedCheck, (friendName, self))
        cnx.commit()
        resultReturn = True

    cur.close()
    cnx.close() 

    return str(resultReturn)

@app.route("/getFriendEnergy", methods=['get', 'post']) #拿體力
def getFriendEnergy():
    friendName = request.form.get("friendName")
    self = request.form.get("self")
    token = request.form.get("token")
    checkExist = "select * from friends where owner=%s and friend=%s;"
    energyCheckQuery = "select * from energy where owner=%s and friend=%s;"
    deleteEnergyQuery = "delete from energy where owner=%s and friend=%s;"
    energyGetQuery = "update usersdata set energy=%s, remainTime=%s where token=%s"
    searchEnergy = "select energy, remainTime from usersdata where token=%s;"
    
    cnx = mysql.connector.connect(**config)
    cur = cnx.cursor()

    resultReturn = False

    cur.execute(checkExist, (self, friendName))
    checkSelf = cur.fetchall()
    
    cur.execute(checkExist, (friendName, self))
    checkFriend = cur.fetchall()
    
    cur.execute(energyCheckQuery, (friendName, self))
    checkEnergy = cur.fetchall()
    
    if len(checkSelf) == 1 and len(checkFriend) == 1 and len(checkEnergy) == 1:
        cur.execute(searchEnergy, (token,))
        result = cur.fetchall()
        if len(result) == 1:
            energy = result[0][0]
            remainTime = result[0][1]
            energy += 5
            if energy >= 30:
                energy = 30
                remainTime = 0
            
            cur.execute(energyGetQuery, (energy, remainTime, token))
            cur.execute(deleteEnergyQuery, (friendName, self))
            cnx.commit()
            resultReturn = True
            
            

    cur.close()
    cnx.close() 

    return str(resultReturn)

@app.route("/sendFriendEnergy", methods=['get', 'post']) #送體力
def sendFriendEnergy():
    friendName = request.form.get("friendName")
    self = request.form.get("self")
    
    checkExist = "select * from friends where owner=%s and friend=%s;"
    sendEnergyQuery = "insert into energy(owner, friend) value(%s, %s);"
    energyCheckQuery = "select * from energy where owner=%s and friend=%s;"
    
    cnx = mysql.connector.connect(**config)
    cur = cnx.cursor()

    cur.execute(checkExist, (self, friendName))
    checkSelf = cur.fetchall()
    
    cur.execute(checkExist, (friendName, self))
    checkFriend = cur.fetchall()

    cur.execute(energyCheckQuery, (friendName, self))
    checkEnergy = cur.fetchall()

    resultReturn = False

    if len(checkSelf) == 1 and len(checkFriend) == 1 and len(checkEnergy) == 0:
        cur.execute(sendEnergyQuery, (self, friendName))
        cnx.commit()
        resultReturn = True

    cur.close()
    cnx.close() 

    return str(resultReturn)

@app.route("/blackListFriend", methods=['get', 'post']) #黑名單
def blackListFriend():
    friendName = request.form.get("friendName")
    self = request.form.get("self")
    checkFriendQery = "select * from friends where owner=%s and friend=%s;"
    checkNeedCheck = "select * from needcheckfriend where owner=%s and friend=%s;"
    checkEnergyQuery = "select * from energy where owner=%s and friend=%s;"
    doBlackList = "insert into blacklist(owner, friend) value(%s, %s);"
    
    cnx = mysql.connector.connect(**config)
    cur = cnx.cursor()
    resultReturn = True
    
    
    cur.execute(checkFriendQery, (self, friendName))
    result = cur.fetchall()
    if len(result) > 0:
        try:
            cur.execute("delete from friends where owner=%s and friend=%s;", (self, friendName))
        except:
            resultReturn = False
        
    cur.execute(checkFriendQery, (friendName, self))
    result = cur.fetchall()
    if len(result) > 0:
        try:
            cur.execute("delete from friends where owner=%s and friend=%s;", (friendName, self))
        except:
            resultReturn = False
        
    cur.execute(checkNeedCheck, (self, friendName))
    result = cur.fetchall()
    if len(result) > 0:
        try:
            cur.execute("delete from needcheckfriend where owner=%s and friend=%s;", (self, friendName))
        except:
            resultReturn = False
        
    cur.execute(checkNeedCheck, (friendName, self))
    result = cur.fetchall()
    if len(result) > 0:
        try:
            cur.execute("delete from needcheckfriend where owner=%s and friend=%s;", (friendName, self))
        except:
            resultReturn = False
        
    cur.execute(checkEnergyQuery, (self, friendName))
    result = cur.fetchall()
    if len(result) > 0:
        try:
            cur.execute("delete from energy where owner=%s and friend=%s;", (self, friendName))
        except:
            resultReturn = False
        
        
    cur.execute(checkEnergyQuery, (friendName, self))
    result = cur.fetchall()
    if len(result) > 0:
        try:
            cur.execute("delete from energy where owner=%s and friend=%s;", (friendName, self))
        except:
            resultReturn = False
    
    cur.execute(doBlackList, (self,friendName))
    cur.execute(doBlackList, (friendName, self))
    
    cnx.commit()
    cur.close()
    cnx.close() 

    return str(resultReturn)

@app.route("/cancelBlackList", methods=['get', 'post'])
def cancelBlackList():
    self= request.form.get("self")
    friendName = request.form.get("friendName")
    
    checkExistQuery = "select * from blacklist where owner=%s and friend=%s;"
    cancelBlackQuery = "delete from blacklist where owner=%s and friend=%s;"
    
    cnx = mysql.connector.connect(**config)
    cur = cnx.cursor()
    resultReturn = True
    
    cur.execute(checkExistQuery, (self, friendName))
    checkSelf = cur.fetchall()
    
    cur.execute(checkExistQuery, (friendName, self))
    checkFriend = cur.fetchall()
    
    if len(checkSelf) == 1 and len(checkFriend) == 1:
        cur.execute(cancelBlackQuery, (self, friendName))
        cur.execute(cancelBlackQuery, (friendName, self))
        cnx.commit()
        resultReturn = True
  
    cur.close()
    cnx.close() 

    return str(resultReturn)
  
@app.route("/updateFriend", methods=['get', 'post']) # 更新好友名單 領體力名單
def updateFriend():
    self = request.form.get("self")
    
    friendsSearch = "select friend from friends where owner=%s;"
    needCheckSearch = "select owner from needcheckfriend where friend=%s;"
    blackListSearch = "select friend from blacklist where owner=%s;"
    energySearch = "select owner from energy where friend=%s;"
    waitAcceptSearch = "select owner from needcheckfriend where owner=%s;"
    checkExist = "select username from users where username=%s;"
    
    friends = [], needCheck = [], blackList = [], energyGet = [], energySend = [], waitAccept = []
    
    cnx = mysql.connector.connect(**config)
    cur = cnx.cursor()

    resultReturn = {"success": False, "friends" : None, "needCheck" : None, "blackList" : None, "energyGet" : None, "energySend" : None, "waitAccept" : None}
    
    cur.execute(checkExist, (self,))
    result = cur.fetchall()
    
    if len(result) == 1:
        cur.execute(friendsSearch, (self,))
        resultFriend = cur.fetchall()
        cur.execute(needCheckSearch, (self,))
        resultNeedCheck = cur.fetchall()
        cur.execute(blackListSearch, (self,))
        resultBlackList = cur.fetchall()
        cur.execute(energySearch, (self,))
        resultEnergyGet = cur.fetchall()
        cur.execute(waitAcceptSearch, (self,))
        resultWaitAccept = cur.fetchall()
        cur.execute("select friend from energy where owner=%s;", (self,))
        resultEnergySend = cur.fetchall()
        
        
        
        
        for i in range(max(len(resultFriend), len(resultNeedCheck), len(resultBlackList), len(resultEnergyGet), len(resultWaitAccept), len(resultEnergySend))):
            if i < len(resultFriend): # 好友
                friends.append(resultFriend[i][0])
            
            if i < len(resultNeedCheck): # 待同意的好友申請
                needCheck.append(resultNeedCheck[i][0])
                
            if i < len(resultWaitAccept): # 等待回覆的好友申請
                waitAccept.append(resultWaitAccept[i][0])
            
            if i < len(resultBlackList): # 黑名單
                blackList.append(resultBlackList[i][0])
            
            if i < len(resultEnergyGet): # 待領取的體力
                energyGet.append(resultEnergyGet[i][0])
            
            if i < len(resultEnergySend): # 待領取的體力
                energySend.append(resultEnergySend[i][0])
                
        resultReturn["friends"] = friends
        resultReturn["needCheck"] = needCheck
        resultReturn["blackList"] = blackList
        resultReturn["waitAccept"] = waitAccept
        resultReturn["energyGet"] = energyGet
        resultReturn["energySend"] = energySend
        resultReturn["success"] = True
        
    cur.close()
    cnx.close()
    
    return resultReturn
  
    
@app.route("/topUp", methods=['get', 'post']) # 儲值
def topUp(): 
    token = request.form.get()
    
if __name__ == "__main__":
    app.run(debug=True, port=5000)
    

