//------------------------------------------------------------------------------
// <auto-generated>
//     Generated by IntelliJ parserGen
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
#pragma warning disable 0168, 0219, 0108, 0414
// ReSharper disable RedundantNameQualifier
using System.Collections;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.PsiPlugin.Tree.Impl;
namespace JetBrains.ReSharper.PsiPlugin.Tree {
  public static partial class PsiFileNavigator {
    [JetBrains.Annotations.Pure]
    [JetBrains.Annotations.CanBeNull]
    [JetBrains.Annotations.ContractAnnotation("null <= null")]
    public static JetBrains.ReSharper.PsiPlugin.Tree.IPsiFile GetByInterfaces (JetBrains.ReSharper.PsiPlugin.Tree.IInterfacesDefinition param) {
      if (param == null) return null;
      TreeElement current = (TreeElement)param;
      if (current.parent is JetBrains.ReSharper.PsiPlugin.Tree.Impl.PsiFile) {
        if (current.parent.GetChildRole (current) != JetBrains.ReSharper.PsiPlugin.Tree.Impl.PsiFile.PSI_INTERFACES) return null;
        current = current.parent;
      } else return null;
      return (JetBrains.ReSharper.PsiPlugin.Tree.IPsiFile) current;
    }
    [JetBrains.Annotations.Pure]
    [JetBrains.Annotations.CanBeNull]
    [JetBrains.Annotations.ContractAnnotation("null <= null")]
    public static JetBrains.ReSharper.PsiPlugin.Tree.IPsiFile GetByPaths (JetBrains.ReSharper.PsiPlugin.Tree.IPathsDeclaration param) {
      if (param == null) return null;
      TreeElement current = (TreeElement)param;
      if (current.parent is JetBrains.ReSharper.PsiPlugin.Tree.Impl.PsiFile) {
        if (current.parent.GetChildRole (current) != JetBrains.ReSharper.PsiPlugin.Tree.Impl.PsiFile.PSI_PATHS) return null;
        current = current.parent;
      } else return null;
      return (JetBrains.ReSharper.PsiPlugin.Tree.IPsiFile) current;
    }
  }
}
