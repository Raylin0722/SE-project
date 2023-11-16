from flask import Flask, request
import mysql.connector
import secrets
import hashlib
from datetime import datetime, timedelta
import json


app = Flask(__name__)

config = {
    'user': 'root',        
    'password': 'test',        
    'database': 'SE_project',        
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
    returnResult = {"sucess" : False, "situation" : 0}
    
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
                returnResult["sucess"] = True     
                
                currentDatetime += timedelta(hours=72)
                
                cur.execute("update usersdata set chestTime=%s where token=%s", (currentDatetime, token))
                cnx.commit()

            else:
                returnResult["situation"] = ""
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
            returnResult["sucess"] = True
            tearFinal -= 10
            cur.execute("update usersdata set tear=%s where token=%s", (tearFinal, token))
            cnx.commit()

    else:
        returnResult["situation"] = -1
    
    
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
    updateTimequery = "UPDATE usersdata SET updateTime=NOW() WHERE token=%s;"
    cur.execute(checkquery, (token,))

    result = cur.fetchall()
    result_num = len(result)
    
    
    if(result_num == 1):
        cur.execute(searchquery, (token,))
        result = cur.fetchall()
        if(len(result) == 1):
            '''create table usersdata(
                updateTime datetime, 
                playerName varchar(50),
                token  varchar(64),
                money integer,
                expLevel integer,
                expTotal integer,
                `character` varchar(200),
                lineup varchar(100),
                tear integer,
                castleLevel integer,
                slingshotLevel integer,
                clearance varchar(200),
                energy integer,
                volume integer,
                backVolume integer,
                shock bool,
                remind bool,
                chestTime datetime,
                faction varchar(200)
            );'''
            if (datetime.now() - result[0][0]).total_seconds() != 0:
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
                data["energy"] = result[0][12]
                data["volume"] = result[0][13]
                data["backVolume"] = result[0][14]
                data["shock"] = result[0][15]
                data["remind"] = result[0][16]
                data["chestTime"] = result[0][17].strftime('%Y-%m-%d %H:%M:%S')
                data["props"] = [value for key, value in json.loads(result[0][18]).items()]
                data["faction"] = result[0][19] 
                cur.execute(updateTimequery, (token,))
                cnx.commit()

    cur.close()
    cnx.close()
    
    return data
    
@app.route("/updateCard", methods=['GET', 'POST'])
def updateCard():
    token = request.form.get('token')
    target = request.form.get("target")
    orginLevel = request.form.get("originLevel")
    mode = request.form.get("mode")

    returnResult = {"success" : False}

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
            if mode == 0 and orginLevel < 5:
                needMoney = updateMoney[mode][target - 1] * (1.2 ** (orginLevel -1))
                if moneyResult[0][0] >= needMoney:
                    money = moneyResult[0][0] - needMoney
                    character = json.loads(moneyResult[0][1])
                    character[str(target)] += 1
                    cur.execute("update usersdata set money=%s, `character`=%s where token=%s;", (money, str(character).replace("'", "\""), token))
                    cnx.commit()
                    returnResult["success"] = True
            elif mode == 1 and orginLevel < 15:
                needMoney = updateMoney[mode] + (500 * orginLevel - 1) # target==1 catleLevel target == 2 slingshotLevel
                if moneyResult[0][0] >= needMoney:
                    money = moneyResult[0][0] - needMoney
                    castleLevel = moneyResult[0][2] + 1 if target == 1 else moneyResult[0][2]
                    slingshotLevel = moneyResult[0][3] + 1 if target == 2 else moneyResult[0][3] 
                    cur.execute("update usersdata set money=%s, castleLevel=%s, slingshotLevel=%s where token=%s;", (money, castleLevel, slingshotLevel, token))
                    cnx.commit()
                    returnResult["success"] = True
                

    cur.close()
    cnx.close() 
    return returnResult


if __name__ == "__main__":
    app.run(debug=True)
