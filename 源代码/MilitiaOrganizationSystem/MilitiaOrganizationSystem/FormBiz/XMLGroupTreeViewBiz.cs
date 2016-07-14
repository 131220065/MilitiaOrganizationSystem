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
    {//处理treeView上的增删改等，业务逻辑层
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

        /*public void addUnderSelectedNode()
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
        }*/

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
            if(treeNode.Nodes.Count == 0)
            {//是叶节点
                return true;
            }
            return false;
        }

        public TreeNode getTreeNodeByText(string text)
        {//根据路径获取treeNode节点
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

        public void reduceCount(Militia militia)
        {//减少民兵的分组上面的民兵个数
            TreeNode groupNode = getTreeNodeByText(militia.Group);
            reduceCount(groupNode, 1);
        }

        public void addCount(TreeNode node, int Count)
        {//让本结点及所有父节点上数量增加Count
            TreeNode startNode = node;//xml

            while(node != null)
            {
                GroupTag tag = (GroupTag)node.Tag;
                tag.Count += Count;
                node.Text = tag.info();

                node = node.Parent;
            }

            //xml
            if (startNode != null)
            {//同时改变xml文件的值
                GroupTag gt = (GroupTag)startNode.Tag;
                gt.tagXmlNode.Attributes["currentCount"].Value = gt.Count.ToString();
                xmlGroupDao.saveXml();
            }
        }

        public void reduceCount(TreeNode node, int Count)
        {//让本结点及所有父节点上数量减少Count
            TreeNode startNode = node;//xml

            while (node != null)
            {
                GroupTag tag = (GroupTag)node.Tag;
                tag.Count -= Count;
                node.Text = tag.info();

                node = node.Parent;
            }

            //xml
            if (startNode != null)
            {//同时更新xml文件中的值
                GroupTag gt = (GroupTag)startNode.Tag;
                gt.tagXmlNode.Attributes["currentCount"].Value = gt.Count.ToString();
                xmlGroupDao.saveXml();
            }
        }

        public void removeGroupNums(List<Raven.Abstractions.Data.FacetValue> fvList)
        {//删除xml文件中关于fvList的部分,fvList中存在的，减少相应数量
            foreach(Raven.Abstractions.Data.FacetValue fv in fvList)
            {
                TreeNode treeNode = getTreeNodeByText(fv.Range);
                if(treeNode != null)
                {//减少相应数量
                    GroupTag gt = (GroupTag)treeNode.Tag;
                    gt.Count -= fv.Hits;
                    treeNode.Text = gt.info();
                    gt.tagXmlNode.Attributes["currentCount"].Value = gt.Count.ToString();
                }
            }
            xmlGroupDao.saveXml();//保存xml文件
        }

        private void refreshTreeNodes(TreeNodeCollection nodes, Dictionary<string, Raven.Abstractions.Data.FacetValue> fdict)
        {
            foreach(TreeNode treeNode in nodes)
            {
                if(treeNode.Nodes.Count == 0)
                {//叶节点
                    GroupTag gt = (GroupTag)treeNode.Tag;
                    Raven.Abstractions.Data.FacetValue fv;
                    if(fdict.TryGetValue(treeNode.Name, out fv))
                    {//字典里面有这个值
                        int needAddCount = fv.Hits - gt.Count;
                        if(needAddCount != 0)
                        {
                            addCount(treeNode, needAddCount);//增和减是一个道理
                        }
                        //为0则啥也不做
                    } else
                    {//没有这个值，则为0
                        if(gt.Count != 0)
                        {
                            reduceCount(treeNode, gt.Count);//减为0
                        }
                    }
                } else
                {//非叶子节点
                    refreshTreeNodes(treeNode.Nodes, fdict);
                }
            }
        }

        public void refreshTreeView(Dictionary<string, Raven.Abstractions.Data.FacetValue> fdict)
        {
            refreshTreeNodes(groups_treeView.Nodes, fdict);
        }
    }
}
