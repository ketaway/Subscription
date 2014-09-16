Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient

#Region "T_SUBSCRIPTION_REPORT"
''' <summary>
''' This object represents the properties and methods of a T_MENU.
''' </summary>
Public Class T_SUBSCRIPTION_REPORT
    Private _id As System.Int16
    Private _mENU_NAME As System.String = String.Empty
    Private _oRDER_NO As System.String = String.Empty
    Private _aCTIVE As System.Boolean
    Private _pARENT_ID As System.Int16
    Private _mENU_ICON As System.String = String.Empty

    Public Sub New()
    End Sub
    '   -- Add the parameters for the stored procedure here
    '@AUTHEN_ID as smallint,
    '@DATEFROM AS VARCHAR(10),
    '@DATETO AS VARCHAR(10)
    Public Function getReport(ByVal AUTHEN_ID As System.Int16, DATEFROM As String, DATETO As String, SERVICE_ID As String, TOPIC_NO As String, TELCO As String) As DataSet
        ' NOTE: If this reference doesn't exist then add SqlService.vb from the template directory to your solution.
        Dim sql As New clsSqlService("subscription")
        sql.AddParameter("@AUTHEN_ID", SqlDbType.SmallInt, AUTHEN_ID)
        sql.AddParameter("@DATEFROM", SqlDbType.VarChar, DATEFROM)
        sql.AddParameter("@DATETO", SqlDbType.VarChar, DATETO)
        sql.AddParameter("@SERVICE_ID", SqlDbType.VarChar, SERVICE_ID)
        sql.AddParameter("@TOPIC_NO", SqlDbType.VarChar, TOPIC_NO)
        sql.AddParameter("@OPERATOR", SqlDbType.VarChar, TELCO)

        Dim DS As New DataSet
        DS = sql.ExecuteSPDataSet("getReport")

        Return DS
    End Function
    Public Function getReportCharging_By_Oper(ByVal AUTHEN_ID As System.Int16, DATEFROM As String, DATETO As String, SERVICE_ID As String, TOPIC_NO As String) As DataSet
        ' NOTE: If this reference doesn't exist then add SqlService.vb from the template directory to your solution.
        Dim sql As New clsSqlService("subscription")
        sql.AddParameter("@AUTHEN_ID", SqlDbType.SmallInt, AUTHEN_ID)
        sql.AddParameter("@DATEFROM", SqlDbType.VarChar, DATEFROM)
        sql.AddParameter("@DATETO", SqlDbType.VarChar, DATETO)
        sql.AddParameter("@SERVICE_ID", SqlDbType.VarChar, SERVICE_ID)
        sql.AddParameter("@TOPIC_NO", SqlDbType.VarChar, TOPIC_NO)

        Dim DS As New DataSet
        DS = sql.ExecuteSPDataSet("getSubscriptionOperatorReport")

        Return DS
    End Function
    Public Function getContentSchedule(SERVICE_ID As Int16, TOPIC_NO As String, DATEFROM As String, DATETO As String) As DataSet
        ' NOTE: If this reference doesn't exist then add SqlService.vb from the template directory to your solution.
        Dim sql As New clsSqlService("subscription")
        sql.AddParameter("@ServiceID", SqlDbType.SmallInt, SERVICE_ID)
        sql.AddParameter("@TopicNo", SqlDbType.VarChar, TOPIC_NO)
        sql.AddParameter("@DATEFROM", SqlDbType.VarChar, DATEFROM)
        sql.AddParameter("@DATETO", SqlDbType.VarChar, DATETO)

        Dim DS As New DataSet
        DS = sql.ExecuteSPDataSet("getContentSchedule")

        Return DS
    End Function
    Public Function getServiceOwner(ByVal AUTHEN_ID As System.Int16) As DataSet
        ' NOTE: If this reference doesn't exist then add SqlService.vb from the template directory to your solution.
        Dim sql As New clsSqlService("subscription")
        sql.AddParameter("@AUTHEN_ID", SqlDbType.SmallInt, AUTHEN_ID)

        Dim DS As New DataSet
        DS = sql.ExecuteSPDataSet("getServiceOwner")

        Return DS
    End Function
    Public Function getSubServiceOwner(ByVal AUTHEN_ID As System.Int16, SERVICE_ID As Int16) As DataSet
        ' NOTE: If this reference doesn't exist then add SqlService.vb from the template directory to your solution.
        Dim sql As New clsSqlService("subscription")
        sql.AddParameter("@AUTHEN_ID", SqlDbType.SmallInt, AUTHEN_ID)
        sql.AddParameter("@SERVICE_ID", SqlDbType.SmallInt, SERVICE_ID)

        Dim DS As New DataSet
        DS = sql.ExecuteSPDataSet("getSubServiceOwner")

        Return DS
    End Function
    'getSubServiceOwner
    Public Sub New(ByVal reader As SqlDataReader)
        Me.LoadFromReader(reader)
    End Sub

#Region "Public Medthod"

    Public Function getLeftMenu(ByVal Authen_ID As Int16) As DataSet
        ' NOTE: If this reference doesn't exist then add SqlService.vb from the template directory to your solution.
        Dim sql As New clsSqlService()
        sql.AddParameter("@Authen_ID", SqlDbType.SmallInt, Authen_ID)
        sql.AutoCloseConnection = True
        Return sql.ExecuteSPDataSet("getLeftMenu")
    End Function

#Region "Gen From CodeSmith"
    Public Sub Delete()
        T_MENU.Delete(Id)
    End Sub

    Public Sub Update()
        Dim sql As New clsSqlService()
        Dim queryParameters As New StringBuilder()

        sql.AddParameter("@MENU_ID", SqlDbType.SmallInt, Id)
        queryParameters.Append("MENU_ID = @MENU_ID")

        sql.AddParameter("@MENU_NAME", SqlDbType.VarChar, MENU_NAME)
        queryParameters.Append(", MENU_NAME = @MENU_NAME")
        sql.AddParameter("@ORDER_NO", SqlDbType.VarChar, ORDER_NO)
        queryParameters.Append(", ORDER_NO = @ORDER_NO")
        sql.AddParameter("@ACTIVE", SqlDbType.Bit, ACTIVE)
        queryParameters.Append(", ACTIVE = @ACTIVE")
        sql.AddParameter("@PARENT_ID", SqlDbType.SmallInt, PARENT_ID)
        queryParameters.Append(", PARENT_ID = @PARENT_ID")
        sql.AddParameter("@MENU_ICON", SqlDbType.VarChar, MENU_ICON)
        queryParameters.Append(", MENU_ICON = @MENU_ICON")

        Dim query As String = [String].Format("Update T_MENU Set {0} Where MENU_ID = @MENU_ID", queryParameters.ToString())
        Dim reader As SqlDataReader = sql.ExecuteSqlReader(query)
    End Sub

    Public Sub Create()
        Dim sql As New clsSqlService()
        Dim queryParameters As New StringBuilder()

        sql.AddParameter("@MENU_ID", SqlDbType.SmallInt, Id)
        queryParameters.Append("@MENU_ID")

        sql.AddParameter("@MENU_NAME", SqlDbType.VarChar, MENU_NAME)
        queryParameters.Append(", @MENU_NAME")
        sql.AddParameter("@ORDER_NO", SqlDbType.VarChar, ORDER_NO)
        queryParameters.Append(", @ORDER_NO")
        sql.AddParameter("@ACTIVE", SqlDbType.Bit, ACTIVE)
        queryParameters.Append(", @ACTIVE")
        sql.AddParameter("@PARENT_ID", SqlDbType.SmallInt, PARENT_ID)
        queryParameters.Append(", @PARENT_ID")
        sql.AddParameter("@MENU_ICON", SqlDbType.VarChar, MENU_ICON)
        queryParameters.Append(", @MENU_ICON")

        Dim query As String = [String].Format("Insert Into T_MENU ({0}) Values ({1})", queryParameters.ToString().Replace("@", ""), queryParameters.ToString())
        Dim reader As SqlDataReader = sql.ExecuteSqlReader(query)
    End Sub

    
#End Region
#End Region

#Region "Private Medthod"
    Protected Sub LoadFromReader(ByVal reader As SqlDataReader)
        If Not IsNothing(reader) AndAlso Not reader.IsClosed Then
            _id = reader.GetInt16(0)
            If Not reader.IsDBNull(1) Then
                _mENU_NAME = reader.GetString(1)
            End If
            If Not reader.IsDBNull(2) Then
                _oRDER_NO = reader.GetString(2)
            End If
            If Not reader.IsDBNull(3) Then
                _aCTIVE = reader.GetBoolean(3)
            End If
            If Not reader.IsDBNull(4) Then
                _pARENT_ID = reader.GetInt16(4)
            End If
            If Not reader.IsDBNull(5) Then
                _mENU_ICON = reader.GetString(5)
            End If
        End If
    End Sub
#End Region



#Region "Public Properties"
    Public Property Id() As System.Int16
        Get
            Return _id
        End Get
        Set(ByVal value As System.Int16)
            _id = value
        End Set
    End Property

    Public Property MENU_NAME() As System.String
        Get
            Return _mENU_NAME
        End Get
        Set(ByVal value As System.String)
            _mENU_NAME = value
        End Set
    End Property

    Public Property ORDER_NO() As System.String
        Get
            Return _oRDER_NO
        End Get
        Set(ByVal value As System.String)
            _oRDER_NO = value
        End Set
    End Property

    Public Property ACTIVE() As System.Boolean
        Get
            Return _aCTIVE
        End Get
        Set(ByVal value As System.Boolean)
            _aCTIVE = value
        End Set
    End Property

    Public Property PARENT_ID() As System.Int16
        Get
            Return _pARENT_ID
        End Get
        Set(ByVal value As System.Int16)
            _pARENT_ID = value
        End Set
    End Property

    Public Property MENU_ICON() As System.String
        Get
            Return _mENU_ICON
        End Get
        Set(ByVal value As System.String)
            _mENU_ICON = value
        End Set
    End Property

#End Region

    Public Shared Function GetT_MENU(ByVal id As String) As T_MENU
        Return New T_MENU(id)
    End Function

    Public Shared Sub Delete(ByVal id As System.Int16)
        Dim sql As New clsSqlService()
        sql.AddParameter("@MENU_ID", SqlDbType.SmallInt, id)
        Dim reader As SqlDataReader = sql.ExecuteSqlReader("Delete T_MENU Where MENU_ID = @MENU_ID")

    End Sub

End Class
#End Region
