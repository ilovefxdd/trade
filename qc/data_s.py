#coding=utf-8
import urllib
import numpy as np
import json
url = "http://stock2.finance.sina.com.cn/futures/api/json.php/IndexService.getInnerFuturesMiniKLine5m?symbol=ru1805"
def getHtml(url): 
    #url1 = "http://stock2.finance.sina.com.cn/futures/api/json.php/IndexService.getInnerFuturesMiniKLine5m?symbol=ru1805"
    page = urllib.urlopen(url)
    html = page.read()
    b=json.loads(html)
    #b[0][0]=url
    print b[0][0]
    return b
getHtml(url)
