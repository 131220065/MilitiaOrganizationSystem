
// MilitiaOrganizationSystemDlg.h : ͷ�ļ�
//

#pragma once
#include "afxcmn.h"
#include "tinyxml\tinystr.h"
#include "tinyxml\tinyxml.h"
#include "tinyxml\CodeTranslater.h"


// CMilitiaOrganizationSystemDlg �Ի���
class CMilitiaOrganizationSystemDlg : public CDialogEx
{
// ����
public:
	CMilitiaOrganizationSystemDlg(CWnd* pParent = NULL);	// ��׼���캯��

// �Ի�������
	enum { IDD = IDD_MILITIAORGANIZATIONSYSTEM_DIALOG };

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
};
