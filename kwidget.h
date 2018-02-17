#ifndef KWIDGET_H 
#define KWIDGET_H 

#include <QtGui> 
#include <QDebug> 
#include <QWidget>

class KWidget : public QWidget
{
	Q_OBJECT

public:
	KWidget(QWidget *parent = 0);

	public slots:
	void draw();

protected:
	void paintEvent(QPaintEvent *event);
	void mousePressEvent(QMouseEvent *event);
	void mouseMoveEvent(QMouseEvent *event);
	void mouseReleaseEvent(QMouseEvent *event);

private:
	
	bool perm;
	//QList<Shape*> shapeList;
};

#endif // PAINTWIDGET_H 
