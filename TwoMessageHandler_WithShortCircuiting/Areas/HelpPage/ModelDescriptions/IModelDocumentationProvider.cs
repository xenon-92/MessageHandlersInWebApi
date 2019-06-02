using System;
using System.Reflection;

namespace TwoMessageHandler_WithShortCircuiting.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}