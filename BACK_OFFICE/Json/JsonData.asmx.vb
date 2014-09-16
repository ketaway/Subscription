
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Services
Public Class jqGridResponse
    Property total As Int16 = 1
    Property records As Int16 = 1
    Property page As Int16 = 1
    Property rows As List(Of GridData)

End Class

Public Class GridData
    Property Prop1 As String = ""
    Property Prop2 As String = ""
    Property Prop3 As String = ""
End Class

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class JsonData
    Inherits System.Web.Services.WebService


    <WebMethod()> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Function GetJSONData() As jqGridResponse
        Dim response As New jqGridResponse

        Dim gd As New GridData
        gd.Prop1 = "Something"
        gd.Prop2 = "Something Else"
        gd.Prop3 = "More Interesting Data"

        response.rows = New List(Of GridData)
        response.rows.Add(gd)

        Return response

    End Function
    

End Class