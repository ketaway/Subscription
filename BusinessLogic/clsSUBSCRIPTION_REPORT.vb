 
Public Class clsSUBSCRIPTION_REPORT
    Private _id As System.Int16
    Private _nAME As System.String = String.Empty
    Private _sURNAME As System.String = String.Empty
    Private _uSERNAME As System.String = String.Empty
    Private _pASSWORD As System.String = String.Empty
    Private _cOMPANY As System.String = String.Empty
    Private _aCTIVE As System.Boolean
    '============ Return Param ==================
    Private _sTATUS As System.String = String.Empty
    Private _mESSAGE As System.String = String.Empty

#Region "Public Methods"
    Private objAUTHENTICATION_Dal As New DataAccess.T_SUBSCRIPTION_REPORT()

#Region "Create Object"

    Public Sub New()
    End Sub

     
#End Region

 
   

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Authen_ID"></param>
    ''' <remarks></remarks>
    Public Function getReport(ByVal AUTHEN_ID As System.Int16, DATEFROM As String, DATETO As String, SERVICE_ID As String, TOPIC_NO As String, TELCO As String) As DataSet
        Return objAUTHENTICATION_Dal.getReport(AUTHEN_ID, DATEFROM, DATETO, SERVICE_ID, TOPIC_NO, TELCO)
    End Function
    Public Function getReportCharging_By_Oper(ByVal AUTHEN_ID As System.Int16, DATEFROM As String, DATETO As String, SERVICE_ID As String, TOPIC_NO As String) As DataSet
        Return objAUTHENTICATION_Dal.getReportCharging_By_Oper(AUTHEN_ID, DATEFROM, DATETO, SERVICE_ID, TOPIC_NO)
    End Function
    Public Function getContentSchedule(SERVICE_ID As Int16, TOPIC_NO As String, DATEFROM As String, DATETO As String) As DataSet
        Return objAUTHENTICATION_Dal.getContentSchedule(SERVICE_ID, TOPIC_NO, DATEFROM, DATETO)
    End Function
    'getContentSchedule
    Public Function getServiceOwner(ByVal AUTHEN_ID As System.Int16) As DataSet
        Return objAUTHENTICATION_Dal.getServiceOwner(AUTHEN_ID)
    End Function

    Public Function getSubServiceOwner(ByVal AUTHEN_ID As System.Int16, SERVICE_ID As System.Int16) As DataSet
        Return objAUTHENTICATION_Dal.getSubServiceOwner(AUTHEN_ID, SERVICE_ID)
    End Function
#End Region


#Region "Private Methods"
    
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
    Public Property Status() As System.Boolean
        Get
            Return _sTATUS
        End Get
        Set(ByVal value As System.Boolean)
            _aCTIVE = _sTATUS
        End Set
    End Property
    Public Property MESSAGE() As System.String
        Get
            Return _mESSAGE
        End Get
        Set(ByVal value As System.String)
            _aCTIVE = _mESSAGE
        End Set
    End Property
#End Region


End Class
