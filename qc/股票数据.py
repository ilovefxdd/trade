#! /usr/bin/python2
# coding=utf-8
import urllib2
import csv
import time
from datetime import datetime
from datetime import timedelta

#http://quotes.money.163.com/service/chddata.html?code=0000001&start=19901219&end=20150911&fields=TCLOSE;HIGH;LOW;TOPEN;LCLOSE;CHG;PCHG;VOTURNOVER;VATURNOVER 　csv数据
#http://api.money.126.net/data/feed/1000002,money.api　　json数据
#http://hq.sinajs.cn/list=sh601006,sh600143
#import re  
#str=u'小明买冰棍花了5元，买糖果花了3元，买游戏花了59元，小明今天一共花了67元。'  
#word = u'元'  
#a = [m.start() for m in re.finditer(word, str)]  
List_code = []

def get_stock_list():
    list_name = ['sha.csv', 'shb.csv', 'sz.csv']
    for name in list_name:
        try:
            csvfile = file(name, 'rb')
            reader = csv.reader(csvfile)

            for line in reader:
                try:
                    # 忽略第一行
                    if reader.line_num == 1:
                        continue
                    list_code.append(line[0])
                    # print(' said: ', '')

                except ValueError:
                    pass
            print name, locals()
            # csvfile.close()
        except IOError as err:  # 使用as将异常对象，并将其赋值给一个标识符
            print('File Error:' + str(err))  # ‘+’用于字符串直接的连接

        finally:
            if 'csvfile' in locals():
                csvfile.close()
                print "close"

def day_plus(str):
    #now = datetime.now()
    day = datetime.strptime(str, "%Y-%m-%d")
    day_diff = timedelta(days=1)
    day = day + day_diff
    return day

def day_str_change(str):
    day = datetime.strptime(str, "%Y-%m-%d")
    return day.strftime('%Y%m%d')


def find_data(code):

    str = ""
    if ('' == str ):
        str = "1970-1-1"
    return str


'''
def stock_header(code):
    url = 'http://quotes.money.163.com/service/chddata.html?code='
    t1 = ('60', '900')
    t2 = ('000', '002', '300', '200')
    t3 = ('399001','399006')
    if code.startswith(t1):
        str = '0' + code
    elif code.startswith('000001'):
        str = 's_sh' + code
    elif code.startswith(t3):
        str = 's_sz' + code
    elif code.startswith(t2):
        str = '1' + code
    else:
        str = code
        print code
    url = url + str
    return url
'''

#'''
def stock_header(code):
    url = 'http://table.finance.yahoo.com/table.csv?s='
    t1 = ('60', '900')
    t2 = ('000', '002', '300', '200')
    t3 = ('399001','399006')
    if code.startswith(t1):
        str = code + '.ss'
    elif code.startswith('000001'):
        str = code + '.ss'
    elif code.startswith(t3):
        str = code + '.sz'
    elif code.startswith(t2):
        str = code + '.sz'
    else:
        str = code
        print code
    url = url + str
    return url
#'''

def deal_url(str_day, url):
    if ("1970-1-1"== str_day):
        print url
        return url;
    day = day_plus(str_day)
    now = datetime.now()
    print day.strftime('%Y%m%d'), now.strftime('%Y%m%d')
    if (day.strftime('%Y%m%d') >= now.strftime('%Y%m%d')):

        return ""
    #163 data  Date    Open   High   Low    Close  Volume Adj Close
    #str_url = url + '&start=' + day.strftime('%Y%m%d') + '&end=' + now.strftime('%Y%m%d')
    #yahoo data 日期 股票代码   名称 收盘价    最高价    最低价    开盘价    前收盘    涨跌额    涨跌幅    换手率    成交量    成交金额   总市值    流通市值   成交笔数
    mon1 = int(now.strftime('%m')) - 1
    mon2 = int(day.strftime('%m')) - 1
    str_url = url + '&d=%s&e=%s&f=%s&g=d&a=%s&b=%s&c=%s&ignore=.csv' % (
    str(mon1), now.strftime('%d'), now.strftime('%Y'), str(mon2), day.strftime('%d'), day.strftime('%Y'))
    #print str_url
    return str_url


def get_day(code, url):
    print code, url
    if ("" == url):
        print "---newst---date---------------------------------"
        return
    # url = 'http://quotes.money.163.com/service/chddata.html?code=1000002'
    # url = 'http://quotes.money.163.com/service/chddata.html?code=0601398&start=20000720&end=20150508'
    #url = 'http://table.finance.yahoo.com/table.csv?s=000002.sz'
    #url = 'http://table.finance.yahoo.com/table.csv?s=000002.sz&d=6&e=22&f=2006&g=d&a=11&b=16&c=1991&ignore=.csv'
    # url = 'http://hq.sinajs.cn/?list=sh600127'
    #http://market.finance.sina.com.cn/downxls.php?date=2016-10-28&symbol=sz300127
    # print url
    req_header = {'User-Agent':'Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.64 Safari/537.11',
        'Accept':'text/html;q=0.9,*/*;q=0.8','Accept-Charset':'ISO-8859-1,utf-8;q=0.7,*;q=0.3',
        'Accept-Encoding':'gzip','Connection':'close','Referer':None #注意如果依然不能抓取的话，这里可以设置抓取网站的host
        }
    req_timeout = 500
    #req = urllib2.Request(url,None,req_header)
    req = urllib2.Request(url)
    #print req
    # 如果不需要设置代理，下面的set_proxy就不用调用了。由于公司网络要代理才能连接外网，所以这里有set_proxy…
    # req.set_proxy('proxy.XXX.com:911', 'http')
    #socket = urllib2.urlopen(req,None,req_timeout)

    try:
        socket = urllib2.urlopen(req,None,req_timeout)
        #print socket
        content = socket.read()
        # content = socket.read().decode('GB18030')
        socket.close()
    except urllib2.HTTPError, e:
        print 'The server couldn\'t fulfill the request.'
        print 'Error code: ', e.code
        print 'Error reason: ', e.reason
    except urllib2.URLError, e:
        print 'We failed to reach a server.'
        print 'Reason: ', e.reason
    else:
        # everything is fine

        rows = content.split('\n')
        i = 0
        for row in rows:
            if i == 0:
                i += 1
                continue
            #忽略第一行

            split_row = row.split(",")
            # split_row[1] = int(split_row[1])
            # full_data.append(split_row)
            full_data = []
            for row_s in split_row:
                if ("" == row_s):
                    print "----------------------------------row null"
                full_data.append(row_s)
            print full_data


            if i == 20:
                break
            i += 1



def get_day_list(code):
    str_day = find_data(code)
    print str_day
    url = stock_header(code)
    #print url
    url = deal_url(str_day, url)
    #print url
    get_day(code, url)


if __name__ == '__main__':
 #   '''
    get_stock_list()
    print len(list_code)
    print list_code
    for code in list_code:
        get_day_list(code)
#'''
   # get_day_list('000002')