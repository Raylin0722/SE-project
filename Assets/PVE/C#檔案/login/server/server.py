from flask import Flask, request
import mysql.connector
import secrets
import hashlib
from datetime import datetime, timedelta
import json


app = Flask(__name__)

config = {
    'user': 'root',        
    'password': '114SE_project',        
    'database': 'software_engineering',        
    'host': 'localhost',        
    'port': '3306'        
}

@app.route('/')
def index():
    return 'index'

@app.route('/register', methods=['GET', 'POST'])
def register():
    username = request.form.get("username")
    email    = request.form.get("email")
    password = request.form.get("password")


    # name_check_query = f"SELECT username FROM users WHERE username='{username}' OR email='{email}';"
    name_check_query = "SELECT username FROM users WHERE username=%s OR email=%s;".format(username=username, email=email)

    msg = ''

    cnx = mysql.connector.connect(**config)
    cur = cnx.cursor(buffered=True)

    cur.execute(name_check_query, (username, email))
    result_num = cur.rowcount

    if result_num != 0:
        msg = 'User or email already existed'
        return msg

    token = secrets.token_hex(32) # 256 bits token

    hashed_password = hashlib.sha256(password.encode('utf-8')).hexdigest()

    # user_insert_query = f"INSERT INTO users(username, email, token, hash) VALUES ('{username}', '{email}', '{token}', '{hashed_password}');"
    user_insert_query = "INSERT INTO users(username, email, token, hash) VALUES (%s, %s, %s, %s);"
    cur.execute(user_insert_query, (username, email, token, hashed_password))
    cnx.commit()
    msg = 'User register success'

    cur.execute("insert into usersdata(updateTime, playerName, token, money, expLevel, expTotal, `character`, lineup, tear, castleLevel, slingshotLevel, clearance, energy, remainTime, volume, backVolume, shock, remind, chestTime, props, faction)"
                "value(now(), %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, now(), %s, %s);", (username, token, 0, 1, 0,'{"1": 1, "2": 1, "3": 1, "4": 1, "5": 1, "6": 0, "7": 0}', [1, 2, 3, 4, 5, 1], 0, 1, 1, 
                                                                                                                 '{"1-1": 0, "1-2": 0, "1-3": 0, "1-4": 0, "1-5": 0, "1-6": 0, "2-1": 0, "2-2": 0, "2-3": 0, "2-4": 0, "2-5": 0, "2-6": 0}', 
                                                                                                                 30, 0, 100, 100, True, True, '{"1" : -1, "2" : 0}', -1))
    cnx.commit()

    cur.execute("insert into `rank`(playerName, chapter, level) value(%s, 1, 0)", username)
    cnx.commit()

    cur.close()
    cnx.close()

    return msg

@app.route('/login', methods=['GET', 'POST'])
def login():
    username = request.form.get('username')
    email    = request.form.get('email')
    password = request.form.get('password')

    hashed_password = hashlib.sha256(password.encode('utf-8')).hexdigest()

    name_check_query = "SELECT username, token, hash FROM users WHERE username=%s OR email=%s;"
    
    msg = ''

    cnx = mysql.connector.connect(**config)
    cur = cnx.cursor(dictionary=True)

    cur.execute(name_check_query, (username, email))

    result = cur.fetchall()
    result_num = len(result)

    if result_num != 1:
        msg = '{} Either no user with name, or more than one'.format(result_num)
        return msg

    queried_hash = result[0]['hash']
    token = secrets.token_hex(32)
    

    if hashed_password != queried_hash:
        msg = 'Incorrect password'
    else:
        msg = '0 User login success\t{token}'.format(token=token)
    
    updateQuery = "UPDATE users SET token=%s WHERE username=%s;"
    cur.execute(updateQuery, (token, username))
    cnx.commit()
    
    updateQuery = "UPDATE usersdata SET token=%s WHERE username=%s;"
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
                
                currentDatetime += timedelta(hours=72)
                
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

    print(token)

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
            if  timeDiff != 0:
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
                data["faction"] = result[0][20] 
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
    clearQuery = "select money, expLevel, expTotal, clearance, tear, playerName from usersdata where token=%s"
    updateQuery = "update usersdata set money=%s, expLevel=%s, expTotal=%s, clearance=%s, updateTime=%s, tear=%s where token=%s;"
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
                if clearance[target] == 0:
                    tear += 2 if level >= 1 and level <= 5 else 3
                
                
                clearance[target] += 1

                money = clearResult[0][0] + (leveltoMoneyExp[0][-1 + level if chapter == 1 else 5 + level]) * (2 ** -(clearance[target] - 1))
                expTotal = clearResult[0][2] + (leveltoMoneyExp[1][-1 + level if chapter == 1 else 5 + level]) * (2 ** -(clearance[target] - 1))
                expLevel = clearResult[0][1]

                if expTotal > 500 * (2.5 ** (expLevel - 1)):
                        expTotal -= 500 * (2.5 ** (expLevel - 1))
                        expLevel += 1
                        
                             
            
                clearance = str(clearance).replace("'", "\"")
                print(updateQuery %(money, expLevel, expTotal, clearance, datetime.now(), tear, token))
                cur.execute(updateQuery, (money, expLevel, expTotal, clearance, datetime.now(), tear, token))
                cnx.commit()
                
                cur.execute(rankQuery, (chapter, level, result[0][5]))
                cnx.commit()
                
                success = True
    return str(success)

@app.route("/updateRank", methods=['get', 'post'])
def updateRank():
    cnx = mysql.connector.connect(**config)
    cur = cnx.cursor()
    mode = int(request.form.get('mode'))
    
    cur.execute("select * from `rank` order by chapter desc, `level` desc;")
    result = cur.fetchall()

    print(result)

    Rank = []
    for i in range(len(result)):
        Rank.append(result[i][0] if mode == 1 else "%s-%s" %(result[i][1], result[i][2]))

    cur.close()
    cnx.close()

    returnRank = {"data": Rank}

    print(returnRank)

    return returnRank

'''@app.route("/addFriend", methods=['get', 'post'])
def addFriend():
    ()

@app.route("getEnergy", methods=['get', 'post'])
def getEnergy():
    ()'''


if __name__ == "__main__":
    app.run(debug=True, port=5000)
    

