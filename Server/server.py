from flask import *
import mysql.connector
import datetime

db_config = {
    "user": "root",
    "password": "12345678",
    "host": "localhost",
    "database": "testing"
}


print("connect success")
connection_pool = mysql.connector.pooling.MySQLConnectionPool(pool_name="mypool", pool_size=5, **db_config)


app = Flask(__name__)

@app.route("/")
def index():
    return "Main Page"

@app.route("/getcard")
def getCard():
    playerName = request.args.get("playerName", "")

    currentTime = datetime.datetime.now()
    print(currentTime)
    return "Time"

@app.route("/upgradecard")
def updateCard():
    playerName = request.args.get("playerName", "")
    return playerName + " upgradecard"

@app.route("/login")
def login():
    playerName = request.args.get("id", "")
    passwd = request.args.get("passwd", "")
    try:
        connect = connection_pool.get_connection()
        cursor = connect.cursor()
        
        #return command
        cursor.execute("SELECT * FROM login where idname='{}';".format(playerName))

        datas = cursor.fetchall()
        for data in datas:
            if data[1] == playerName:
                if data[2] == passwd:
                    return "Pass! Hello {}".format(playerName) 
    finally:  
        cursor.close()
        connect.close()
    return "Failed!"


@app.route("/createAccount")
def createAccount():
    playerName = request.args.get("id", "")
    passwd = request.args.get("passwd", "")

    if playerName == "" or passwd == "":
        return "Invalid player name or password!"
    try:
        connect = connection_pool.get_connection()
        cursor = connect.cursor()

        cursor.execute("SELECT * FROM login where idname='{}';".format(playerName))
        
        datas = cursor.fetchall()

        if datas == []:# No data of the playerName insert new to DB
            cursor.execute("INSERT INTO login(idname, passwd) VALUE('{}', '{}');".format(playerName, passwd))
            connect.commit()
        else:
            for data in datas:
                print(data[1])
                if data[1] == playerName:
                    return "This name already exist!"
    finally:
        cursor.close()
        connect.close()
        
    return "Create new account!"

@app.route("/pve")
def pve():
    playerName = request.args.get("playerName", "")
    return playerName

if __name__ == "__main__":
    app.run()


