
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/15/2015 11:43:33
-- Generated from EDMX file: C:\Users\Mateusz\Documents\Visual Studio 2013\Projects\BD2\Database\Database.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CandidateDocument]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Documents] DROP CONSTRAINT [FK_CandidateDocument];
GO
IF OBJECT_ID(N'[dbo].[FK_EvaluationSkillsEvaluation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SkillsEvaluations] DROP CONSTRAINT [FK_EvaluationSkillsEvaluation];
GO
IF OBJECT_ID(N'[dbo].[FK_EvaluationSoftSkillsEvaluation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SoftSkillsEvaluations] DROP CONSTRAINT [FK_EvaluationSoftSkillsEvaluation];
GO
IF OBJECT_ID(N'[dbo].[FK_CandidateEvaluation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Evaluations] DROP CONSTRAINT [FK_CandidateEvaluation];
GO
IF OBJECT_ID(N'[dbo].[FK_CandidateRecruitmentStage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RecruitmentStages] DROP CONSTRAINT [FK_CandidateRecruitmentStage];
GO
IF OBJECT_ID(N'[dbo].[FK_SkillsEvaluationSkill]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SkillsEvaluations] DROP CONSTRAINT [FK_SkillsEvaluationSkill];
GO
IF OBJECT_ID(N'[dbo].[FK_SoftSkillsEvaluationSoftSkill]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SoftSkillsEvaluations] DROP CONSTRAINT [FK_SoftSkillsEvaluationSoftSkill];
GO
IF OBJECT_ID(N'[dbo].[FK_RecruitmentStageStage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RecruitmentStages] DROP CONSTRAINT [FK_RecruitmentStageStage];
GO
IF OBJECT_ID(N'[dbo].[FK_CandidateDecision]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Candidates] DROP CONSTRAINT [FK_CandidateDecision];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonCandidate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Candidates] DROP CONSTRAINT [FK_PersonCandidate];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Persons] DROP CONSTRAINT [FK_PersonUser];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Persons]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Persons];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Candidates]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Candidates];
GO
IF OBJECT_ID(N'[dbo].[Documents]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Documents];
GO
IF OBJECT_ID(N'[dbo].[Skills]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Skills];
GO
IF OBJECT_ID(N'[dbo].[Evaluations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Evaluations];
GO
IF OBJECT_ID(N'[dbo].[Decisions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Decisions];
GO
IF OBJECT_ID(N'[dbo].[RecruitmentStages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RecruitmentStages];
GO
IF OBJECT_ID(N'[dbo].[SoftSkills]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SoftSkills];
GO
IF OBJECT_ID(N'[dbo].[SoftSkillsEvaluations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SoftSkillsEvaluations];
GO
IF OBJECT_ID(N'[dbo].[SkillsEvaluations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SkillsEvaluations];
GO
IF OBJECT_ID(N'[dbo].[Stage]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Stage];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Persons'
CREATE TABLE [dbo].[Persons] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [SurName] nvarchar(max)  NOT NULL,
    [Mail] nvarchar(max)  NOT NULL,
    [Phone] nvarchar(max)  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [Pesel] nvarchar(max)  NOT NULL,
    [User_Id] int  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Login] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Role] tinyint  NOT NULL
);
GO

-- Creating table 'Candidates'
CREATE TABLE [dbo].[Candidates] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Person_Id] int  NOT NULL,
    [Decision_Id] int  NOT NULL
);
GO

-- Creating table 'Documents'
CREATE TABLE [dbo].[Documents] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CandidateId] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Extension] nvarchar(max)  NOT NULL,
    [Type] tinyint  NOT NULL,
    [File] varbinary(max)  NOT NULL
);
GO

-- Creating table 'Skills'
CREATE TABLE [dbo].[Skills] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Evaluations'
CREATE TABLE [dbo].[Evaluations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [IsEvaluated] bit  NOT NULL,
    [Candidate_Id] int  NOT NULL
);
GO

-- Creating table 'Decisions'
CREATE TABLE [dbo].[Decisions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] tinyint  NOT NULL,
    [Answer] nvarchar(max)  NULL,
    [Reason] nvarchar(max)  NULL
);
GO

-- Creating table 'RecruitmentStages'
CREATE TABLE [dbo].[RecruitmentStages] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Mark] tinyint  NOT NULL,
    [Comment] nvarchar(max)  NULL,
    [CandidateId] int  NOT NULL,
    [IsCurrent] bit  NOT NULL,
    [StageId] int  NOT NULL
);
GO

-- Creating table 'SoftSkills'
CREATE TABLE [dbo].[SoftSkills] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'SoftSkillsEvaluations'
CREATE TABLE [dbo].[SoftSkillsEvaluations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Mark] tinyint  NOT NULL,
    [EvaluationId] int  NOT NULL,
    [SoftSkillId] int  NOT NULL
);
GO

-- Creating table 'SkillsEvaluations'
CREATE TABLE [dbo].[SkillsEvaluations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Mark] tinyint  NOT NULL,
    [EvaluationId] int  NOT NULL,
    [SkillId] int  NOT NULL
);
GO

-- Creating table 'Stage'
CREATE TABLE [dbo].[Stage] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Priority] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Persons'
ALTER TABLE [dbo].[Persons]
ADD CONSTRAINT [PK_Persons]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Candidates'
ALTER TABLE [dbo].[Candidates]
ADD CONSTRAINT [PK_Candidates]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Documents'
ALTER TABLE [dbo].[Documents]
ADD CONSTRAINT [PK_Documents]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Skills'
ALTER TABLE [dbo].[Skills]
ADD CONSTRAINT [PK_Skills]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Evaluations'
ALTER TABLE [dbo].[Evaluations]
ADD CONSTRAINT [PK_Evaluations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Decisions'
ALTER TABLE [dbo].[Decisions]
ADD CONSTRAINT [PK_Decisions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RecruitmentStages'
ALTER TABLE [dbo].[RecruitmentStages]
ADD CONSTRAINT [PK_RecruitmentStages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SoftSkills'
ALTER TABLE [dbo].[SoftSkills]
ADD CONSTRAINT [PK_SoftSkills]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SoftSkillsEvaluations'
ALTER TABLE [dbo].[SoftSkillsEvaluations]
ADD CONSTRAINT [PK_SoftSkillsEvaluations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SkillsEvaluations'
ALTER TABLE [dbo].[SkillsEvaluations]
ADD CONSTRAINT [PK_SkillsEvaluations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Stage'
ALTER TABLE [dbo].[Stage]
ADD CONSTRAINT [PK_Stage]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CandidateId] in table 'Documents'
ALTER TABLE [dbo].[Documents]
ADD CONSTRAINT [FK_CandidateDocument]
    FOREIGN KEY ([CandidateId])
    REFERENCES [dbo].[Candidates]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CandidateDocument'
CREATE INDEX [IX_FK_CandidateDocument]
ON [dbo].[Documents]
    ([CandidateId]);
GO

-- Creating foreign key on [EvaluationId] in table 'SkillsEvaluations'
ALTER TABLE [dbo].[SkillsEvaluations]
ADD CONSTRAINT [FK_EvaluationSkillsEvaluation]
    FOREIGN KEY ([EvaluationId])
    REFERENCES [dbo].[Evaluations]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EvaluationSkillsEvaluation'
CREATE INDEX [IX_FK_EvaluationSkillsEvaluation]
ON [dbo].[SkillsEvaluations]
    ([EvaluationId]);
GO

-- Creating foreign key on [EvaluationId] in table 'SoftSkillsEvaluations'
ALTER TABLE [dbo].[SoftSkillsEvaluations]
ADD CONSTRAINT [FK_EvaluationSoftSkillsEvaluation]
    FOREIGN KEY ([EvaluationId])
    REFERENCES [dbo].[Evaluations]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EvaluationSoftSkillsEvaluation'
CREATE INDEX [IX_FK_EvaluationSoftSkillsEvaluation]
ON [dbo].[SoftSkillsEvaluations]
    ([EvaluationId]);
GO

-- Creating foreign key on [Candidate_Id] in table 'Evaluations'
ALTER TABLE [dbo].[Evaluations]
ADD CONSTRAINT [FK_CandidateEvaluation]
    FOREIGN KEY ([Candidate_Id])
    REFERENCES [dbo].[Candidates]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CandidateEvaluation'
CREATE INDEX [IX_FK_CandidateEvaluation]
ON [dbo].[Evaluations]
    ([Candidate_Id]);
GO

-- Creating foreign key on [CandidateId] in table 'RecruitmentStages'
ALTER TABLE [dbo].[RecruitmentStages]
ADD CONSTRAINT [FK_CandidateRecruitmentStage]
    FOREIGN KEY ([CandidateId])
    REFERENCES [dbo].[Candidates]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CandidateRecruitmentStage'
CREATE INDEX [IX_FK_CandidateRecruitmentStage]
ON [dbo].[RecruitmentStages]
    ([CandidateId]);
GO

-- Creating foreign key on [SkillId] in table 'SkillsEvaluations'
ALTER TABLE [dbo].[SkillsEvaluations]
ADD CONSTRAINT [FK_SkillsEvaluationSkill]
    FOREIGN KEY ([SkillId])
    REFERENCES [dbo].[Skills]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SkillsEvaluationSkill'
CREATE INDEX [IX_FK_SkillsEvaluationSkill]
ON [dbo].[SkillsEvaluations]
    ([SkillId]);
GO

-- Creating foreign key on [SoftSkillId] in table 'SoftSkillsEvaluations'
ALTER TABLE [dbo].[SoftSkillsEvaluations]
ADD CONSTRAINT [FK_SoftSkillsEvaluationSoftSkill]
    FOREIGN KEY ([SoftSkillId])
    REFERENCES [dbo].[SoftSkills]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SoftSkillsEvaluationSoftSkill'
CREATE INDEX [IX_FK_SoftSkillsEvaluationSoftSkill]
ON [dbo].[SoftSkillsEvaluations]
    ([SoftSkillId]);
GO

-- Creating foreign key on [StageId] in table 'RecruitmentStages'
ALTER TABLE [dbo].[RecruitmentStages]
ADD CONSTRAINT [FK_RecruitmentStageStage]
    FOREIGN KEY ([StageId])
    REFERENCES [dbo].[Stage]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RecruitmentStageStage'
CREATE INDEX [IX_FK_RecruitmentStageStage]
ON [dbo].[RecruitmentStages]
    ([StageId]);
GO

-- Creating foreign key on [Person_Id] in table 'Candidates'
ALTER TABLE [dbo].[Candidates]
ADD CONSTRAINT [FK_PersonCandidate]
    FOREIGN KEY ([Person_Id])
    REFERENCES [dbo].[Persons]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonCandidate'
CREATE INDEX [IX_FK_PersonCandidate]
ON [dbo].[Candidates]
    ([Person_Id]);
GO

-- Creating foreign key on [User_Id] in table 'Persons'
ALTER TABLE [dbo].[Persons]
ADD CONSTRAINT [FK_PersonUser]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonUser'
CREATE INDEX [IX_FK_PersonUser]
ON [dbo].[Persons]
    ([User_Id]);
GO

-- Creating foreign key on [Decision_Id] in table 'Candidates'
ALTER TABLE [dbo].[Candidates]
ADD CONSTRAINT [FK_CandidateDecision]
    FOREIGN KEY ([Decision_Id])
    REFERENCES [dbo].[Decisions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CandidateDecision'
CREATE INDEX [IX_FK_CandidateDecision]
ON [dbo].[Candidates]
    ([Decision_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------