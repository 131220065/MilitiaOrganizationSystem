
// MilitiaOrganizationSystemDlg.h : ͷ�ļ�
//

#pragma once
#include "afxcmn.h"
#include "tinyxml\tinystr.h"
#include "tinyxml\tinyxml.h"
#include "tinyxml\CodeTranslater.h"


// CMilitiaOrganizationSystemDlg �Ի���
class XMLFileTree : public CDialogEx
{
// ����
public:
	XMLFileTree(CWnd* pParent = NULL);	// ��׼���캯��
	
// �Ի�������
	enum { IDD = IDD_XMLFileTree };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV ֧��


// ʵ��
protected:
	HICON m_hIcon;

	// ���ɵ���Ϣӳ�亯��
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
public:
	CTreeCtrl m_Groups;
	CImageList m_imageList;
	
	void loadXMLFile(CString str_Dir, HTREEITEM tree_Root);
private:
	void showXMLElementInFileTree(TiXmlElement* root_Element, HTREEITEM tree_Root);
public:
	afx_msg void OnSize(UINT nType, int cx, int cy);
//	afx_msg void OnDestroy();
	afx_msg void OnClose();
};
