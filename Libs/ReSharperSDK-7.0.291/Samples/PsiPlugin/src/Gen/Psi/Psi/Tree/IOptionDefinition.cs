//------------------------------------------------------------------------------
// <auto-generated>
//     Generated by IntelliJ parserGen
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
#pragma warning disable 0168, 0219, 0108, 0414
// ReSharper disable RedundantNameQualifier
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
namespace JetBrains.ReSharper.PsiPlugin.Tree {
  public partial interface IOptionDefinition : JetBrains.ReSharper.PsiPlugin.Tree.IPsiTreeNode {
    JetBrains.ReSharper.Psi.Tree.ITokenNode OptionIdentifierValue { get; }
  
    JetBrains.ReSharper.Psi.Tree.ITokenNode OptionIntegerValue { get; }
  
    JetBrains.ReSharper.PsiPlugin.Tree.IOptionName OptionName { get; }
  
    JetBrains.ReSharper.PsiPlugin.Tree.IOptionStringValue OptionStringValue { get; }
  
    JetBrains.ReSharper.PsiPlugin.Tree.IOptionName SetOptionName (JetBrains.ReSharper.PsiPlugin.Tree.IOptionName param);
  
    JetBrains.ReSharper.PsiPlugin.Tree.IOptionStringValue SetOptionStringValue (JetBrains.ReSharper.PsiPlugin.Tree.IOptionStringValue param);
  
  }
}
