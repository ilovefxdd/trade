#-------------------------------------------------
#
# Project created by QtCreator 2018-01-02T08:55:29
#
#-------------------------------------------------

QT       += core gui

greaterThan(QT_MAJOR_VERSION,4): QT += widgets

TARGET = qtctp
TEMPLATE = app


SOURCES += main.cpp\
        mainwindow.cpp \
    md.cpp \
    mdthread.cpp

HEADERS  += mainwindow.h \
    md.h \
    mdthread.h \
    ThostFtdcMdApi.h \
    ThostFtdcTraderApi.h \
    ThostFtdcUserApiDataType.h \
    ThostFtdcUserApiStruct.h

FORMS    += mainwindow.ui


win32:CONFIG(release, debug|release): LIBS += -L$$PWD/./ -lthostmduserapi
else:win32:CONFIG(debug, debug|release): LIBS += -L$$PWD/./ -lthostmduserapid
else:unix: LIBS += -L$$PWD/./ -lthostmduserapi

INCLUDEPATH += $$PWD/.
DEPENDPATH += $$PWD/.

win32-g++:CONFIG(release, debug|release): PRE_TARGETDEPS += $$PWD/./libthostmduserapi.a
else:win32-g++:CONFIG(debug, debug|release): PRE_TARGETDEPS += $$PWD/./libthostmduserapid.a
else:win32:!win32-g++:CONFIG(release, debug|release): PRE_TARGETDEPS += $$PWD/./thostmduserapi.lib
else:win32:!win32-g++:CONFIG(debug, debug|release): PRE_TARGETDEPS += $$PWD/./thostmduserapid.lib
else:unix: PRE_TARGETDEPS += $$PWD/./libthostmduserapi.a
