Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient

#Region "T_MENU"
    ''' <summary>
    ''' This object represents the properties and methods of a T_MENU.
    ''' </summary>
    Public Class T_MENU
        Private _id As System.Int16
        Private _mENU_NAME As System.String = String.Empty
        Private _oRDER_NO As System.String = String.Empty
        Private _aCTIVE As System.Boolean
        Private _pARENT_ID As System.Int16
        Private _mENU_ICON As System.String = String.Empty

        Public Sub New()
        End Sub

        Public Sub New(ByVal id As System.Int16)
            ' NOTE: If this reference doesn't exist then add SqlService.vb from the template directory to your solution.
        Dim sql As New clsSqlService()
            sql.AddParameter("@MENU_ID", SqlDbType.VarChar, id)
            Dim reader As SqlDataReader = sql.ExecuteSqlReader("SELECT * FROM T_MENU WHERE MENU_ID = @MENU_ID")

            If reader.Read() Then
                Me.LoadFromReader(reader)
                reader.Close()
            Else
                If Not reader.IsClosed Then
                    reader.Close()
                End If
                Throw New ApplicationException("T_MENU does not exist.")
            End If
        End Sub

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
    '    - =============================================
    'CREATE PROCEDURE getNavBar
    '	-- Add the parameters for the stored procedure here
    '	@MenuID as smallint,
    '	@ParentID as smallint,
    '	@MenuName as varchar(100) output,
    '	@ParentMenuName as varchar(100) output

    Public Sub getNavBar(ByVal MenuID As Int16, ByVal ParentID As Int16, ByRef MenuName As String, ByRef ParentMenuName As String)
        ' NOTE: If this reference doesn't exist then add SqlService.vb from the template directory to your solution.
        Dim sql As New clsSqlService()
        sql.AddParameter("@MenuID", SqlDbType.SmallInt, MenuID)
        sql.AddParameter("@ParentID", SqlDbType.SmallInt, ParentID)
        sql.AddOutputParameter("@MenuName", SqlDbType.VarChar, 100)
        sql.AddOutputParameter("@ParentMenuName", SqlDbType.VarChar, 100)
        sql.AutoCloseConnection = True
        sql.ExecuteSP("getNavBar")
        MenuName = sql.Parameters("@MenuName").Value
        ParentMenuName = sql.Parameters("@ParentMenuName").Value
    End Sub
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

    Public Shared Function NewT_MENU(ByVal id As System.Int16) As T_MENU
        Dim newEntity As New T_MENU()
        newEntity._id = id

        Return newEntity
    End Function
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
