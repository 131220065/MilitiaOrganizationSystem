using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MilitiaOrganizationSystem
{
    class XMLGroupTreeViewBiz
    {//处理treeView上的增删改(与数据相关的部分)
        private XMLGroupDao xmlGroupDao;//xml访问层
        private TreeView groups_treeView;//树视图


        public XMLGroupTreeViewBiz(TreeView groups_TreeView, SqlBiz sqlBiz)
        {//构造函数
            xmlGroupDao = new XMLGroupDao(sqlBiz, groups_TreeView);
            this.groups_treeView = groups_TreeView;

            FormBizs.groupBiz = this;//唯一的分组任务界面
        }

        public void focus()
        {//获得焦点
            groups_treeView.Focus();
        }

        public void addUnderSelectedNode()
        {//在选中节点的下面添加组
            
            TreeNode selectNode = groups_treeView.SelectedNode;
            if (selectNode == null)
            {//如果没有选中节点，则从根节点添加组
                addRoot();
            }
            else
            {
                TreeNode node = new TreeNode("新建组");

                selectNode.Nodes.Add(node);
                selectNode.Expand();//增加后，展开

                GroupTag tag = (GroupTag)(selectNode.Tag);
                node.Tag = xmlGroupDao.addGroupFromParent(tag.tagXmlNode, "新建组");
                node.ToolTipText = xmlGroupDao.getToolTipText(((GroupTag)node.Tag).tagXmlNode);

                groups_treeView.SelectedNode = node;//选中新建的组
                node.BeginEdit();//开始编辑名称
                
            }
            
        }

        public void addRoot()
        {//增加根节点
            
            TreeNode node = new TreeNode("新建组");
            groups_treeView.Nodes.Add(node);
            groups_treeView.SelectedNode = node;
            
            node.Tag = xmlGroupDao.addGroupFromRoot("新建组");
            node.ToolTipText = xmlGroupDao.getToolTipText(((GroupTag)node.Tag).tagXmlNode);

            node.BeginEdit();
        }

        public void modifyName(TreeNode node, string newName)
        {//编辑组名
            GroupTag tag = (GroupTag)(node.Tag);
            xmlGroupDao.modifyGroupName(tag.tagXmlNode, newName);

        }

        public void modifyAll(TreeNode node, Dictionary<string, string> newAttributes)
        {//编辑所有属性
            GroupTag tag = (GroupTag)(node.Tag);
            xmlGroupDao.modifyAllAttribute(tag.tagXmlNode, newAttributes);
        }

        public void deleSelectedNode()
        {//删除选中的节点
            TreeNode selectNode = groups_treeView.SelectedNode;
            if (selectNode == null)
            {
                return;
            }
            if (selectNode.Nodes.Count > 0)
            {//需要被删除的节点下有子节点，则提示警告是否删除
                DialogResult re = MessageBox.Show("此节点下有子节点，确认是否删除此组及此组下的所有组？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (re == DialogResult.Cancel)
                {
                    return;
                }
            }
            //删除，不仅要删除xml文件中的对应项，还要删除数据库中的分组
            GroupTag tag = (GroupTag)(selectNode.Tag);
            xmlGroupDao.deleteGroup(tag.tagXmlNode);//删除xml里面
            selectNode.Nodes.Remove(selectNode);//treeview中删除
        }

        public void refresh()
        {//刷新，同步xml文件与treeView
            groups_treeView.Nodes.Clear();
            xmlGroupDao.loadToTreeView();
            groups_treeView.ExpandAll();
        }

        public void addXmlGroupTask(string xmlFile)
        {//添加分组任务
            xmlGroupDao.addXml(xmlFile);
        }

        public void exportXmlGroupTask(string fileName)
        {//将分组任务导出
            xmlGroupDao.exportXml(fileName);
        }

        public bool allowDropAt(TreeNode treeNode)
        {//判断是否允许拖放到这个节点
            /*if(treeNode.Tag == null)
            {//这是个民兵节点
                return false;
            }
            GroupTag tag = (GroupTag)treeNode.Tag;
            if(tag.militias != null)
            {//不为空，说明可以分组
                return true;
            }*/
            if(treeNode.Nodes.Count == 0)
            {//是叶节点
                return true;
            }
            return false;
        }

        /*public TreeNode getTreeNodeByText(TreeNodeCollection Nodes, string text)
        {
            foreach(TreeNode treeNode in Nodes)
            {
                if(treeNode.Text == text)
                {
                    return treeNode;
                }
            }
            return null;
        }*/

        public TreeNode getTreeNodeByText(string text)
        {
            /*string[] groups = text.Split(new Char[] { '/' });
            TreeNodeCollection Nodes = groups_treeView.Nodes;
            TreeNode treeNode = null;
            foreach (string groupName in groups)
            {
                treeNode = getTreeNodeByText(Nodes, groupName);
                if(treeNode == null)
                {
                    return null;
                }
                Nodes = treeNode.Nodes;
            }*/
            TreeNode[] groupNodes = groups_treeView.Nodes.Find(text, true);
            if(groupNodes.Count() == 0)
            {
                return null;
            } else if(groupNodes.Count() > 1)
            {
                MessageBox.Show(groupNodes[0].Name + " ? " + groupNodes[1].Name + "?" + groupNodes.Count());
            }


            return groupNodes[0];
        }

        /*public void removeMilitaNode(Militia militia)
        {//删除一个民兵的信息（可能在编辑界面删除了某个分了组的民兵）
            ;//找到他原来的组节点
            TreeNode groupNode = getTreeNodeByText(militia.Group);
            reduceCount(groupNode);
            //改变组节点上显示的民兵数量
            *int index = groupTag.militias.FindIndex(delegate (Militia m) {//不同session查询出的militia对象不是同一个,故根据Id判断
                if (m.Id == militia.Id)
                {
                    return true;
                }
                return false;
            });
            if (index >= 0)
            {
                groupTag.militias.RemoveAt(index);
                groupNode.Nodes.RemoveAt(index);
            }*
        }*/

        public void reduceCount(Militia militia)
        {//减少民兵的分组上面的民兵个数
            TreeNode groupNode = getTreeNodeByText(militia.Group);
            reduceCount(groupNode, 1);

            //xml
            GroupTag gt = (GroupTag)groupNode.Tag;
            gt.tagXmlNode.Attributes["currentCount"].Value = gt.Count.ToString();
            xmlGroupDao.saveXml();
        }

        public void addCount(TreeNode node, int Count)
        {
            TreeNode startNode = node;//xml

            while(node != null)
            {
                GroupTag tag = (GroupTag)node.Tag;
                tag.Count += Count;
                node.Text = tag.info();

                node = node.Parent;
            }

            //xml
            GroupTag gt = (GroupTag)startNode.Tag;
            gt.tagXmlNode.Attributes["currentCount"].Value = gt.Count.ToString();
            xmlGroupDao.saveXml();
        }

        public void reduceCount(TreeNode node, int Count)
        {
            while (node != null)
            {
                GroupTag tag = (GroupTag)node.Tag;
                tag.Count -= Count;
                node.Text = tag.info();

                node = node.Parent;
            }
        }
    }
}
