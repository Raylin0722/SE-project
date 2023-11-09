from flask import Flask, request
import mysql.connector
import secrets
import hashlib

app = Flask(__name__)

config = {
    'user': 'root',        
    'password': 'password',        
    'database': 'mysql',        
    'host': 'localhost',        
    'port': '3306'        
}

@app.route('/')
def index():
    return 'index'

@app.route('/register', methods=['GET', 'POST'])
def register():
    username = request.form.get("username")
    password = request.form.get("password")

    name_check_query = "SELECT username FROM users WHERE username='{0}';".format(username)

    msg = ''

    cnx = mysql.connector.connect(**config)
    cur = cnx.cursor(buffered=True)

    cur.execute(name_check_query)
    result_num = cur.rowcount
    print(result_num)

    if result_num != 0:
        msg = 'User already existed'
        return msg

    hashed_password = hashlib.sha256(password.encode('utf-8')).hexdigest()

    user_insert_query = "INSERT INTO users(username, hash) VALUES ('{0}', '{1}');".format(username, hashed_password)
    cur.execute(user_insert_query)
    cnx.commit()
    msg = 'User register success'

    cur.close()
    cnx.close()

    return msg

@app.route('/login', methods=['GET', 'POST'])
def login():
    username = request.form.get('username')
    password = request.form.get('password')

    hashed_password = hashlib.sha256(password.encode('utf-8')).hexdigest()

    name_check_query = "SELECT username, hash, score FROM users WHERE username='{0}';".format(username)
    
    msg = ''

    cnx = mysql.connector.connect(**config)
    cur = cnx.cursor(dictionary=True)

    cur.execute(name_check_query)

    result = cur.fetchall()
    result_num = len(result)
    print(result_num)

    if result_num != 1:
        msg = '{} Either no user with name, or more than one'.format(result_num)
        return msg

    queried_hash = result[0]['hash']
    queried_score = result[0]['score']

    if hashed_password != queried_hash:
        msg = 'Incorrect password'
    else:
        msg = '0\t{0}'.format(queried_score)

    cur.close()
    cnx.close()

    return msg

@app.route('/savedata', methods=['GET', 'POST'])
def savedata():
    username = request.form.get('username')
    new_score = request.form.get('score')

    name_check_query = "SELECT username FROM users WHERE username='{0}';".format(username)
    
    msg = ''

    cnx = mysql.connector.connect(**config)
    cur = cnx.cursor()

    cur.execute(name_check_query)

    result = cur.fetchall()
    result_num = len(result)
    print(result_num)

    if result_num != 1:
        msg = 'Either no user with name, or more than one'
        return msg

    score_update_query = "UPDATE users SET score='{0}' WHERE username='{1}';".format(new_score, username)
    cur.execute(score_update_query)
    cnx.commit()

    msg = 'score updated success'

    cur.close()
    cnx.close()

    return msg

@app.route("/open")
def checkLegal():#判斷是否可開啟寶箱
    
    return

def openChest(type:bool):
    normalChoice = [50, 85, 90, 95, 100] # 錢 經驗 淚水 道具 普通
    normalItemofChest = {
        0 : {"money" : 250}, 
        1 : {"exp" : 450},
        2 : {"tear" : 2},
        3 : {"props" : [[
                "aaa",
                "bbb",
                "ccc",
                "ddd" #代填ID
            ],200
        ]},
        4 : {"normalCharacter" : [[
                "a",
                "b",
                "c",
                "d" #代填ID
            ],200
        ]}
    }

    rareChoice = [40, 70, 80, 85, 90, 100] # 錢 經驗 淚水 道具 普通 稀有
    rareItemofChest = {
        0 : {"money" : 350}, 
        1 : {"exp" : 900},
        2 : {"tear" : 5},
        3 : {"props" : [[
                "aaa",
                "bbb",
                "ccc",
                "ddd" #代填ID
            ],200
        ]},
        4 : {"normalCharacter" : [[
                "a",
                "b",
                "c",
                "d" #代填ID
            ],200
        ]},
        
        5 :  {"rareCharacter" : [[
                "aa",
                "bb",
                "cc",
                "dd" #代填ID
            ],200
        ]}
        
    }

    choice = secrets.randbelow(100)

    for i in range(5 if type else 6):
        if choice < (normalChoice[i] if type else rareChoice[i]):
            choice = i
            resultofOpen = (normalItemofChest[i] if type else rareItemofChest[i])
            break



    if choice < 3: #錢 經驗 淚水 直接回傳
        return resultofOpen
    elif choice == 3: #道具
        return {"props" : [ secrets.choice(resultofOpen["props"][0]), 200]}
    elif choice == 4: #角色 
        return {"normalCharacter" : [ secrets.choice(resultofOpen["normalCharacter"][0]), 200]}
    elif (not type) and choice == 5:
        return {"rareCharacter" : [ secrets.choice(resultofOpen["rareCharacter"][0]), 200]}

@app.route("/returnData")
def returnData(token:str):
    # 確定 token 合法 這邊先默認都合法等資料庫OK再來改
    data = {
            "sussess": False,
            "token": None, 
            "money": None, 
            "exp" : None, # 前為等級 後為total
            "character": None, 
            "lineup": None, 
            "tear": None,
            "castlelevel": None, 
            "slingshotlevel": None, 
            "clearance": None, 
            "energy": None, 
            "setting": None, 
            "chesttime": None 
    }
    if(True):
        data["sussess"]  = True
        data["token"] = token
        
        # 執行玩家資料搜尋把資料填到上面的 data
        
        
    
    return data
    
    
    return data
    


if __name__ == "__main__":
    app.run(debug=True)
