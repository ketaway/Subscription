Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports BACK_OFFICE.SchemaExplorer

Namespace BusinessObjects
#Region "T_AUTHENTICATION"
    ''' <summary>
    ''' This object represents the properties and methods of a T_AUTHENTICATION.
    ''' </summary>
    Public Class T_AUTHENTICATION
        Private _id As System.Int16
        Private _nAME As System.String = String.Empty
        Private _sURNAME As System.String = String.Empty
        Private _uSERNAME As System.String = String.Empty
        Private _pASSWORD As System.String = String.Empty
        Private _cOMPANY As System.String = String.Empty
        Private _aCTIVE As System.Boolean

        Public Sub New()
        End Sub

        Public Sub New(ByVal id As System.Int16)
            ' NOTE: If this reference doesn't exist then add SqlService.vb from the template directory to your solution.
            Dim sql As New SqlService()
            sql.AddParameter("@AUTHEN_ID", SqlDbType.VarChar, id)
            Dim reader As SqlDataReader = sql.ExecuteSqlReader("SELECT * FROM T_AUTHENTICATION WHERE AUTHEN_ID = @AUTHEN_ID")

            If reader.Read() Then
                Me.LoadFromReader(reader)
                reader.Close()
            Else
                If Not reader.IsClosed Then
                    reader.Close()
                End If
                Throw New ApplicationException("T_AUTHENTICATION does not exist.")
            End If
        End Sub

        Public Sub New(ByVal reader As SqlDataReader)
            Me.LoadFromReader(reader)
        End Sub

        Protected Sub LoadFromReader(ByVal reader As SqlDataReader)
            If Not IsNothing(reader) AndAlso Not reader.IsClosed Then
                _id = reader.GetInt16(0)
                If Not reader.IsDBNull(1) Then
                    _nAME = reader.GetString(1)
                End If
                If Not reader.IsDBNull(2) Then
                    _sURNAME = reader.GetString(2)
                End If
                If Not reader.IsDBNull(3) Then
                    _uSERNAME = reader.GetString(3)
                End If
                If Not reader.IsDBNull(4) Then
                    _pASSWORD = reader.GetString(4)
                End If
                If Not reader.IsDBNull(5) Then
                    _cOMPANY = reader.GetString(5)
                End If
                If Not reader.IsDBNull(6) Then
                    _aCTIVE = reader.GetBoolean(6)
                End If
            End If
        End Sub

        Public Sub Delete()
            T_AUTHENTICATION.Delete(Id)
        End Sub

        Public Sub Update()
            Dim sql As New SqlService()
            Dim queryParameters As New StringBuilder()

            sql.AddParameter("@AUTHEN_ID", SqlDbType.SmallInt, Id)
            queryParameters.Append("AUTHEN_ID = @AUTHEN_ID")

            sql.AddParameter("@NAME", SqlDbType.NVarChar, NAME)
            queryParameters.Append(", NAME = @NAME")
            sql.AddParameter("@SURNAME", SqlDbType.NVarChar, SURNAME)
            queryParameters.Append(", SURNAME = @SURNAME")
            sql.AddParameter("@USERNAME", SqlDbType.VarChar, USERNAME)
            queryParameters.Append(", USERNAME = @USERNAME")
            sql.AddParameter("@PASSWORD", SqlDbType.VarChar, PASSWORD)
            queryParameters.Append(", PASSWORD = @PASSWORD")
            sql.AddParameter("@COMPANY", SqlDbType.VarChar, COMPANY)
            queryParameters.Append(", COMPANY = @COMPANY")
            sql.AddParameter("@ACTIVE", SqlDbType.Bit, ACTIVE)
            queryParameters.Append(", ACTIVE = @ACTIVE")

            Dim query As String = [String].Format("Update T_AUTHENTICATION Set {0} Where AUTHEN_ID = @AUTHEN_ID", queryParameters.ToString())
            Dim reader As SqlDataReader = sql.ExecuteSqlReader(query)
        End Sub

        Public Sub Create()
            Dim sql As New SqlService()
            Dim queryParameters As New StringBuilder()

            sql.AddParameter("@AUTHEN_ID", SqlDbType.SmallInt, Id)
            queryParameters.Append("@AUTHEN_ID")

            sql.AddParameter("@NAME", SqlDbType.NVarChar, NAME)
            queryParameters.Append(", @NAME")
            sql.AddParameter("@SURNAME", SqlDbType.NVarChar, SURNAME)
            queryParameters.Append(", @SURNAME")
            sql.AddParameter("@USERNAME", SqlDbType.VarChar, USERNAME)
            queryParameters.Append(", @USERNAME")
            sql.AddParameter("@PASSWORD", SqlDbType.VarChar, PASSWORD)
            queryParameters.Append(", @PASSWORD")
            sql.AddParameter("@COMPANY", SqlDbType.VarChar, COMPANY)
            queryParameters.Append(", @COMPANY")
            sql.AddParameter("@ACTIVE", SqlDbType.Bit, ACTIVE)
            queryParameters.Append(", @ACTIVE")

            Dim query As String = [String].Format("Insert Into T_AUTHENTICATION ({0}) Values ({1})", queryParameters.ToString().Replace("@", ""), queryParameters.ToString())
            Dim reader As SqlDataReader = sql.ExecuteSqlReader(query)
        End Sub

        Public Shared Function NewT_AUTHENTICATION(ByVal id As System.Int16) As T_AUTHENTICATION
            Dim newEntity As New T_AUTHENTICATION()
            newEntity._id = id

            Return newEntity
        End Function

#Region "Public Properties"
        Public Property Id() As System.Int16
            Get
                Return _id
            End Get
            Set(ByVal value As System.Int16)
                _id = value
            End Set
        End Property

        Public Property NAME() As System.String
            Get
                Return _nAME
            End Get
            Set(ByVal value As System.String)
                _nAME = value
            End Set
        End Property

        Public Property SURNAME() As System.String
            Get
                Return _sURNAME
            End Get
            Set(ByVal value As System.String)
                _sURNAME = value
            End Set
        End Property

        Public Property USERNAME() As System.String
            Get
                Return _uSERNAME
            End Get
            Set(ByVal value As System.String)
                _uSERNAME = value
            End Set
        End Property

        Public Property PASSWORD() As System.String
            Get
                Return _pASSWORD
            End Get
            Set(ByVal value As System.String)
                _pASSWORD = value
            End Set
        End Property

        Public Property COMPANY() As System.String
            Get
                Return _cOMPANY
            End Get
            Set(ByVal value As System.String)
                _cOMPANY = value
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

#End Region

        Public Shared Function GetT_AUTHENTICATION(ByVal id As String) As T_AUTHENTICATION
            Return New T_AUTHENTICATION(id)
        End Function

        Public Shared Sub Delete(ByVal id As System.Int16)
            Dim sql As New SqlService()
            sql.AddParameter("@AUTHEN_ID", SqlDbType.SmallInt, id)

            Dim reader As SqlDataReader = sql.ExecuteSqlReader("Delete T_AUTHENTICATION Where AUTHEN_ID = @AUTHEN_ID")
        End Sub

    End Class
#End Region
End Namespace
