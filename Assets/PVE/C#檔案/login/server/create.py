import hashlib 
import secrets
import mysql.connector
names = [
    "Abigail",
    "Alexander",
    "Amelia",
    "Aria",
    "Shun",
    "Avery",
    "Benjamin",
    "Caleb",
    "Carter",
    "Chloe",
    "Charlotte",
    "Eleanor",
    "Elijah",
    "Ella",
    "Emily",
    "Emma",
    "Ethan",
    "Evelyn",
    "Ezra",
    "Gabriel",
    "Grace",
    "Harper",
    "Harrison",
    "Hazel",
    "Henry",
    "Vivy",
    "Isabella",
    "Jackson",
    "James",
    "Layla",
    "Liam",
    "Lily",
    "Logan",
    "Luna",
    "Lucas",
    "Mason",
    "Viper",
    "Michael",
    "Mila",
    "Nathan",
    "Noah",
    "Oliver",
    "Ruby",
    "Scarlett",
    "Sebastian",
    "Sofia",
    "Sophia",
    "Sofa",
    "William",
    "Xavier",
]

chapter = [1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2]
level = [3, 5, 2, 4, 6, 1, 5, 3, 4, 2, 6, 1, 4, 5, 3, 6, 2, 4, 6, 1, 5, 3, 2, 6, 4, 5, 3, 2, 1, 4, 3, 6, 2, 3, 5, 2, 4, 1, 3, 2, 4, 6, 1, 3, 2, 5, 1, 4, 3, 2, 6, 5]

initUserQuery = "INSERT INTO users(username, token, hash) VALUES (%s, %s, %s);"
initusersdataQuery="insert into usersdata(updateTime, playerName, token, money, expLevel, expTotal, `character`, lineup, tear, castleLevel, slingshotLevel, clearance, energy, remainTime, volume, backVolume, shock, remind, chestTime, props, faction)"\
                    "value(now(), %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, now(), %s, %s);"
initRankQuery = "insert into `rank`(playerName, chapter, level, faction) value(%s, %s, %s, %s)"
initTopUp = "insert into topUp(token, nowDate, canUse) value(%s, curdate(), 3);"

config = {
    'user': 'root',
    'password': 'test',
    'database': 'se_project',
    'host': 'localhost',
    'port': '3306'
}
factionAll = []
faction = []
clearance = []
cnx = mysql.connector.connect(**config)
cur = cnx.cursor(buffered=True)

for i in range(50):
    clear = {"1-1": 0, "1-2": 0, "1-3": 0, "1-4": 0, "1-5": 0, "1-6": 0, "2-1": 0, "2-2": 0, "2-3": 0, "2-4": 0, "2-5": 0, "2-6": 0}
    if chapter[i] == 1:
        for j in range(level[i]):
            clear["1-"+str(j+1)] = 1
    elif chapter[i] == 2:
        for j in range(6):
            clear["1-"+str(j+1)] = 1
        for j in range(level[i]):
            clear["2-"+str(j+1)] = 1
    clearance.append(clear)
    fac = [0, 0, 0, 0, 0, 0]
    if level[i] % 2 == 0:
        fac[3] = 1
    else:
        fac[2] = 1
    
    if chapter[i] == 1 and level[i] != 6:
        fac[1] = 3 if level[i] % 2 == 0 else 2
    elif chapter[i] == 1 and level[i] == 6:
        fac[4] = 1; fac[1] = 4
    elif chapter[i] == 2 and level[i] != 6:
        fac[1] = 4; fac[4] = 1
    elif chapter[i] == 2 and level[i] == 6:
        fac[5] = 1; fac[1] = 5; fac[4] = 1
    print(fac)
    faction.append(fac[1])
    factionAll.append(fac)
for i in range(50):
    token = secrets.token_hex(32)
    hashed_password = hashlib.sha256('12345678'.encode('utf-8')).hexdigest()
    cur.execute(initRankQuery, (names[i], chapter[i], level[i], faction[i]))
    cur.execute(initUserQuery, (names[i], token, hashed_password))
    cur.execute(initTopUp, (token, ))
    cur.execute(initusersdataQuery, (names[i], token, 1000, 5, 0, '{"1": 5, "2": 5, "3": 5, "4": 5, "5": 5, "6": 5, "7": 5}', '[1, 2, 3, 4, 5, 1]', 100, 10, 10, str(clearance[i]).replace("'", "\""),
                                     30, 0, 100, 100, True, True, '{"1" : -1, "2" : 0}', str(factionAll[i])))
    
    cnx.commit()

cur.close()
cnx.close()
