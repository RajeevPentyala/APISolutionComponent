﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISolutionComponent
{
    public enum EntityObjectType
    {
        Entity = 1,
        Attribute = 2,
        Relationship = 3,
        AttributePicklistValue = 4,
        AttributeLookupValue = 5,
        ViewAttribute = 6,
        LocalizedLabel = 7,
        RelationshipExtraCondition = 8,
        OptionSet = 9,
        EntityRelationship = 10,
        EntityRelationshipRole = 11,
        EntityRelationshipRelationships = 12,
        ManagedProperty = 13,
        EntityKey = 14,
        EntityKeyAttribute = 15,
        Privilege = 16,
        PrivilegeObjectTypeCodes = 17,
        EntityIndex = 18,
        Role = 20,
        RolePrivileges = 21,
        DisplayString = 22,
        DisplayStringMap = 23,
        OrganizationUI = 24,
        Organization = 25,
        SavedQuery = 26,
        Workflow = 29,
        ProcessTrigger = 30,
        Report = 31,
        ReportEntity = 32,
        ReportCategory = 33,
        ReportVisibility = 34,
        ActivityMimeAttachment = 35,
        Template = 36,
        ContractTemplate = 37,
        KbArticleTemplate = 38,
        MailMergeTemplate = 39,
        EntityMap = 46,
        AttributeMap = 47,
        RibbonCommand = 48,
        RibbonContextGroup = 49,
        RibbonCustomization = 50,
        RibbonRule = 52,
        RibbonTabToCommandMap = 53,
        RibbonDiff = 55,
        SavedQueryVisualization = 59,
        SystemForm = 60,
        WebResource = 61,
        SiteMap = 62,
        ConnectionRole = 63,
        ComplexControl = 64,
        HierarchyRule = 65,
        CustomControl = 66,
        CustomControlResource = 69,
        FieldSecurityProfile = 70,
        FieldPermission = 71,
        CustomControlDefaultConfig = 68,
        AppModule = 80,
        AppModuleSiteMap = 81,
        AppModuleRibbonCommand = 82,
        AppModuleRoles = 88,
        PluginType = 90,
        PluginAssembly = 91,
        SdkMessageProcessingStep = 92,
        SdkMessageProcessingStepImage = 93,
        ServiceEndpoint = 95,
        RoutingRule = 150,
        RoutingRuleItem = 151,
        SLA = 152,
        SLAItem = 153,
        ConvertRule = 154,
        ConvertRuleItem = 155,
        KnowledgeRecord = 156,
        ChannelPropertyGroup = 157,
        ChannelProperty = 158,
        DependencyFeature = 160,
        SimilarityRule = 167,
        SimilarityRuleCondition = 168,
        ChannelAccessProfileRule = 169,
        ChannelAccessProfileRuleItem = 170,
        ChannelAccessProfileEntityAccessLevel = 171,
        ChannelAccessProfile = 172,
        MobileOfflineProfile = 161,
        MobileOfflineProfileItem = 162,
        MobileOfflineProfileItemAssociation = 163,
        RecommendationModel = 173,
        RecommendationModelMapping = 174,
        KnowledgeSearchModel = 175,
        TextAnalyticsEntityMapping = 176,
        TopicModelConfiguration = 177,
        EmailSignature = 178,
        AdvancedSimilarityRule = 179,
        EntityDataSourceMapping = 180,
        EntityDataProvider = 181,
        EntityDataSource = 183,
        AppConfig = 191,
        AppConfigInstance = 192,
        SdkMessage = 201,
        SdkMessageFilter = 202,
        SdkMessagePair = 203,
        SdkMessageRequest = 204,
        SdkMessageRequestField = 205,
        SdkMessageResponse = 206,
        SdkMessageResponseField = 207,
        ImportMap = 208,
        StoredProcedureCatalog = 209,
        WebWizard = 210,
        Dialogs = 211,
        ImportEntityMapping = 212,
        ColumnMapping = 213,
        LookUpMapping = 214,
        PickListMapping = 215,
        TransformationMapping = 216,
        TransformationParameterMapping = 217,
        ImportData = 218,
        ImportFile = 219,
        ImportLog = 220,
        OwnerMapping = 221,
        NavigationSetting = 250,
        NavigationSettingItem = 251,
        GlobalSearchConfiguration = 252,
        CardType = 260,
        SolutionComponentDefinition = 270,
        CanvasApp = 300,
        Connector = 371,
        EnvironmentVariableDefinition = 380,
        EnvironmentVariableValue = 381,
        msdyn_AITemplate = 400,
        msdyn_AIModel = 401,
        msdyn_AIConfiguration = 402,
        EntityAnalyticsConfig = 430,
        AttributeImageConfig = 431,
        EntityImageConfig = 432
    }

    public class ResponseComponent
    {
        public string ComponentName { get; set; }
        public string ComponentType { get; set; }
        public string EntityLogicalName { get; set; }
        public string EntityId { get; set; }
        public string ComponentKeyAttributeName { get; set; }
        public string ComponentNameAttributeName { get; set; }
        public string ComponentDisplayNameAttributeName { get; set; }
        public string ComponentDescriptionAttributeName { get; set; }
        public string ComponentStateAttributeName { get; set; }
        public string ParentComponentId { get; set; }
    }
}
