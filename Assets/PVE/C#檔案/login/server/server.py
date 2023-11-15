from flask import Flask, request
import mysql.connector
import secrets
import hashlib
from datetime import datetime
import json


app = Flask(__name__)

config = {
    'user': 'root',        
    'password': 'gpgpr87693',        
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
    name_check_query = "SELECT username FROM users WHERE username='{username}' OR email='{email}';".format(username=username, email=email)

    msg = ''

    cnx = mysql.connector.connect(**config)
    cur = cnx.cursor(buffered=True)

    cur.execute(name_check_query)
    result_num = cur.rowcount

    if result_num != 0:
        msg = 'User or email already existed'
        return msg

    token = secrets.token_hex(32) # 256 bits token

    hashed_password = hashlib.sha256(password.encode('utf-8')).hexdigest()

    # user_insert_query = f"INSERT INTO users(username, email, token, hash) VALUES ('{username}', '{email}', '{token}', '{hashed_password}');"
    user_insert_query = "INSERT INTO users(username, email, token, hash) VALUES ('{username}', '{email}', '{token}', '{hashed_password}');".format(username=username, email=email, token=token, hashed_password=hashed_password)
    print(user_insert_query)
    cur.execute(user_insert_query)
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

    name_check_query = "SELECT username, token, hash FROM users WHERE username='{username}' OR email='{email}';".format(username=username, email=email)
    
    msg = ''

    cnx = mysql.connector.connect(**config)
    cur = cnx.cursor(dictionary=True)

    cur.execute(name_check_query)

    result = cur.fetchall()
    result_num = len(result)

    if result_num != 1:
        msg = '{} Either no user with name, or more than one'.format(result_num)
        return msg

    queried_hash = result[0]['hash']
    token = secrets.token_hex(32)
    
    print(token)

    if hashed_password != queried_hash:
        msg = 'Incorrect password'
    else:
        msg = '0 User login success\t{token}'.format(token=token)
    
    updateQuery = "UPDATE users SET token='{0}' WHERE username='{1}';".format(token, username)
    print(updateQuery)
    cur.execute(updateQuery)
    cnx.commit()
    
    cur.execute(name_check_query)
    result = cur.fetchall()
    print(result)
    
    cur.close()
    cnx.close()

    return msg

@app.route("/openChest", methods=['GET', 'POST'])
def checkLegal():#判斷是否可開啟寶箱
    openType = request.args.get('openType')
    token = request.args.get('token')
    #openType = request.form.get('openType')
    #token = request.form.get('token')
    
    cnx = mysql.connector.connect(**config)
    cur = cnx.cursor()
    
    checkquery = "SELECT token FROM users WHERE token='{0}'".format(token)
    cur.execute(checkquery)

    result = cur.fetchall()

    returnResult = {"sucess" : False}
    

    if len(result) == 1 and openType: #normal
        timequery = "SELECT money, expLevel, expTotal, tear, props, `character` , chestTime FROM usersdata WHERE token='{0}'".format(token) 
        # money : 0 expLevel : 1 expTotal : 2 character : 3 tear : 4 chestTime : 5 props : 6
        cur.execute(timequery)
        timeresult = cur.fetchall()
        if len(timeresult) == 1 :
            currentDatetime = datetime.now()
            print(timeresult[0][6] <= currentDatetime)
            if timeresult[0][6] <= currentDatetime:
                data = openChest(openType)
                if data['result'] == 0:
                    money = timeresult[0][0] + data["num"]["money"]
                elif data['result'] == 1:
                    expTotal = timeresult[0][2] + data['num']["exp"]
                    expLevel = timeresult[0][1]
                    if expTotal > 500 * (2.5 ** (expLevel - 2)):
                        expTotal -= 500 * (2.5 ** (expLevel - 2))
                        expLevel += 1
                elif data['result'] == 2:
                    tear = timeresult[0][4] + data['num']["tear"]
                elif data['result'] == 3:
                    props = json.loads(timeresult[0][6][1:-1])
                    props[data['num']["props"]] += 1
                elif data['result'] == 4:
                    character = json.loads(timeresult[0][3][1:-1])
                    if character[data['num']["normalCharacter"]] == 0:
                        character[data['num']["normalCharacter"]] = 1
                    else:
                        money = timeresult[0][0] + 500
                returnResult["sucess"] = True     

    elif len(result) == 1 and (not openType): # rare
        tearcheck = "SELECT money, expLevel, expTotal, tear, props, `character` FROM usersdata WHERE token='{0}'".format(token) # 要調整
        # money : 0 expLevel : 1 expTotal : 2 character : 3 tear : 4 props : 5
        cur.execute(tearcheck)
        tearresult = cur.fetchall()
        
        print("tearresult", end=' ')
        print(tearresult)
        if len(tearresult) == 1 and tearresult[0][4] > 10:# 這裡要調整
            data = openChest(openType)
            if data['result'] == 0:
                    money = timeresult[0][0] + data["money"]
            elif data['result'] == 1:
                expTotal = timeresult[0][2] + data["exp"]
                expLevel = timeresult[0][1]
                if expTotal > 500 * (2.5 ** (expLevel - 2)):
                    expTotal -= 500 * (2.5 ** (expLevel - 2))
                    expLevel += 1
            elif data['result'] == 2:
                tear = timeresult[0][4] + data["tear"]
            elif data['result'] == 3:
                props = json.loads(timeresult[0][5][1:-1])
                props[data["props"]] += 1
            elif data['result'] == 4 :
                character = json.loads(timeresult[0][3][1:-1])
                if character[data["normalCharacter"]] == 0:
                    character[data["normalCharacter"]] = 1
                else:
                    money = timeresult[0][0] + 500
            elif data['result'] == 5:
                character = json.loads(timeresult[0][3][1:-1])
                if character[data["normalCharacter"]] == 0:
                    character[data["normalCharacter"]] = 1
                else:
                    money = timeresult[0][0] + 500
            returnResult["sucess"] = True

    cur.close()
    cnx.close() 

    return returnResult

def openChest(openType:bool): # openType true : normal openType : false rare
    normalChoice = [50, 85, 90, 95, 100] # 錢 經驗 淚水 道具 普通
    normalItemofChest = {
        0 : {"money" : 250}, 
        1 : {"exp" : 450},
        2 : {"tear" : 2},
        3 : {"props" : [2]},
        4 : {"normalCharacter" : [1, 4, 5]}
    }

    rareChoice = [40, 70, 80, 85, 90, 100] # 錢 經驗 淚水 道具 普通 稀有
    rareItemofChest = {
        0 : {"money" : 350}, 
        1 : {"exp" : 900},
        2 : {"tear" : 5},
        3 : {"props" : [[2], 1]}, # 1 冷風 2 炸彈 
        4 : {"normalCharacter" : [1, 4, 5]}, # 1 天使 2 小小人 3 肌肉男 4 沒穿衣服 5 小女孩 6 蝸哞 7 工程師 未決定哪個是稀有哪個是普通
        5 : {"rareCharacter" : [2, 3, 6, 7]}
    }

    choice = secrets.randbelow(100)

    for i in range(5 if openType else 6):
        if choice < (normalChoice[i] if openType else rareChoice[i]):
            choice = i
            resultofOpen = (normalItemofChest[i] if openType else rareItemofChest[i])
            break

    print(resultofOpen)
    if choice < 3: #錢 經驗 淚水 直接回傳
        return {"result" : choice, "num" : resultofOpen}
    elif choice == 3: #道具
        print(resultofOpen["props"])
        return {"result" : choice, "props" : secrets.choice(resultofOpen["props"])}
    elif choice == 4: #角色 
        print(resultofOpen["normalCharacter"])
        return {"result" : choice, "normalCharacter" : secrets.choice(resultofOpen["normalCharacter"])}
    elif (not openType) and choice == 5:
        print(resultofOpen["rareCharacter"])
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
    
    checkquery = "SELECT token FROM users WHERE token='{0}';".format(token)
    searchquery = "SELECT * FROM usersdata WHERE token='{0}';".format(token)
    updateTimequery = "UPDATE usersdata SET updateTime=NOW() WHERE token='{0}';".format(token)
    cur.execute(checkquery)

    result = cur.fetchall()
    result_num = len(result)
    
    
    if(result_num == 1):
        cur.execute(searchquery)
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
            if (datetime.now() - result[0][0]).total_seconds() < 3:
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
                cur.execute(updateTimequery)
                cnx.commit()

    cur.close()
    cnx.close()
    
    return data
    
@app.route("/updateCard", methods=['GET', 'POST'])
def updateCard():
    token = request.form.get('token')
    target = request.form.get("target")
    orginLevel = request.form.get("originLevel")
    newLevel = request.form.get("newLevel")
    mode = request.form.get("mode")


    cnx = mysql.connector.connect(**config)
    cur = cnx.cursor()
    
    checkquery = "SELECT token FROM users WHERE token='{0}'".format(token)
    cur.execute(checkquery)
    result = cur.fetchall()

    if len(result) == 1:
        moneyquery = "SELECT money, character, castleLevel, slingshotLevel FROM usersdata WHERE token='{0}'".format(token)
        cur.execute(moneyquery)
        moneyResult = cur.fetchall()
        if len(moneyResult) == 1:
            # 這邊需要升級時所需的經驗值 還要區分是卡片(應該有id 1-7 mode 1)  還是彈弓主堡 (mode 2) 存在陣列 target 0 mode 1 目標
            ()

    cur.close()
    cnx.close() 
    return 


if __name__ == "__main__":
    app.run(debug=True)
