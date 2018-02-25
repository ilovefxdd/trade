# -*- coding: UTF-8 -*-
from struct import *
from datetime import datetime, timedelta
import os  
import copy
import os.path
from vtObject import *
import threading  
import imp
import csv
import string
import multiprocessing
from  mog import *

lock=multiprocessing.Lock()#一个锁
EMPTY_STRING = ''
EMPTY_UNICODE = u''
EMPTY_INT = 0

jgb={"IF":1,"IC":1,"IH":1,"TF":1,"T":1,"ru":5,"cu":10,"ag":1,"au":0.05,"rb":1,"bu":2,"al":5,"zn":5,"pb":5,"fu":1,"hc":2,"wr":1,"ni":10,"sn":10,"j":1,"m":1,"v":5,"p":2,"y":2,"c":1,"bb":1,"fb":0.05,"i":0.5,"jd":1,"a":1,"m":1,"l":5,"pp":1,"cs":1,"wh":1,"SR":1,"TA":2,"OI":2,"MA":1,"FG":1,"RS":1,"RM":1,"TC":0.2,"CF":5,"SF":2,"SM":2,"AP":1,"ME":1,"RO":1,"WS":1,'jm':0.5}
mdict={'_id':10,'syb':'','date':'2001','xtz':'','bar':{'open':0,'high':0,'low':0,'close':0,'vol':0,'openin':0}}


class readt(object):
    def __init__(self):
	self.bars=[]
	self.qc=0
	self.tz=''
  	    
    def readbar(self,path):
	pass
 
def writebar(bar,filen):
      filen.write(pack("6sffffii4s4s", bar.symbol,bar.open,bar.high,bar.low,bar.close,bar.volume,bar.openInterest,bar.date,bar.time[0:6]))
   #   print bar.close
def writecsv(bar,filen,cm):
    bcsv=[]
    bcsv.append( bar.symbol)
    bcsv.append( bar.open)
    bcsv.append( bar.high)
    bcsv.append( bar.low)
    bcsv.append( bar.close)
    bcsv.append( bar.volume)
    bcsv.append( bar.openInterest)
    bcsv.append( bar.date)
    bcsv.append( bar.time[0:5])
    jgbs=bar.symbol.rstrip(string.digits)
    zxjg=jgb[jgbs] 
    jt=0
    ct=0
  #  print cm.qc
    if cm.qc!=0:
	jt=(bar.close-cm.qc)/zxjg
	#print bar.close,cm.qc,zxjg
        #print jt
	if (26>jt>0 ):
	    ct=96+jt
	else :
	    if (jt>26):
	      ct=120
	    else :
		if jt==0:
		   ct=48
		else:
		    if -26<jt<0:
			ct=abs(jt)+64
		    else:
			if jt<-26:
			    ct=90
	cm.tz=cm.tz+chr(int(ct))
	#print cm.tz
    bcsv.append(chr(int(ct)) )
   # print bcsv
    filen.writerow(bcsv)
    return 
def geth(tk,myfile,date):
    try :
	  h=ord(myfile.read(1))
	  m=ord(myfile.read(1))
	  s=ord(myfile.read(1))
	  ms=ord(myfile.read(1))*10
	  if (ms==0):
		imp.acquire_lock()
		tk.time=str(h+100)[1:3]+':'+str(m+100)[1:3]+':'+str(s+100)[1:3]
		tk.datetime=datetime.strptime(' '.join([date, tk.time]), '%Y%m%d %H:%M:%S') 
		imp.release_lock()
		   # print 'date1' ,self.tk.datetime
	  else:
		imp.acquire_lock()
		tk.time=str(h+100)[1:3]+':'+str(m+100)[1:3]+':'+str(s+100)[1:3]+'.'+str(ms)
		tk.datetime=datetime.strptime(' '.join([date, tk.time]), '%Y%m%d %H:%M:%S.%f')  
		imp.release_lock()
		   # print 'date2',self.tk.datetime
    except TypeError:
	  pass
        
def getp(myfile,tk,date):
      (d1,d2,d3,d4,d5,d6,d7,d8,d9,d10,d11)=unpack("iiiiiiiiiii",myfile.read(4+4+4+4+4+4+4+4+4+4+4))
      f=float(d1/1000)
      tk.LastPrice=f
	
      f=float(d2/1000)
      tk.HighestPrice=f
      f=float(d3/1000)
      tk.LowestPrice=f
	
      tk.OpenInterest=d4
      tk.Volume=d5
      tk.date=date
      tk.BidVolume1=d8
      tk.AskVolume1=d9
      tk.BidPrice1=d10
      tk.AskPrice1=d11
      
def pretick(tick,barfile,bar,cm):  
    #  print bar.date, tick.vtSymbol,bar.datetime,bar.vtSymbol
      if not bar.vtSymbol:
		    
	  bar.vtSymbol = tick.vtSymbol
	  bar.symbol = tick.vtSymbol
				   #    bar.exchange = tick.exchange
#	  print bar.symbol,bar.datetime
      if not bar.datetime:  
	  bar.open = tick.LastPrice
	  bar.high = tick.LastPrice
	  bar.low = tick.LastPrice
	  bar.close = tick.LastPrice
			    
	  bar.date = tick.date
	  bar.time = tick.time
	  bar.datetime = tick.datetime#.replace(second=0, microsecond=0)
	  bar.volume = tick.Volume
	  bar.openInterest = tick.OpenInterest        
			    # 则继续累加新的K线
      else:      
	  if( bar.datetime.minute != tick.datetime.minute or bar.datetime.hour != tick.datetime.hour):    
	     newBar = copy.copy(bar)  
	     bar.open = tick.LastPrice
	     bar.high = tick.LastPrice
	     bar.low = tick.LastPrice
	     bar.close = tick.LastPrice
				       
	     bar.date = tick.date
	     bar.time = tick.time
	     bar.datetime = tick.datetime#.replace(second=0, microsecond=0)
	     bar.volume = tick.Volume
	     bar.openInterest = tick.OpenInterest  
	     
	  #   print bar.open,bar.high,bar.low,bar.close,bar.datetime,bar.volume,bar.openInterest
	   #  writebar(newBar,barfile) 记录ＢＡＲ
	     writecsv(newBar,barfile,cm)
	     cm.qc=newBar.close
	  else:
	     bar.high = max(bar.high, tick.LastPrice)
	     bar.low = min(bar.low, tick.LastPrice)
	     bar.volume = tick.Volume
	     bar.openInterest = tick.OpenInterest   
	   #  print	bar.close ,tick.LastPrice
      return 
def gethead(myfile,cm):
      s=myfile.read(8)
      (y)=unpack("h",myfile.read(2))
      yy=y[0]
      m=100+ord(myfile.read(1))
      d=100+ord(myfile.read(1))
      ms=str(m)[1:3]
      ds=str(d)[1:3]
      (d1,d2,d3,d4,d5,d6,d7)=unpack("iiiiiii",myfile.read(4+4+4+4+4+4+4))
      LastPrice = d6/1000        # 今日开盘价
      cm.open=LastPrice			   
      cm.openInterest =d4          # 持仓量	  
      cm.preClosePrice =d4/1000
				 
      cm.date = str(yy)+ms+ds              # 日期 20151009
				 #  print yy,'|',ms,'|',ds
      cm.datetime = None                    # python的datetime时间对象	  
      cm.upperLimit = d5/1000          # 涨停价
      cm.lowerLimit =d6/1000        # 跌停价	s  
      s=myfile.read(36)
			     
      # print 'self.tk',self.tk.LastPrice  
def protick(filen):
    cm =readt()
      
    vl=os.path.basename(filen)
    tf,ext= os.path.splitext(filen)  
    ext=ext[1:3]  
    if not(ext=='tk'):
	return
    if os.path.getsize(filen)<1000000:
	return
    dirp=tf+'.csv'
    if  os.path.isfile(dirp):
	  return
    else:		      
      #barfile=open(dirp,'wb')
      f=file(dirp,'wb')
      barfile=csv.writer(f)
      tk=VtTickData()
      
      vln,ext= os.path.splitext(vl)  

     # print jgbs ,":",zxjg
      tk.vtSymbol=vln
      cm.myfile=open(filen,'rb')
      cm.myfile.seek(0,0)
      cm.myfile.seek(0,2)
      lens =cm.myfile.tell()
      cm.myfile.seek(0,0)
      gethead(cm.myfile,cm)
      i=76
      bar=VtBarData()
      cm.tz=''
      cm.qc=0
      
      while (True):      
	    geth(tk,cm.myfile,cm.date)
	    getp(cm.myfile,tk,cm.date)
	    
	    pretick(tk,barfile,bar,cm)
	#    print bar.close
	    i=i+48		    
	    if (i==lens):
		
		  f.close()
		  try:  
		    dbClient = MongoClient("localhost",27017,connectTimeoutMS=500)
		    mdict['syb']=vln
		    mdict['date']=cm.date
		    mdict['xtz']=cm.tz
		    mdict['_id']=vln+cm.date+tk.time
		    mdict['bar']['open']=cm.open
		    mdict['bar']['high']=tk.HighestPrice
		    mdict['bar']['low']=tk.LowestPrice
		    mdict['bar']['close']=tk.LastPrice
		    mdict['bar']['vol']=tk.Volume
		    mdict['bar']['openin']=tk.OpenInterest
		    dbName='m1_db'
		    db = dbClient[dbName]
		    collectionName='m1_col'
		    collection = db[collectionName]
		    collection.insert_one(mdict)  		  	  	 
		    dbClient.close
		  except ConnectionFailure:
		    pass		  
		  break   	   		      
def getfile(dirpath): 
      list=[]
      for (root, dirs, files) in os.walk(dirpath):
	    for filen in files:
		  pathf= os.path.join(root,filen)
		  list.append(pathf)
		#  protick(pathf)             
		  #time.sleep(2)
      return list

def main():    
    list=getfile("G:\\ticks")
  #  protick(list[0])
    pool=multiprocessing.Pool(processes=5)#限制并行进程数为3
    pool.map(protick,list)   
   # getfile("G:\\ticks")
     
if __name__ == '__main__':
    main()