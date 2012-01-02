﻿// SharpDevelop samples
// Copyright (c) 2006, AlphaSierraPapa
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without modification, are
// permitted provided that the following conditions are met:
//
// - Redistributions of source code must retain the above copyright notice, this list
//   of conditions and the following disclaimer.
//
// - Redistributions in binary form must reproduce the above copyright notice, this list
//   of conditions and the following disclaimer in the documentation and/or other materials
//   provided with the distribution.
//
// - Neither the name of the SharpDevelop team nor the names of its contributors may be used to
//   endorse or promote products derived from this software without specific prior written
//   permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS &AS IS& AND ANY EXPRESS
// OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY
// AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
// CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
// DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER
// IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT
// OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;

using ICSharpCode.NRefactory.Ast;
using ICSharpCode.NRefactory;
using ICSharpCode.NRefactory.Visitors;

namespace NRefactoryDemo
{
	public partial class AstView
	{
		INode unit;

        public INode Unit
        {
			get {
				return unit;
			}
			set {
				unit = value;
				UpdateTree();
			}
		}
		
		void UpdateTree()
		{
			tree.Nodes.Clear();

            GetImmediateChildrenVisitor v = new GetImmediateChildrenVisitor();
		    unit.AcceptVisitor(v, null);
            
			tree.Nodes.Add(new CollectionNode("RootNode", v.Children));
			tree.SelectedNode = tree.Nodes[0];
		}
		
		public AstView()
		{
			InitializeComponent();
		}

	    private void DeleteSelectedNode()
		{
			if (tree.SelectedNode is ElementNode) {
				INode element = (tree.SelectedNode as ElementNode).element;
				if (tree.SelectedNode.Parent is CollectionNode) {
					if (MessageBox.Show("Remove selected node from parent collection?", "Remove node", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
					    == DialogResult.Yes)
					{
						IList col = (tree.SelectedNode.Parent as CollectionNode).collection;
						col.Remove(element);
						(tree.SelectedNode.Parent as CollectionNode).Update();
					}
				} else if (tree.SelectedNode.Parent is ElementNode) {
					if (MessageBox.Show("Set selected property to null?", "Remove node", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
					    == DialogResult.Yes)
					{
						// get parent element
						element = (tree.SelectedNode.Parent as ElementNode).element;
						string propertyName = (string)tree.SelectedNode.Tag;
						element.GetType().GetProperty(propertyName).SetValue(element, null, null);
						(tree.SelectedNode.Parent as ElementNode).Update();
					}
				}
			} else if (tree.SelectedNode is CollectionNode) {
				if (MessageBox.Show("Remove all elements from selected collection?", "Clear collection", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
				    == DialogResult.Yes)
				{
					IList col = (tree.SelectedNode as CollectionNode).collection;
					col.Clear();
					(tree.SelectedNode as CollectionNode).Update();
				}
			}
		}
		
//		public void EditSelectedNode()
//		{
//			TreeNode node = tree.SelectedNode;
//			while (!(node is ElementNode)) {
//				if (node == null) {
//					return;
//				}
//				node = node.Parent;
//			}
//			INode element = ((ElementNode)node).element;
//			using (EditDialog dlg = new EditDialog(element)) {
//				dlg.ShowDialog();
//			}
//			((ElementNode)node).Update();
//		}
		
		public void ApplyTransformation(IAstVisitor visitor)
		{
			if (tree.SelectedNode == tree.Nodes[0]) {
				unit.AcceptVisitor(visitor, null);
				UpdateTree();
			} else {
				string name = visitor.GetType().Name;
				ElementNode elementNode = tree.SelectedNode as ElementNode;
				CollectionNode collectionNode = tree.SelectedNode as CollectionNode;
				if (elementNode != null) {
					if (MessageBox.Show(("Apply " + name + " to selected element '" + elementNode.Text + "'?"),
					                    "Apply transformation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
					    == DialogResult.Yes)
					{
						elementNode.element.AcceptVisitor(visitor, null);
						elementNode.Update();
					}
				} else if (collectionNode != null) {
					if (MessageBox.Show(("Apply " + name + " to all elements in selected collection '" + collectionNode.Text + "'?"),
					                    "Apply transformation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
					    == DialogResult.Yes)
					{
						foreach (TreeNode subNode in collectionNode.Nodes) {
							if (subNode is ElementNode) {
								(subNode as ElementNode).element.AcceptVisitor(visitor, null);
							}
						}
						collectionNode.Update();
					}
				}
			}
		}
		
		static TreeNode CreateNode(object child)
		{
			if (child == null) {
				return new TreeNode("*null reference*");
			} else if (child is INode) {
				return new ElementNode(child as INode);
			} else {
				return new TreeNode(child.ToString());
			}
		}
		
		class CollectionNode : TreeNode
		{
			internal IList collection;
			string baseName;
			
			public CollectionNode(string text, IList children) : base(text)
			{
				this.baseName = text;
				this.collection = children;
				Update();
			}
			
			public void Update()
			{
				if (collection.Count == 0) {
					Text = baseName + " (empty collection)";
				} else if (collection.Count == 1) {
					Text = baseName + " (collection with 1 element)";
				} else {
					Text = baseName + " (collection with " + collection.Count + " elements)";
				}
				Nodes.Clear();
				foreach (object child in collection) {
					Nodes.Add(CreateNode(child));
				}
				Expand();
			}
		}
		
		class ElementNode : TreeNode
		{
			internal readonly INode element;
			
			public ElementNode(INode node)
			{
				this.element = node;
				Update();
			}
			
			public void Update()
			{
				Nodes.Clear();
				Type type = element.GetType();
				Text = type.Name;
				if (Tag != null) { // HACK: after editing property element
					Text = Tag.ToString() + " = " + Text;
				}
				if (!(element is INullable && (element as INullable).IsNull)) {
					AddProperties(type, element);
					if (element.Children.Count > 0) {
						Nodes.Add(new CollectionNode("Children", element.Children));
					}
				}
			}
			
			void AddProperties(Type type, INode node)
			{
				if (type == typeof(AbstractNode))
					return;
				foreach (PropertyInfo pi in type.GetProperties(BindingFlags.Instance | BindingFlags.Public)) {
					if (pi.DeclaringType != type) // don't add derived properties
						continue;
					if (pi.Name == "IsNull")
						continue;
					object value = pi.GetValue(node, null);
					if (value is IList) {
						Nodes.Add(new CollectionNode(pi.Name, (IList)value));
					} else if (value is string) {
						Text += " " + pi.Name + "='" + value + "'";
					} else {
						TreeNode treeNode = CreateNode(value);
						treeNode.Text = pi.Name + " = " + treeNode.Text;
						treeNode.Tag = pi.Name;
						Nodes.Add(treeNode);
					}
				}
				AddProperties(type.BaseType, node);
			}
		}
		
		void TreeKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Delete) {
				DeleteSelectedNode();
			} else if (e.KeyData == Keys.Space || e.KeyData == Keys.Enter) {
				//EditSelectedNode();
			}
		}

        /// <summary>
        /// The INode.Children property is not reliably populated for some annoying reason.  Therefore we 
        /// must have a custom implementation of getting children for each node type.
        /// Recursive extract method won't work on node types that aren't handled in here.
        /// </summary>
        private class GetImmediateChildrenVisitor : AbstractAstVisitor
        {
            public List<INode> Children { get; private set; }

            public GetImmediateChildrenVisitor()
            {
                Children = new List<INode>();
            }

            public override object VisitBlockStatement(BlockStatement bs, object data)
            {
                Children = bs.Children;
                return null;
            }

            public override object VisitIfElseStatement(IfElseStatement ifElseStatement, object data)
            {
                Children = ifElseStatement.TrueStatement.ConvertAll(x => (INode)x);
                return null;
            }

            public override object VisitSwitchStatement(SwitchStatement switchStatement, object data)
            {
                Children = switchStatement.SwitchSections.ConvertAll(x => (INode)x);
                return null;
            }

            public override object VisitSwitchSection(SwitchSection switchSection, object data)
            {
                Children = switchSection.Children;
                return null;
            }
        }

	}
}
