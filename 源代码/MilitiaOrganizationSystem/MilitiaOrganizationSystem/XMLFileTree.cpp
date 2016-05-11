
// MilitiaOrganizationSystemDlg.cpp : ʵ���ļ�
//

#include "stdafx.h"
#include "MilitiaOrganizationSystem.h"
#include "XMLFileTree.h"
#include "afxdialogex.h"


#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// ����Ӧ�ó��򡰹��ڡ��˵���� CAboutDlg �Ի���

class CAboutDlg : public CDialogEx
{
public:
	CAboutDlg();

// �Ի�������
	enum { IDD = IDD_ABOUTBOX };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV ֧��

// ʵ��
protected:
	DECLARE_MESSAGE_MAP()
public:
//	afx_msg void OnIdbtnImportgroupxml();
};

CAboutDlg::CAboutDlg() : CDialogEx(CAboutDlg::IDD)
{
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialogEx)
//	ON_COMMAND(IDBTN_IMPORTGROUPXML, &CAboutDlg::OnIdbtnImportgroupxml)
END_MESSAGE_MAP()


// CMilitiaOrganizationSystemDlg �Ի���



XMLFileTree::XMLFileTree(CWnd* pParent /*=NULL*/)
	: CDialogEx(XMLFileTree::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void XMLFileTree::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_GROUPTREE, m_Groups);
}

BEGIN_MESSAGE_MAP(XMLFileTree, CDialogEx)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_WM_SIZE()
//	ON_WM_DESTROY()
ON_WM_CLOSE()
//ON_WM_RBUTTONDOWN()
//ON_WM_RBUTTONUP()
ON_NOTIFY(NM_RCLICK, IDC_GROUPTREE, &XMLFileTree::OnNMRClickGrouptree)
ON_COMMAND(ID_MENU_VIEW, &XMLFileTree::OnMenuView)
ON_COMMAND(ID_MENU_DELETE, &XMLFileTree::OnMenuDelete)
ON_COMMAND(ID_MENU_ADD, &XMLFileTree::OnMenuAdd)
ON_COMMAND(ID_MENU_MODIFY, &XMLFileTree::OnMenuModify)
ON_NOTIFY(TVN_ENDLABELEDIT, IDC_GROUPTREE, &XMLFileTree::OnTvnEndlabeleditGrouptree)
END_MESSAGE_MAP()


// CMilitiaOrganizationSystemDlg ��Ϣ�������

BOOL XMLFileTree::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		BOOL bNameValid;
		CString strAboutMenu;
		bNameValid = strAboutMenu.LoadString(IDS_ABOUTBOX);
		ASSERT(bNameValid);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
		}
	}

	// ���ô˶Ի����ͼ�ꡣ��Ӧ�ó��������ڲ��ǶԻ���ʱ����ܽ��Զ�
	//  ִ�д˲���
	SetIcon(m_hIcon, TRUE);			// ���ô�ͼ��
	SetIcon(m_hIcon, FALSE);		// ����Сͼ��

	// TODO: �ڴ���Ӷ���ĳ�ʼ������

	//��ʼ�����οؼ�
	m_imageList.Create(32, 32, ILC_COLOR8|ILC_MASK, 0, 1);
	m_imageList.Add(AfxGetApp()->LoadIconW(IDI_ICON1));
	m_imageList.Add(AfxGetApp()->LoadIconW(IDI_ICON1));

	m_Groups.SetImageList(&m_imageList, TVSIL_NORMAL);//���οؼ���imageList
	HTREEITEM m_TreeRoot = m_Groups.InsertItem(L"�������������");//������ڵ�
	loadXMLFile(L"C:\\Users\\Hzq\\Desktop\\�������\\Ӳ��˵��\\militia.xml", m_TreeRoot);

	return TRUE;  // ���ǽ��������õ��ؼ������򷵻� TRUE
}

void XMLFileTree::OnSysCommand(UINT nID, LPARAM lParam)
{
		CDialogEx::OnSysCommand(nID, lParam);
}

// �����Ի��������С����ť������Ҫ����Ĵ���
//  �����Ƹ�ͼ�ꡣ����ʹ���ĵ�/��ͼģ�͵� MFC Ӧ�ó���
//  �⽫�ɿ���Զ���ɡ�

void XMLFileTree::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // ���ڻ��Ƶ��豸������

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// ʹͼ���ڹ����������о���
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// ����ͼ��
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialogEx::OnPaint();
	}
}

//���û��϶���С������ʱϵͳ���ô˺���ȡ�ù��
//��ʾ��
HCURSOR XMLFileTree::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

//��ʾһ��Ԫ�ص������ӽڵ㵽�ļ���
void XMLFileTree::showXMLElementInFileTree(TiXmlElement* root_Element, HTREEITEM tree_Root) {
	TiXmlElement* firstElement = root_Element->FirstChildElement();
	if(firstElement == NULL) {
		return;
	}
	HTREEITEM treeTemp = m_Groups.InsertItem(CodeTranslater::UTF8ToGBK(firstElement->FirstAttribute()->Value()), 1, 0, tree_Root);
	showXMLElementInFileTree(firstElement, treeTemp);
	TiXmlElement* nextElement;
	while( (nextElement = firstElement->NextSiblingElement()) != NULL) {
		firstElement = nextElement;
		treeTemp = m_Groups.InsertItem(CodeTranslater::UTF8ToGBK(nextElement->FirstAttribute()->Value()), 1, 1, tree_Root);
		showXMLElementInFileTree(nextElement, treeTemp);
	}
}

//��ʾ�ļ���
void XMLFileTree::loadXMLFile(CString str_FilePath, HTREEITEM tree_Root) {

	USES_CONVERSION;
	TiXmlDocument doc(T2A(str_FilePath));
	bool loadOk = doc.LoadFile();
	if(!loadOk) {
		m_Groups.InsertItem(L"û�д��ļ�������ʧ�ܣ�\n", 0, 0, tree_Root);
		return;
	}
	TiXmlElement* rootElement = doc.RootElement();
	
	showXMLElementInFileTree(rootElement, tree_Root);

}



//void CAboutDlg::OnIdbtnImportgroupxml()
//{
//	// TODO: �ڴ���������������
//}


void XMLFileTree::OnSize(UINT nType, int cx, int cy)
{//������洰�ڱ仯������Ӧ
	CDialogEx::OnSize(nType, cx, cy);

	// TODO: �ڴ˴������Ϣ����������
	if(m_Groups.GetSafeHwnd()) {
		m_Groups.MoveWindow(0, 0, cx, cy);
	}
}


//void XMLFileTree::OnDestroy()
//{
//	CDialogEx::OnDestroy();
//
//	// TODO: �ڴ˴������Ϣ����������
//}


void XMLFileTree::OnClose()
{
	// TODO: �ڴ������Ϣ�����������/�����Ĭ��ֵ

	ShowWindow(SW_HIDE);
}

HTREEITEM XMLFileTree::getSelectItem() {
	CPoint pt;
    GetCursorPos(&pt);//�õ���ǰ����λ��
	m_Groups.ScreenToClient(&pt);//����Ļ����ת��Ϊ�ͻ�������
    HTREEITEM tree_Item = m_Groups.HitTest(pt);//����HitTest�ҵ���Ӧ��������ڵ�
    return tree_Item;
}



void XMLFileTree::OnNMRClickGrouptree(NMHDR *pNMHDR, LRESULT *pResult)
{
	// TODO: �ڴ���ӿؼ�֪ͨ����������
	//��ʱ������Ļ���꣬��������menu
    CPoint ScreenPt;
    GetCursorPos(&ScreenPt);

    //��ȡ����ǰ���ѡ������ڵ�
	HTREEITEM selectItem = getSelectItem();
    if (selectItem != NULL)
    {
		m_Groups.SelectItem(selectItem); //ʹ�Ҽ����������ڵ㱻ѡ��
			
        CMenu menu;
        menu.LoadMenuW(IDR_MENU3);
        CMenu* pPopup = menu.GetSubMenu(0);//װ�ص�һ���Ӳ˵��������ǲ˵��ĵ�һ��
        pPopup->TrackPopupMenu(TPM_LEFTALIGN, ScreenPt.x, ScreenPt.y, this);//�����˵�
        }
	*pResult = 0;
}


void XMLFileTree::OnMenuView()
{
	// TODO: �ڴ���������������
}


void XMLFileTree::OnMenuDelete()
{
	// TODO: �ڴ���������������
	HTREEITEM selectItem = m_Groups.GetSelectedItem();
	if(m_Groups.GetChildItem(selectItem) == NULL) {//����������Ҷ��㣬ֱ��ɾ������Ӧȷ�������Ƿ��������֮����ӣ�
		m_Groups.DeleteItem(selectItem);
	} else {//���û�ȷ���Ƿ�ɾ��
		MessageBox(_T("hello world!"),_T("��ʾ!"));
	}
}


void XMLFileTree::OnMenuAdd()
{
	// TODO: �ڴ���������������
	HTREEITEM selectItem = m_Groups.GetSelectedItem();
	HTREEITEM addItem = m_Groups.InsertItem(_T("�½���"), 1, 1, selectItem);
	m_Groups.EditLabel(addItem);//�༭
}


void XMLFileTree::OnMenuModify()
{
	// TODO: �ڴ���������������
	HTREEITEM selectItem = m_Groups.GetSelectedItem();
	m_Groups.EditLabel(selectItem);//�༭
	
}


void XMLFileTree::OnTvnEndlabeleditGrouptree(NMHDR *pNMHDR, LRESULT *pResult)
{//����ǩ�༭���ʱ
	LPNMTVDISPINFO pTVDispInfo = reinterpret_cast<LPNMTVDISPINFO>(pNMHDR);
	// TODO: �ڴ���ӿؼ�֪ͨ����������
	CString strText;

	m_Groups.GetEditControl()->GetWindowTextW(strText);

	m_Groups.SetItemText(pTVDispInfo->item.hItem, strText);

	*pResult = 0;
}
