Imports DataAccess
Public Class clsMENU
    'Private _id As System.Int16
    'Private _nAME As System.String = String.Empty
    'Private _sURNAME As System.String = String.Empty
    'Private _uSERNAME As System.String = String.Empty
    'Private _pASSWORD As System.String = String.Empty
    'Private _cOMPANY As System.String = String.Empty
    'Private _aCTIVE As System.Boolean
    '============ Return Param ==================
    Private _sTATUS As System.String = String.Empty
    Private _mESSAGE As System.String = String.Empty

#Region "Public Methods"
    Private objMENU_Dal As New T_MENU

#Region "Create Object"

    Public Sub New()
    End Sub

    'Public Sub New(strUsername As String)
    '    getAuthen(strUsername)
    'End Sub

    'Public Sub New(Authen_ID As Integer)
    '    getAuthen(Authen_ID)
    'End Sub

    ' ''' <summary>
    ' ''' Create Object and check Authen
    ' ''' </summary>
    ' ''' <param name="strUsername"></param>
    ' ''' <param name="strPassword"></param>
    ' ''' <remarks></remarks>
    'Public Sub New(strUsername As String, strPassword As String)
    '    chkAuthen(strUsername, strPassword)
    'End Sub
#End Region

    ' ''' <summary>
    ' ''' ดึงข้อมูล Authentication ด้วย ID
    ' ''' </summary>
    ' ''' <param name="strUsername"></param>
    ' ''' <remarks></remarks>
    'Public Sub getAuthen(strUsername As String)
    '    objAUTHENTICATION_Dal.getAuthen(strUsername)
    '    setDBField_From_DAL()
    'End Sub

    ' ''' <summary>
    ' ''' 
    ' ''' </summary>
    ' ''' <param name="Authen_ID"></param>
    ' ''' <remarks></remarks>
    'Public Sub getAuthen(Authen_ID As Integer)
    '    objAUTHENTICATION_Dal.getAuthen(Authen_ID)
    '    setDBField_From_DAL()
    'End Sub


    Public Function getLeftMenu(Authen_ID As Int16) As DataSet
        Return objMENU_Dal.getLeftMenu(Authen_ID)
    End Function
    'Public Sub getNavBar(ByVal MenuID As Int16, ByVal ParentID As Int16, ByRef MenuName As String, ByRef ParentMenuName As String)
    Public Sub getNavBar(ByVal MenuID As Int16, ByVal ParentID As Int16, ByRef MenuName As String, ByRef ParentMenuName As String)
        objMENU_Dal.getNavBar(MenuID, ParentID, MenuName, ParentMenuName)
    End Sub

#End Region


#Region "Private Methods"
    ' ''' <summary>
    ' ''' เพื่อ set ค่า Property ของ Database Field จาก DAL ทั้งหมด ลง Class นี้
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private Sub setDBField_From_DAL()
    '    _id = objAUTHENTICATION_Dal.Id
    '    _nAME = objAUTHENTICATION_Dal.NAME
    '    _sURNAME = objAUTHENTICATION_Dal.SURNAME
    '    _uSERNAME = objAUTHENTICATION_Dal.USERNAME
    '    _pASSWORD = objAUTHENTICATION_Dal.PASSWORD
    '    _cOMPANY = objAUTHENTICATION_Dal.COMPANY
    '    _aCTIVE = objAUTHENTICATION_Dal.ACTIVE
    '    _sTATUS = objAUTHENTICATION_Dal.Status
    '    _mESSAGE = objAUTHENTICATION_Dal.MESSAGE
    'End Sub
#End Region

#Region "Public Properties"
    'Public Property Id() As System.Int16
    '    Get
    '        Return _id
    '    End Get
    '    Set(ByVal value As System.Int16)
    '        _id = value
    '    End Set
    'End Property

    'Public Property NAME() As System.String
    '    Get
    '        Return _nAME
    '    End Get
    '    Set(ByVal value As System.String)
    '        _nAME = value
    '    End Set
    'End Property

    'Public Property SURNAME() As System.String
    '    Get
    '        Return _sURNAME
    '    End Get
    '    Set(ByVal value As System.String)
    '        _sURNAME = value
    '    End Set
    'End Property

    'Public Property USERNAME() As System.String
    '    Get
    '        Return _uSERNAME
    '    End Get
    '    Set(ByVal value As System.String)
    '        _uSERNAME = value
    '    End Set
    'End Property

    'Public Property PASSWORD() As System.String
    '    Get
    '        Return _pASSWORD
    '    End Get
    '    Set(ByVal value As System.String)
    '        _pASSWORD = value
    '    End Set
    'End Property

    'Public Property COMPANY() As System.String
    '    Get
    '        Return _cOMPANY
    '    End Get
    '    Set(ByVal value As System.String)
    '        _cOMPANY = value
    '    End Set
    'End Property

    'Public Property ACTIVE() As System.Boolean
    '    Get
    '        Return _aCTIVE
    '    End Get
    '    Set(ByVal value As System.Boolean)
    '        _aCTIVE = value
    '    End Set
    'End Property
    'Public Property Status() As System.Boolean
    '    Get
    '        Return _sTATUS
    '    End Get
    '    Set(ByVal value As System.Boolean)
    '        _aCTIVE = _sTATUS
    '    End Set
    'End Property
    'Public Property MESSAGE() As System.String
    '    Get
    '        Return _mESSAGE
    '    End Get
    '    Set(ByVal value As System.String)
    '        _aCTIVE = _mESSAGE
    '    End Set
    'End Property
#End Region


End Class
