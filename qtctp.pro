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

