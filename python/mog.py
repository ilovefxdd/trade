# encoding: UTF-8
from pymongo import MongoClient, ASCENDING
from pymongo.errors import ConnectionFailure

class Cmong(object):
	"""docstring for ClassName"""

	def __init__(self):
		self.dbClient = None   

	def dbConnect(self):
         if not self.dbClient:
           
            try:
                # 设置MongoDB操作的超时时间为0.5秒
                self.dbClient = MongoClient("localhost",27017)
                
                # 调用server_info查询服务器状态，防止服务器异常并未连接成功
                self.dbClient.server_info()
                   
            except ConnectionFailure:
               pass
	def dbInsert(self, dbName, collectionName, d):
        
         if self.dbClient:
            db = self.dbClient[dbName]
            collection = db[collectionName]
            collection.insert_one(d)
         else:
            print "DATA_INSERT_FAILED"
    
    #----------------------------------------------------------------------
	def dbQuery(self, dbName, collectionName, d, sortKey='', sortDirection=ASCENDING):
     
         if self.dbClient:
            db = self.dbClient[dbName]
            collection = db[collectionName]
            
            if sortKey:
                cursor = collection.find(d).sort(sortKey, sortDirection)    # 对查询出来的数据进行排序
            else:
                cursor = collection.find(d)

            if cursor:
                return list(cursor)
            else:
                return []
         else:
           return []
        
    #----------------------------------------------------------------------
	def dbUpdate(self, dbName, collectionName, d, flt, upsert=False):
        
         if self.dbClient:
            db = self.dbClient[dbName]
            collection = db[collectionName]
            collection.replace_one(flt, d, upsert)
         else:
            print "error"      
            
    #----------------------------------------------------------------------
def main():
	cm =Cmong()
	d = {'xtz':{'$regex' : ".*aAc0BbA.*"}}
	cm.dbConnect() 
	data=cm.dbQuery('m1_db', 'm1_col', d)
	print data
		  
	
if __name__ == '__main__':
    main()