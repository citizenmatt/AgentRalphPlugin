//------------------------------------------------------------------------------
// <auto-generated>
//     Generated by IntelliJ parserGen
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
#pragma warning disable 0168, 0219, 0108, 0414
// ReSharper disable RedundantNameQualifier
using System;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.PsiPlugin.Parsing;
namespace JetBrains.ReSharper.PsiPlugin.Tree.Impl {
  internal partial class PathDeclaration : PsiCompositeElement, JetBrains.ReSharper.PsiPlugin.Tree.IPathDeclaration {
    public const short PSI_PATH_NAME= ChildRole.LAST + 1;
    internal PathDeclaration() : base() {
    }
    public override JetBrains.ReSharper.Psi.ExtensionsAPI.Tree.NodeType NodeType {
      get { return JetBrains.ReSharper.PsiPlugin.Tree.Impl.ElementType.PATH_DECLARATION; }
    }
    public override void Accept(JetBrains.ReSharper.PsiPlugin.Tree.TreeNodeVisitor visitor) {
      visitor.VisitPathDeclaration(this);
    }
    public override void Accept<TContext>(JetBrains.ReSharper.PsiPlugin.Tree.TreeNodeVisitor<TContext> visitor, TContext context) {
      visitor.VisitPathDeclaration(this, context);
    }
    public override TReturn Accept<TContext, TReturn>(JetBrains.ReSharper.PsiPlugin.Tree.TreeNodeVisitor<TContext, TReturn> visitor, TContext context) {
      return visitor.VisitPathDeclaration(this, context);
    }
    private static readonly JetBrains.ReSharper.Psi.ExtensionsAPI.Tree.NodeTypeDictionary<short> CHILD_ROLES = new JetBrains.ReSharper.Psi.ExtensionsAPI.Tree.NodeTypeDictionary<short>(
      new System.Collections.Generic.KeyValuePair<JetBrains.ReSharper.Psi.ExtensionsAPI.Tree.NodeType, short>[]
      {
        new System.Collections.Generic.KeyValuePair<JetBrains.ReSharper.Psi.ExtensionsAPI.Tree.NodeType, short>(JetBrains.ReSharper.PsiPlugin.Tree.Impl.ElementType.PATH_NAME, PSI_PATH_NAME),
      }
    );
    public override short GetChildRole (JetBrains.ReSharper.Psi.ExtensionsAPI.Tree.TreeElement child) {
      return CHILD_ROLES[child.NodeType];
    }
    public virtual  JetBrains.ReSharper.PsiPlugin.Tree.IPathName PathName {
      get
      {
        CompositeElement current = this;  
    
        JetBrains.ReSharper.PsiPlugin.Tree.IPathName result = null;
        TreeElement current0 = current.FindChildByRole (JetBrains.ReSharper.PsiPlugin.Tree.Impl.PathDeclaration.PSI_PATH_NAME);
        if (current0 != null) {
          result = (JetBrains.ReSharper.PsiPlugin.Tree.IPathName) current0;
        }
        return result;
      }
    }
    public virtual JetBrains.ReSharper.PsiPlugin.Tree.IPathName SetPathName (JetBrains.ReSharper.PsiPlugin.Tree.IPathName param)
    {
      using (JetBrains.Application.WriteLockCookie.Create (this.IsPhysical()))
      {
        TreeElement current = null, next = GetNextFilteredChild (current), result = null;
        next = GetNextFilteredChild (current);
        if (next == null) {
          if (param == null) return null;
          result = current = (TreeElement)JetBrains.ReSharper.Psi.ExtensionsAPI.Tree.ModificationUtil.AddChildAfter (this, current, (JetBrains.ReSharper.Psi.Tree.ITreeNode)param);
        } else {
          if (next.NodeType == JetBrains.ReSharper.PsiPlugin.Tree.Impl.ElementType.PATH_NAME) {
            if (param != null) {
              result = current = (TreeElement)JetBrains.ReSharper.Psi.ExtensionsAPI.Tree.ModificationUtil.ReplaceChild(next, (JetBrains.ReSharper.Psi.Tree.ITreeNode)param);
            } else {
              current = GetNextFilteredChild (next);
              JetBrains.ReSharper.Psi.ExtensionsAPI.Tree.ModificationUtil.DeleteChild (next);
            }
          } else {
            if (param == null) return null;
            result = (TreeElement)JetBrains.ReSharper.Psi.ExtensionsAPI.Tree.ModificationUtil.AddChildBefore(next, (JetBrains.ReSharper.Psi.Tree.ITreeNode)param);
            current = next;
          }
        }
        return (JetBrains.ReSharper.PsiPlugin.Tree.IPathName)result;
      }
    }
    public override string ToString() {
      return "IPathDeclaration";
    }
  }
}