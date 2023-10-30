from flask import Flask, request
import mysql.connector
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

if __name__ == "__main__":
    app.run(debug=True)
