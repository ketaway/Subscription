Public Class Class1
    Inherits com.hp.AISMM7.MM7ReceiverBase


    Protected Overrides Sub onDeliveryReportFileReceived(context As System.Web.HttpContext, obj As com.hp.AISMM7.DataType.DeliveryReportFile, attachment As System.Collections.ArrayList, transactionId As com.hp.AISMM7.DataType.TransactionID)

    End Sub

    Protected Overrides Sub onDeliveryReportReqReceived(context As System.Web.HttpContext, obj As com.hp.AISMM7.DataType.deliveryReportReqType, transactionId As com.hp.AISMM7.DataType.TransactionID)

    End Sub

    Protected Overrides Sub onDeliveryReqReceived(context As System.Web.HttpContext, obj As com.hp.AISMM7.DataType.DeliverReq, attachment As System.Collections.ArrayList, transactionId As com.hp.AISMM7.DataType.TransactionID)

    End Sub
End Class
