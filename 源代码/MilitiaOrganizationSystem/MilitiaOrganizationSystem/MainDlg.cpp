// MainDlg.cpp : ʵ���ļ�
//

#include "stdafx.h"
#include "MilitiaOrganizationSystem.h"
#include "MainDlg.h"
#include "XMLFileTree.h"
#include "afxdialogex.h"


// MainDlg �Ի���

IMPLEMENT_DYNAMIC(MainDlg, CDialogEx)

MainDlg::MainDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(MainDlg::IDD, pParent)
{
	m_xmlFileTreeDlg = NULL;//��ֵΪNULL
}

MainDlg::~MainDlg()
{
	if(m_xmlFileTreeDlg != NULL) {
		delete m_xmlFileTreeDlg;
	}
}

void MainDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}


BEGIN_MESSAGE_MAP(MainDlg, CDialogEx)
//	ON_WM_LBUTTONDOWN()
//	ON_WM_MENUSELECT()
	ON_COMMAND(IDBTN_IMPORTGROUPXML, &MainDlg::OnIdbtnImportgroupxml)
END_MESSAGE_MAP()


// MainDlg ��Ϣ�������




void MainDlg::OnIdbtnImportgroupxml()
{
	// TODO: �ڴ���������������
	if(m_xmlFileTreeDlg == NULL) {
		m_xmlFileTreeDlg = new XMLFileTree;
		m_xmlFileTreeDlg->Create(IDD_XMLFileTree, GetDesktopWindow());
	}
	
	m_xmlFileTreeDlg->ShowWindow(SW_NORMAL);

}
