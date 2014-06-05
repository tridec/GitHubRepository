'****************************************
''NOTE TO DEVELOPERS: When updating this class, please make sure the new or updated data is copied into the VOADev version of the class.
''   Both Dev and Admin should contain the same constants, but variables only used on DEV or ADMIN should be commented out.
''   Also, add any NEW ModuleTypeAuditIDs to the AuditModules table - this is a lookup table and will need to be rescripted.
'****************************************
Public Class clsModuleTypeAudit
    Public Const ModuleTypeAdmin As Integer = -1
    Public Const TreeEdits As Integer = 0
    Public Const AwardList As Integer = 4
    Public Const FAQandQAAdmin As Integer = 5
    Public Const ContentPage As Integer = 12
    Public Const DocumentList As Integer = 16
    Public Const ManageAccounts As Integer = 17
    Public Const MainTreeEdit As Integer = 18
    Public Const ProposalDashboard As Integer = 35
    Public Const CustomerAcqRequest As Integer = 38
    Public Const AcquisitionRequestManagement As Integer = 39
    Public Const ectosTaskOrder As Integer = 41
    Public Const ectosQA As Integer = 42
    Public Const ectosTaskOrderTepCom As Integer = 43
    '' Public Const ectosTaskOrderTep As Integer = 44 '' - Used Only On Dev 
    '' Public Const ectosTaskOrderReport As Integer = 45 '' - Used Only On Dev 
    Public Const ectosTaskOrderMod As Integer = 46
    '' Public Const ectosRTEPTep As Integer = 47 '' - Used Only On Dev 
    Public Const ectosRTEPTEPCom As Integer = 48
    Public Const VRM As Integer = 49
    Public Const ectosRTEP As Integer = 50
    Public Const ectosRevision As Integer = 51
    Public Const ectosMessagesDocuments As Integer = 52
    '' Public Const ectosBidIntention As Integer = 53 '' - Used Only On Dev 
    Public Const ectosContract As Integer = 54
    Public Const ectosContractMod As Integer = 55
    Public Const ectosVendorUser As Integer = 56
    Public Const InnovationTopicAdmin As Integer = 57
    Public Const InnovationTopicFiscalYearAdmin As Integer = 58
    Public Const ectosOptionTEPCom As Integer = 65
    Public Const ectosOption As Integer = 66
    '' Public Const ectosOptionTEP As Integer = 67 '' - Used Only on Dev 
    Public Const ACT As Integer = 68
    Public Const MajorInitiative As Integer = 69
    Public Const Proposals As Integer = 70
    Public Const ProposalsTypeAdmin As Integer = 71
    Public Const InternalInnovation As Integer = 72
    Public Const InternalInnovationReview As Integer = 73
    Public Const InternalInnovationGroup As Integer = 74
    Public Const InternalInnovationPanel As Integer = 75
    Public Const ectosTechnicalEvaluation As Integer = 76
End Class
