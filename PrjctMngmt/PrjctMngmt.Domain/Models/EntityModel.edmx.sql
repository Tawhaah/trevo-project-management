
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 11/16/2011 02:47:49
-- Generated from EDMX file: C:\Users\Pepe\Documents\Visual Studio 2010\Projects\trevo-project-management\PrjctMngmt\PrjctMngmt.Domain\Models\EntityModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [PrjctMngmt2];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ProjectTask]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Tasks] DROP CONSTRAINT [FK_ProjectTask];
GO
IF OBJECT_ID(N'[dbo].[FK_TaskTaskAssignment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TaskAssignments] DROP CONSTRAINT [FK_TaskTaskAssignment];
GO
IF OBJECT_ID(N'[dbo].[FK_DeveloperMessage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Messages] DROP CONSTRAINT [FK_DeveloperMessage];
GO
IF OBJECT_ID(N'[dbo].[FK_TopicMessage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Messages] DROP CONSTRAINT [FK_TopicMessage];
GO
IF OBJECT_ID(N'[dbo].[FK_DeveloperDocument]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Documents] DROP CONSTRAINT [FK_DeveloperDocument];
GO
IF OBJECT_ID(N'[dbo].[FK_DeveloperTaskAssignment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TaskAssignments] DROP CONSTRAINT [FK_DeveloperTaskAssignment];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectNote]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Notes] DROP CONSTRAINT [FK_ProjectNote];
GO
IF OBJECT_ID(N'[dbo].[FK_ClientProject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Projects] DROP CONSTRAINT [FK_ClientProject];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectProjectAssignment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjectAssignments] DROP CONSTRAINT [FK_ProjectProjectAssignment];
GO
IF OBJECT_ID(N'[dbo].[FK_DeveloperProjectAssignment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjectAssignments] DROP CONSTRAINT [FK_DeveloperProjectAssignment];
GO
IF OBJECT_ID(N'[dbo].[FK_IssueIssueAssignment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IssueAssignments] DROP CONSTRAINT [FK_IssueIssueAssignment];
GO
IF OBJECT_ID(N'[dbo].[FK_DeveloperIssueAssignment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IssueAssignments] DROP CONSTRAINT [FK_DeveloperIssueAssignment];
GO
IF OBJECT_ID(N'[dbo].[FK_DeveloperIssueAttachment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IssueAttachments] DROP CONSTRAINT [FK_DeveloperIssueAttachment];
GO
IF OBJECT_ID(N'[dbo].[FK_IssueIssueAttachment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IssueAttachments] DROP CONSTRAINT [FK_IssueIssueAttachment];
GO
IF OBJECT_ID(N'[dbo].[FK_MilestoneIssue]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Issues] DROP CONSTRAINT [FK_MilestoneIssue];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectMilestone]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Milestones] DROP CONSTRAINT [FK_ProjectMilestone];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectCategoryProject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Projects] DROP CONSTRAINT [FK_ProjectCategoryProject];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectIssue]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Issues] DROP CONSTRAINT [FK_ProjectIssue];
GO
IF OBJECT_ID(N'[dbo].[FK_DeveloperNote]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Notes] DROP CONSTRAINT [FK_DeveloperNote];
GO
IF OBJECT_ID(N'[dbo].[FK_TaskCategoryTask]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Tasks] DROP CONSTRAINT [FK_TaskCategoryTask];
GO
IF OBJECT_ID(N'[dbo].[FK_TeamDeveloper]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Developers] DROP CONSTRAINT [FK_TeamDeveloper];
GO
IF OBJECT_ID(N'[dbo].[FK_IssueCategoryIssue]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Issues] DROP CONSTRAINT [FK_IssueCategoryIssue];
GO
IF OBJECT_ID(N'[dbo].[FK_ConferenceConferenceAttendant]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ConferenceAttendants] DROP CONSTRAINT [FK_ConferenceConferenceAttendant];
GO
IF OBJECT_ID(N'[dbo].[FK_DeveloperConferenceAttendant]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ConferenceAttendants] DROP CONSTRAINT [FK_DeveloperConferenceAttendant];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Developers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Developers];
GO
IF OBJECT_ID(N'[dbo].[Teams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Teams];
GO
IF OBJECT_ID(N'[dbo].[Tasks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tasks];
GO
IF OBJECT_ID(N'[dbo].[TaskCategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TaskCategories];
GO
IF OBJECT_ID(N'[dbo].[Messages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Messages];
GO
IF OBJECT_ID(N'[dbo].[Documents]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Documents];
GO
IF OBJECT_ID(N'[dbo].[TaskAssignments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TaskAssignments];
GO
IF OBJECT_ID(N'[dbo].[Notes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Notes];
GO
IF OBJECT_ID(N'[dbo].[Topics]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Topics];
GO
IF OBJECT_ID(N'[dbo].[Clients]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Clients];
GO
IF OBJECT_ID(N'[dbo].[ProjectCategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProjectCategories];
GO
IF OBJECT_ID(N'[dbo].[Projects]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Projects];
GO
IF OBJECT_ID(N'[dbo].[IssueCategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IssueCategories];
GO
IF OBJECT_ID(N'[dbo].[Issues]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Issues];
GO
IF OBJECT_ID(N'[dbo].[ProjectAssignments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProjectAssignments];
GO
IF OBJECT_ID(N'[dbo].[Milestones]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Milestones];
GO
IF OBJECT_ID(N'[dbo].[IssueAttachments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IssueAttachments];
GO
IF OBJECT_ID(N'[dbo].[IssueAssignments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IssueAssignments];
GO
IF OBJECT_ID(N'[dbo].[Conferences]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Conferences];
GO
IF OBJECT_ID(N'[dbo].[ConferenceAttendants]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ConferenceAttendants];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Developers'
CREATE TABLE [dbo].[Developers] (
    [DeveloperID] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [Position] nvarchar(max)  NULL,
    [UserName] nvarchar(max)  NOT NULL,
    [TeamName] nvarchar(max)  NULL
);
GO

-- Creating table 'Teams'
CREATE TABLE [dbo].[Teams] (
    [TeamName] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NULL
);
GO

-- Creating table 'Tasks'
CREATE TABLE [dbo].[Tasks] (
    [TaskID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [StartDate] datetime  NULL,
    [EndDate] datetime  NULL,
    [ExpectedWorkHours] int  NULL,
    [TimeSpend] int  NULL,
    [Status] nvarchar(max)  NULL,
    [Description] nvarchar(max)  NULL,
    [ProjectID] int  NOT NULL,
    [TaskCategoryName] nvarchar(max)  NULL,
    [Finished] tinyint  NOT NULL
);
GO

-- Creating table 'TaskCategories'
CREATE TABLE [dbo].[TaskCategories] (
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Messages'
CREATE TABLE [dbo].[Messages] (
    [MessageID] int IDENTITY(1,1) NOT NULL,
    [Data] nvarchar(max)  NOT NULL,
    [EntryDate] datetime  NOT NULL,
    [DeveloperID] int  NOT NULL,
    [TopicID] int  NOT NULL,
    [EditDate] datetime  NULL
);
GO

-- Creating table 'Documents'
CREATE TABLE [dbo].[Documents] (
    [DocumentID] int IDENTITY(1,1) NOT NULL,
    [FileName] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [EntryDate] datetime  NOT NULL,
    [DeveloperID] int  NOT NULL,
    [MimeType] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'TaskAssignments'
CREATE TABLE [dbo].[TaskAssignments] (
    [TaskAssignmentID] int IDENTITY(1,1) NOT NULL,
    [TaskID] int  NOT NULL,
    [DeveloperID] int  NOT NULL
);
GO

-- Creating table 'Notes'
CREATE TABLE [dbo].[Notes] (
    [NoteID] int IDENTITY(1,1) NOT NULL,
    [Data] nvarchar(max)  NOT NULL,
    [ProjectID] int  NOT NULL,
    [DeveloperID] int  NOT NULL,
    [LocX] nvarchar(max)  NOT NULL,
    [LocY] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Topics'
CREATE TABLE [dbo].[Topics] (
    [TopicID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NULL
);
GO

-- Creating table 'Clients'
CREATE TABLE [dbo].[Clients] (
    [ClientID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Representative] nvarchar(max)  NOT NULL,
    [Address] nvarchar(max)  NULL,
    [City] nvarchar(max)  NULL,
    [Country] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NULL
);
GO

-- Creating table 'ProjectCategories'
CREATE TABLE [dbo].[ProjectCategories] (
    [ProjectCategoryID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Projects'
CREATE TABLE [dbo].[Projects] (
    [ProjectID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [StartDate] datetime  NULL,
    [EndDate] datetime  NULL,
    [Status] nvarchar(max)  NULL,
    [Description] nvarchar(max)  NULL,
    [ClientID] int  NULL,
    [ProjectCategoryID] int  NULL
);
GO

-- Creating table 'IssueCategories'
CREATE TABLE [dbo].[IssueCategories] (
    [IssueCategoryName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Issues'
CREATE TABLE [dbo].[Issues] (
    [IssueID] int IDENTITY(1,1) NOT NULL,
    [Subject] nvarchar(max)  NOT NULL,
    [Priority] nvarchar(max)  NULL,
    [Severity] nvarchar(max)  NULL,
    [Status] nvarchar(max)  NULL,
    [Description] nvarchar(max)  NOT NULL,
    [EntryDate] datetime  NULL,
    [MilestoneID] int  NULL,
    [ProjectID] int  NOT NULL,
    [IssueCategoryName] nvarchar(max)  NULL
);
GO

-- Creating table 'ProjectAssignments'
CREATE TABLE [dbo].[ProjectAssignments] (
    [ProjectAssignmentID] int IDENTITY(1,1) NOT NULL,
    [RoleName] nvarchar(max)  NULL,
    [Description] nvarchar(max)  NULL,
    [ProjectID] int  NOT NULL,
    [DeveloperID] int  NOT NULL
);
GO

-- Creating table 'Milestones'
CREATE TABLE [dbo].[Milestones] (
    [MilestoneID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [DueDate] datetime  NULL,
    [ProjectID] int  NULL
);
GO

-- Creating table 'IssueAttachments'
CREATE TABLE [dbo].[IssueAttachments] (
    [IssueAttachmentID] int IDENTITY(1,1) NOT NULL,
    [Filename] nvarchar(max)  NOT NULL,
    [EntryDate] datetime  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [DeveloperID] int  NOT NULL,
    [IssueID] int  NOT NULL,
    [MimeType] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'IssueAssignments'
CREATE TABLE [dbo].[IssueAssignments] (
    [IssueAssignmentID] int IDENTITY(1,1) NOT NULL,
    [IssueID] int  NOT NULL,
    [DeveloperID] int  NOT NULL
);
GO

-- Creating table 'Conferences'
CREATE TABLE [dbo].[Conferences] (
    [ConferenceID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Date] datetime  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [Address] nvarchar(max)  NOT NULL,
    [Country] nvarchar(max)  NOT NULL,
    [ContactPhone] nvarchar(max)  NOT NULL,
    [Organizer] nvarchar(max)  NOT NULL,
    [Room] nvarchar(max)  NOT NULL,
    [Latitude] int  NULL,
    [Longitude] int  NULL
);
GO

-- Creating table 'ConferenceAttendants'
CREATE TABLE [dbo].[ConferenceAttendants] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ConferenceID] int  NOT NULL,
    [DeveloperID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [DeveloperID] in table 'Developers'
ALTER TABLE [dbo].[Developers]
ADD CONSTRAINT [PK_Developers]
    PRIMARY KEY CLUSTERED ([DeveloperID] ASC);
GO

-- Creating primary key on [TeamName] in table 'Teams'
ALTER TABLE [dbo].[Teams]
ADD CONSTRAINT [PK_Teams]
    PRIMARY KEY CLUSTERED ([TeamName] ASC);
GO

-- Creating primary key on [TaskID] in table 'Tasks'
ALTER TABLE [dbo].[Tasks]
ADD CONSTRAINT [PK_Tasks]
    PRIMARY KEY CLUSTERED ([TaskID] ASC);
GO

-- Creating primary key on [Name] in table 'TaskCategories'
ALTER TABLE [dbo].[TaskCategories]
ADD CONSTRAINT [PK_TaskCategories]
    PRIMARY KEY CLUSTERED ([Name] ASC);
GO

-- Creating primary key on [MessageID] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [PK_Messages]
    PRIMARY KEY CLUSTERED ([MessageID] ASC);
GO

-- Creating primary key on [DocumentID] in table 'Documents'
ALTER TABLE [dbo].[Documents]
ADD CONSTRAINT [PK_Documents]
    PRIMARY KEY CLUSTERED ([DocumentID] ASC);
GO

-- Creating primary key on [TaskAssignmentID] in table 'TaskAssignments'
ALTER TABLE [dbo].[TaskAssignments]
ADD CONSTRAINT [PK_TaskAssignments]
    PRIMARY KEY CLUSTERED ([TaskAssignmentID] ASC);
GO

-- Creating primary key on [NoteID] in table 'Notes'
ALTER TABLE [dbo].[Notes]
ADD CONSTRAINT [PK_Notes]
    PRIMARY KEY CLUSTERED ([NoteID] ASC);
GO

-- Creating primary key on [TopicID] in table 'Topics'
ALTER TABLE [dbo].[Topics]
ADD CONSTRAINT [PK_Topics]
    PRIMARY KEY CLUSTERED ([TopicID] ASC);
GO

-- Creating primary key on [ClientID] in table 'Clients'
ALTER TABLE [dbo].[Clients]
ADD CONSTRAINT [PK_Clients]
    PRIMARY KEY CLUSTERED ([ClientID] ASC);
GO

-- Creating primary key on [ProjectCategoryID] in table 'ProjectCategories'
ALTER TABLE [dbo].[ProjectCategories]
ADD CONSTRAINT [PK_ProjectCategories]
    PRIMARY KEY CLUSTERED ([ProjectCategoryID] ASC);
GO

-- Creating primary key on [ProjectID] in table 'Projects'
ALTER TABLE [dbo].[Projects]
ADD CONSTRAINT [PK_Projects]
    PRIMARY KEY CLUSTERED ([ProjectID] ASC);
GO

-- Creating primary key on [IssueCategoryName] in table 'IssueCategories'
ALTER TABLE [dbo].[IssueCategories]
ADD CONSTRAINT [PK_IssueCategories]
    PRIMARY KEY CLUSTERED ([IssueCategoryName] ASC);
GO

-- Creating primary key on [IssueID] in table 'Issues'
ALTER TABLE [dbo].[Issues]
ADD CONSTRAINT [PK_Issues]
    PRIMARY KEY CLUSTERED ([IssueID] ASC);
GO

-- Creating primary key on [ProjectAssignmentID] in table 'ProjectAssignments'
ALTER TABLE [dbo].[ProjectAssignments]
ADD CONSTRAINT [PK_ProjectAssignments]
    PRIMARY KEY CLUSTERED ([ProjectAssignmentID] ASC);
GO

-- Creating primary key on [MilestoneID] in table 'Milestones'
ALTER TABLE [dbo].[Milestones]
ADD CONSTRAINT [PK_Milestones]
    PRIMARY KEY CLUSTERED ([MilestoneID] ASC);
GO

-- Creating primary key on [IssueAttachmentID] in table 'IssueAttachments'
ALTER TABLE [dbo].[IssueAttachments]
ADD CONSTRAINT [PK_IssueAttachments]
    PRIMARY KEY CLUSTERED ([IssueAttachmentID] ASC);
GO

-- Creating primary key on [IssueAssignmentID] in table 'IssueAssignments'
ALTER TABLE [dbo].[IssueAssignments]
ADD CONSTRAINT [PK_IssueAssignments]
    PRIMARY KEY CLUSTERED ([IssueAssignmentID] ASC);
GO

-- Creating primary key on [ConferenceID] in table 'Conferences'
ALTER TABLE [dbo].[Conferences]
ADD CONSTRAINT [PK_Conferences]
    PRIMARY KEY CLUSTERED ([ConferenceID] ASC);
GO

-- Creating primary key on [ID] in table 'ConferenceAttendants'
ALTER TABLE [dbo].[ConferenceAttendants]
ADD CONSTRAINT [PK_ConferenceAttendants]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ProjectID] in table 'Tasks'
ALTER TABLE [dbo].[Tasks]
ADD CONSTRAINT [FK_ProjectTask]
    FOREIGN KEY ([ProjectID])
    REFERENCES [dbo].[Projects]
        ([ProjectID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectTask'
CREATE INDEX [IX_FK_ProjectTask]
ON [dbo].[Tasks]
    ([ProjectID]);
GO

-- Creating foreign key on [TaskID] in table 'TaskAssignments'
ALTER TABLE [dbo].[TaskAssignments]
ADD CONSTRAINT [FK_TaskTaskAssignment]
    FOREIGN KEY ([TaskID])
    REFERENCES [dbo].[Tasks]
        ([TaskID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TaskTaskAssignment'
CREATE INDEX [IX_FK_TaskTaskAssignment]
ON [dbo].[TaskAssignments]
    ([TaskID]);
GO

-- Creating foreign key on [DeveloperID] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [FK_DeveloperMessage]
    FOREIGN KEY ([DeveloperID])
    REFERENCES [dbo].[Developers]
        ([DeveloperID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DeveloperMessage'
CREATE INDEX [IX_FK_DeveloperMessage]
ON [dbo].[Messages]
    ([DeveloperID]);
GO

-- Creating foreign key on [TopicID] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [FK_TopicMessage]
    FOREIGN KEY ([TopicID])
    REFERENCES [dbo].[Topics]
        ([TopicID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TopicMessage'
CREATE INDEX [IX_FK_TopicMessage]
ON [dbo].[Messages]
    ([TopicID]);
GO

-- Creating foreign key on [DeveloperID] in table 'Documents'
ALTER TABLE [dbo].[Documents]
ADD CONSTRAINT [FK_DeveloperDocument]
    FOREIGN KEY ([DeveloperID])
    REFERENCES [dbo].[Developers]
        ([DeveloperID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DeveloperDocument'
CREATE INDEX [IX_FK_DeveloperDocument]
ON [dbo].[Documents]
    ([DeveloperID]);
GO

-- Creating foreign key on [DeveloperID] in table 'TaskAssignments'
ALTER TABLE [dbo].[TaskAssignments]
ADD CONSTRAINT [FK_DeveloperTaskAssignment]
    FOREIGN KEY ([DeveloperID])
    REFERENCES [dbo].[Developers]
        ([DeveloperID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DeveloperTaskAssignment'
CREATE INDEX [IX_FK_DeveloperTaskAssignment]
ON [dbo].[TaskAssignments]
    ([DeveloperID]);
GO

-- Creating foreign key on [ProjectID] in table 'Notes'
ALTER TABLE [dbo].[Notes]
ADD CONSTRAINT [FK_ProjectNote]
    FOREIGN KEY ([ProjectID])
    REFERENCES [dbo].[Projects]
        ([ProjectID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectNote'
CREATE INDEX [IX_FK_ProjectNote]
ON [dbo].[Notes]
    ([ProjectID]);
GO

-- Creating foreign key on [ClientID] in table 'Projects'
ALTER TABLE [dbo].[Projects]
ADD CONSTRAINT [FK_ClientProject]
    FOREIGN KEY ([ClientID])
    REFERENCES [dbo].[Clients]
        ([ClientID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientProject'
CREATE INDEX [IX_FK_ClientProject]
ON [dbo].[Projects]
    ([ClientID]);
GO

-- Creating foreign key on [ProjectID] in table 'ProjectAssignments'
ALTER TABLE [dbo].[ProjectAssignments]
ADD CONSTRAINT [FK_ProjectProjectAssignment]
    FOREIGN KEY ([ProjectID])
    REFERENCES [dbo].[Projects]
        ([ProjectID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectProjectAssignment'
CREATE INDEX [IX_FK_ProjectProjectAssignment]
ON [dbo].[ProjectAssignments]
    ([ProjectID]);
GO

-- Creating foreign key on [DeveloperID] in table 'ProjectAssignments'
ALTER TABLE [dbo].[ProjectAssignments]
ADD CONSTRAINT [FK_DeveloperProjectAssignment]
    FOREIGN KEY ([DeveloperID])
    REFERENCES [dbo].[Developers]
        ([DeveloperID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DeveloperProjectAssignment'
CREATE INDEX [IX_FK_DeveloperProjectAssignment]
ON [dbo].[ProjectAssignments]
    ([DeveloperID]);
GO

-- Creating foreign key on [IssueID] in table 'IssueAssignments'
ALTER TABLE [dbo].[IssueAssignments]
ADD CONSTRAINT [FK_IssueIssueAssignment]
    FOREIGN KEY ([IssueID])
    REFERENCES [dbo].[Issues]
        ([IssueID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_IssueIssueAssignment'
CREATE INDEX [IX_FK_IssueIssueAssignment]
ON [dbo].[IssueAssignments]
    ([IssueID]);
GO

-- Creating foreign key on [DeveloperID] in table 'IssueAssignments'
ALTER TABLE [dbo].[IssueAssignments]
ADD CONSTRAINT [FK_DeveloperIssueAssignment]
    FOREIGN KEY ([DeveloperID])
    REFERENCES [dbo].[Developers]
        ([DeveloperID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DeveloperIssueAssignment'
CREATE INDEX [IX_FK_DeveloperIssueAssignment]
ON [dbo].[IssueAssignments]
    ([DeveloperID]);
GO

-- Creating foreign key on [DeveloperID] in table 'IssueAttachments'
ALTER TABLE [dbo].[IssueAttachments]
ADD CONSTRAINT [FK_DeveloperIssueAttachment]
    FOREIGN KEY ([DeveloperID])
    REFERENCES [dbo].[Developers]
        ([DeveloperID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DeveloperIssueAttachment'
CREATE INDEX [IX_FK_DeveloperIssueAttachment]
ON [dbo].[IssueAttachments]
    ([DeveloperID]);
GO

-- Creating foreign key on [IssueID] in table 'IssueAttachments'
ALTER TABLE [dbo].[IssueAttachments]
ADD CONSTRAINT [FK_IssueIssueAttachment]
    FOREIGN KEY ([IssueID])
    REFERENCES [dbo].[Issues]
        ([IssueID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_IssueIssueAttachment'
CREATE INDEX [IX_FK_IssueIssueAttachment]
ON [dbo].[IssueAttachments]
    ([IssueID]);
GO

-- Creating foreign key on [MilestoneID] in table 'Issues'
ALTER TABLE [dbo].[Issues]
ADD CONSTRAINT [FK_MilestoneIssue]
    FOREIGN KEY ([MilestoneID])
    REFERENCES [dbo].[Milestones]
        ([MilestoneID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MilestoneIssue'
CREATE INDEX [IX_FK_MilestoneIssue]
ON [dbo].[Issues]
    ([MilestoneID]);
GO

-- Creating foreign key on [ProjectID] in table 'Milestones'
ALTER TABLE [dbo].[Milestones]
ADD CONSTRAINT [FK_ProjectMilestone]
    FOREIGN KEY ([ProjectID])
    REFERENCES [dbo].[Projects]
        ([ProjectID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectMilestone'
CREATE INDEX [IX_FK_ProjectMilestone]
ON [dbo].[Milestones]
    ([ProjectID]);
GO

-- Creating foreign key on [ProjectCategoryID] in table 'Projects'
ALTER TABLE [dbo].[Projects]
ADD CONSTRAINT [FK_ProjectCategoryProject]
    FOREIGN KEY ([ProjectCategoryID])
    REFERENCES [dbo].[ProjectCategories]
        ([ProjectCategoryID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectCategoryProject'
CREATE INDEX [IX_FK_ProjectCategoryProject]
ON [dbo].[Projects]
    ([ProjectCategoryID]);
GO

-- Creating foreign key on [ProjectID] in table 'Issues'
ALTER TABLE [dbo].[Issues]
ADD CONSTRAINT [FK_ProjectIssue]
    FOREIGN KEY ([ProjectID])
    REFERENCES [dbo].[Projects]
        ([ProjectID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectIssue'
CREATE INDEX [IX_FK_ProjectIssue]
ON [dbo].[Issues]
    ([ProjectID]);
GO

-- Creating foreign key on [DeveloperID] in table 'Notes'
ALTER TABLE [dbo].[Notes]
ADD CONSTRAINT [FK_DeveloperNote]
    FOREIGN KEY ([DeveloperID])
    REFERENCES [dbo].[Developers]
        ([DeveloperID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DeveloperNote'
CREATE INDEX [IX_FK_DeveloperNote]
ON [dbo].[Notes]
    ([DeveloperID]);
GO

-- Creating foreign key on [TaskCategoryName] in table 'Tasks'
ALTER TABLE [dbo].[Tasks]
ADD CONSTRAINT [FK_TaskCategoryTask]
    FOREIGN KEY ([TaskCategoryName])
    REFERENCES [dbo].[TaskCategories]
        ([Name])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TaskCategoryTask'
CREATE INDEX [IX_FK_TaskCategoryTask]
ON [dbo].[Tasks]
    ([TaskCategoryName]);
GO

-- Creating foreign key on [TeamName] in table 'Developers'
ALTER TABLE [dbo].[Developers]
ADD CONSTRAINT [FK_TeamDeveloper]
    FOREIGN KEY ([TeamName])
    REFERENCES [dbo].[Teams]
        ([TeamName])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TeamDeveloper'
CREATE INDEX [IX_FK_TeamDeveloper]
ON [dbo].[Developers]
    ([TeamName]);
GO

-- Creating foreign key on [IssueCategoryName] in table 'Issues'
ALTER TABLE [dbo].[Issues]
ADD CONSTRAINT [FK_IssueCategoryIssue]
    FOREIGN KEY ([IssueCategoryName])
    REFERENCES [dbo].[IssueCategories]
        ([IssueCategoryName])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_IssueCategoryIssue'
CREATE INDEX [IX_FK_IssueCategoryIssue]
ON [dbo].[Issues]
    ([IssueCategoryName]);
GO

-- Creating foreign key on [ConferenceID] in table 'ConferenceAttendants'
ALTER TABLE [dbo].[ConferenceAttendants]
ADD CONSTRAINT [FK_ConferenceConferenceAttendant]
    FOREIGN KEY ([ConferenceID])
    REFERENCES [dbo].[Conferences]
        ([ConferenceID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ConferenceConferenceAttendant'
CREATE INDEX [IX_FK_ConferenceConferenceAttendant]
ON [dbo].[ConferenceAttendants]
    ([ConferenceID]);
GO

-- Creating foreign key on [DeveloperID] in table 'ConferenceAttendants'
ALTER TABLE [dbo].[ConferenceAttendants]
ADD CONSTRAINT [FK_DeveloperConferenceAttendant]
    FOREIGN KEY ([DeveloperID])
    REFERENCES [dbo].[Developers]
        ([DeveloperID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DeveloperConferenceAttendant'
CREATE INDEX [IX_FK_DeveloperConferenceAttendant]
ON [dbo].[ConferenceAttendants]
    ([DeveloperID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------