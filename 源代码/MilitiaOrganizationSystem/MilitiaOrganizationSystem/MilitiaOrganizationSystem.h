
// MilitiaOrganizationSystem.h : PROJECT_NAME Ӧ�ó������ͷ�ļ�
//

#pragma once

#ifndef __AFXWIN_H__
	#error "�ڰ������ļ�֮ǰ������stdafx.h�������� PCH �ļ�"
#endif

#include "resource.h"		// ������


// CMilitiaOrganizationSystemApp:
// �йش����ʵ�֣������ MilitiaOrganizationSystem.cpp
//

class CMilitiaOrganizationSystemApp : public CWinApp
{
public:
	CMilitiaOrganizationSystemApp();

// ��д
public:
	virtual BOOL InitInstance();

// ʵ��

	DECLARE_MESSAGE_MAP()
};

extern CMilitiaOrganizationSystemApp theApp;