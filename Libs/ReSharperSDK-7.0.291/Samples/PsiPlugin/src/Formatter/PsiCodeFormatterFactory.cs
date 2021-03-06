﻿using JetBrains.ProjectModel;
using JetBrains.ReSharper.PsiPlugin.Grammar;

namespace JetBrains.ReSharper.PsiPlugin.Formatter
{
  [ProjectFileType(typeof (PsiProjectFileType))]
  public class PsiCodeFormatterFactory : IPsiCodeFormatterFactory
  {
    #region IPsiCodeFormatterFactory Members

    public PsiFormattingVisitor CreateFormattingVisitor(FormattingStageData formattingData)
    {
      return new PsiFormattingVisitor(formattingData);
    }

    #endregion
  }
}
