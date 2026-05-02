--
-- PostgreSQL database cluster dump
--

\restrict c5uQye4zq18xtON7otWN0GdbfOHcqhzjZNEDhAX6DemWBHvmq7JcOhRPRkTl4Mx

SET default_transaction_read_only = off;

SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;

--
-- Roles
--

CREATE ROLE postgres;
ALTER ROLE postgres WITH SUPERUSER INHERIT CREATEROLE CREATEDB LOGIN REPLICATION BYPASSRLS PASSWORD 'SCRAM-SHA-256$4096:WxfbjBZCm1a/QdikNsDm6A==$Hn4uTOG/375ftM6SLlqmb/ppJoSmBOhrsAa5EGZm4ro=:rXSRa5bTCmS+9j6gZpctgjH7E7YTbKAGy9Sg5L28vvw=';






\unrestrict c5uQye4zq18xtON7otWN0GdbfOHcqhzjZNEDhAX6DemWBHvmq7JcOhRPRkTl4Mx

--
-- Databases
--

--
-- Database "template1" dump
--

\connect template1

--
-- PostgreSQL database dump
--

\restrict KYx09fmtRETjTYTj1n3Shq5VJK88FnAfO0v07oz2p0iAVtUbPHDtLtOBgsG7xiI

-- Dumped from database version 14.22
-- Dumped by pg_dump version 14.22

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- PostgreSQL database dump complete
--

\unrestrict KYx09fmtRETjTYTj1n3Shq5VJK88FnAfO0v07oz2p0iAVtUbPHDtLtOBgsG7xiI

--
-- Database "KHHub_AIManagement" dump
--

--
-- PostgreSQL database dump
--

\restrict Yih9uuYM3lEKYU6xk8uwoXM7rqqIhbFzSMxSllyatrQ0bOqs2DeBaLqMHL87mh3

-- Dumped from database version 14.22
-- Dumped by pg_dump version 14.22

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: KHHub_AIManagement; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE "KHHub_AIManagement" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'en_US.utf8';


ALTER DATABASE "KHHub_AIManagement" OWNER TO postgres;

\unrestrict Yih9uuYM3lEKYU6xk8uwoXM7rqqIhbFzSMxSllyatrQ0bOqs2DeBaLqMHL87mh3
\connect "KHHub_AIManagement"
\restrict Yih9uuYM3lEKYU6xk8uwoXM7rqqIhbFzSMxSllyatrQ0bOqs2DeBaLqMHL87mh3

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: AIManagementApplicationAIProviders; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AIManagementApplicationAIProviders" (
    "Id" uuid NOT NULL,
    "ApplicationName" text NOT NULL,
    "SupportedProviders" text NOT NULL,
    "SupportedEmbedderProviders" text NOT NULL,
    "SupportedVectorStoreProviders" text NOT NULL
);


ALTER TABLE public."AIManagementApplicationAIProviders" OWNER TO postgres;

--
-- Name: AIManagementDocumentChunks; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AIManagementDocumentChunks" (
    "Id" uuid NOT NULL,
    "WorkspaceId" uuid NOT NULL,
    "DataSourceId" uuid NOT NULL,
    "Content" text NOT NULL,
    "ChunkIndex" integer NOT NULL,
    "ExtraProperties" text NOT NULL,
    "ConcurrencyStamp" character varying(40) NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL,
    "CreatorId" uuid,
    "LastModificationTime" timestamp without time zone,
    "LastModifierId" uuid
);


ALTER TABLE public."AIManagementDocumentChunks" OWNER TO postgres;

--
-- Name: AIManagementMcpServers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AIManagementMcpServers" (
    "Id" uuid NOT NULL,
    "Name" character varying(128) NOT NULL,
    "DisplayName" character varying(256),
    "Description" character varying(1024),
    "TransportType" integer NOT NULL,
    "IsActive" boolean NOT NULL,
    "Command" character varying(1024),
    "Arguments" character varying(4096),
    "WorkingDirectory" character varying(1024),
    "EnvironmentVariables" character varying(8192),
    "Endpoint" character varying(2048),
    "Headers" character varying(8192),
    "AuthType" integer NOT NULL,
    "ApiKey" character varying(512),
    "CustomAuthHeaderName" character varying(128),
    "CustomAuthHeaderValue" character varying(1024),
    "ExtraProperties" text NOT NULL,
    "ConcurrencyStamp" character varying(40) NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL,
    "CreatorId" uuid,
    "LastModificationTime" timestamp without time zone,
    "LastModifierId" uuid
);


ALTER TABLE public."AIManagementMcpServers" OWNER TO postgres;

--
-- Name: AIManagementWorkspaceDataSources; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AIManagementWorkspaceDataSources" (
    "Id" uuid NOT NULL,
    "WorkspaceId" uuid NOT NULL,
    "FileName" character varying(256) NOT NULL,
    "BlobName" character varying(512) NOT NULL,
    "ContentType" character varying(128) NOT NULL,
    "FileSize" bigint NOT NULL,
    "IsProcessed" boolean NOT NULL,
    "ProcessedTime" timestamp without time zone,
    "Description" text,
    "LastError" character varying(2000),
    "LastErrorTime" timestamp without time zone,
    "ExtraProperties" text NOT NULL,
    "ConcurrencyStamp" character varying(40) NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL,
    "CreatorId" uuid,
    "LastModificationTime" timestamp without time zone,
    "LastModifierId" uuid
);


ALTER TABLE public."AIManagementWorkspaceDataSources" OWNER TO postgres;

--
-- Name: AIManagementWorkspaceMcpServers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AIManagementWorkspaceMcpServers" (
    "WorkspaceId" uuid NOT NULL,
    "McpServerId" uuid NOT NULL,
    "IsEnabled" boolean NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL,
    "CreatorId" uuid
);


ALTER TABLE public."AIManagementWorkspaceMcpServers" OWNER TO postgres;

--
-- Name: AIManagementWorkspaces; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AIManagementWorkspaces" (
    "Id" uuid NOT NULL,
    "ApplicationName" text,
    "Name" text NOT NULL,
    "Provider" text,
    "ApiKey" text,
    "ModelName" text,
    "SystemPrompt" text,
    "Temperature" real,
    "ApiBaseUrl" text,
    "Description" text,
    "IsActive" boolean NOT NULL,
    "OverrideSystemConfiguration" boolean NOT NULL,
    "IsSystem" boolean NOT NULL,
    "RequiredPermissionName" text,
    "EmbedderProvider" text,
    "EmbedderModelName" text,
    "EmbedderApiKey" text,
    "EmbedderApiBaseUrl" text,
    "VectorStoreProvider" text,
    "VectorStoreSettings" text,
    "ExtraProperties" text NOT NULL,
    "ConcurrencyStamp" character varying(40) NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL,
    "CreatorId" uuid,
    "LastModificationTime" timestamp without time zone,
    "LastModifierId" uuid
);


ALTER TABLE public."AIManagementWorkspaces" OWNER TO postgres;

--
-- Name: AbpEventInbox; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpEventInbox" (
    "Id" uuid NOT NULL,
    "ExtraProperties" text NOT NULL,
    "MessageId" text NOT NULL,
    "EventName" character varying(256) NOT NULL,
    "EventData" bytea NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL,
    "Status" integer NOT NULL,
    "HandledTime" timestamp without time zone,
    "RetryCount" integer NOT NULL,
    "NextRetryTime" timestamp without time zone
);


ALTER TABLE public."AbpEventInbox" OWNER TO postgres;

--
-- Name: AbpEventOutbox; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpEventOutbox" (
    "Id" uuid NOT NULL,
    "ExtraProperties" text NOT NULL,
    "EventName" character varying(256) NOT NULL,
    "EventData" bytea NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL
);


ALTER TABLE public."AbpEventOutbox" OWNER TO postgres;

--
-- Name: __AIManagementService_Migrations; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."__AIManagementService_Migrations" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__AIManagementService_Migrations" OWNER TO postgres;

--
-- Data for Name: AIManagementApplicationAIProviders; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AIManagementApplicationAIProviders" ("Id", "ApplicationName", "SupportedProviders", "SupportedEmbedderProviders", "SupportedVectorStoreProviders") FROM stdin;
3a20e9c3-5b15-a13f-656d-79688c30b604	KHHub.AIManagementService	OpenAI,Ollama	OpenAI,Ollama	
\.


--
-- Data for Name: AIManagementDocumentChunks; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AIManagementDocumentChunks" ("Id", "WorkspaceId", "DataSourceId", "Content", "ChunkIndex", "ExtraProperties", "ConcurrencyStamp", "CreationTime", "CreatorId", "LastModificationTime", "LastModifierId") FROM stdin;
\.


--
-- Data for Name: AIManagementMcpServers; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AIManagementMcpServers" ("Id", "Name", "DisplayName", "Description", "TransportType", "IsActive", "Command", "Arguments", "WorkingDirectory", "EnvironmentVariables", "Endpoint", "Headers", "AuthType", "ApiKey", "CustomAuthHeaderName", "CustomAuthHeaderValue", "ExtraProperties", "ConcurrencyStamp", "CreationTime", "CreatorId", "LastModificationTime", "LastModifierId") FROM stdin;
\.


--
-- Data for Name: AIManagementWorkspaceDataSources; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AIManagementWorkspaceDataSources" ("Id", "WorkspaceId", "FileName", "BlobName", "ContentType", "FileSize", "IsProcessed", "ProcessedTime", "Description", "LastError", "LastErrorTime", "ExtraProperties", "ConcurrencyStamp", "CreationTime", "CreatorId", "LastModificationTime", "LastModifierId") FROM stdin;
\.


--
-- Data for Name: AIManagementWorkspaceMcpServers; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AIManagementWorkspaceMcpServers" ("WorkspaceId", "McpServerId", "IsEnabled", "CreationTime", "CreatorId") FROM stdin;
\.


--
-- Data for Name: AIManagementWorkspaces; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AIManagementWorkspaces" ("Id", "ApplicationName", "Name", "Provider", "ApiKey", "ModelName", "SystemPrompt", "Temperature", "ApiBaseUrl", "Description", "IsActive", "OverrideSystemConfiguration", "IsSystem", "RequiredPermissionName", "EmbedderProvider", "EmbedderModelName", "EmbedderApiKey", "EmbedderApiBaseUrl", "VectorStoreProvider", "VectorStoreSettings", "ExtraProperties", "ConcurrencyStamp", "CreationTime", "CreatorId", "LastModificationTime", "LastModifierId") FROM stdin;
3a20e9c3-52d0-af7d-9974-20bce0c3e76f	KHHub.AIManagementService	OllamaAssistant	Ollama		llama3.2	\N	\N	http://localhost:11434	\N	t	f	f	\N	\N	\N	\N	\N	\N	\N	{}	d916eb0a2f3d4afbb163ba7972d96751	2026-04-29 11:54:16.630015	\N	\N	\N
3a20e9c3-5576-11a8-3022-595e5a80e4e9	KHHub.AIManagementService	OpenAIAssistant	OpenAI		gpt-5.4	\N	\N	https://api.openai.com/v1	\N	t	f	f	\N	\N	\N	\N	\N	\N	\N	{}	39e2c1bf4d6545f08806b3dfab44e748	2026-04-29 11:54:17.229355	\N	\N	\N
\.


--
-- Data for Name: AbpEventInbox; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpEventInbox" ("Id", "ExtraProperties", "MessageId", "EventName", "EventData", "CreationTime", "Status", "HandledTime", "RetryCount", "NextRetryTime") FROM stdin;
\.


--
-- Data for Name: AbpEventOutbox; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpEventOutbox" ("Id", "ExtraProperties", "EventName", "EventData", "CreationTime") FROM stdin;
\.


--
-- Data for Name: __AIManagementService_Migrations; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."__AIManagementService_Migrations" ("MigrationId", "ProductVersion") FROM stdin;
20260429043501_Initial	10.0.2
\.


--
-- Name: AIManagementApplicationAIProviders PK_AIManagementApplicationAIProviders; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AIManagementApplicationAIProviders"
    ADD CONSTRAINT "PK_AIManagementApplicationAIProviders" PRIMARY KEY ("Id");


--
-- Name: AIManagementDocumentChunks PK_AIManagementDocumentChunks; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AIManagementDocumentChunks"
    ADD CONSTRAINT "PK_AIManagementDocumentChunks" PRIMARY KEY ("Id");


--
-- Name: AIManagementMcpServers PK_AIManagementMcpServers; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AIManagementMcpServers"
    ADD CONSTRAINT "PK_AIManagementMcpServers" PRIMARY KEY ("Id");


--
-- Name: AIManagementWorkspaceDataSources PK_AIManagementWorkspaceDataSources; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AIManagementWorkspaceDataSources"
    ADD CONSTRAINT "PK_AIManagementWorkspaceDataSources" PRIMARY KEY ("Id");


--
-- Name: AIManagementWorkspaceMcpServers PK_AIManagementWorkspaceMcpServers; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AIManagementWorkspaceMcpServers"
    ADD CONSTRAINT "PK_AIManagementWorkspaceMcpServers" PRIMARY KEY ("WorkspaceId", "McpServerId");


--
-- Name: AIManagementWorkspaces PK_AIManagementWorkspaces; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AIManagementWorkspaces"
    ADD CONSTRAINT "PK_AIManagementWorkspaces" PRIMARY KEY ("Id");


--
-- Name: AbpEventInbox PK_AbpEventInbox; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpEventInbox"
    ADD CONSTRAINT "PK_AbpEventInbox" PRIMARY KEY ("Id");


--
-- Name: AbpEventOutbox PK_AbpEventOutbox; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpEventOutbox"
    ADD CONSTRAINT "PK_AbpEventOutbox" PRIMARY KEY ("Id");


--
-- Name: __AIManagementService_Migrations PK___AIManagementService_Migrations; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."__AIManagementService_Migrations"
    ADD CONSTRAINT "PK___AIManagementService_Migrations" PRIMARY KEY ("MigrationId");


--
-- Name: IX_AIManagementDocumentChunks_DataSourceId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AIManagementDocumentChunks_DataSourceId" ON public."AIManagementDocumentChunks" USING btree ("DataSourceId");


--
-- Name: IX_AIManagementDocumentChunks_WorkspaceId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AIManagementDocumentChunks_WorkspaceId" ON public."AIManagementDocumentChunks" USING btree ("WorkspaceId");


--
-- Name: IX_AIManagementMcpServers_Name; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "IX_AIManagementMcpServers_Name" ON public."AIManagementMcpServers" USING btree ("Name");


--
-- Name: IX_AIManagementWorkspaceDataSources_IsProcessed; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AIManagementWorkspaceDataSources_IsProcessed" ON public."AIManagementWorkspaceDataSources" USING btree ("IsProcessed");


--
-- Name: IX_AIManagementWorkspaceDataSources_WorkspaceId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AIManagementWorkspaceDataSources_WorkspaceId" ON public."AIManagementWorkspaceDataSources" USING btree ("WorkspaceId");


--
-- Name: IX_AIManagementWorkspaceMcpServers_McpServerId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AIManagementWorkspaceMcpServers_McpServerId" ON public."AIManagementWorkspaceMcpServers" USING btree ("McpServerId");


--
-- Name: IX_AIManagementWorkspaceMcpServers_WorkspaceId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AIManagementWorkspaceMcpServers_WorkspaceId" ON public."AIManagementWorkspaceMcpServers" USING btree ("WorkspaceId");


--
-- Name: IX_AIManagementWorkspaces_Name; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "IX_AIManagementWorkspaces_Name" ON public."AIManagementWorkspaces" USING btree ("Name");


--
-- Name: IX_AbpEventInbox_MessageId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpEventInbox_MessageId" ON public."AbpEventInbox" USING btree ("MessageId");


--
-- Name: IX_AbpEventInbox_Status_CreationTime; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpEventInbox_Status_CreationTime" ON public."AbpEventInbox" USING btree ("Status", "CreationTime");


--
-- Name: IX_AbpEventOutbox_CreationTime; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpEventOutbox_CreationTime" ON public."AbpEventOutbox" USING btree ("CreationTime");


--
-- Name: AIManagementWorkspaceMcpServers FK_AIManagementWorkspaceMcpServers_AIManagementWorkspaces_Work~; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AIManagementWorkspaceMcpServers"
    ADD CONSTRAINT "FK_AIManagementWorkspaceMcpServers_AIManagementWorkspaces_Work~" FOREIGN KEY ("WorkspaceId") REFERENCES public."AIManagementWorkspaces"("Id") ON DELETE CASCADE;


--
-- PostgreSQL database dump complete
--

\unrestrict Yih9uuYM3lEKYU6xk8uwoXM7rqqIhbFzSMxSllyatrQ0bOqs2DeBaLqMHL87mh3

--
-- Database "KHHub_Administration" dump
--

--
-- PostgreSQL database dump
--

\restrict fkgRYldqv1iUouTB737YRIMYSnP1Md9S48kEThNAnqNomliDMVGp62prT6b3XZn

-- Dumped from database version 14.22
-- Dumped by pg_dump version 14.22

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: KHHub_Administration; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE "KHHub_Administration" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'en_US.utf8';


ALTER DATABASE "KHHub_Administration" OWNER TO postgres;

\unrestrict fkgRYldqv1iUouTB737YRIMYSnP1Md9S48kEThNAnqNomliDMVGp62prT6b3XZn
\connect "KHHub_Administration"
\restrict fkgRYldqv1iUouTB737YRIMYSnP1Md9S48kEThNAnqNomliDMVGp62prT6b3XZn

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: AbpEventInbox; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpEventInbox" (
    "Id" uuid NOT NULL,
    "ExtraProperties" text NOT NULL,
    "MessageId" text NOT NULL,
    "EventName" character varying(256) NOT NULL,
    "EventData" bytea NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL,
    "Status" integer NOT NULL,
    "HandledTime" timestamp without time zone,
    "RetryCount" integer NOT NULL,
    "NextRetryTime" timestamp without time zone
);


ALTER TABLE public."AbpEventInbox" OWNER TO postgres;

--
-- Name: AbpEventOutbox; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpEventOutbox" (
    "Id" uuid NOT NULL,
    "ExtraProperties" text NOT NULL,
    "EventName" character varying(256) NOT NULL,
    "EventData" bytea NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL
);


ALTER TABLE public."AbpEventOutbox" OWNER TO postgres;

--
-- Name: AbpFeatureGroups; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpFeatureGroups" (
    "Id" uuid NOT NULL,
    "Name" character varying(128) NOT NULL,
    "DisplayName" character varying(256) NOT NULL,
    "ExtraProperties" text
);


ALTER TABLE public."AbpFeatureGroups" OWNER TO postgres;

--
-- Name: AbpFeatureValues; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpFeatureValues" (
    "Id" uuid NOT NULL,
    "Name" character varying(128) NOT NULL,
    "Value" character varying(128) NOT NULL,
    "ProviderName" character varying(64),
    "ProviderKey" character varying(64)
);


ALTER TABLE public."AbpFeatureValues" OWNER TO postgres;

--
-- Name: AbpFeatures; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpFeatures" (
    "Id" uuid NOT NULL,
    "GroupName" character varying(128) NOT NULL,
    "Name" character varying(128) NOT NULL,
    "ParentName" character varying(128),
    "DisplayName" character varying(256) NOT NULL,
    "Description" character varying(256),
    "DefaultValue" character varying(256),
    "IsVisibleToClients" boolean NOT NULL,
    "IsAvailableToHost" boolean NOT NULL,
    "AllowedProviders" character varying(256),
    "ValueType" character varying(2048),
    "ExtraProperties" text
);


ALTER TABLE public."AbpFeatures" OWNER TO postgres;

--
-- Name: AbpPermissionGrants; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpPermissionGrants" (
    "Id" uuid NOT NULL,
    "TenantId" uuid,
    "Name" character varying(128) NOT NULL,
    "ProviderName" character varying(64) NOT NULL,
    "ProviderKey" character varying(64) NOT NULL
);


ALTER TABLE public."AbpPermissionGrants" OWNER TO postgres;

--
-- Name: AbpPermissionGroups; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpPermissionGroups" (
    "Id" uuid NOT NULL,
    "Name" character varying(128) NOT NULL,
    "DisplayName" character varying(256) NOT NULL,
    "ExtraProperties" text
);


ALTER TABLE public."AbpPermissionGroups" OWNER TO postgres;

--
-- Name: AbpPermissions; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpPermissions" (
    "Id" uuid NOT NULL,
    "GroupName" character varying(128),
    "Name" character varying(128) NOT NULL,
    "ResourceName" character varying(256),
    "ManagementPermissionName" character varying(128),
    "ParentName" character varying(128),
    "DisplayName" character varying(256) NOT NULL,
    "IsEnabled" boolean NOT NULL,
    "MultiTenancySide" smallint NOT NULL,
    "Providers" character varying(128),
    "StateCheckers" character varying(256),
    "ExtraProperties" text
);


ALTER TABLE public."AbpPermissions" OWNER TO postgres;

--
-- Name: AbpResourcePermissionGrants; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpResourcePermissionGrants" (
    "Id" uuid NOT NULL,
    "TenantId" uuid,
    "Name" character varying(128) NOT NULL,
    "ProviderName" character varying(64) NOT NULL,
    "ProviderKey" character varying(64) NOT NULL,
    "ResourceName" character varying(256) NOT NULL,
    "ResourceKey" character varying(256) NOT NULL
);


ALTER TABLE public."AbpResourcePermissionGrants" OWNER TO postgres;

--
-- Name: AbpSettingDefinitions; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpSettingDefinitions" (
    "Id" uuid NOT NULL,
    "Name" character varying(128) NOT NULL,
    "DisplayName" character varying(256) NOT NULL,
    "Description" character varying(512),
    "DefaultValue" character varying(2048),
    "IsVisibleToClients" boolean NOT NULL,
    "Providers" character varying(1024),
    "IsInherited" boolean NOT NULL,
    "IsEncrypted" boolean NOT NULL,
    "ExtraProperties" text
);


ALTER TABLE public."AbpSettingDefinitions" OWNER TO postgres;

--
-- Name: AbpSettings; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpSettings" (
    "Id" uuid NOT NULL,
    "Name" character varying(128) NOT NULL,
    "Value" character varying(2048) NOT NULL,
    "ProviderName" character varying(64),
    "ProviderKey" character varying(64)
);


ALTER TABLE public."AbpSettings" OWNER TO postgres;

--
-- Name: AbpTextTemplateContents; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpTextTemplateContents" (
    "Id" uuid NOT NULL,
    "TenantId" uuid,
    "Name" character varying(128) NOT NULL,
    "CultureName" character varying(10),
    "Content" character varying(65535) NOT NULL,
    "ExtraProperties" text NOT NULL,
    "ConcurrencyStamp" character varying(40) NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL,
    "CreatorId" uuid,
    "LastModificationTime" timestamp without time zone,
    "LastModifierId" uuid,
    "IsDeleted" boolean DEFAULT false NOT NULL,
    "DeleterId" uuid,
    "DeletionTime" timestamp without time zone
);


ALTER TABLE public."AbpTextTemplateContents" OWNER TO postgres;

--
-- Name: AbpTextTemplateDefinitionContentRecords; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpTextTemplateDefinitionContentRecords" (
    "Id" uuid NOT NULL,
    "DefinitionId" uuid NOT NULL,
    "FileName" character varying(256) NOT NULL,
    "FileContent" text
);


ALTER TABLE public."AbpTextTemplateDefinitionContentRecords" OWNER TO postgres;

--
-- Name: AbpTextTemplateDefinitionRecords; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpTextTemplateDefinitionRecords" (
    "Id" uuid NOT NULL,
    "Name" character varying(128) NOT NULL,
    "DisplayName" character varying(256),
    "IsLayout" boolean NOT NULL,
    "Layout" character varying(256),
    "LocalizationResourceName" character varying(256),
    "IsInlineLocalized" boolean NOT NULL,
    "DefaultCultureName" character varying(10),
    "RenderEngine" character varying(64),
    "ExtraProperties" text
);


ALTER TABLE public."AbpTextTemplateDefinitionRecords" OWNER TO postgres;

--
-- Name: __AdministrationService_Migrations; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."__AdministrationService_Migrations" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__AdministrationService_Migrations" OWNER TO postgres;

--
-- Data for Name: AbpEventInbox; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpEventInbox" ("Id", "ExtraProperties", "MessageId", "EventName", "EventData", "CreationTime", "Status", "HandledTime", "RetryCount", "NextRetryTime") FROM stdin;
\.


--
-- Data for Name: AbpEventOutbox; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpEventOutbox" ("Id", "ExtraProperties", "EventName", "EventData", "CreationTime") FROM stdin;
\.


--
-- Data for Name: AbpFeatureGroups; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpFeatureGroups" ("Id", "Name", "DisplayName", "ExtraProperties") FROM stdin;
3a20e9c3-4e85-707e-1ee2-6c23358e7de2	SettingManagement	L:AbpSettingManagement,Feature:SettingManagementGroup	{}
3a20e9c3-4f74-cd85-c4e0-fd773ef33bb5	AuditLogging	L:AbpAuditLogging,Feature:AuditLoggingGroup	{}
3a20e9c3-548b-2b62-617b-62920d366a3d	LanguageManagement	L:LanguageManagement,Feature:LanguageManagementGroup	{}
3a20e9c3-6417-3d3c-b55c-52cbddbf488a	TextManagement	L:TextTemplateManagement,Feature:TextManagementGroup	{}
3a20e9c3-6433-75ff-ecce-97e363f54231	Identity	L:AbpIdentity,Feature:IdentityGroup	{}
3a20e9c3-9937-b7b5-65e1-197d7166a0a6	Account	L:AbpAccount,Feature:AccountGroup	{}
\.


--
-- Data for Name: AbpFeatureValues; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpFeatureValues" ("Id", "Name", "Value", "ProviderName", "ProviderKey") FROM stdin;
\.


--
-- Data for Name: AbpFeatures; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpFeatures" ("Id", "GroupName", "Name", "ParentName", "DisplayName", "Description", "DefaultValue", "IsVisibleToClients", "IsAvailableToHost", "AllowedProviders", "ValueType", "ExtraProperties") FROM stdin;
3a20e9c3-4e86-d5b1-c50b-0130aeb15260	SettingManagement	SettingManagement.Enable	\N	L:AbpSettingManagement,Feature:SettingManagementEnable	L:AbpSettingManagement,Feature:SettingManagementEnableDescription	true	t	f	\N	{"name":"ToggleStringValueType","properties":{},"validator":{"name":"BOOLEAN","properties":{}}}	{}
3a20e9c3-4e89-c152-92e3-250d8a5f6190	SettingManagement	SettingManagement.AllowChangingEmailSettings	SettingManagement.Enable	L:AbpSettingManagement,Feature:AllowChangingEmailSettings	\N	false	t	f	\N	{"name":"ToggleStringValueType","properties":{},"validator":{"name":"BOOLEAN","properties":{}}}	{}
3a20e9c3-4f76-aaab-95e8-608d41f93939	AuditLogging	AuditLogging.Enable	\N	L:AbpAuditLogging,Feature:AuditLoggingEnable	L:AbpAuditLogging,Feature:AuditLoggingEnableDescription	true	t	t	\N	{"name":"ToggleStringValueType","properties":{},"validator":{"name":"BOOLEAN","properties":{}}}	{}
3a20e9c3-4f7d-06e6-a017-4699c1e7b3d0	AuditLogging	AuditLogging.SettingManagement	AuditLogging.Enable	L:AbpAuditLogging,Feature:AuditLoggingSettingManagementEnable	L:AbpAuditLogging,Feature:AuditLoggingSettingManagementEnableDescription	false	t	t	\N	{"name":"ToggleStringValueType","properties":{},"validator":{"name":"BOOLEAN","properties":{}}}	{}
3a20e9c3-548b-92b6-698a-fd6ef0cf6c0c	LanguageManagement	LanguageManagement.Enable	\N	L:LanguageManagement,Feature:LanguageManagementEnable	L:LanguageManagement,Feature:LanguageManagementEnableDescription	true	t	t	\N	{"name":"ToggleStringValueType","properties":{},"validator":{"name":"BOOLEAN","properties":{}}}	{}
3a20e9c3-6429-385c-2b57-53661502125c	TextManagement	TextManagement.Enable	\N	L:TextTemplateManagement,Feature:TextManagementEnable	L:TextTemplateManagement,Feature:TextManagementEnableDescription	true	t	t	\N	{"name":"ToggleStringValueType","properties":{},"validator":{"name":"BOOLEAN","properties":{}}}	{}
3a20e9c3-6433-6539-348a-692d756037b0	Identity	Identity.TwoFactor	\N	L:AbpIdentity,Feature:TwoFactor	L:AbpIdentity,Feature:TwoFactorDescription	Optional	t	t	\N	{"itemSource":{"items":[{"value":"Optional","displayText":{"resourceName":"AbpIdentity","name":"Feature:TwoFactor.Optional"}},{"value":"Disabled","displayText":{"resourceName":"AbpIdentity","name":"Feature:TwoFactor.Disabled"}},{"value":"Forced","displayText":{"resourceName":"AbpIdentity","name":"Feature:TwoFactor.Forced"}}]},"name":"SelectionStringValueType","properties":{},"validator":{"name":"NULL","properties":{}}}	{}
3a20e9c3-643b-ad59-e8b8-2ed6577b3f52	Identity	Identity.MaxUserCount	\N	L:AbpIdentity,Feature:MaximumUserCount	L:AbpIdentity,Feature:MaximumUserCountDescription	0	t	t	\N	{"name":"FreeTextStringValueType","properties":{},"validator":{"name":"NUMERIC","properties":{"MinValue":0,"MaxValue":2147483647}}}	{}
3a20e9c3-643b-b966-f5aa-859657da4624	Identity	Account.EnableLdapLogin	\N	L:AbpIdentity,Feature:EnableLdapLogin	\N	False	t	t	\N	{"name":"ToggleStringValueType","properties":{},"validator":{"name":"BOOLEAN","properties":{}}}	{}
3a20e9c3-643b-c200-f887-be727d6614c8	Identity	Identity.EnableOAuthLogin	\N	L:AbpIdentity,Feature:EnableOAuthLogin	\N	False	t	t	\N	{"name":"ToggleStringValueType","properties":{},"validator":{"name":"BOOLEAN","properties":{}}}	{}
\.


--
-- Data for Name: AbpPermissionGrants; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpPermissionGrants" ("Id", "TenantId", "Name", "ProviderName", "ProviderKey") FROM stdin;
3a20e9c3-545f-9f86-7692-f19c0bd23fbd	\N	OpenIddictPro.Application	R	admin
3a20e9c3-546d-0bb8-efa9-a97b365efdae	\N	AbpIdentity.Roles.Delete	R	admin
3a20e9c3-546d-0d8f-0d54-1fb3c5cc8288	\N	AbpIdentity.Users	R	admin
3a20e9c3-546d-1093-6a66-7b2b2df35d02	\N	AbpIdentity.ClaimTypes.Create	R	admin
3a20e9c3-546d-23bb-ea99-231ec3463853	\N	AbpIdentity.Users.ManagePermissions	R	admin
3a20e9c3-546d-27cf-6a67-a7159dd2458a	\N	AbpIdentity.Roles.Create	R	admin
3a20e9c3-546d-2eb7-08b8-2b49caeabf2d	\N	AuditLogging.ViewChangeHistory:Volo.Abp.Identity.IdentityRole	R	admin
3a20e9c3-546d-2eec-7c4b-ab1e2fb75b39	\N	OpenIddictPro.Scope.Delete	R	admin
3a20e9c3-546d-32e0-4890-da99fd6ef7df	\N	OpenIddictPro.Scope.Create	R	admin
3a20e9c3-546d-40e9-880c-d42b04bea1ba	\N	AbpIdentity.Users.Export	R	admin
3a20e9c3-546d-4406-0db9-9ff64243ab5a	\N	AbpIdentity.OrganizationUnits.ManageRoles	R	admin
3a20e9c3-546d-50d7-49d5-b07221d112b8	\N	AbpIdentity.Users.Create	R	admin
3a20e9c3-546d-5b11-5f79-3c72e6d17297	\N	AbpIdentity.Users.Update.ManageRoles	R	admin
3a20e9c3-546d-68e4-8f2e-dbb7e90ac9f3	\N	OpenIddictPro.Application.Delete	R	admin
3a20e9c3-546d-6d55-e9cf-72c2e11f91d2	\N	AbpIdentity.SettingManagement	R	admin
3a20e9c3-546d-71d7-d718-c725d810f9e9	\N	OpenIddictPro.Application.Update	R	admin
3a20e9c3-546d-7aa7-be9c-9da1d00361a8	\N	AbpIdentity.Users.Delete	R	admin
3a20e9c3-546d-828a-ea47-d8d28981f3d9	\N	OpenIddictPro.Application.ManagePermissions	R	admin
3a20e9c3-546d-88bb-28a0-c0d0020584e3	\N	OpenIddictPro.Scope.Update	R	admin
3a20e9c3-546d-896f-2b53-b1c4ac451436	\N	AbpIdentity.Sessions	R	admin
3a20e9c3-546d-9305-7ca2-d514e646f680	\N	AbpIdentity.Roles	R	admin
3a20e9c3-546d-96c6-b62a-84602c2d7731	\N	AbpIdentity.Users.ViewDetails	R	admin
3a20e9c3-546d-9837-057e-adf915865d91	\N	AbpIdentity.Users.Import	R	admin
3a20e9c3-546d-9d6a-4538-b7aacaf58963	\N	AbpIdentity.ClaimTypes	R	admin
3a20e9c3-546d-a2b9-2692-56d50b0163fc	\N	AuditLogging.ViewChangeHistory:Volo.Abp.OpenIddict.Pro.Applications.Application	R	admin
3a20e9c3-546d-a4e4-389b-bf5a81b84de4	\N	AbpIdentity.Users.Update.ManageOU	R	admin
3a20e9c3-546d-aa65-22bf-3585712febeb	\N	AuditLogging.ViewChangeHistory:Volo.Abp.OpenIddict.Pro.Scopes.Scope	R	admin
3a20e9c3-546d-adb1-338a-fe93791ac1dc	\N	AbpIdentity.OrganizationUnits	R	admin
3a20e9c3-546d-b0a9-5a5e-79241ea5e4c1	\N	AbpIdentity.Roles.ManagePermissions	R	admin
3a20e9c3-546d-b180-5dc3-94d985fc5f0e	\N	AbpIdentity.ClaimTypes.Delete	R	admin
3a20e9c3-546d-b224-45aa-e7e0dce75f9d	\N	AbpIdentity.OrganizationUnits.ManageMembers	R	admin
3a20e9c3-546d-b730-a28f-1db48a63c817	\N	OpenIddictPro.Scope	R	admin
3a20e9c3-546d-c478-78ef-29948b535d7c	\N	AuditLogging.ViewChangeHistory:Volo.Abp.Identity.IdentityUser	R	admin
3a20e9c3-546d-c526-3113-a5c59f8752eb	\N	AbpIdentity.Users.Impersonation	R	admin
3a20e9c3-546d-c984-1bcf-fa84f25c72ba	\N	AbpIdentity.Users.Update	R	admin
3a20e9c3-546d-d722-e72e-55cee7e357a4	\N	AbpIdentity.OrganizationUnits.ManageOU	R	admin
3a20e9c3-546d-ea1c-f25b-e85865adb31b	\N	AbpIdentity.ClaimTypes.Update	R	admin
3a20e9c3-546d-ef8c-ac6a-5a05e6e82aa4	\N	OpenIddictPro.Application.Create	R	admin
3a20e9c3-546d-fa45-8966-9ac2a27a439a	\N	AbpIdentity.SecurityLogs	R	admin
3a20e9c3-546d-fd84-9c94-ca98ea58c386	\N	AbpIdentity.Roles.Update	R	admin
3a20e9c3-5835-1fa1-13fe-71d6d74bbd70	\N	TextTemplateManagement.TextTemplates	R	admin
3a20e9c3-5835-30c2-aa19-92336eb6cdc2	\N	TextTemplateManagement.TextTemplates.EditContents	R	admin
3a20e9c3-5835-543b-ebc1-9d38e2325bed	\N	SettingManagement.Emailing	R	admin
3a20e9c3-5835-5bb1-ead8-986a29190e55	\N	AdministrationService.Dashboard.Host	R	admin
3a20e9c3-5835-6431-c63b-697bf2d704bd	\N	AuditLogging.AuditLogs.SettingManagement	R	admin
3a20e9c3-5835-70d3-0442-f6217bf6e81e	\N	AuditLogging.AuditLogs.Export	R	admin
3a20e9c3-5835-7812-070e-4e3a1f37adc6	\N	FeatureManagement.ManageHostFeatures	R	admin
3a20e9c3-5835-945f-3725-988b4f337ef1	\N	SettingManagement.TimeZone	R	admin
3a20e9c3-5835-c48f-f219-efe3917c16d6	\N	SettingManagement.Emailing.Test	R	admin
3a20e9c3-5835-e08e-7015-2d0ccdeaa309	\N	AuditLogging.AuditLogs	R	admin
3a20e9c3-a781-1776-6dad-d8f01935e7be	\N	AbpAccount.SettingManagement	R	admin
3a20eee2-7d2b-119e-44b2-809a51f6aabd	\N	AIManagement.Workspaces	R	admin
3a20eee2-7d2b-1431-69cd-48998a556f90	\N	AIManagement.McpServers.Delete	R	admin
3a20eee2-7d2b-15ea-5261-a24b0178bab1	\N	MasterDataService.Provinces.Create	R	admin
3a20eee2-7d2b-1929-0c44-2a45fe0ced17	\N	LanguageManagement.Languages.Delete	R	admin
3a20eee2-7d2b-2158-ed06-330c6cd02095	\N	AIManagement.Workspaces.Delete	R	admin
3a20eee2-7d2b-22da-3f42-e095a3828e3d	\N	AIManagement.WorkspaceDataSources.ReIndex	R	admin
3a20eee2-7d2b-242d-6e50-0fd5518a259b	\N	LanguageManagement.LanguageTexts.Edit	R	admin
3a20eee2-7d2b-396d-f63e-7b1b277ea116	\N	MasterDataService.Provinces.Edit	R	admin
3a20eee2-7d2b-3b0a-1562-bc46f06e8db0	\N	LanguageManagement.Languages	R	admin
3a20eee2-7d2b-40f7-0650-696031206a2c	\N	MasterDataService.Provinces	R	admin
3a20eee2-7d2b-475e-cc0b-04d134833e50	\N	AIManagement.McpServers	R	admin
3a20eee2-7d2b-5b6c-318a-a9023f9d3a21	\N	AIManagement.WorkspaceDataSources	R	admin
3a20eee2-7d2b-5b81-febd-3926970c449a	\N	AIManagement.Workspaces.Playground	R	admin
3a20eee2-7d2b-62a9-0287-667b281d04ff	\N	LanguageManagement.Languages.Create	R	admin
3a20eee2-7d2b-a50d-4e26-21371c09b7fd	\N	LanguageManagement.Languages.Edit	R	admin
3a20eee2-7d2b-a57e-a0a9-8a6ecc0204af	\N	AIManagement.WorkspaceDataSources.Download	R	admin
3a20eee2-7d2b-a750-d350-fb133cb8a8ad	\N	LanguageManagement.LanguageTexts	R	admin
3a20eee2-7d2b-a7cb-38e4-7cc4eb74983b	\N	MasterDataService.Provinces.Delete	R	admin
3a20eee2-7d2b-b2c2-999b-8207a6b2abd7	\N	LanguageManagement.Languages.ChangeDefault	R	admin
3a20eee2-7d2b-b350-c0b1-d36c60545a48	\N	AIManagement.McpServers.Create	R	admin
3a20eee2-7d2b-b486-1dbe-820ac1f8c4f1	\N	AIManagement.WorkspaceDataSources.Create	R	admin
3a20eee2-7d2b-bc9a-4a79-0a2078cfb56a	\N	AIManagement.McpServers.Update	R	admin
3a20eee2-7d2b-ca12-6d9b-85c891eff91d	\N	AIManagement.Workspaces.Update	R	admin
3a20eee2-7d2b-cf0d-6e4c-241fdd535f45	\N	AIManagement.WorkspaceDataSources.Update	R	admin
3a20eee2-7d2b-e1af-7536-e00b945db6a3	\N	AIManagement.Workspaces.Create	R	admin
3a20eee2-7d2b-fb1e-648c-0186eca29906	\N	AIManagement.WorkspaceDataSources.Delete	R	admin
3a20eee2-7d2b-fec5-7f03-e4fbe455cb6a	\N	AIManagement.Workspaces.ManagePermissions	R	admin
3a20ef15-9161-0c70-e351-736b18602c6d	\N	MasterDataService.Wards.Create	R	admin
3a20ef15-9161-104f-03b9-a2f4e1be43a5	\N	MasterDataService.Wards	R	admin
3a20ef15-9161-7843-cb4a-f3952049cb5d	\N	MasterDataService.Wards.Delete	R	admin
3a20ef15-9161-fcf7-3959-c5ed4e176379	\N	MasterDataService.Wards.Edit	R	admin
3a20f045-8c8b-ac8d-76d3-94bb437db8d7	\N	MasterDataService.ArticleCategories.Edit	R	admin
3a20f045-8c8b-cfa6-f269-560d7933f259	\N	MasterDataService.ArticleCategories.Create	R	admin
3a20f045-8c8b-e9c8-8e83-79324a64b6c1	\N	MasterDataService.ArticleCategories	R	admin
3a20f045-8c8c-23ff-2d01-f5f6f6e4aad4	\N	MasterDataService.ArticleTags.Delete	R	admin
3a20f045-8c8c-2492-3de4-82f3b292a162	\N	MasterDataService.ArticleTags.Edit	R	admin
3a20f045-8c8c-3c62-1b35-e806fe7cf3dd	\N	MasterDataService.ArticleTags.Create	R	admin
3a20f045-8c8c-47c8-fe15-19e982163787	\N	MasterDataService.ArticleTags	R	admin
3a20f045-8c8c-4eaf-9bbf-a026e52a14c0	\N	MasterDataService.Articles	R	admin
3a20f045-8c8c-714f-4b7d-1d22870a1ed3	\N	MasterDataService.ArticleCategories.Delete	R	admin
3a20f045-8c8d-0ef8-3e90-3aceb2d7c898	\N	MasterDataService.ArticleViews.Create	R	admin
3a20f045-8c8d-47e7-028c-81c0da9065eb	\N	MasterDataService.ArticleViews	R	admin
3a20f045-8c8d-608e-c392-34cc3256844e	\N	MasterDataService.Articles.Create	R	admin
3a20f045-8c8d-8614-643f-d8a6ca0f48ed	\N	MasterDataService.ArticleTagMappings.Delete	R	admin
3a20f045-8c8d-9936-87bf-38682958d8a3	\N	MasterDataService.ArticleTagMappings.Create	R	admin
3a20f045-8c8d-9a4e-985b-d757ddb4f230	\N	MasterDataService.Articles.Delete	R	admin
3a20f045-8c8d-ae68-28d2-1e33ba9c3f7b	\N	MasterDataService.ArticleViews.Edit	R	admin
3a20f045-8c8d-bd4b-d1b2-da4b87db897e	\N	MasterDataService.ArticleTagMappings	R	admin
3a20f045-8c8d-cd76-d173-06b3d43a1b03	\N	MasterDataService.Articles.Edit	R	admin
3a20f045-8c8d-e3cf-161c-f7641f8c3e9d	\N	MasterDataService.ArticleViews.Delete	R	admin
3a20f045-8c8d-ea94-6081-2e0fe0f3e5cb	\N	MasterDataService.ArticleTagMappings.Edit	R	admin
3a20f591-6547-49e7-9f38-898782534410	\N	MasterDataService.MediaFiles	R	admin
3a20f591-6548-0dd8-d2b0-98dd10f6b128	\N	MasterDataService.PlaceTagMappings	R	admin
3a20f591-6548-0f07-1dcb-4eb1db53d9e3	\N	MasterDataService.EntityFiles	R	admin
3a20f591-6548-1108-5f12-3999d287afe6	\N	MasterDataService.EntityFiles.Create	R	admin
3a20f591-6548-2063-850e-0cec43bff77a	\N	MasterDataService.PlaceCategories	R	admin
3a20f591-6548-234d-a7c0-db4a4ab41415	\N	MasterDataService.PlaceTags	R	admin
3a20f591-6548-2cd7-6db0-eee21fc0da98	\N	MasterDataService.PlaceReviews.Create	R	admin
3a20f591-6548-312c-0587-cdfcedbc7301	\N	MasterDataService.PlaceViews.Create	R	admin
3a20f591-6548-31fe-490b-41cffb009491	\N	MasterDataService.PlaceViews	R	admin
3a20f591-6548-459c-93b2-4517644051ce	\N	MasterDataService.PlaceTags.Create	R	admin
3a20f591-6548-493d-1600-7b3cbb305fdf	\N	MasterDataService.PlaceFavorites.Delete	R	admin
3a20f591-6548-4e0c-908a-4826c3e81689	\N	MasterDataService.EntityFiles.Edit	R	admin
3a20f591-6548-5b94-312f-13056a3729a6	\N	MasterDataService.MediaFiles.Delete	R	admin
3a20f591-6548-61c4-2793-7d515697c828	\N	MasterDataService.Places.Create	R	admin
3a20f591-6548-65e2-6ef0-faa02de1806d	\N	MasterDataService.PlaceCategories.Edit	R	admin
3a20f591-6548-6e9a-9ce9-1187824259b3	\N	MasterDataService.PlaceViews.Edit	R	admin
3a20f591-6548-747b-fc3c-b204b407974f	\N	MasterDataService.MediaFiles.Edit	R	admin
3a20f591-6548-7745-ca35-d126ad4707d1	\N	MasterDataService.Places.Edit	R	admin
3a20f591-6548-84d8-8231-8dfdc550b9df	\N	MasterDataService.PlaceTagMappings.Delete	R	admin
3a20f591-6548-86de-c2ca-b71b6567f7c3	\N	MasterDataService.PlaceViews.Delete	R	admin
3a20f591-6548-89f2-7f89-8310b0e6486e	\N	MasterDataService.PlaceReviews.Delete	R	admin
3a20f591-6548-9ad2-9254-96892fc6f657	\N	MasterDataService.EntityFiles.Delete	R	admin
3a20f591-6548-9b8e-8c22-c44835ff1df8	\N	MasterDataService.Places.Delete	R	admin
3a20f591-6548-9cc6-eed8-acf20477104f	\N	MasterDataService.PlaceFavorites.Create	R	admin
3a20f591-6548-a4a3-fbb0-a970a6d934a3	\N	MasterDataService.PlaceFavorites	R	admin
3a20f591-6548-b21c-8bff-9c460dc4d0a1	\N	MasterDataService.PlaceTags.Edit	R	admin
3a20f591-6548-b47e-6dcf-63e871c8ecf8	\N	MasterDataService.PlaceReviews.Edit	R	admin
3a20f591-6548-ba75-3d31-6a20ff10f030	\N	MasterDataService.PlaceTagMappings.Edit	R	admin
3a20f591-6548-bcf6-e141-86ae530ec455	\N	MasterDataService.MediaFiles.Create	R	admin
3a20f591-6548-c54a-dcbc-a2c56f8811cf	\N	MasterDataService.PlaceFavorites.Edit	R	admin
3a20f591-6548-c886-759a-1e1e11639b72	\N	MasterDataService.PlaceCategories.Create	R	admin
3a20f591-6548-cd47-78d8-08bca12157f7	\N	MasterDataService.PlaceReviews	R	admin
3a20f591-6548-cf43-ee30-0a36ea947b85	\N	MasterDataService.PlaceTagMappings.Create	R	admin
3a20f591-6548-cfce-4a20-3f480db39ff1	\N	MasterDataService.Places	R	admin
3a20f591-6548-d6a1-0662-60795e9f58c0	\N	MasterDataService.PlaceCategories.Delete	R	admin
3a20f591-6548-e483-247b-64ec0decf130	\N	MasterDataService.PlaceTags.Delete	R	admin
\.


--
-- Data for Name: AbpPermissionGroups; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpPermissionGroups" ("Id", "Name", "DisplayName", "ExtraProperties") FROM stdin;
3a20e9c3-4f4c-e52c-64b0-d85c31f54d86	AuditLogging	L:AbpAuditLogging,Permission:AuditLogging	{"_CurrentProviderName":"Volo.Abp.AuditLogging.AbpAuditLoggingPermissionDefinitionProvider"}
3a20e9c3-5465-0fe4-6cf8-bfc9d7144871	LanguageManagement	L:LanguageManagement,Permission:LanguageManagement	{"_CurrentProviderName":"Volo.Abp.LanguageManagement.LanguageManagementPermissionDefinitionProvider"}
3a20e9c3-5833-8b1b-ca37-810e8e7cddba	AIManagement	L:AIManagement,Permission:AIManagement	{"_CurrentProviderName":"Volo.AIManagement.Permissions.AIManagementPermissionDefinitionProvider"}
3a20e9c3-631e-eb28-5e64-3ae37f62fbed	TextTemplateManagement	L:TextTemplateManagement,Permission:TextTemplateManagement	{"_CurrentProviderName":"Volo.Abp.TextTemplateManagement.Authorization.TextTemplateManagementPermissionDefinitionProvider"}
3a20e9c3-6326-e76e-186f-88f888a3547e	FeatureManagement	L:AbpFeatureManagement,Permission:FeatureManagement	{"_CurrentProviderName":"Volo.Abp.FeatureManagement.FeaturePermissionDefinitionProvider"}
3a20e9c3-6326-fcca-ba92-4dfd2944de9f	AdministrationService	L:AdministrationService,AdministrationService	{"_CurrentProviderName":"KHHub.AdministrationService.Permissions.AdministrationServicePermissionDefinitionProvider"}
3a20e9c3-6327-745c-01de-3b6356eafdd5	SettingManagement	L:AbpSettingManagement,Permission:SettingManagement	{"_CurrentProviderName":"Volo.Abp.SettingManagement.SettingManagementPermissionDefinitionProvider"}
3a20e9c3-632b-bbce-8611-406dbdda045d	OpenIddictPro	L:AbpOpenIddict,Permission:OpenIddictPro	{"_CurrentProviderName":"Volo.Abp.OpenIddict.Permissions.AbpOpenIddictProPermissionDefinitionProvider"}
3a20e9c3-632b-c4c7-096b-9f9cd1d96960	AbpIdentity	L:AbpIdentity,Permission:IdentityManagement	{"_CurrentProviderName":"Volo.Abp.Identity.IdentityPermissionDefinitionProvider"}
3a20e9c3-993d-fe39-c538-6782a555aff4	AbpAccount	L:AbpAccount,Permission:Account	{"_CurrentProviderName":"Volo.Abp.Account.AccountPermissionDefinitionProvider"}
3a20eed5-35c7-a66b-c525-de4b9b8b56e8	MasterDataService	F:MasterDataService	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
\.


--
-- Data for Name: AbpPermissions; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpPermissions" ("Id", "GroupName", "Name", "ResourceName", "ManagementPermissionName", "ParentName", "DisplayName", "IsEnabled", "MultiTenancySide", "Providers", "StateCheckers", "ExtraProperties") FROM stdin;
3a20e9c3-4f54-9bd5-1edd-a443493a85bd	AuditLogging	AuditLogging.AuditLogs	\N	\N	\N	L:AbpAuditLogging,Permission:AuditLogs	t	3	\N	[{"T":"F","A":true,"N":["AuditLogging.Enable"]}]	{"_CurrentProviderName":"Volo.Abp.AuditLogging.AbpAuditLoggingPermissionDefinitionProvider"}
3a20e9c3-4f59-738f-1253-df08b2d17e03	AuditLogging	AuditLogging.AuditLogs.SettingManagement	\N	\N	AuditLogging.AuditLogs	L:AbpAuditLogging,Permission:SettingManagement	t	3	\N	[{"T":"F","A":true,"N":["AuditLogging.SettingManagement"]}]	{"_CurrentProviderName":"Volo.Abp.AuditLogging.AbpAuditLoggingPermissionDefinitionProvider"}
3a20e9c3-4f59-b9cc-c0c5-e136ee4ab859	AuditLogging	AuditLogging.AuditLogs.Export	\N	\N	AuditLogging.AuditLogs	L:AbpAuditLogging,Permission:Export	t	3	\N	\N	{"_CurrentProviderName":"Volo.Abp.AuditLogging.AbpAuditLoggingPermissionDefinitionProvider"}
3a20e9c3-5469-2ca4-1184-7a546025fdd1	LanguageManagement	LanguageManagement.LanguageTexts	\N	\N	\N	L:LanguageManagement,Permission:LanguageTexts	t	3	\N	[{"T":"F","A":true,"N":["LanguageManagement.Enable"]}]	{"_CurrentProviderName":"Volo.Abp.LanguageManagement.LanguageManagementPermissionDefinitionProvider"}
3a20e9c3-546c-11ef-914e-e14620bc7837	LanguageManagement	LanguageManagement.Languages.ChangeDefault	\N	\N	LanguageManagement.Languages	L:LanguageManagement,Permission:LanguagesChangeDefault	t	3	\N	[{"T":"F","A":true,"N":["LanguageManagement.Enable"]}]	{"_CurrentProviderName":"Volo.Abp.LanguageManagement.LanguageManagementPermissionDefinitionProvider"}
3a20e9c3-546c-2210-6a32-c81550964e33	LanguageManagement	LanguageManagement.LanguageTexts.Edit	\N	\N	LanguageManagement.LanguageTexts	L:LanguageManagement,Permission:LanguageTextsEdit	t	3	\N	[{"T":"F","A":true,"N":["LanguageManagement.Enable"]}]	{"_CurrentProviderName":"Volo.Abp.LanguageManagement.LanguageManagementPermissionDefinitionProvider"}
3a20e9c3-546c-9055-bc17-8a6d6f3644c0	LanguageManagement	LanguageManagement.Languages.Edit	\N	\N	LanguageManagement.Languages	L:LanguageManagement,Permission:LanguagesEdit	t	2	\N	[{"T":"F","A":true,"N":["LanguageManagement.Enable"]}]	{"_CurrentProviderName":"Volo.Abp.LanguageManagement.LanguageManagementPermissionDefinitionProvider"}
3a20e9c3-546c-b87d-1215-78eec321c532	LanguageManagement	LanguageManagement.Languages.Create	\N	\N	LanguageManagement.Languages	L:LanguageManagement,Permission:LanguagesCreate	t	2	\N	[{"T":"F","A":true,"N":["LanguageManagement.Enable"]}]	{"_CurrentProviderName":"Volo.Abp.LanguageManagement.LanguageManagementPermissionDefinitionProvider"}
3a20e9c3-546c-c4ac-f6bf-efa2be218c73	LanguageManagement	LanguageManagement.Languages	\N	\N	\N	L:LanguageManagement,Permission:Languages	t	3	\N	[{"T":"F","A":true,"N":["LanguageManagement.Enable"]}]	{"_CurrentProviderName":"Volo.Abp.LanguageManagement.LanguageManagementPermissionDefinitionProvider"}
3a20e9c3-546c-fb6a-7e15-64afab99cd41	LanguageManagement	LanguageManagement.Languages.Delete	\N	\N	LanguageManagement.Languages	L:LanguageManagement,Permission:LanguagesDelete	t	2	\N	[{"T":"F","A":true,"N":["LanguageManagement.Enable"]}]	{"_CurrentProviderName":"Volo.Abp.LanguageManagement.LanguageManagementPermissionDefinitionProvider"}
3a20e9c3-5836-b053-d630-58ecd52ac66b	AIManagement	AIManagement.Workspaces	\N	\N	\N	L:AIManagement,Permission:Workspaces	t	3	\N	\N	{"_CurrentProviderName":"Volo.AIManagement.Permissions.AIManagementPermissionDefinitionProvider"}
3a20e9c3-5838-0455-0668-7b7ea463eb38	AIManagement	AIManagement.McpServers.Create	\N	\N	AIManagement.McpServers	L:AIManagement,Permission:McpServers:Create	t	3	\N	\N	{"_CurrentProviderName":"Volo.AIManagement.Permissions.AIManagementPermissionDefinitionProvider"}
3a20e9c3-5838-0c14-be62-9429ffc25bb6	AIManagement	AIManagement.McpServers.Delete	\N	\N	AIManagement.McpServers	L:AIManagement,Permission:McpServers:Delete	t	3	\N	\N	{"_CurrentProviderName":"Volo.AIManagement.Permissions.AIManagementPermissionDefinitionProvider"}
3a20e9c3-5838-1412-aac1-5b409d63c994	AIManagement	AIManagement.McpServers.Update	\N	\N	AIManagement.McpServers	L:AIManagement,Permission:McpServers:Update	t	3	\N	\N	{"_CurrentProviderName":"Volo.AIManagement.Permissions.AIManagementPermissionDefinitionProvider"}
3a20e9c3-5838-2543-a0af-cd4050e377c3	AIManagement	AIManagement.McpServers	\N	\N	\N	L:AIManagement,Permission:McpServers	t	3	\N	\N	{"_CurrentProviderName":"Volo.AIManagement.Permissions.AIManagementPermissionDefinitionProvider"}
3a20e9c3-5838-3a9a-bec5-49e898cb0fa9	AIManagement	AIManagement.WorkspaceDataSources	\N	\N	\N	L:AIManagement,Permission:WorkspaceDataSources	t	3	\N	\N	{"_CurrentProviderName":"Volo.AIManagement.Permissions.AIManagementPermissionDefinitionProvider"}
3a20e9c3-5838-5203-3a9c-4f8c1ce2c5eb	AIManagement	AIManagement.Workspaces.Create	\N	\N	AIManagement.Workspaces	L:AIManagement,Permission:Workspaces:Create	t	3	\N	\N	{"_CurrentProviderName":"Volo.AIManagement.Permissions.AIManagementPermissionDefinitionProvider"}
3a20e9c3-5838-5846-3b19-48ff904efe86	AIManagement	AIManagement.WorkspaceDataSources.Create	\N	\N	AIManagement.WorkspaceDataSources	L:AIManagement,Permission:WorkspaceDataSources:Create	t	3	\N	\N	{"_CurrentProviderName":"Volo.AIManagement.Permissions.AIManagementPermissionDefinitionProvider"}
3a20e9c3-5838-77d1-2656-da9500b1aac0	AIManagement	AIManagement.WorkspaceDataSources.Download	\N	\N	AIManagement.WorkspaceDataSources	L:AIManagement,Permission:WorkspaceDataSources:Download	t	3	\N	\N	{"_CurrentProviderName":"Volo.AIManagement.Permissions.AIManagementPermissionDefinitionProvider"}
3a20e9c3-5838-a1c3-da4b-34d9b3e835e6	AIManagement	AIManagement.Workspaces.Delete	\N	\N	AIManagement.Workspaces	L:AIManagement,Permission:Workspaces:Delete	t	3	\N	\N	{"_CurrentProviderName":"Volo.AIManagement.Permissions.AIManagementPermissionDefinitionProvider"}
3a20e9c3-5838-a251-499c-1601476d8b15	AIManagement	AIManagement.WorkspaceDataSources.ReIndex	\N	\N	AIManagement.WorkspaceDataSources	L:AIManagement,Permission:WorkspaceDataSources:ReIndex	t	3	\N	\N	{"_CurrentProviderName":"Volo.AIManagement.Permissions.AIManagementPermissionDefinitionProvider"}
3a20e9c3-5838-b7f6-7404-6c3e417cd796	AIManagement	AIManagement.Workspaces.Playground	\N	\N	AIManagement.Workspaces	L:AIManagement,Permission:Workspaces:Playground	t	3	\N	\N	{"_CurrentProviderName":"Volo.AIManagement.Permissions.AIManagementPermissionDefinitionProvider"}
3a20e9c3-5838-d57b-1c06-5a2f11b16067	AIManagement	AIManagement.Workspaces.ManagePermissions	\N	\N	AIManagement.Workspaces	L:AIManagement,Permission:Workspaces:ManagePermissions	t	3	\N	\N	{"_CurrentProviderName":"Volo.AIManagement.Permissions.AIManagementPermissionDefinitionProvider"}
3a20e9c3-5838-da0f-f662-86df2fb2c40f	AIManagement	AIManagement.Workspaces.Update	\N	\N	AIManagement.Workspaces	L:AIManagement,Permission:Workspaces:Update	t	3	\N	\N	{"_CurrentProviderName":"Volo.AIManagement.Permissions.AIManagementPermissionDefinitionProvider"}
3a20e9c3-5838-e4e4-e5f7-7c4830b979be	AIManagement	AIManagement.WorkspaceDataSources.Update	\N	\N	AIManagement.WorkspaceDataSources	L:AIManagement,Permission:WorkspaceDataSources:Update	t	3	\N	\N	{"_CurrentProviderName":"Volo.AIManagement.Permissions.AIManagementPermissionDefinitionProvider"}
3a20e9c3-5838-fe38-76cc-621f7e16b280	AIManagement	AIManagement.WorkspaceDataSources.Delete	\N	\N	AIManagement.WorkspaceDataSources	L:AIManagement,Permission:WorkspaceDataSources:Delete	t	3	\N	\N	{"_CurrentProviderName":"Volo.AIManagement.Permissions.AIManagementPermissionDefinitionProvider"}
3a20e9c3-5839-0600-0228-4baba4d5ebca	\N	Volo.AIManagement.Workspaces.Workspace.Consume	Volo.AIManagement.Workspaces.Workspace	AIManagement.Workspaces.ManagePermissions	\N	L:AIManagement,Permission:Workspaces.Consume	t	3	\N	\N	{"_CurrentProviderName":"Volo.AIManagement.Permissions.AIManagementPermissionDefinitionProvider"}
3a20e9c3-6320-3d3e-9d86-c7e9e1acf089	TextTemplateManagement	TextTemplateManagement.TextTemplates	\N	\N	\N	L:TextTemplateManagement,Permission:TextTemplates	t	3	\N	[{"T":"F","A":true,"N":["TextManagement.Enable"]}]	{"_CurrentProviderName":"Volo.Abp.TextTemplateManagement.Authorization.TextTemplateManagementPermissionDefinitionProvider"}
3a20e9c3-6326-9d24-af41-db22a1b9e395	TextTemplateManagement	TextTemplateManagement.TextTemplates.EditContents	\N	\N	TextTemplateManagement.TextTemplates	L:TextTemplateManagement,Permission:EditContents	t	3	\N	[{"T":"F","A":true,"N":["TextManagement.Enable"]}]	{"_CurrentProviderName":"Volo.Abp.TextTemplateManagement.Authorization.TextTemplateManagementPermissionDefinitionProvider"}
3a20e9c3-6326-b4aa-3c93-3fcd4e35d925	AdministrationService	AdministrationService.Dashboard.Host	\N	\N	\N	L:AdministrationService,Permission:Dashboard	t	3	\N	\N	{"_CurrentProviderName":"KHHub.AdministrationService.Permissions.AdministrationServicePermissionDefinitionProvider"}
3a20e9c3-6327-4e81-dd45-5365644ee425	SettingManagement	SettingManagement.Emailing	\N	\N	\N	L:AbpSettingManagement,Permission:Emailing	t	3	\N	\N	{"_CurrentProviderName":"Volo.Abp.SettingManagement.SettingManagementPermissionDefinitionProvider"}
3a20e9c3-6327-86bb-3240-887f377eebdd	FeatureManagement	FeatureManagement.ManageHostFeatures	\N	\N	\N	L:AbpFeatureManagement,Permission:FeatureManagement.ManageHostFeatures	t	2	\N	\N	{"_CurrentProviderName":"Volo.Abp.FeatureManagement.FeaturePermissionDefinitionProvider"}
3a20e9c3-632b-0062-be04-45a1536ab996	AbpIdentity	AbpIdentity.ClaimTypes.Update	\N	\N	AbpIdentity.ClaimTypes	L:AbpIdentity,Permission:Edit	t	2	\N	\N	{"_CurrentProviderName":"Volo.Abp.Identity.IdentityPermissionDefinitionProvider"}
3a20e9c3-632b-0746-84e2-71043dfd728c	AbpIdentity	AbpIdentity.SecurityLogs	\N	\N	\N	L:AbpIdentity,Permission:SecurityLogs	t	3	\N	\N	{"_CurrentProviderName":"Volo.Abp.Identity.IdentityPermissionDefinitionProvider"}
3a20e9c3-632b-0832-89b0-34fab946c915	AbpIdentity	AbpIdentity.Users.Update.ManageRoles	\N	\N	AbpIdentity.Users.Update	L:AbpIdentity,Permission:ManageRoles	t	3	\N	\N	{"_CurrentProviderName":"Volo.Abp.Identity.IdentityPermissionDefinitionProvider"}
3a20e9c3-632b-0a3f-e89c-e7fc4d9815cb	OpenIddictPro	OpenIddictPro.Application.ManagePermissions	\N	\N	OpenIddictPro.Application	L:AbpOpenIddict,Permission:ManagePermissions	t	2	\N	\N	{"_CurrentProviderName":"Volo.Abp.OpenIddict.Permissions.AbpOpenIddictProPermissionDefinitionProvider"}
3a20e9c3-632b-0b42-6c52-f0a46b137b01	AbpIdentity	AbpIdentity.OrganizationUnits.ManageOU	\N	\N	AbpIdentity.OrganizationUnits	L:AbpIdentity,Permission:ManageOU	t	3	\N	\N	{"_CurrentProviderName":"Volo.Abp.Identity.IdentityPermissionDefinitionProvider"}
3a20e9c3-632b-0e6e-bf37-8316a2e68f9a	SettingManagement	SettingManagement.Emailing.Test	\N	\N	SettingManagement.Emailing	L:AbpSettingManagement,Permission:EmailingTest	t	3	\N	\N	{"_CurrentProviderName":"Volo.Abp.SettingManagement.SettingManagementPermissionDefinitionProvider"}
3a20e9c3-632b-11dc-3785-6af738a41435	AbpIdentity	AbpIdentity.ClaimTypes	\N	\N	\N	L:AbpIdentity,Permission:ClaimManagement	t	2	\N	\N	{"_CurrentProviderName":"Volo.Abp.Identity.IdentityPermissionDefinitionProvider"}
3a20e9c3-632b-1f33-b5a7-09961ed0f22f	OpenIddictPro	OpenIddictPro.Application	\N	\N	\N	L:AbpOpenIddict,Permission:Application	t	2	\N	\N	{"_CurrentProviderName":"Volo.Abp.OpenIddict.Permissions.AbpOpenIddictProPermissionDefinitionProvider"}
3a20e9c3-632b-2032-c977-a445a68bb5a8	AbpIdentity	AbpIdentity.Roles.Create	\N	\N	AbpIdentity.Roles	L:AbpIdentity,Permission:Create	t	3	\N	\N	{"_CurrentProviderName":"Volo.Abp.Identity.IdentityPermissionDefinitionProvider"}
3a20e9c3-632b-218a-7559-cc84f18ff57e	AbpIdentity	AbpIdentity.SettingManagement	\N	\N	\N	L:AbpIdentity,Permission:SettingManagement	t	3	\N	\N	{"_CurrentProviderName":"Volo.Abp.Identity.IdentityPermissionDefinitionProvider"}
3a20e9c3-632b-2212-2b83-d86958fcd8c1	OpenIddictPro	OpenIddictPro.Scope	\N	\N	\N	L:AbpOpenIddict,Permission:Scope	t	2	\N	\N	{"_CurrentProviderName":"Volo.Abp.OpenIddict.Permissions.AbpOpenIddictProPermissionDefinitionProvider"}
3a20e9c3-632b-3bc5-b630-ddd6870d66c9	OpenIddictPro	OpenIddictPro.Scope.Delete	\N	\N	OpenIddictPro.Scope	L:AbpOpenIddict,Permission:Delete	t	2	\N	\N	{"_CurrentProviderName":"Volo.Abp.OpenIddict.Permissions.AbpOpenIddictProPermissionDefinitionProvider"}
3a20e9c3-632b-4e9d-082e-74a40abde397	AbpIdentity	AbpIdentity.Roles	\N	\N	\N	L:AbpIdentity,Permission:RoleManagement	t	3	\N	\N	{"_CurrentProviderName":"Volo.Abp.Identity.IdentityPermissionDefinitionProvider"}
3a20e9c3-632b-530b-4e06-0db4ecc16ef1	OpenIddictPro	OpenIddictPro.Scope.Create	\N	\N	OpenIddictPro.Scope	L:AbpOpenIddict,Permission:Create	t	2	\N	\N	{"_CurrentProviderName":"Volo.Abp.OpenIddict.Permissions.AbpOpenIddictProPermissionDefinitionProvider"}
3a20e9c3-632b-53ff-dc54-1078ce0d76da	OpenIddictPro	AuditLogging.ViewChangeHistory:Volo.Abp.OpenIddict.Pro.Scopes.Scope	\N	\N	OpenIddictPro.Scope	L:AbpOpenIddict,Permission:ViewChangeHistory	t	2	\N	\N	{"_CurrentProviderName":"Volo.Abp.OpenIddict.Permissions.AbpOpenIddictProPermissionDefinitionProvider"}
3a20e9c3-632b-5f66-bc20-d2d76206fd84	AbpIdentity	AbpIdentity.Users.Import	\N	\N	AbpIdentity.Users	L:AbpIdentity,Permission:Import	t	3	\N	\N	{"_CurrentProviderName":"Volo.Abp.Identity.IdentityPermissionDefinitionProvider"}
3a20e9c3-632b-601b-846b-2baedefd4169	AbpIdentity	AbpIdentity.OrganizationUnits	\N	\N	\N	L:AbpIdentity,Permission:OrganizationUnitManagement	t	3	\N	\N	{"_CurrentProviderName":"Volo.Abp.Identity.IdentityPermissionDefinitionProvider"}
3a20e9c3-632b-60f9-1c25-0348600c8a68	AbpIdentity	AbpIdentity.Roles.ManagePermissions	\N	\N	AbpIdentity.Roles	L:AbpIdentity,Permission:ChangePermissions	t	3	\N	\N	{"_CurrentProviderName":"Volo.Abp.Identity.IdentityPermissionDefinitionProvider"}
3a20e9c3-632b-666f-8af3-11b68f2fd9e0	AbpIdentity	AbpIdentity.OrganizationUnits.ManageRoles	\N	\N	AbpIdentity.OrganizationUnits	L:AbpIdentity,Permission:ManageRoles	t	3	\N	\N	{"_CurrentProviderName":"Volo.Abp.Identity.IdentityPermissionDefinitionProvider"}
3a20e9c3-632b-6ad4-2b76-f5c683737f7e	AbpIdentity	AbpIdentity.ClaimTypes.Create	\N	\N	AbpIdentity.ClaimTypes	L:AbpIdentity,Permission:Create	t	2	\N	\N	{"_CurrentProviderName":"Volo.Abp.Identity.IdentityPermissionDefinitionProvider"}
3a20e9c3-632b-732f-a1c8-8b9e3445c6cd	AbpIdentity	AbpIdentity.Sessions	\N	\N	\N	L:AbpIdentity,Permission:Sessions	t	3	\N	\N	{"_CurrentProviderName":"Volo.Abp.Identity.IdentityPermissionDefinitionProvider"}
3a20e9c3-632b-7343-a585-da1e7f518b0a	AbpIdentity	AbpIdentity.OrganizationUnits.ManageMembers	\N	\N	AbpIdentity.OrganizationUnits	L:AbpIdentity,Permission:ManageUsers	t	3	\N	\N	{"_CurrentProviderName":"Volo.Abp.Identity.IdentityPermissionDefinitionProvider"}
3a20e9c3-632b-79c3-33d8-1893decffa06	AbpIdentity	AbpIdentity.Users.ViewDetails	\N	\N	AbpIdentity.Users	L:AbpIdentity,Permission:ViewDetails	t	3	\N	\N	{"_CurrentProviderName":"Volo.Abp.Identity.IdentityPermissionDefinitionProvider"}
3a20e9c3-632b-8d04-1fd6-2729fc538e32	AbpIdentity	AbpIdentity.Roles.Delete	\N	\N	AbpIdentity.Roles	L:AbpIdentity,Permission:Delete	t	3	\N	\N	{"_CurrentProviderName":"Volo.Abp.Identity.IdentityPermissionDefinitionProvider"}
3a20e9c3-632b-8efc-3866-8a002095128a	OpenIddictPro	OpenIddictPro.Application.Create	\N	\N	OpenIddictPro.Application	L:AbpOpenIddict,Permission:Create	t	2	\N	\N	{"_CurrentProviderName":"Volo.Abp.OpenIddict.Permissions.AbpOpenIddictProPermissionDefinitionProvider"}
3a20e9c3-632b-95fd-2a75-fc6354f99e65	SettingManagement	SettingManagement.TimeZone	\N	\N	\N	L:AbpSettingManagement,Permission:TimeZone	t	3	\N	\N	{"_CurrentProviderName":"Volo.Abp.SettingManagement.SettingManagementPermissionDefinitionProvider"}
3a20e9c3-632b-9c67-742c-ed2751d41fcb	AbpIdentity	AbpIdentity.ClaimTypes.Delete	\N	\N	AbpIdentity.ClaimTypes	L:AbpIdentity,Permission:Delete	t	2	\N	\N	{"_CurrentProviderName":"Volo.Abp.Identity.IdentityPermissionDefinitionProvider"}
3a20e9c3-632b-a273-9c9c-efc7bfe358ce	OpenIddictPro	OpenIddictPro.Application.Delete	\N	\N	OpenIddictPro.Application	L:AbpOpenIddict,Permission:Delete	t	2	\N	\N	{"_CurrentProviderName":"Volo.Abp.OpenIddict.Permissions.AbpOpenIddictProPermissionDefinitionProvider"}
3a20e9c3-632b-a3fa-a5d7-fc2396c17657	AbpIdentity	AbpIdentity.Users.Create	\N	\N	AbpIdentity.Users	L:AbpIdentity,Permission:Create	t	3	\N	\N	{"_CurrentProviderName":"Volo.Abp.Identity.IdentityPermissionDefinitionProvider"}
3a20e9c3-632b-c405-9f1b-0eff71da34b1	AbpIdentity	AbpIdentity.Roles.Update	\N	\N	AbpIdentity.Roles	L:AbpIdentity,Permission:Edit	t	3	\N	\N	{"_CurrentProviderName":"Volo.Abp.Identity.IdentityPermissionDefinitionProvider"}
3a20e9c3-632b-c547-402b-4929e0b8937b	AbpIdentity	AbpIdentity.Users.Impersonation	\N	\N	AbpIdentity.Users	L:AbpIdentity,Permission:Impersonation	t	3	\N	\N	{"_CurrentProviderName":"Volo.Abp.Identity.IdentityPermissionDefinitionProvider"}
3a20e9c3-632b-cb64-9a61-8c23b8576dc6	OpenIddictPro	AuditLogging.ViewChangeHistory:Volo.Abp.OpenIddict.Pro.Applications.Application	\N	\N	OpenIddictPro.Application	L:AbpOpenIddict,Permission:ViewChangeHistory	t	2	\N	\N	{"_CurrentProviderName":"Volo.Abp.OpenIddict.Permissions.AbpOpenIddictProPermissionDefinitionProvider"}
3a20e9c3-632b-de81-d7ab-cf3ad0045584	AbpIdentity	AbpIdentity.Users.Update	\N	\N	AbpIdentity.Users	L:AbpIdentity,Permission:Edit	t	3	\N	\N	{"_CurrentProviderName":"Volo.Abp.Identity.IdentityPermissionDefinitionProvider"}
3a20e9c3-632b-e48f-c52b-b3e808289a36	AbpIdentity	AbpIdentity.Users.Export	\N	\N	AbpIdentity.Users	L:AbpIdentity,Permission:Export	t	3	\N	\N	{"_CurrentProviderName":"Volo.Abp.Identity.IdentityPermissionDefinitionProvider"}
3a20e9c3-632b-e5a3-a60a-c3bfe67e515d	AbpIdentity	AbpIdentity.UserLookup	\N	\N	\N	L:AbpIdentity,Permission:UserLookup	t	3	C	\N	{"_CurrentProviderName":"Volo.Abp.Identity.IdentityPermissionDefinitionProvider"}
3a20e9c3-632b-e5f6-492e-15ededf2f498	AbpIdentity	AbpIdentity.Users	\N	\N	\N	L:AbpIdentity,Permission:UserManagement	t	3	\N	\N	{"_CurrentProviderName":"Volo.Abp.Identity.IdentityPermissionDefinitionProvider"}
3a20e9c3-632b-ea3c-5390-7f8a22618384	AbpIdentity	AbpIdentity.Users.Update.ManageOU	\N	\N	AbpIdentity.Users.Update	L:AbpIdentity,Permission:ManageOU	t	3	\N	\N	{"_CurrentProviderName":"Volo.Abp.Identity.IdentityPermissionDefinitionProvider"}
3a20e9c3-632b-f33e-f1d6-71f54c075710	AbpIdentity	AbpIdentity.Users.ManagePermissions	\N	\N	AbpIdentity.Users	L:AbpIdentity,Permission:ChangePermissions	t	3	\N	\N	{"_CurrentProviderName":"Volo.Abp.Identity.IdentityPermissionDefinitionProvider"}
3a20e9c3-632b-f41a-a8e2-b6947dfbd695	OpenIddictPro	OpenIddictPro.Application.Update	\N	\N	OpenIddictPro.Application	L:AbpOpenIddict,Permission:Edit	t	2	\N	\N	{"_CurrentProviderName":"Volo.Abp.OpenIddict.Permissions.AbpOpenIddictProPermissionDefinitionProvider"}
3a20e9c3-632b-fa6f-bb15-b35128c658a4	AbpIdentity	AbpIdentity.Users.Delete	\N	\N	AbpIdentity.Users	L:AbpIdentity,Permission:Delete	t	3	\N	\N	{"_CurrentProviderName":"Volo.Abp.Identity.IdentityPermissionDefinitionProvider"}
3a20e9c3-632b-fde1-bdcf-dab023b72215	OpenIddictPro	OpenIddictPro.Scope.Update	\N	\N	OpenIddictPro.Scope	L:AbpOpenIddict,Permission:Edit	t	2	\N	\N	{"_CurrentProviderName":"Volo.Abp.OpenIddict.Permissions.AbpOpenIddictProPermissionDefinitionProvider"}
3a20e9c3-993d-d3a4-38ad-4354cd8d4022	AbpAccount	AbpAccount.SettingManagement	\N	\N	\N	L:AbpAccount,Permission:SettingManagement	t	3	\N	\N	{"_CurrentProviderName":"Volo.Abp.Account.AccountPermissionDefinitionProvider"}
3a20eed5-35c8-0eb1-2fb6-179e3f053f70	MasterDataService	MasterDataService.Provinces	\N	\N	\N	L:MasterDataService,Permission:Provinces	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20eed5-35c9-1f7e-d37d-792b69a0c601	MasterDataService	MasterDataService.Provinces.Create	\N	\N	MasterDataService.Provinces	L:MasterDataService,Permission:Create	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20eed5-35c9-e7c4-21a3-8a809e7d8ef3	MasterDataService	MasterDataService.Provinces.Delete	\N	\N	MasterDataService.Provinces	L:MasterDataService,Permission:Delete	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20eed5-35c9-ed1c-c116-ac195bd3ea89	MasterDataService	MasterDataService.Provinces.Edit	\N	\N	MasterDataService.Provinces	L:MasterDataService,Permission:Edit	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20e9c3-632b-06ed-7d55-0f4e2865fc13	AbpIdentity	AuditLogging.ViewChangeHistory:Volo.Abp.Identity.IdentityUser	\N	\N	AbpIdentity.Users	L:AbpIdentity,Permission:ViewChangeHistory	f	3	\N	\N	{"_CurrentProviderName":"Volo.Abp.Identity.IdentityPermissionDefinitionProvider"}
3a20e9c3-632b-8d92-0199-287d9ea2a2c7	AbpIdentity	AuditLogging.ViewChangeHistory:Volo.Abp.Identity.IdentityRole	\N	\N	AbpIdentity.Roles	L:AbpIdentity,Permission:ViewChangeHistory	f	3	\N	\N	{"_CurrentProviderName":"Volo.Abp.Identity.IdentityPermissionDefinitionProvider"}
3a20ef15-82ca-1f9b-ac3c-e7d22e4c2ca5	MasterDataService	MasterDataService.Wards	\N	\N	\N	L:MasterDataService,Permission:Wards	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20ef15-82ca-42f6-7039-0a0c91c48f56	MasterDataService	MasterDataService.Wards.Edit	\N	\N	MasterDataService.Wards	L:MasterDataService,Permission:Edit	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20ef15-82ca-90bf-d4c1-64742eb3873c	MasterDataService	MasterDataService.Wards.Delete	\N	\N	MasterDataService.Wards	L:MasterDataService,Permission:Delete	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20ef15-82ca-fcbc-28e5-266fcb4fbd74	MasterDataService	MasterDataService.Wards.Create	\N	\N	MasterDataService.Wards	L:MasterDataService,Permission:Create	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f045-8199-0777-8291-9ce4758a9426	MasterDataService	MasterDataService.ArticleViews.Delete	\N	\N	MasterDataService.ArticleViews	L:MasterDataService,Permission:Delete	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f045-8199-2fff-14bf-9df9032e925e	MasterDataService	MasterDataService.ArticleTags.Delete	\N	\N	MasterDataService.ArticleTags	L:MasterDataService,Permission:Delete	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f045-8199-3dd5-599b-f119a1678f4d	MasterDataService	MasterDataService.ArticleCategories.Delete	\N	\N	MasterDataService.ArticleCategories	L:MasterDataService,Permission:Delete	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f045-8199-417d-edc0-1ffbaa4c3809	MasterDataService	MasterDataService.ArticleTagMappings.Delete	\N	\N	MasterDataService.ArticleTagMappings	L:MasterDataService,Permission:Delete	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f045-8199-582a-af99-1cef55411716	MasterDataService	MasterDataService.ArticleViews	\N	\N	\N	L:MasterDataService,Permission:ArticleViews	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f045-8199-6052-70d6-37a06bb229dd	MasterDataService	MasterDataService.ArticleCategories.Create	\N	\N	MasterDataService.ArticleCategories	L:MasterDataService,Permission:Create	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f045-8199-6c65-18f9-ec2454d61da1	MasterDataService	MasterDataService.Articles.Create	\N	\N	MasterDataService.Articles	L:MasterDataService,Permission:Create	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f045-8199-73d8-da67-4b15eb6e88c5	MasterDataService	MasterDataService.Articles	\N	\N	\N	L:MasterDataService,Permission:Articles	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f045-8199-77aa-d865-e26b2f5645ac	MasterDataService	MasterDataService.Articles.Delete	\N	\N	MasterDataService.Articles	L:MasterDataService,Permission:Delete	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f045-8199-7e2e-f8a7-f2711f401b7d	MasterDataService	MasterDataService.ArticleTagMappings	\N	\N	\N	L:MasterDataService,Permission:ArticleTagMappings	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f045-8199-821c-c9f4-034b6946fcdf	MasterDataService	MasterDataService.ArticleViews.Edit	\N	\N	MasterDataService.ArticleViews	L:MasterDataService,Permission:Edit	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f045-8199-83f2-81c3-219c4fa9cfd4	MasterDataService	MasterDataService.ArticleTags.Edit	\N	\N	MasterDataService.ArticleTags	L:MasterDataService,Permission:Edit	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f045-8199-8552-7bb9-dad4be4b9981	MasterDataService	MasterDataService.ArticleCategories.Edit	\N	\N	MasterDataService.ArticleCategories	L:MasterDataService,Permission:Edit	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f045-8199-88ab-6d18-4a47b5426f9d	MasterDataService	MasterDataService.ArticleTagMappings.Create	\N	\N	MasterDataService.ArticleTagMappings	L:MasterDataService,Permission:Create	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f045-8199-88f6-8bbf-296885e7d7c1	MasterDataService	MasterDataService.ArticleTagMappings.Edit	\N	\N	MasterDataService.ArticleTagMappings	L:MasterDataService,Permission:Edit	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f045-8199-96d1-ca49-ed60ecea1f29	MasterDataService	MasterDataService.ArticleCategories	\N	\N	\N	L:MasterDataService,Permission:ArticleCategories	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f045-8199-a91e-0e44-91b1a20b27cc	MasterDataService	MasterDataService.ArticleViews.Create	\N	\N	MasterDataService.ArticleViews	L:MasterDataService,Permission:Create	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f045-8199-bb74-573d-f78d271a5140	MasterDataService	MasterDataService.ArticleTags.Create	\N	\N	MasterDataService.ArticleTags	L:MasterDataService,Permission:Create	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f045-8199-c3df-da77-4dee3445c24f	MasterDataService	MasterDataService.Articles.Edit	\N	\N	MasterDataService.Articles	L:MasterDataService,Permission:Edit	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f045-8199-cb80-806d-82ff484434d8	MasterDataService	MasterDataService.ArticleTags	\N	\N	\N	L:MasterDataService,Permission:ArticleTags	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f591-5821-01ab-c73c-bcc372147f59	MasterDataService	MasterDataService.Places.Edit	\N	\N	MasterDataService.Places	L:MasterDataService,Permission:Edit	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f591-5821-0b51-d063-748a0bde1b92	MasterDataService	MasterDataService.PlaceViews	\N	\N	\N	L:MasterDataService,Permission:PlaceViews	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f591-5821-0f2c-9867-fdc10d31686c	MasterDataService	MasterDataService.Places	\N	\N	\N	L:MasterDataService,Permission:Places	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f591-5821-18e3-db68-2cd4803f0038	MasterDataService	MasterDataService.EntityFiles.Delete	\N	\N	MasterDataService.EntityFiles	L:MasterDataService,Permission:Delete	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f591-5821-1cf5-199f-f7cad1c0f46f	MasterDataService	MasterDataService.EntityFiles.Edit	\N	\N	MasterDataService.EntityFiles	L:MasterDataService,Permission:Edit	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f591-5821-1fc8-52c5-5e03a8dd47a4	MasterDataService	MasterDataService.PlaceFavorites.Edit	\N	\N	MasterDataService.PlaceFavorites	L:MasterDataService,Permission:Edit	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f591-5821-25d2-8d02-a316ca64963d	MasterDataService	MasterDataService.PlaceTags.Create	\N	\N	MasterDataService.PlaceTags	L:MasterDataService,Permission:Create	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f591-5821-2b1b-6dd1-a8c02bd5013b	MasterDataService	MasterDataService.MediaFiles.Create	\N	\N	MasterDataService.MediaFiles	L:MasterDataService,Permission:Create	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f591-5821-37ec-acf2-ded62a27f168	MasterDataService	MasterDataService.PlaceViews.Delete	\N	\N	MasterDataService.PlaceViews	L:MasterDataService,Permission:Delete	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f591-5821-4a99-73bc-87fc129b3e37	MasterDataService	MasterDataService.EntityFiles	\N	\N	\N	L:MasterDataService,Permission:EntityFiles	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f591-5821-6856-d92f-018e0a02ce76	MasterDataService	MasterDataService.PlaceReviews.Create	\N	\N	MasterDataService.PlaceReviews	L:MasterDataService,Permission:Create	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f591-5821-74f6-8263-856d4046dd07	MasterDataService	MasterDataService.PlaceCategories.Delete	\N	\N	MasterDataService.PlaceCategories	L:MasterDataService,Permission:Delete	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f591-5821-7b8c-fede-2cfa1637fcb7	MasterDataService	MasterDataService.EntityFiles.Create	\N	\N	MasterDataService.EntityFiles	L:MasterDataService,Permission:Create	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f591-5821-8228-0c51-26f14c1b934c	MasterDataService	MasterDataService.PlaceViews.Create	\N	\N	MasterDataService.PlaceViews	L:MasterDataService,Permission:Create	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f591-5821-8c08-fe81-2c9c8908f07a	MasterDataService	MasterDataService.MediaFiles.Delete	\N	\N	MasterDataService.MediaFiles	L:MasterDataService,Permission:Delete	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f591-5821-904a-88ab-9d36a4d9eea6	MasterDataService	MasterDataService.Places.Delete	\N	\N	MasterDataService.Places	L:MasterDataService,Permission:Delete	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f591-5821-9874-dd1b-fd24b0811d90	MasterDataService	MasterDataService.PlaceReviews.Edit	\N	\N	MasterDataService.PlaceReviews	L:MasterDataService,Permission:Edit	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f591-5821-98e4-27e5-92b2284199bb	MasterDataService	MasterDataService.PlaceReviews.Delete	\N	\N	MasterDataService.PlaceReviews	L:MasterDataService,Permission:Delete	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f591-5821-9d04-5b3e-1175a6689c77	MasterDataService	MasterDataService.PlaceFavorites	\N	\N	\N	L:MasterDataService,Permission:PlaceFavorites	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f591-5821-9ecb-38ec-9b5d056dd4d7	MasterDataService	MasterDataService.PlaceTagMappings.Delete	\N	\N	MasterDataService.PlaceTagMappings	L:MasterDataService,Permission:Delete	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f591-5821-a92d-9196-b1a00f1ddc6a	MasterDataService	MasterDataService.PlaceTags.Edit	\N	\N	MasterDataService.PlaceTags	L:MasterDataService,Permission:Edit	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f591-5821-ab10-93fd-102e6901ac65	MasterDataService	MasterDataService.PlaceCategories	\N	\N	\N	L:MasterDataService,Permission:PlaceCategories	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f591-5821-ad5c-2f10-c1e0c7c0f919	MasterDataService	MasterDataService.PlaceFavorites.Create	\N	\N	MasterDataService.PlaceFavorites	L:MasterDataService,Permission:Create	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f591-5821-b3cb-b766-151095a6be4e	MasterDataService	MasterDataService.PlaceTags	\N	\N	\N	L:MasterDataService,Permission:PlaceTags	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f591-5821-b56b-c40b-8cb9bed37562	MasterDataService	MasterDataService.Places.Create	\N	\N	MasterDataService.Places	L:MasterDataService,Permission:Create	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f591-5821-bfcc-0be3-932ed0f75e82	MasterDataService	MasterDataService.PlaceTags.Delete	\N	\N	MasterDataService.PlaceTags	L:MasterDataService,Permission:Delete	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f591-5821-c0d4-dc39-b905cea1620f	MasterDataService	MasterDataService.PlaceViews.Edit	\N	\N	MasterDataService.PlaceViews	L:MasterDataService,Permission:Edit	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f591-5821-c7cb-6ec4-80e0bc14e917	MasterDataService	MasterDataService.MediaFiles.Edit	\N	\N	MasterDataService.MediaFiles	L:MasterDataService,Permission:Edit	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f591-5821-cb87-80bc-07db17b6d295	MasterDataService	MasterDataService.MediaFiles	\N	\N	\N	L:MasterDataService,Permission:MediaFiles	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f591-5821-d5de-42ce-5a5753ec8988	MasterDataService	MasterDataService.PlaceTagMappings	\N	\N	\N	L:MasterDataService,Permission:PlaceTagMappings	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f591-5821-db1b-0227-92e67faf38bd	MasterDataService	MasterDataService.PlaceCategories.Edit	\N	\N	MasterDataService.PlaceCategories	L:MasterDataService,Permission:Edit	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f591-5821-dc1e-2739-f573bd0ea229	MasterDataService	MasterDataService.PlaceFavorites.Delete	\N	\N	MasterDataService.PlaceFavorites	L:MasterDataService,Permission:Delete	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f591-5821-ec79-0f11-2654864e7b87	MasterDataService	MasterDataService.PlaceReviews	\N	\N	\N	L:MasterDataService,Permission:PlaceReviews	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f591-5821-f56f-192c-1b3660272693	MasterDataService	MasterDataService.PlaceTagMappings.Create	\N	\N	MasterDataService.PlaceTagMappings	L:MasterDataService,Permission:Create	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f591-5821-f71a-ad6a-32b789de8932	MasterDataService	MasterDataService.PlaceCategories.Create	\N	\N	MasterDataService.PlaceCategories	L:MasterDataService,Permission:Create	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
3a20f591-5821-fe01-b448-60011dd95a4b	MasterDataService	MasterDataService.PlaceTagMappings.Edit	\N	\N	MasterDataService.PlaceTagMappings	L:MasterDataService,Permission:Edit	t	3	\N	\N	{"_CurrentProviderName":"KHHub.MasterDataService.Permissions.MasterDataServicePermissionDefinitionProvider"}
\.


--
-- Data for Name: AbpResourcePermissionGrants; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpResourcePermissionGrants" ("Id", "TenantId", "Name", "ProviderName", "ProviderKey", "ResourceName", "ResourceKey") FROM stdin;
\.


--
-- Data for Name: AbpSettingDefinitions; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpSettingDefinitions" ("Id", "Name", "DisplayName", "Description", "DefaultValue", "IsVisibleToClients", "Providers", "IsInherited", "IsEncrypted", "ExtraProperties") FROM stdin;
3a20e9c3-4dd2-1132-0cd4-573c76936e47	Abp.Timing.TimeZone	L:AbpTiming,DisplayName:Abp.Timing.Timezone	L:AbpTiming,Description:Abp.Timing.Timezone		t	\N	t	f	{}
3a20e9c3-4f2f-1465-1fc7-a3edfe9cec76	Abp.Mailing.Smtp.UserName	L:AbpEmailing,DisplayName:Abp.Mailing.Smtp.UserName	L:AbpEmailing,Description:Abp.Mailing.Smtp.UserName	\N	f	\N	t	f	{}
3a20e9c3-4f2f-3219-793c-c18db51011e3	Abp.Mailing.DefaultFromAddress	L:AbpEmailing,DisplayName:Abp.Mailing.DefaultFromAddress	L:AbpEmailing,Description:Abp.Mailing.DefaultFromAddress	noreply@abp.io	f	\N	t	f	{}
3a20e9c3-4f2f-37b4-1053-1bc98035f1e3	Abp.Mailing.Smtp.UseDefaultCredentials	L:AbpEmailing,DisplayName:Abp.Mailing.Smtp.UseDefaultCredentials	L:AbpEmailing,Description:Abp.Mailing.Smtp.UseDefaultCredentials	true	f	\N	t	f	{}
3a20e9c3-4f2f-39fe-7f29-3c84eeb005ea	Abp.Mailing.Smtp.Domain	L:AbpEmailing,DisplayName:Abp.Mailing.Smtp.Domain	L:AbpEmailing,Description:Abp.Mailing.Smtp.Domain	\N	f	\N	t	f	{}
3a20e9c3-4f2f-5b8d-e358-a0c7eb752139	Abp.Mailing.DefaultFromDisplayName	L:AbpEmailing,DisplayName:Abp.Mailing.DefaultFromDisplayName	L:AbpEmailing,Description:Abp.Mailing.DefaultFromDisplayName	ABP application	f	\N	t	f	{}
3a20e9c3-4f2f-7273-a722-fc139b7d58a1	Abp.Mailing.Smtp.Port	L:AbpEmailing,DisplayName:Abp.Mailing.Smtp.Port	L:AbpEmailing,Description:Abp.Mailing.Smtp.Port	25	f	\N	t	f	{}
3a20e9c3-4f2f-a4a0-f55d-74f30a08e8dc	Abp.AuditLogging.IsPeriodicDeleterEnabled	L:AbpAuditLogging,DisplayName:IsPeriodicDeleterEnabled	L:AbpAuditLogging,Description:IsPeriodicDeleterEnabled	false	f	G,C	f	f	{}
3a20e9c3-4f2f-d30a-64cd-7a82e95737c1	Abp.Mailing.Smtp.EnableSsl	L:AbpEmailing,DisplayName:Abp.Mailing.Smtp.EnableSsl	L:AbpEmailing,Description:Abp.Mailing.Smtp.EnableSsl	false	f	\N	t	f	{}
3a20e9c3-4f2f-d484-6f13-64699b85f35f	Abp.Mailing.Smtp.Host	L:AbpEmailing,DisplayName:Abp.Mailing.Smtp.Host	L:AbpEmailing,Description:Abp.Mailing.Smtp.Host	127.0.0.1	f	\N	t	f	{}
3a20e9c3-4f2f-d960-2cd8-2ea3315bacaf	Abp.Mailing.Smtp.Password	L:AbpEmailing,DisplayName:Abp.Mailing.Smtp.Password	L:AbpEmailing,Description:Abp.Mailing.Smtp.Password	\N	f	\N	t	t	{}
3a20e9c3-4f30-0d20-f894-7f7ba5c81ea7	Abp.AuditLogging.ExpiredDeleterPeriod	L:AbpAuditLogging,DisplayName:ExpiredDeleterPeriod	L:AbpAuditLogging,Description:ExpiredDeleterPeriod	\N	f	\N	f	f	{}
3a20e9c3-4f30-f66b-095b-e649c0dbce75	Abp.AuditLogging.IsExpiredDeleterEnabled	L:AbpAuditLogging,DisplayName:IsExpiredDeleterEnabled	L:AbpAuditLogging,Description:IsExpiredDeleterEnabled	false	f	\N	f	f	{}
3a20e9c3-4dd1-5c21-361e-f348b7291c15	Abp.Localization.DefaultLanguage	L:AbpLocalization,DisplayName:Abp.Localization.DefaultLanguage	L:AbpLocalization,Description:Abp.Localization.DefaultLanguage	en	t	\N	t	f	{}
3a20e9c3-669f-07cf-7c16-d9205f90b5dd	Abp.Identity.Password.ForceUsersToPeriodicallyChangePassword	L:AbpIdentity,DisplayName:Abp.Identity.Password.ForceUsersToPeriodicallyChangePassword	L:AbpIdentity,Description:Abp.Identity.Password.ForceUsersToPeriodicallyChangePassword	False	t	\N	t	f	{}
3a20e9c3-669f-08e2-7d90-5ecbe8bbb020	Abp.Ldap.Ldaps	L:AbpLdap,DisplayName:Abp.Ldap.Ldaps	L:AbpLdap,Description:Abp.Ldap.Ldaps	false	f	\N	t	f	{}
3a20e9c3-669f-1682-fdc3-b4ec1a4b2109	Abp.Identity.Lockout.LockoutDuration	L:AbpIdentity,DisplayName:Abp.Identity.Lockout.LockoutDuration	L:AbpIdentity,Description:Abp.Identity.Lockout.LockoutDuration	300	t	\N	t	f	{}
3a20e9c3-669f-19a3-80b1-9d5181734199	Abp.Identity.Password.RequiredLength	L:AbpIdentity,DisplayName:Abp.Identity.Password.RequiredLength	L:AbpIdentity,Description:Abp.Identity.Password.RequiredLength	6	t	\N	t	f	{}
3a20e9c3-669f-32fb-12db-250768d8ddd9	Abp.Identity.SignIn.RequireConfirmedEmail	L:AbpIdentity,DisplayName:Abp.Identity.SignIn.RequireConfirmedEmail	L:AbpIdentity,Description:Abp.Identity.SignIn.RequireConfirmedEmail	False	t	\N	t	f	{}
3a20e9c3-669f-3cd9-a5a8-81eb3c61d0ed	Abp.Identity.Session.PreventConcurrentLogin	L:AbpIdentity,DisplayName:Abp.Identity.PreventConcurrentLogin	L:AbpIdentity,Description:Abp.Identity.PreventConcurrentLogin	Disabled	t	\N	t	f	{}
3a20e9c3-669f-47e1-06fd-f3e770011873	Abp.Identity.Password.RequireDigit	L:AbpIdentity,DisplayName:Abp.Identity.Password.RequireDigit	L:AbpIdentity,Description:Abp.Identity.Password.RequireDigit	False	t	\N	t	f	{}
3a20e9c3-669f-518c-7416-338dc96475a8	Abp.Identity.EnableOAuthLogin	L:AbpIdentity,DisplayName:Abp.Identity.EnableOAuthLogin	L:AbpIdentity,Description:Abp.Identity.EnableOAuthLogin	false	f	\N	t	f	{}
3a20e9c3-669f-538e-417f-81626bc3f3db	Abp.Identity.Password.RequiredUniqueChars	L:AbpIdentity,DisplayName:Abp.Identity.Password.RequiredUniqueChars	L:AbpIdentity,Description:Abp.Identity.Password.RequiredUniqueChars	1	t	\N	t	f	{}
3a20e9c3-669f-5d58-9a39-0c0fb185d388	Abp.Identity.OAuthLogin.ClientSecret	L:AbpIdentity,DisplayName:Abp.Identity.ClientSecret	L:AbpIdentity,Description:Abp.Identity.ClientSecret	\N	f	\N	t	t	{}
3a20e9c3-669f-6108-24ae-4d391887afaa	Abp.Account.EnableLdapLogin	L:AbpIdentity,DisplayName:Abp.Identity.EnableLdapLogin	L:AbpIdentity,Description:Abp.Identity.EnableLdapLogin	false	f	\N	t	f	{}
3a20e9c3-669f-67ab-d050-b68429689e52	Abp.Identity.Password.RequireUppercase	L:AbpIdentity,DisplayName:Abp.Identity.Password.RequireUppercase	L:AbpIdentity,Description:Abp.Identity.Password.RequireUppercase	False	t	\N	t	f	{}
3a20e9c3-669f-67b9-5bb3-1b06e11915d9	Abp.Ldap.UserName	L:AbpLdap,DisplayName:Abp.Ldap.UserName	L:AbpLdap,Description:Abp.Ldap.UserName		f	\N	t	f	{}
3a20e9c3-669f-6cdb-0784-cb7d64b203e7	Abp.Identity.Password.PasswordChangePeriodDays	L:AbpIdentity,DisplayName:Abp.Identity.Password.PasswordChangePeriodDays	L:AbpIdentity,Description:Abp.Identity.Password.PasswordChangePeriodDays	0	t	\N	t	f	{}
3a20e9c3-669f-76d0-e9ff-76083279c3c8	Abp.Identity.TwoFactor.UsersCanChange	L:AbpIdentity,DisplayName:Abp.Identity.UsersCanChange	L:AbpIdentity,Description:Abp.Identity.UsersCanChange	True	t	\N	t	f	{}
3a20e9c3-669f-80ae-cc5d-7e6736e1dd57	Abp.Ldap.Domain	L:AbpLdap,DisplayName:Abp.Ldap.Domain	L:AbpLdap,Description:Abp.Ldap.Domain		f	\N	t	f	{}
3a20e9c3-669f-9db5-54b5-c598f5c251b6	Abp.Identity.OAuthLogin.RequireHttpsMetadata	L:AbpIdentity,DisplayName:Abp.Identity.RequireHttpsMetadata	L:AbpIdentity,Description:Abp.Identity.RequireHttpsMetadata	false	f	\N	t	f	{}
3a20e9c3-669f-a319-c18c-d0cbbb267d8d	Abp.Identity.Password.PreventPasswordReuseCount	L:AbpIdentity,DisplayName:Abp.Identity.Password.PreventPasswordReuseCount	L:AbpIdentity,Description:Abp.Identity.Password.PreventPasswordReuseCount	6	t	\N	t	f	{}
3a20e9c3-669f-ac0b-de7b-a507675f48aa	Abp.Identity.SignIn.RequireEmailVerificationToRegister	L:AbpIdentity,DisplayName:Abp.Identity.SignIn.RequireEmailVerificationToRegister	L:AbpIdentity,Description:Abp.Identity.SignIn.RequireEmailVerificationToRegister	False	t	\N	t	f	{}
3a20e9c3-669f-b005-952a-11b32fffad00	Abp.Identity.Password.RequireNonAlphanumeric	L:AbpIdentity,DisplayName:Abp.Identity.Password.RequireNonAlphanumeric	L:AbpIdentity,Description:Abp.Identity.Password.RequireNonAlphanumeric	False	t	\N	t	f	{}
3a20e9c3-669f-b199-9d75-16c135c7a5fe	Abp.Identity.SignIn.RequireConfirmedPhoneNumber	L:AbpIdentity,DisplayName:Abp.Identity.SignIn.RequireConfirmedPhoneNumber	L:AbpIdentity,Description:Abp.Identity.SignIn.RequireConfirmedPhoneNumber	False	t	\N	t	f	{}
3a20e9c3-669f-b1c1-2fad-64750b526833	Abp.Identity.OAuthLogin.ClientId	L:AbpIdentity,DisplayName:Abp.Identity.ClientId	L:AbpIdentity,Description:Abp.Identity.ClientId	\N	f	\N	t	f	{}
3a20e9c3-669f-b2b6-9c01-80c6b6ba5b17	Abp.Identity.SignIn.EnablePhoneNumberConfirmation	L:AbpIdentity,DisplayName:Abp.Identity.SignIn.EnablePhoneNumberConfirmation	L:AbpIdentity,Description:Abp.Identity.SignIn.EnablePhoneNumberConfirmation	True	t	\N	t	f	{}
3a20e9c3-669f-bc80-8c9b-e922bdfc9a7a	Abp.Identity.TwoFactor.Behaviour	L:AbpIdentity,DisplayName:Abp.Identity.TwoFactorBehaviour	L:AbpIdentity,Description:Abp.Identity.TwoFactorBehaviour	Optional	t	\N	t	f	{}
3a20e9c3-669f-bd47-46f5-c2fb16a73b25	Abp.Identity.User.IsUserNameUpdateEnabled	L:AbpIdentity,DisplayName:Abp.Identity.User.IsUserNameUpdateEnabled	L:AbpIdentity,Description:Abp.Identity.User.IsUserNameUpdateEnabled	True	t	\N	t	f	{}
3a20e9c3-669f-c466-4aff-f5a0e4b77370	Abp.Identity.Lockout.MaxFailedAccessAttempts	L:AbpIdentity,DisplayName:Abp.Identity.Lockout.MaxFailedAccessAttempts	L:AbpIdentity,Description:Abp.Identity.Lockout.MaxFailedAccessAttempts	5	t	\N	t	f	{}
3a20e9c3-669f-c609-cfc1-de1917fb5125	Abp.Identity.OAuthLogin.Scope	L:AbpIdentity,DisplayName:Abp.Identity.Scope	L:AbpIdentity,Description:Abp.Identity.Scope	\N	f	\N	t	f	{}
3a20e9c3-669f-ca4d-d514-cfa6d3830f5f	Abp.Identity.Password.RequireLowercase	L:AbpIdentity,DisplayName:Abp.Identity.Password.RequireLowercase	L:AbpIdentity,Description:Abp.Identity.Password.RequireLowercase	False	t	\N	t	f	{}
3a20e9c3-669f-cc5f-aaad-bcae9e477c6a	Abp.Ldap.ServerPort	L:AbpLdap,DisplayName:Abp.Ldap.ServerPort	L:AbpLdap,Description:Abp.Ldap.ServerPort	389	f	\N	t	f	{}
3a20e9c3-669f-cdcd-2078-d97869fc6d6c	Abp.Identity.Password.EnablePreventPasswordReuse	L:AbpIdentity,DisplayName:Abp.Identity.Password.EnablePreventPasswordReuse	L:AbpIdentity,Description:Abp.Identity.Password.EnablePreventPasswordReuse	False	t	\N	t	f	{}
3a20e9c3-669f-cfd4-df72-1cb5e099e151	Abp.Identity.User.IsEmailUpdateEnabled	L:AbpIdentity,DisplayName:Abp.Identity.User.IsEmailUpdateEnabled	L:AbpIdentity,Description:Abp.Identity.User.IsEmailUpdateEnabled	True	t	\N	t	f	{}
3a20e9c3-669f-d40f-3176-5934f7b25ddc	Abp.Ldap.Password	L:AbpLdap,DisplayName:Abp.Ldap.Password	L:AbpLdap,Description:Abp.Ldap.Password		f	\N	t	t	{}
3a20e9c3-669f-d785-7843-8c4b46cb9dd7	Abp.Identity.OAuthLogin.ValidateIssuerName	L:AbpIdentity,DisplayName:Abp.Identity.ValidateIssuerName	L:AbpIdentity,Description:Abp.Identity.ValidateIssuerName	false	f	\N	t	f	{}
3a20e9c3-669f-da7f-346d-8c23b742b5ba	Abp.Identity.OrganizationUnit.MaxUserMembershipCount	L:AbpIdentity,Identity.OrganizationUnit.MaxUserMembershipCount	L:AbpIdentity,Identity.OrganizationUnit.MaxUserMembershipCount	2147483647	t	\N	t	f	{}
3a20e9c3-669f-dcfb-ec23-7ad9020dcead	Abp.Ldap.BaseDc	L:AbpLdap,DisplayName:Abp.Ldap.BaseDc	L:AbpLdap,Description:Abp.Ldap.BaseDc		f	\N	t	f	{}
3a20e9c3-669f-e411-1b55-41eae5429c2d	Abp.Identity.Lockout.AllowedForNewUsers	L:AbpIdentity,DisplayName:Abp.Identity.Lockout.AllowedForNewUsers	L:AbpIdentity,Description:Abp.Identity.Lockout.AllowedForNewUsers	True	t	\N	t	f	{}
3a20e9c3-669f-f0ff-7b45-3b4dd4ff5171	Abp.Identity.OAuthLogin.Authority	L:AbpIdentity,DisplayName:Abp.Identity.Authority	L:AbpIdentity,Description:Abp.Identity.Authority	\N	f	\N	t	f	{}
3a20e9c3-669f-f4df-aa7f-3aa0a338e7b4	Abp.Ldap.ServerHost	L:AbpLdap,DisplayName:Abp.Ldap.ServerHost	L:AbpLdap,Description:Abp.Ldap.ServerHost		f	\N	t	f	{}
3a20e9c3-669f-f714-a5c2-1ac6b35ddad8	Abp.Identity.OAuthLogin.ValidateEndpoints	L:AbpIdentity,DisplayName:Abp.Identity.ValidateEndpoints	L:AbpIdentity,Description:Abp.Identity.ValidateEndpoints	false	f	\N	t	f	{}
3a20e9c3-995f-090c-1348-fef0896f5a41	Abp.Account.TwoFactorLogin.IsRememberBrowserEnabled	L:AbpAccount,DisplayName:IsRememberBrowserEnabled	\N	true	t	\N	t	f	{}
3a20e9c3-995f-09b8-6672-b1a23fbce05a	Abp.Account.Idle.CountdownSeconds	L:AbpAccount,IdleCountdownSeconds	L:AbpAccount,IdleCountdownSecondsInfo	60	t	\N	t	f	{}
3a20e9c3-995f-1892-4ab0-2ba6ce504b19	Abp.Account.ExternalLoginPasswordVerified	F:Abp.Account.ExternalLoginPasswordVerified	\N	\N	f	\N	t	f	{}
3a20e9c3-995f-20ed-e11f-c8dee15bd52a	Abp.Account.Idle.Enabled	L:AbpAccount,DisplayName:Idle.Enabled	L:AbpAccount,Description:Idle.Enabled	False	t	\N	t	f	{}
3a20e9c3-995f-3a3c-a2d1-335972c52d59	Abp.Account.Captcha.VerifyBaseUrl	L:AbpAccount,DisplayName:VerificationUrl	L:AbpAccount,Description:VerificationUrl	https://www.google.com/	t	\N	t	f	{}
3a20e9c3-995f-67fb-45ad-5769c2b37bc6	Abp.Account.PreventEmailEnumeration	L:AbpAccount,DisplayName:PreventEmailEnumeration	L:AbpAccount,Description:PreventEmailEnumeration	false	t	\N	t	f	{}
3a20e9c3-995f-708c-36e1-ee8d5c2bf2bb	Abp.Account.ProfilePictureSource	L:AbpAccount,DisplayName:UseGravatar	L:AbpAccount,Description:UseGravatar	False	t	\N	t	f	{}
3a20e9c3-995f-780d-f432-48d57f21a55a	Abp.Account.VerifyPasswordDuringExternalLogin	L:AbpAccount,DisplayName:VerifyPasswordDuringExternalLogin	L:AbpAccount,Description:VerifyPasswordDuringExternalLogin	false	t	\N	t	f	{}
3a20e9c3-995f-7c90-b91c-99900cc3c45c	Abp.Account.EnableLocalLogin	L:AbpAccount,DisplayName:EnableLocalLogin	L:AbpAccount,Description:EnableLocalLogin	true	t	\N	t	f	{}
3a20e9c3-995f-7cd6-f455-2b5cf7123836	Abp.Account.Captcha.UseCaptchaOnRegistration	L:AbpAccount,DisplayName:UseCaptchaOnRegistration	L:AbpAccount,Description:UseCaptchaOnRegistration	false	t	\N	t	f	{}
3a20e9c3-995f-8bc5-695e-0e7599cece32	Abp.Account.Captcha.UseCaptchaOnLogin	L:AbpAccount,DisplayName:UseCaptchaOnLogin	L:AbpAccount,Description:UseCaptchaOnLogin	false	t	\N	t	f	{}
3a20e9c3-995f-9181-c4ca-455a76812773	Abp.Account.ExternalProviders	F:Abp.Account.ExternalProviders	\N	\N	f	\N	t	f	{}
3a20e9c3-995f-929d-a51e-a42aa6a1900a	Abp.Account.IsSelfRegistrationEnabled	L:AbpAccount,DisplayName:IsSelfRegistrationEnabled	L:AbpAccount,Description:IsSelfRegistrationEnabled	true	t	\N	t	f	{}
3a20e9c3-995f-95dd-5c2f-fde807e5b159	Abp.Account.Captcha.UseCaptchaOnForgotPassword	L:AbpAccount,DisplayName:UseCaptchaOnForgotPassword	L:AbpAccount,Description:UseCaptchaOnForgotPassword	false	t	\N	t	f	{}
3a20e9c3-995f-9dc5-022f-f329dcb2f844	Abp.Account.Passkey.MaximumPasskeysPerUser	L:AbpAccount,DisplayName:Passkey.MaximumPasskeysPerUser	L:AbpAccount,Description:Passkey.MaximumPasskeysPerUser	10	t	\N	t	f	{}
3a20e9c3-995f-ae92-fce2-deab0686957b	Abp.Account.Idle.IdleTimeoutMinutes	L:AbpAccount,DisplayName:Idle.IdleTimeoutMinutes	L:AbpAccount,Description:Idle.IdleTimeoutMinutes	60	t	\N	t	f	{}
3a20e9c3-995f-aead-5ef2-e3e93c9d7777	Abp.Account.Captcha.SiteSecret	L:AbpAccount,DisplayName:SiteSecret	L:AbpAccount,Description:SiteSecret	\N	f	\N	t	t	{}
3a20e9c3-995f-c832-9061-b38fc27a0c64	Abp.Account.Passkey.Enabled	L:AbpAccount,DisplayName:Passkey.Enabled	L:AbpAccount,Description:Passkey.Enabled	False	t	\N	t	f	{}
3a20e9c3-995f-e0d3-3e31-627572f996db	Abp.Account.Captcha.Version	L:AbpAccount,DisplayName:Version	L:AbpAccount,Description:Version	3	t	\N	t	f	{}
3a20e9c3-995f-e57e-ac8b-a5c0505ea386	Abp.Account.Captcha.Score	L:AbpAccount,DisplayName:Score	L:AbpAccount,Description:Score	0.5	t	\N	t	f	{}
3a20e9c3-995f-f417-d6dc-3305562e78e9	Abp.Account.Captcha.SiteKey	L:AbpAccount,DisplayName:SiteKey	L:AbpAccount,Description:SiteKey	\N	t	\N	t	f	{}
\.


--
-- Data for Name: AbpSettings; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpSettings" ("Id", "Name", "Value", "ProviderName", "ProviderKey") FROM stdin;
3a20eef9-fbfa-b357-bc8d-1d728a50382e	Abp.Localization.DefaultLanguage	en;en	T	\N
\.


--
-- Data for Name: AbpTextTemplateContents; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpTextTemplateContents" ("Id", "TenantId", "Name", "CultureName", "Content", "ExtraProperties", "ConcurrencyStamp", "CreationTime", "CreatorId", "LastModificationTime", "LastModifierId", "IsDeleted", "DeleterId", "DeletionTime") FROM stdin;
\.


--
-- Data for Name: AbpTextTemplateDefinitionContentRecords; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpTextTemplateDefinitionContentRecords" ("Id", "DefinitionId", "FileName", "FileContent") FROM stdin;
3a20e9c3-6492-c8bb-300b-484e19d9076d	3a20e9c3-6492-6e7a-f1bb-33d7e7ab161a	Message.tpl	{{model.message}}
3a20e9c3-6492-e31b-56d0-731eb4953176	3a20e9c3-6458-5bfb-5a69-ac990a3d64be	Layout.tpl	<!DOCTYPE html>\r\n<html lang="en" xmlns="http://www.w3.org/1999/xhtml">\r\n<head>\r\n    <meta charset="utf-8" />\r\n</head>\r\n<body>\r\n    {{content}}\r\n</body>\r\n</html>
\.


--
-- Data for Name: AbpTextTemplateDefinitionRecords; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpTextTemplateDefinitionRecords" ("Id", "Name", "DisplayName", "IsLayout", "Layout", "LocalizationResourceName", "IsInlineLocalized", "DefaultCultureName", "RenderEngine", "ExtraProperties") FROM stdin;
3a20e9c3-6458-5bfb-5a69-ac990a3d64be	Abp.StandardEmailTemplates.Layout	L:AbpEmailing,TextTemplate:StandardEmailTemplates.Layout	t	\N	\N	t	\N	\N	{"VirtualPath":"/Volo/Abp/Emailing/Templates/Layout.tpl"}
3a20e9c3-6492-6e7a-f1bb-33d7e7ab161a	Abp.StandardEmailTemplates.Message	L:AbpEmailing,TextTemplate:StandardEmailTemplates.Message	f	Abp.StandardEmailTemplates.Layout	\N	t	\N	\N	{"VirtualPath":"/Volo/Abp/Emailing/Templates/Message.tpl"}
\.


--
-- Data for Name: __AdministrationService_Migrations; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."__AdministrationService_Migrations" ("MigrationId", "ProductVersion") FROM stdin;
20260429043403_Initial	10.0.2
\.


--
-- Name: AbpEventInbox PK_AbpEventInbox; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpEventInbox"
    ADD CONSTRAINT "PK_AbpEventInbox" PRIMARY KEY ("Id");


--
-- Name: AbpEventOutbox PK_AbpEventOutbox; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpEventOutbox"
    ADD CONSTRAINT "PK_AbpEventOutbox" PRIMARY KEY ("Id");


--
-- Name: AbpFeatureGroups PK_AbpFeatureGroups; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpFeatureGroups"
    ADD CONSTRAINT "PK_AbpFeatureGroups" PRIMARY KEY ("Id");


--
-- Name: AbpFeatureValues PK_AbpFeatureValues; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpFeatureValues"
    ADD CONSTRAINT "PK_AbpFeatureValues" PRIMARY KEY ("Id");


--
-- Name: AbpFeatures PK_AbpFeatures; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpFeatures"
    ADD CONSTRAINT "PK_AbpFeatures" PRIMARY KEY ("Id");


--
-- Name: AbpPermissionGrants PK_AbpPermissionGrants; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpPermissionGrants"
    ADD CONSTRAINT "PK_AbpPermissionGrants" PRIMARY KEY ("Id");


--
-- Name: AbpPermissionGroups PK_AbpPermissionGroups; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpPermissionGroups"
    ADD CONSTRAINT "PK_AbpPermissionGroups" PRIMARY KEY ("Id");


--
-- Name: AbpPermissions PK_AbpPermissions; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpPermissions"
    ADD CONSTRAINT "PK_AbpPermissions" PRIMARY KEY ("Id");


--
-- Name: AbpResourcePermissionGrants PK_AbpResourcePermissionGrants; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpResourcePermissionGrants"
    ADD CONSTRAINT "PK_AbpResourcePermissionGrants" PRIMARY KEY ("Id");


--
-- Name: AbpSettingDefinitions PK_AbpSettingDefinitions; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpSettingDefinitions"
    ADD CONSTRAINT "PK_AbpSettingDefinitions" PRIMARY KEY ("Id");


--
-- Name: AbpSettings PK_AbpSettings; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpSettings"
    ADD CONSTRAINT "PK_AbpSettings" PRIMARY KEY ("Id");


--
-- Name: AbpTextTemplateContents PK_AbpTextTemplateContents; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpTextTemplateContents"
    ADD CONSTRAINT "PK_AbpTextTemplateContents" PRIMARY KEY ("Id");


--
-- Name: AbpTextTemplateDefinitionContentRecords PK_AbpTextTemplateDefinitionContentRecords; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpTextTemplateDefinitionContentRecords"
    ADD CONSTRAINT "PK_AbpTextTemplateDefinitionContentRecords" PRIMARY KEY ("Id");


--
-- Name: AbpTextTemplateDefinitionRecords PK_AbpTextTemplateDefinitionRecords; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpTextTemplateDefinitionRecords"
    ADD CONSTRAINT "PK_AbpTextTemplateDefinitionRecords" PRIMARY KEY ("Id");


--
-- Name: __AdministrationService_Migrations PK___AdministrationService_Migrations; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."__AdministrationService_Migrations"
    ADD CONSTRAINT "PK___AdministrationService_Migrations" PRIMARY KEY ("MigrationId");


--
-- Name: IX_AbpEventInbox_MessageId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpEventInbox_MessageId" ON public."AbpEventInbox" USING btree ("MessageId");


--
-- Name: IX_AbpEventInbox_Status_CreationTime; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpEventInbox_Status_CreationTime" ON public."AbpEventInbox" USING btree ("Status", "CreationTime");


--
-- Name: IX_AbpEventOutbox_CreationTime; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpEventOutbox_CreationTime" ON public."AbpEventOutbox" USING btree ("CreationTime");


--
-- Name: IX_AbpFeatureGroups_Name; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "IX_AbpFeatureGroups_Name" ON public."AbpFeatureGroups" USING btree ("Name");


--
-- Name: IX_AbpFeatureValues_Name_ProviderName_ProviderKey; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "IX_AbpFeatureValues_Name_ProviderName_ProviderKey" ON public."AbpFeatureValues" USING btree ("Name", "ProviderName", "ProviderKey");


--
-- Name: IX_AbpFeatures_GroupName; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpFeatures_GroupName" ON public."AbpFeatures" USING btree ("GroupName");


--
-- Name: IX_AbpFeatures_Name; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "IX_AbpFeatures_Name" ON public."AbpFeatures" USING btree ("Name");


--
-- Name: IX_AbpPermissionGrants_TenantId_Name_ProviderName_ProviderKey; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "IX_AbpPermissionGrants_TenantId_Name_ProviderName_ProviderKey" ON public."AbpPermissionGrants" USING btree ("TenantId", "Name", "ProviderName", "ProviderKey");


--
-- Name: IX_AbpPermissionGroups_Name; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "IX_AbpPermissionGroups_Name" ON public."AbpPermissionGroups" USING btree ("Name");


--
-- Name: IX_AbpPermissions_GroupName; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpPermissions_GroupName" ON public."AbpPermissions" USING btree ("GroupName");


--
-- Name: IX_AbpPermissions_ResourceName_Name; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "IX_AbpPermissions_ResourceName_Name" ON public."AbpPermissions" USING btree ("ResourceName", "Name");


--
-- Name: IX_AbpResourcePermissionGrants_TenantId_Name_ResourceName_Reso~; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "IX_AbpResourcePermissionGrants_TenantId_Name_ResourceName_Reso~" ON public."AbpResourcePermissionGrants" USING btree ("TenantId", "Name", "ResourceName", "ResourceKey", "ProviderName", "ProviderKey");


--
-- Name: IX_AbpSettingDefinitions_Name; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "IX_AbpSettingDefinitions_Name" ON public."AbpSettingDefinitions" USING btree ("Name");


--
-- Name: IX_AbpSettings_Name_ProviderName_ProviderKey; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "IX_AbpSettings_Name_ProviderName_ProviderKey" ON public."AbpSettings" USING btree ("Name", "ProviderName", "ProviderKey");


--
-- Name: IX_AbpTextTemplateDefinitionContentRecords_DefinitionId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpTextTemplateDefinitionContentRecords_DefinitionId" ON public."AbpTextTemplateDefinitionContentRecords" USING btree ("DefinitionId");


--
-- Name: IX_AbpTextTemplateDefinitionRecords_Name; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "IX_AbpTextTemplateDefinitionRecords_Name" ON public."AbpTextTemplateDefinitionRecords" USING btree ("Name");


--
-- Name: AbpTextTemplateDefinitionContentRecords FK_AbpTextTemplateDefinitionContentRecords_AbpTextTemplateDefi~; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpTextTemplateDefinitionContentRecords"
    ADD CONSTRAINT "FK_AbpTextTemplateDefinitionContentRecords_AbpTextTemplateDefi~" FOREIGN KEY ("DefinitionId") REFERENCES public."AbpTextTemplateDefinitionRecords"("Id") ON DELETE CASCADE;


--
-- PostgreSQL database dump complete
--

\unrestrict fkgRYldqv1iUouTB737YRIMYSnP1Md9S48kEThNAnqNomliDMVGp62prT6b3XZn

--
-- Database "KHHub_AuditLogging" dump
--

--
-- PostgreSQL database dump
--

\restrict 0W9d0yKbK5Rgq7CldbU4vSTCDbXaRDuRYBkTNMlJTqGogj0QR0z6J9kFhT2FNO0

-- Dumped from database version 14.22
-- Dumped by pg_dump version 14.22

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: KHHub_AuditLogging; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE "KHHub_AuditLogging" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'en_US.utf8';


ALTER DATABASE "KHHub_AuditLogging" OWNER TO postgres;

\unrestrict 0W9d0yKbK5Rgq7CldbU4vSTCDbXaRDuRYBkTNMlJTqGogj0QR0z6J9kFhT2FNO0
\connect "KHHub_AuditLogging"
\restrict 0W9d0yKbK5Rgq7CldbU4vSTCDbXaRDuRYBkTNMlJTqGogj0QR0z6J9kFhT2FNO0

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: AbpAuditLogActions; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpAuditLogActions" (
    "Id" uuid NOT NULL,
    "TenantId" uuid,
    "AuditLogId" uuid NOT NULL,
    "ServiceName" character varying(256),
    "MethodName" character varying(128),
    "Parameters" character varying(2000),
    "ExecutionTime" timestamp without time zone NOT NULL,
    "ExecutionDuration" integer NOT NULL,
    "ExtraProperties" text
);


ALTER TABLE public."AbpAuditLogActions" OWNER TO postgres;

--
-- Name: AbpAuditLogExcelFiles; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpAuditLogExcelFiles" (
    "Id" uuid NOT NULL,
    "TenantId" uuid,
    "FileName" character varying(256),
    "CreationTime" timestamp without time zone NOT NULL,
    "CreatorId" uuid
);


ALTER TABLE public."AbpAuditLogExcelFiles" OWNER TO postgres;

--
-- Name: AbpAuditLogs; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpAuditLogs" (
    "Id" uuid NOT NULL,
    "ApplicationName" character varying(96),
    "UserId" uuid,
    "UserName" character varying(256),
    "TenantId" uuid,
    "TenantName" character varying(64),
    "ImpersonatorUserId" uuid,
    "ImpersonatorUserName" character varying(256),
    "ImpersonatorTenantId" uuid,
    "ImpersonatorTenantName" character varying(64),
    "ExecutionTime" timestamp without time zone NOT NULL,
    "ExecutionDuration" integer NOT NULL,
    "ClientIpAddress" character varying(64),
    "ClientName" character varying(128),
    "ClientId" character varying(64),
    "CorrelationId" character varying(64),
    "BrowserInfo" character varying(512),
    "HttpMethod" character varying(16),
    "Url" character varying(256),
    "Exceptions" text,
    "Comments" character varying(256),
    "HttpStatusCode" integer,
    "ExtraProperties" text NOT NULL,
    "ConcurrencyStamp" character varying(40) NOT NULL
);


ALTER TABLE public."AbpAuditLogs" OWNER TO postgres;

--
-- Name: AbpEntityChanges; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpEntityChanges" (
    "Id" uuid NOT NULL,
    "AuditLogId" uuid NOT NULL,
    "TenantId" uuid,
    "ChangeTime" timestamp without time zone NOT NULL,
    "ChangeType" smallint NOT NULL,
    "EntityTenantId" uuid,
    "EntityId" character varying(128),
    "EntityTypeFullName" character varying(512) NOT NULL,
    "ExtraProperties" text
);


ALTER TABLE public."AbpEntityChanges" OWNER TO postgres;

--
-- Name: AbpEntityPropertyChanges; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpEntityPropertyChanges" (
    "Id" uuid NOT NULL,
    "TenantId" uuid,
    "EntityChangeId" uuid NOT NULL,
    "NewValue" character varying(512),
    "OriginalValue" character varying(512),
    "PropertyName" character varying(128) NOT NULL,
    "PropertyTypeFullName" character varying(512) NOT NULL
);


ALTER TABLE public."AbpEntityPropertyChanges" OWNER TO postgres;

--
-- Name: AbpEventInbox; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpEventInbox" (
    "Id" uuid NOT NULL,
    "ExtraProperties" text NOT NULL,
    "MessageId" text NOT NULL,
    "EventName" character varying(256) NOT NULL,
    "EventData" bytea NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL,
    "Status" integer NOT NULL,
    "HandledTime" timestamp without time zone,
    "RetryCount" integer NOT NULL,
    "NextRetryTime" timestamp without time zone
);


ALTER TABLE public."AbpEventInbox" OWNER TO postgres;

--
-- Name: AbpEventOutbox; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpEventOutbox" (
    "Id" uuid NOT NULL,
    "ExtraProperties" text NOT NULL,
    "EventName" character varying(256) NOT NULL,
    "EventData" bytea NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL
);


ALTER TABLE public."AbpEventOutbox" OWNER TO postgres;

--
-- Name: __AuditLoggingService_Migrations; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."__AuditLoggingService_Migrations" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__AuditLoggingService_Migrations" OWNER TO postgres;

--
-- Data for Name: AbpAuditLogActions; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpAuditLogActions" ("Id", "TenantId", "AuditLogId", "ServiceName", "MethodName", "Parameters", "ExecutionTime", "ExecutionDuration", "ExtraProperties") FROM stdin;
3a20e9d0-339b-f43a-dfab-1633f8242ebd	\N	3a20e9d0-339b-75e1-97a2-1b643fc6335b	Volo.Abp.Account.ExternalProviders.AccountExternalProviderAppService	GetAllAsync	{}	2026-04-29 12:08:19.882309	2	{}
3a20e9d0-38d9-4529-3f67-02e72f1fba75	\N	3a20e9d0-38d9-2690-eaa2-242ca3e1ee78	Volo.Abp.OpenIddict.Controllers.TokenController	HandleAsync	{}	2026-04-29 12:08:21.634101	26	{}
3a20e9d3-5deb-820d-0b95-ba01280b505a	\N	3a20e9d3-5deb-eaa3-9a99-c14dca78b47f	Volo.Abp.Account.ExternalProviders.AccountExternalProviderAppService	GetAllAsync	{}	2026-04-29 12:11:47.823505	1	{}
3a20e9d3-60f8-cbf4-feac-ed7c0e241717	\N	3a20e9d3-60f8-193b-4295-e37321bce724	Volo.Abp.OpenIddict.Controllers.TokenController	HandleAsync	{}	2026-04-29 12:11:48.488017	34	{}
3a20eed6-38ff-afed-f9b5-465757e05bf5	\N	3a20eed6-38ff-5228-ec6d-7469bf6aa437	Volo.Abp.OpenIddict.Controllers.TokenController	HandleAsync	{}	2026-04-30 11:33:00.961188	30	{}
3a20eef8-ad94-b369-abd6-86c56dab4b83	\N	3a20eef8-ad94-cf7c-5297-45ab2132c7cf	Volo.Abp.Account.ExternalProviders.AccountExternalProviderAppService	GetAllAsync	{}	2026-04-30 12:10:38.832727	1	{}
3a20eef8-b23d-eca0-a8ca-aa0703547ad7	\N	3a20eef8-b23d-528a-c191-fad5aaebd608	Volo.Abp.OpenIddict.Controllers.TokenController	HandleAsync	{}	2026-04-30 12:10:40.179048	35	{}
3a20eef8-ee84-a96f-63bd-6ff97f037284	\N	3a20eef8-ee84-5c60-f85d-c12d9e1d43f9	KHHub.MasterDataService.Services.Provinces.ProvincesAppService	CreateAsync	{"input":{"code":"056","name":"Kh\\u00E1nh H\\u00F2a"}}	2026-04-30 12:10:55.770558	90	{}
3a20eef9-9ac6-7a10-c6e3-2a790189e0f3	\N	3a20eef9-9ac6-d0ed-de14-19c6f5b385d6	KHHub.MasterDataService.Services.Provinces.ProvincesAppService	CreateAsync	{"input":{"code":"059","name":"TPHCM"}}	2026-04-30 12:11:39.93401	37	{}
3a20eef9-a9ae-fc3a-e17a-ad9f4f2a0a64	\N	3a20eef9-a9ae-062c-3c3b-85612e5ee0cc	KHHub.MasterDataService.Services.Provinces.ProvincesAppService	DeleteAsync	{"id":"3a20eef9-9aae-3f2d-4bc7-0a4af4ff3f5f"}	2026-04-30 12:11:43.694852	91	{}
3a20eef9-fc1c-a4fd-d56f-b992cfef93d7	\N	3a20eef9-fc1c-f7e3-001d-904aa278d958	Volo.Abp.LanguageManagement.LanguageAppService	SetAsDefaultAsync	{"id":"3a20e9c3-5116-2824-2f31-7676b507fe77"}	2026-04-30 12:12:04.765681	115	{}
3a20eef9-fc1d-1302-9eb2-bd6fdcba3e05	\N	3a20eef9-fc1c-f7e3-001d-904aa278d958	Volo.Abp.LanguageManagement.LanguageController	SetAsDefaultAsync	{"id":"3a20e9c3-5116-2824-2f31-7676b507fe77"}	2026-04-30 12:12:04.760158	121	{}
3a20ef16-d652-a0c5-30f3-bb29386267b0	\N	3a20ef16-d652-eb3f-5305-3483903c6032	KHHub.MasterDataService.Services.Wards.WardsAppService	CreateAsync	{"input":{"code":"TNT","name":"T\\u00E2y Nha Trang","provinceId":"3a20eef8-ee24-1d42-8ea0-c2dfffd00643"}}	2026-04-30 12:43:35.706416	51	{}
3a20ef17-12ce-ff01-9a98-e4b55782d55e	\N	3a20ef17-12ce-099a-6ca5-977fb8359049	KHHub.MasterDataService.Services.Provinces.ProvincesAppService	CreateAsync	{"input":{"code":"021","name":"TP H\\u1ED3 Ch\\u00ED Minh"}}	2026-04-30 12:43:51.225545	18	{}
3a20ef17-3007-1598-4564-d1e23c56c558	\N	3a20ef17-3007-4846-7b40-1f1f0cb6bc18	KHHub.MasterDataService.Services.Wards.WardsAppService	UpdateAsync	{"id":"3a20ef16-d623-968d-d968-01c767fc522d","input":{"code":"TNT","name":"T\\u00E2y Nha Trang","provinceId":"3a20ef17-12c0-488c-6366-3bf268cbf282","concurrencyStamp":"de477b0247f74f84a3e67daeaeb83bee"}}	2026-04-30 12:43:58.673774	48	{}
3a20ef17-3fb1-4afd-5d5f-cab70ea37411	\N	3a20ef17-3fb1-1666-5ad2-6a1b2a6ff3e7	KHHub.MasterDataService.Services.Wards.WardsAppService	UpdateAsync	{"id":"3a20ef16-d623-968d-d968-01c767fc522d","input":{"code":"TNT","name":"T\\u00E2y Nha Trang","provinceId":"3a20eef8-ee24-1d42-8ea0-c2dfffd00643","concurrencyStamp":"0a9b9a4062a14954a34a0ea7a3f49979"}}	2026-04-30 12:44:02.720454	15	{}
3a20efd5-5815-1bed-44a4-837ba8858277	\N	3a20efd5-5815-2525-9742-611a53a471be	Volo.Abp.Account.DynamicClaimsAppService	RefreshAsync	{}	2026-04-30 16:11:40.646707	8	{}
3a20efd5-5815-a6f2-474a-c05b85e0c2f1	\N	3a20efd5-5815-2525-9742-611a53a471be	Volo.Abp.Account.DynamicClaimsController	RefreshAsync	{}	2026-04-30 16:11:40.64011	16	{}
3a20efd5-5d73-8892-65ce-c0ad4fe72361	\N	3a20efd5-5d73-9536-ec1c-233c72f64eea	Volo.Abp.OpenIddict.Controllers.TokenController	HandleAsync	{}	2026-04-30 16:11:41.998073	12	{}
3a20f046-5b25-3e2a-9e21-15a46de3b6c5	\N	3a20f046-5b25-d32d-e11a-de3ae45c9990	Volo.Abp.Account.DynamicClaimsAppService	RefreshAsync	{}	2026-04-30 18:15:07.15783	3	{}
3a20f046-5b26-10b2-a60b-e74ebcba7c8a	\N	3a20f046-5b25-d32d-e11a-de3ae45c9990	Volo.Abp.Account.DynamicClaimsController	RefreshAsync	{}	2026-04-30 18:15:07.13689	24	{}
3a20f046-693e-bc84-e918-b077e6edbc9f	\N	3a20f046-693e-eb64-61ab-692a0578ea12	Volo.Abp.OpenIddict.Controllers.TokenController	HandleAsync	{}	2026-04-30 18:15:10.57756	11	{}
3a20f3f0-cb2d-91e2-11ae-dc9de035bb99	\N	3a20f3f0-cb2d-b59b-cc87-3a24cccf6aa3	Volo.Abp.Account.ExternalProviders.AccountExternalProviderAppService	GetAllAsync	{}	2026-05-01 11:20:07.995773	2	{}
3a20f3f0-d06c-f0f1-3bad-b360d4766f97	\N	3a20f3f0-d06c-312b-599d-16ec0cbfe963	Volo.Abp.OpenIddict.Controllers.TokenController	HandleAsync	{}	2026-05-01 11:20:09.726403	26	{}
3a20f3f1-7082-64e9-0459-30d23ea00cfb	\N	3a20f3f1-7081-310a-4b98-1def345e2581	KHHub.MasterDataService.Services.ArticleCategories.ArticleCategoriesAppService	CreateAsync	{"input":{"name":"Vi\\u1EC7c l\\u00E0m","slug":"viec-lam","description":"Vi\\u1EC7c l\\u00E0m","icon":"fa fa-tag","parentId":"00000000-0000-0000-0000-000000000000","displayOrder":1,"isActive":true,"thumbnailUrl":null}}	2026-05-01 11:20:50.846013	91	{}
3a20f40c-b4e5-c21e-0f8e-522c688d5777	\N	3a20f40c-b4e5-e4f2-6158-876cbcbcecb9	KHHub.MasterDataService.Services.Articles.ArticlesAppService	CreateAsync	{"input":{"title":"test tile ","slug":"test-tile","summary":"test","content":"\\u003Cp\\u003E\\u003Cimg src=\\u0022/uploads/articles/67fdd05cc52771d171a63a20f40bce0d.png\\u0022 alt=\\u0022test\\u0022 width=\\u0022314\\u0022 height=\\u0022314\\u0022\\u003E n\\u1ED9i dung test\\u003C/p\\u003E","thumbnailUrl":"/uploads/articles/07eefd3d633ea3c24c173a20f40c3609.png","coverImageUrl":"/uploads/articles/9eb671d514e223b7eaec3a20f40c58cd.jpeg","type":0,"authorName":"author","source":"s\\u0111\\u1EA5","sourceUrl":"dsadsad","status":2,"publishedAt":"2026-05-01T00:00:00","isFeatured":false,"isHot":false,"isTrending":false,"viewCount":0,"likeCount":0,"shareCount":0,"commentCount":0,"readingTime":0,"seoTitle":"teiel\\u00E1","seoDescription":"kalsdjsakl","seoKeywords":"jsl\\u1EA1dlk\\u00E1","articleCategoryId":"00000000-0000-0000-0000-000000000000"}}	2026-05-01 11:50:37.881883	28	{}
3a20f420-19e9-0ea4-eea1-152631efbb67	\N	3a20f420-19e9-83bc-4ba7-2dcc62356cc9	KHHub.MasterDataService.Services.Articles.ArticlesAppService	CreateAsync	{"input":{"title":"C\\u1EA7n tuy\\u1EC3n nh\\u00E2n vi\\u00EAn l\\u00E0m vi\\u1EC7c t\\u1EA1i Nha trang","slug":"can-tuyen-nhan-vien-lam-viec-tai-nha-trang","summary":"C\\u1EA7n tuy\\u1EC3n nh\\u00E2n vi\\u00EAn l\\u00E0m vi\\u1EC7c t\\u1EA1i Nha trang summary","content":"\\u003Cp\\u003EC\\u1EA7n tuy\\u1EC3n nh\\u0026acirc;n vi\\u0026ecirc;n l\\u0026agrave;m vi\\u1EC7c t\\u1EA1i Nha trang\\u003C/p\\u003E","thumbnailUrl":"/uploads/articles/b5121e674a0436dac4853a20f41fb7d0.jpeg","coverImageUrl":"/uploads/articles/ef882d01588e9e40fb893a20f41ff2da.png","type":0,"authorName":"test","source":null,"sourceUrl":null,"status":0,"publishedAt":"2026-05-01T00:00:00","isFeatured":true,"isHot":true,"isTrending":true,"viewCount":0,"likeCount":0,"shareCount":0,"commentCount":0,"readingTime":2,"seoTitle":"C\\u1EA7n tuy\\u1EC3n nh\\u00E2n vi\\u00EAn l\\u00E0m vi\\u1EC7c t\\u1EA1i Nha trang","seoDescription":"Can tuyen nhan vien lam viec tai Nha trang summary","seoKeywords":"vi\\u1EC7c l\\u00E0m, vi\\u1EC7c l\\u00E0m nha trang","articleCategoryId":"3a20f3f1-702a-7f07-9196-92139196a7a2"}}	2026-05-01 12:11:48.861367	98	{}
3a20f5a8-1e06-7577-a5f2-fd80d4a0d26e	\N	3a20f5a8-1e06-f71e-9e23-fc50127336a9	Volo.Abp.OpenIddict.Controllers.TokenController	HandleAsync	{}	2026-05-01 19:19:59.901963	18	{}
3a20f420-9950-9d92-c174-33266a56f060	\N	3a20f420-9950-f8aa-d850-9321044f13fd	KHHub.MasterDataService.Services.Articles.ArticlesAppService	UpdateAsync	{"id":"3a20f420-198b-0739-b05c-03c17b1df049","input":{"title":"C\\u1EA7n tuy\\u1EC3n nh\\u00E2n vi\\u00EAn l\\u00E0m vi\\u1EC7c t\\u1EA1i Nha trang","slug":"can-tuyen-nhan-vien-lam-viec-tai-nha-trang","summary":"C\\u1EA7n tuy\\u1EC3n nh\\u00E2n vi\\u00EAn l\\u00E0m vi\\u1EC7c t\\u1EA1i Nha trang summary","content":"\\u003Cp\\u003EC\\u1EA7n tuy\\u1EC3n nh\\u0026acirc;n vi\\u0026ecirc;n l\\u0026agrave;m vi\\u1EC7c t\\u1EA1i Nha trang\\u003C/p\\u003E","thumbnailUrl":"/uploads/articles/b5121e674a0436dac4853a20f41fb7d0.jpeg","coverImageUrl":"/uploads/articles/ef882d01588e9e40fb893a20f41ff2da.png","type":0,"authorName":"test","source":null,"sourceUrl":null,"status":2,"publishedAt":"2026-05-01T00:00:00","isFeatured":true,"isHot":true,"isTrending":true,"viewCount":0,"likeCount":0,"shareCount":0,"commentCount":0,"readingTime":2,"seoTitle":"C\\u1EA7n tuy\\u1EC3n nh\\u00E2n vi\\u00EAn l\\u00E0m vi\\u1EC7c t\\u1EA1i Nha trang","seoDescription":"Can tuyen nhan vien lam viec tai Nha trang summary","seoKeywords":"vi\\u1EC7c l\\u00E0m, vi\\u1EC7c l\\u00E0m nha trang","articleCategoryId":"3a20f3f1-702a-7f07-9196-92139196a7a2","concurrencyStamp":"58ad06cd167d42459c6afd140626d183"}}	2026-05-01 12:12:21.517427	60	{}
3a20f424-122f-59c6-21bf-cba9c6cc1417	\N	3a20f424-122f-dcae-a99e-278458eb3e7c	KHHub.MasterDataService.Services.Articles.ArticlesAppService	UpdateAsync	{"id":"3a20f420-198b-0739-b05c-03c17b1df049","input":{"title":"C\\u1EA7n tuy\\u1EC3n nh\\u00E2n vi\\u00EAn l\\u00E0m vi\\u1EC7c t\\u1EA1i Nha trang","slug":"can-tuyen-nhan-vien-lam-viec-tai-nha-trang","summary":"C\\u1EA7n tuy\\u1EC3n nh\\u00E2n vi\\u00EAn l\\u00E0m vi\\u1EC7c t\\u1EA1i Nha trang summary","content":"\\u003Cp\\u003EC\\u1EA7n tuy\\u1EC3n nh\\u0026acirc;n vi\\u0026ecirc;n l\\u0026agrave;m vi\\u1EC7c t\\u1EA1i Nha trang\\u003C/p\\u003E","thumbnailUrl":"/uploads/articles/b5121e674a0436dac4853a20f41fb7d0.jpeg","coverImageUrl":"/uploads/articles/ef882d01588e9e40fb893a20f41ff2da.png","type":0,"authorName":"test","source":null,"sourceUrl":null,"status":2,"publishedAt":"2026-05-01T00:00:00","isFeatured":true,"isHot":true,"isTrending":true,"viewCount":0,"likeCount":0,"shareCount":0,"commentCount":0,"readingTime":2,"seoTitle":"C\\u1EA7n tuy\\u1EC3n nh\\u00E2n vi\\u00EAn l\\u00E0m vi\\u1EC7c t\\u1EA1i Nha trang","seoDescription":"Can tuyen nhan vien lam viec tai Nha trang summary","seoKeywords":"vi\\u1EC7c l\\u00E0m | vi\\u1EC7c l\\u00E0m nha trang","articleCategoryId":"3a20f3f1-702a-7f07-9196-92139196a7a2","concurrencyStamp":"4ca287383a0d494e9330773425892b27"}}	2026-05-01 12:16:09.088647	41	{}
3a20f428-8cbc-6998-c3ce-778779434a8b	\N	3a20f428-8cbc-d3f6-040f-a22c814cf84a	Volo.Abp.Account.DynamicClaimsAppService	RefreshAsync	{}	2026-05-01 12:21:02.607516	6	{}
3a20f428-8cbc-f7c9-895b-6462af048681	\N	3a20f428-8cbc-d3f6-040f-a22c814cf84a	Volo.Abp.Account.DynamicClaimsController	RefreshAsync	{}	2026-05-01 12:21:02.604462	11	{}
3a20f428-91cb-28f3-6eec-6ed6b3ead709	\N	3a20f428-91cb-968c-a12c-8561cdd366e2	Volo.Abp.OpenIddict.Controllers.TokenController	HandleAsync	{}	2026-05-01 12:21:03.729252	20	{}
3a20f437-1cc7-2357-bee0-35a3d2655f9f	\N	3a20f437-1cc7-d26d-55e2-388adc73b422	KHHub.MasterDataService.Services.Articles.ArticlesAppService	GetArticleCategoryLookupAsync	{"input":{"filter":"Vi\\u00EAje","skipCount":0,"maxResultCount":1000}}	2026-05-01 12:36:57.000038	14	{}
3a20f43a-2c43-fe1a-cc29-9c480213414d	\N	3a20f43a-2c43-a27f-c13c-381d4eb9ab1b	Volo.Abp.AspNetCore.Mvc.ApplicationConfigurations.AbpApplicationLocalizationAppService	GetAsync	{"input":{"cultureName":"vi","onlyDynamics":true}}	2026-05-01 12:40:17.49485	46	{}
3a20f43a-2c44-eb8a-6736-0e62cb447296	\N	3a20f43a-2c43-a27f-c13c-381d4eb9ab1b	Volo.Abp.AspNetCore.Mvc.ApplicationConfigurations.AbpApplicationLocalizationController	GetAsync	{"input":{"cultureName":"vi","onlyDynamics":true}}	2026-05-01 12:40:17.494678	49	{}
3a20f45b-7e17-1baa-ead5-30336ce53dee	\N	3a20f45b-7e17-990c-9ce5-827fd85cbf25	Volo.Abp.AspNetCore.Mvc.ApplicationConfigurations.AbpApplicationConfigurationController	GetAsync	{"options":{"includeLocalizationResources":false}}	2026-05-01 13:16:41.196553	32	{}
3a20f45b-7e17-ebef-419c-79f1fd85d732	\N	3a20f45b-7e17-990c-9ce5-827fd85cbf25	Volo.Abp.AspNetCore.Mvc.ApplicationConfigurations.AbpApplicationConfigurationAppService	GetAsync	{"options":{"includeLocalizationResources":false}}	2026-05-01 13:16:41.197106	30	{}
3a20f460-8e9e-5134-2763-d3fe2ebbefd2	\N	3a20f460-8e9e-ef23-90f8-dc086134efbd	Volo.Abp.Account.DynamicClaimsController	RefreshAsync	{}	2026-05-01 13:22:13.131698	10	{}
3a20f460-8e9e-e874-39ce-c44807367f9f	\N	3a20f460-8e9e-ef23-90f8-dc086134efbd	Volo.Abp.Account.DynamicClaimsAppService	RefreshAsync	{}	2026-05-01 13:22:13.136061	4	{}
3a20f460-9339-0be8-d4a2-af69aabb0b5a	\N	3a20f460-9339-ba35-4be8-860b1042c0ea	Volo.Abp.OpenIddict.Controllers.TokenController	HandleAsync	{}	2026-05-01 13:22:14.103766	13	{}
3a20f46f-90ca-688a-ce1a-bfb014ededc3	\N	3a20f46f-90ca-b480-6386-9ba333784116	KHHub.MasterDataService.Services.ArticleTags.ArticleTagsAppService	CreateAsync	{"input":{"name":"Tin t\\u1EE9c","slug":"tin-tuc","description":"test","usageCount":1}}	2026-05-01 13:38:36.662054	74	{}
3a20f46f-cfef-1b05-2fd8-88a852ab6709	\N	3a20f46f-cfef-06b4-afda-76c3a2eec4ac	KHHub.MasterDataService.Services.ArticleTagMappings.ArticleTagMappingsAppService	CreateAsync	{"input":{"isPrimary":true,"order":1,"articleTagId":"3a20f46f-9080-b44a-f445-0a29214af6f1","articleId":"3a20f420-198b-0739-b05c-03c17b1df049"}}	2026-05-01 13:38:52.868584	40	{}
3a20f48b-8da5-17fb-c9a3-248a2bba012a	\N	3a20f48b-8da5-c2ff-3c37-5f20547a92ff	Volo.Abp.Account.AccountAppService	GetProfilePictureFileAsync	{"id":"3a20e9c3-58f2-bbe9-d4ca-25656eaca802"}	2026-05-01 14:09:10.874509	10	{}
3a20f48b-8da5-70d9-3b09-27c216cb9ad8	\N	3a20f48b-8da5-c2ff-3c37-5f20547a92ff	Volo.Abp.Account.AccountController	GetProfilePictureFileAsync	{"id":"3a20e9c3-58f2-bbe9-d4ca-25656eaca802"}	2026-05-01 14:09:10.87318	13	{}
3a20f492-9c05-01c1-a09d-6d1de9674bb0	\N	3a20f492-9c05-bbac-ffc0-cb776418047e	KHHub.MasterDataService.Services.ArticleCategories.ArticleCategoriesAppService	UpdateAsync	{"id":"3a20f3f1-702a-7f07-9196-92139196a7a2","input":{"name":"Vi\\u1EC7c l\\u00E0m","slug":"viec-lam","description":"Vi\\u1EC7c l\\u00E0m","icon":"fa fa-tag","parentId":"3a20f3f1-702a-7f07-9196-92139196a7a2","displayOrder":1,"isActive":true,"thumbnailUrl":"/uploads/articles/1eec5ff34a17720f22f53a20f4927605.png","concurrencyStamp":"d04a9490beb24d2f8d762454d305d625"}}	2026-05-01 14:16:53.340948	35	{}
3a20f498-ef2e-22fc-9ced-19c53bf1386e	\N	3a20f498-ef2e-9c2e-6981-0c74333db8d0	Volo.Abp.Account.DynamicClaimsController	RefreshAsync	{}	2026-05-01 14:23:47.877119	4	{}
3a20f498-ef2e-55c2-cca2-662cd3968d4e	\N	3a20f498-ef2e-9c2e-6981-0c74333db8d0	Volo.Abp.Account.DynamicClaimsAppService	RefreshAsync	{}	2026-05-01 14:23:47.879014	2	{}
3a20f498-f394-5941-7fd0-a1a84471166f	\N	3a20f498-f394-0125-a6bf-fc75913a1724	Volo.Abp.OpenIddict.Controllers.TokenController	HandleAsync	{}	2026-05-01 14:23:48.805686	12	{}
3a20f568-c18a-3a21-bfa6-8d0c07a55aa0	\N	3a20f568-c18a-1ff8-c0b8-08bafd4dbbc5	Volo.Abp.Account.ExternalProviders.AccountExternalProviderAppService	GetAllAsync	{}	2026-05-01 18:10:47.084492	1	{}
3a20f568-c744-eddc-4782-181bcc6607aa	\N	3a20f568-c744-60bf-6640-f5b6362a62be	Volo.Abp.OpenIddict.Controllers.TokenController	HandleAsync	{}	2026-05-01 18:10:48.868482	31	{}
3a20f56f-a3e5-d06b-de0d-0f8d90261e33	\N	3a20f56f-a3e5-381c-4ba0-25b6d5fd7f25	KHHub.MasterDataService.Services.Articles.ArticlesAppService	CreateAsync		2026-05-01 18:18:18.777301	66	{}
3a20f570-5cb1-7ab9-ffb5-d2bd8c9a9d50	\N	3a20f570-5cb1-2a77-9cd5-dfe4d982cdca	KHHub.MasterDataService.Services.Articles.ArticlesAppService	UpdateAsync		2026-05-01 18:19:06.101114	58	{}
3a20f572-555d-74e3-610f-275b3af5e055	\N	3a20f572-555d-b3b9-7630-78f5ee332188	Volo.Abp.Identity.IdentityRoleController	CreateAsync	{"input":{"name":"Ng\\u01B0\\u1EDDi d\\u00F9ng","isDefault":true,"isPublic":true,"extraProperties":{}}}	2026-05-01 18:21:15.300178	48	{}
3a20f572-555d-ef2b-1fb2-f8d18d90129f	\N	3a20f572-555d-b3b9-7630-78f5ee332188	Volo.Abp.Identity.IdentityRoleAppService	CreateAsync	{"input":{"name":"Ng\\u01B0\\u1EDDi d\\u00F9ng","isDefault":true,"isPublic":true,"extraProperties":{}}}	2026-05-01 18:21:15.301058	47	{}
3a20f5a8-18a7-584d-fe68-4f9cabb1d1a4	\N	3a20f5a8-18a6-316e-3a08-0e29806c43f4	Volo.Abp.Account.DynamicClaimsAppService	RefreshAsync	{}	2026-05-01 19:19:58.662117	17	{}
3a20f5a8-18a7-d1ee-a378-1634ef0dc325	\N	3a20f5a8-18a6-316e-3a08-0e29806c43f4	Volo.Abp.Account.DynamicClaimsController	RefreshAsync	{}	2026-05-01 19:19:58.656314	33	{}
\.


--
-- Data for Name: AbpAuditLogExcelFiles; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpAuditLogExcelFiles" ("Id", "TenantId", "FileName", "CreationTime", "CreatorId") FROM stdin;
\.


--
-- Data for Name: AbpAuditLogs; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpAuditLogs" ("Id", "ApplicationName", "UserId", "UserName", "TenantId", "TenantName", "ImpersonatorUserId", "ImpersonatorUserName", "ImpersonatorTenantId", "ImpersonatorTenantName", "ExecutionTime", "ExecutionDuration", "ClientIpAddress", "ClientName", "ClientId", "CorrelationId", "BrowserInfo", "HttpMethod", "Url", "Exceptions", "Comments", "HttpStatusCode", "ExtraProperties", "ConcurrencyStamp") FROM stdin;
3a20e9d0-339b-75e1-97a2-1b643fc6335b	KHHub.AuthServer	\N	\N	\N	\N	\N	\N	\N	\N	2026-04-29 12:08:19.692114	813	::1	\N	\N	3c5f176bad3245688664ae46192b7e95	Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36	POST	/Account/Login	\N		302	{}	e7550df370aa471e85125e538282abf2
3a20e9d0-38d9-2690-eaa2-242ca3e1ee78	KHHub.AuthServer	\N	\N	\N	\N	\N	\N	\N	\N	2026-04-29 12:08:21.631304	218	::1	\N	\N	9b11853e98564ef09d70dec2f23e492e	Microsoft ASP.NET Core OpenIdConnect handler	POST	/connect/token	\N		200	{}	1a340aa226284bd18bda11c6307a48cb
3a20e9d3-5deb-eaa3-9a99-c14dca78b47f	KHHub.AuthServer	\N	\N	\N	\N	\N	\N	\N	\N	2026-04-29 12:11:47.786611	161	::1	\N	\N	ef41dd71b475460980c3f8c7e42bbc6a	Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36	POST	/Account/Login	\N		302	{}	b0a69882253c44faa632342cd6e59703
3a20e9d3-60f8-193b-4295-e37321bce724	KHHub.AuthServer	\N	\N	\N	\N	\N	\N	\N	\N	2026-04-29 12:11:48.487633	241	::1	\N	\N	45ea7a33d50a4c49bb58a72007e049d0	Microsoft ASP.NET Core OpenIdConnect handler	POST	/connect/token	\N		200	{}	58724df77a5f47ffa4f2f47bab3a0291
3a20eed6-38ff-5228-ec6d-7469bf6aa437	KHHub.AuthServer	\N	\N	\N	\N	\N	\N	\N	\N	2026-04-30 11:33:00.960919	222	::1	\N	\N	2f9781f285dc4229a2334af17d90f55a	Microsoft ASP.NET Core OpenIdConnect handler	POST	/connect/token	\N		200	{}	1c99ceb60d4e4b248abc0390f82443ec
3a20eef8-ad94-cf7c-5297-45ab2132c7cf	KHHub.AuthServer	\N	\N	\N	\N	\N	\N	\N	\N	2026-04-30 12:10:38.660096	590	::1	\N	\N	0685587333a84a9b92e82a2096be51d7	Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36	POST	/Account/Login	\N		302	{}	c6cd4635580f4ea3b78fea4bf122f6a7
3a20eef8-b23d-528a-c191-fad5aaebd608	KHHub.AuthServer	\N	\N	\N	\N	\N	\N	\N	\N	2026-04-30 12:10:40.176505	269	::1	\N	\N	dea17575fdb047d3a10a45fda8beb106	Microsoft ASP.NET Core OpenIdConnect handler	POST	/connect/token	\N		200	{}	9214688b888e4a599ec9808b84c54d81
3a20eef8-ee84-5c60-f85d-c12d9e1d43f9	KHHub.MasterDataService	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	admin	\N	\N	\N	\N	\N	\N	2026-04-30 12:10:55.754855	119	::1	\N	Web	9f057142e5de4254bb337388c8559118	\N	POST	/api/masterdata/provinces	\N		200	{}	48b74608307942e58ee5baf31144763e
3a20eef9-9ac6-d0ed-de14-19c6f5b385d6	KHHub.MasterDataService	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	admin	\N	\N	\N	\N	\N	\N	2026-04-30 12:11:39.922317	52	::1	\N	Web	420193b845a740a78d6b23ac3a0add25	\N	POST	/api/masterdata/provinces	\N		200	{}	2ed11cefe2114fabb2dc10eb7cd329ae
3a20eef9-a9ae-062c-3c3b-85612e5ee0cc	KHHub.MasterDataService	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	admin	\N	\N	\N	\N	\N	\N	2026-04-30 12:11:43.687996	103	::1	\N	Web	efd403e93e334373acf9bf279d63e03a	\N	DELETE	/api/masterdata/provinces/3a20eef9-9aae-3f2d-4bc7-0a4af4ff3f5f	\N		204	{}	ba198eb496cd4c228eaa2e59a59052e5
3a20eef9-fc1c-f7e3-001d-904aa278d958	KHHub.LanguageService	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	admin	\N	\N	\N	\N	\N	\N	2026-04-30 12:12:04.75066	139	::1	\N	Web	179426eb4b7a41a18c03b4fdb4566544	\N	PUT	/api/language-management/languages/3a20e9c3-5116-2824-2f31-7676b507fe77/set-as-default	\N		204	{}	67621ba4ab7e42c7927b79f13f29dfbd
3a20ef16-d652-eb3f-5305-3483903c6032	KHHub.MasterDataService	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	admin	\N	\N	\N	\N	\N	\N	2026-04-30 12:43:35.685951	74	::1	\N	Web	52bc1f7229314eee972715f9b2426228	\N	POST	/api/masterdata/wards	\N		200	{}	9cfd54466ce5475a8b6d3418cbf247a2
3a20ef17-12ce-099a-6ca5-977fb8359049	KHHub.MasterDataService	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	admin	\N	\N	\N	\N	\N	\N	2026-04-30 12:43:51.218575	27	::1	\N	Web	6fa229a5843b42448d2eded68d3c87a9	\N	POST	/api/masterdata/provinces	\N		200	{}	2b1c1df7769746d68ffb4913b299661e
3a20ef17-3007-4846-7b40-1f1f0cb6bc18	KHHub.MasterDataService	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	admin	\N	\N	\N	\N	\N	\N	2026-04-30 12:43:58.664202	63	::1	\N	Web	967059ae43e84f23a6da90f1714aa38f	\N	PUT	/api/masterdata/wards/3a20ef16-d623-968d-d968-01c767fc522d	\N		200	{}	ec45a52d49254f27a87c290a34c9e622
3a20ef17-3fb1-1666-5ad2-6a1b2a6ff3e7	KHHub.MasterDataService	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	admin	\N	\N	\N	\N	\N	\N	2026-04-30 12:44:02.71951	18	::1	\N	Web	2f9ade92a8a84b16afc6e01c84b7b44a	\N	PUT	/api/masterdata/wards/3a20ef16-d623-968d-d968-01c767fc522d	\N		200	{}	009aa664316845ea8030e93f1e02ff9c
3a20efd5-5815-2525-9742-611a53a471be	KHHub.AuthServer	\N	\N	\N	\N	\N	\N	\N	\N	2026-04-30 16:11:40.613013	204	::1	\N	\N	bda4963ffac74de3844c8cd59964d579	\N	POST	/api/account/dynamic-claims/refresh	[\n  {\n    "code": "Volo.Authorization:010001",\n    "message": "Authorization failed! Given policy has not granted.",\n    "details": null,\n    "data": null,\n    "validationErrors": null\n  }\n]		401	{}	c45c1a7549384c22b2979d98534cef3e
3a20efd5-5d73-9536-ec1c-233c72f64eea	KHHub.AuthServer	\N	\N	\N	\N	\N	\N	\N	\N	2026-04-30 16:11:41.997802	198	::1	\N	\N	7ca1eb7cd1ac4d0ca86cf8b6f96f34bc	Microsoft ASP.NET Core OpenIdConnect handler	POST	/connect/token	\N		200	{}	a1e2071f45a446f8ba617b2eb204871b
3a20f046-5b25-d32d-e11a-de3ae45c9990	KHHub.AuthServer	\N	\N	\N	\N	\N	\N	\N	\N	2026-04-30 18:15:07.126989	45	::1	\N	\N	f8559ae07f6f4bb69515e22e638b926a	\N	POST	/api/account/dynamic-claims/refresh	[\n  {\n    "code": "Volo.Authorization:010001",\n    "message": "Authorization failed! Given policy has not granted.",\n    "details": null,\n    "data": null,\n    "validationErrors": null\n  }\n]		401	{}	ffb10853680f41c1871f4c50b8be0c1e
3a20f046-693e-eb64-61ab-692a0578ea12	KHHub.AuthServer	\N	\N	\N	\N	\N	\N	\N	\N	2026-04-30 18:15:10.577272	205	::1	\N	\N	f0e16b6304d44500ab18ad7b50b31658	Microsoft ASP.NET Core OpenIdConnect handler	POST	/connect/token	\N		200	{}	01af661c193242fea43596bb1865923c
3a20f3f0-cb2d-b59b-cc87-3a24cccf6aa3	KHHub.AuthServer	\N	\N	\N	\N	\N	\N	\N	\N	2026-05-01 11:20:07.807813	812	::1	\N	\N	d2d39d7826f0488d80f257aac24ce2c1	Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36	POST	/Account/Login	\N		302	{}	c171cda2536c4bf6944fe03854e8bdd4
3a20f3f0-d06c-312b-599d-16ec0cbfe963	KHHub.AuthServer	\N	\N	\N	\N	\N	\N	\N	\N	2026-05-01 11:20:09.722793	242	::1	\N	\N	d71b324fa58b4d4db8ff9180066b1d30	Microsoft ASP.NET Core OpenIdConnect handler	POST	/connect/token	\N		200	{}	78d8ecb7a6434e548a142498dc955b2c
3a20f3f1-7081-310a-4b98-1def345e2581	KHHub.MasterDataService	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	admin	\N	\N	\N	\N	\N	\N	2026-05-01 11:20:50.830892	113	::1	\N	Web	9cc81f3ee5134bd09b5307a5e3de730f	\N	POST	/api/masterdata/article-categories	\N		200	{}	56b583a7c5214b5f832141cb93b705c3
3a20f40c-b4e5-e4f2-6158-876cbcbcecb9	KHHub.MasterDataService	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	admin	\N	\N	\N	\N	\N	\N	2026-05-01 11:50:37.852802	72	::1	\N	Web	8139734848f443d7b83e5a64812c50fb	\N	POST	/api/masterdata/articles	[\n  {\n    "code": null,\n    "message": "The ArticleCategory field is required.",\n    "details": null,\n    "data": {},\n    "validationErrors": null\n  }\n]		403	{}	d062984bbe6b4012af08885ae1defd9d
3a20f420-19e9-83bc-4ba7-2dcc62356cc9	KHHub.MasterDataService	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	admin	\N	\N	\N	\N	\N	\N	2026-05-01 12:11:48.838671	131	::1	\N	Web	36bdf9e149504793a11de4dc3255b9ab	\N	POST	/api/masterdata/articles	\N		200	{}	0cb6c71a5ae8424d9438dc85daaafbd3
3a20f420-9950-f8aa-d850-9321044f13fd	KHHub.MasterDataService	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	admin	\N	\N	\N	\N	\N	\N	2026-05-01 12:12:21.501321	83	::1	\N	Web	34baa868d549499b841c129da5b1022a	\N	PUT	/api/masterdata/articles/3a20f420-198b-0739-b05c-03c17b1df049	\N		200	{}	51e27e127ac94cfebbaf7b3f83e207a2
3a20f424-122f-dcae-a99e-278458eb3e7c	KHHub.MasterDataService	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	admin	\N	\N	\N	\N	\N	\N	2026-05-01 12:16:09.082432	52	::1	\N	Web	59cd4c1ee77c403b9ce5ffd164e735b2	\N	PUT	/api/masterdata/articles/3a20f420-198b-0739-b05c-03c17b1df049	\N		200	{}	5ae5a57848294f9e9a7a9df49dca3808
3a20f428-8cbc-d3f6-040f-a22c814cf84a	KHHub.AuthServer	\N	\N	\N	\N	\N	\N	\N	\N	2026-05-01 12:21:02.582344	66	::1	\N	\N	7bb5866b1a1b4fcdaf267542e814a06f	\N	POST	/api/account/dynamic-claims/refresh	[\n  {\n    "code": "Volo.Authorization:010001",\n    "message": "Authorization failed! Given policy has not granted.",\n    "details": null,\n    "data": null,\n    "validationErrors": null\n  }\n]		401	{}	6252d68e0548437da2b2062f71e44452
3a20f428-91cb-968c-a12c-8561cdd366e2	KHHub.AuthServer	\N	\N	\N	\N	\N	\N	\N	\N	2026-05-01 12:21:03.72894	218	::1	\N	\N	3e8851457569440b905c09e994957882	Microsoft ASP.NET Core OpenIdConnect handler	POST	/connect/token	\N		200	{}	5bcd3a6d92244b6dab4912e574d74d9d
3a20f437-1cc7-d26d-55e2-388adc73b422	KHHub.MasterDataService	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	admin	\N	\N	\N	\N	\N	\N	2026-05-01 12:36:56.998598	32	::1	\N	Web	6b343b4226c549dab597bdc910701873	\N	GET	/api/masterdata/articles/article-category-lookup	[\n  {\n    "code": null,\n    "message": "Có một lỗi nội bộ xảy ra trong quá trình thực hiện yêu cầu của bạn!",\n    "details": null,\n    "data": null,\n    "validationErrors": null\n  }\n]		500	{}	173564be2d2e4895a7acecd51544e9c3
3a20f43a-2c43-a27f-c13c-381d4eb9ab1b	KHHub.AdministrationService	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	admin	\N	\N	\N	\N	\N	\N	2026-05-01 12:40:17.492878	107	::1	\N	Web	dd9d07515d2b429fa9cda1e7e6c29ee5	\N	GET	/api/abp/application-localization	[\n  {\n    "code": null,\n    "message": "Có một lỗi nội bộ xảy ra trong quá trình thực hiện yêu cầu của bạn!",\n    "details": null,\n    "data": null,\n    "validationErrors": null\n  }\n]		500	{}	afae5c940a22436d8afa203b0a9e4032
3a20f45b-7e17-990c-9ce5-827fd85cbf25	KHHub.AdministrationService	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	admin	\N	\N	\N	\N	\N	\N	2026-05-01 13:16:41.193512	45	::1	\N	Web	5c556dac88244955bdaaca5902f7f236	\N	GET	/api/abp/application-configuration	[\n  {\n    "code": null,\n    "message": "Có một lỗi nội bộ xảy ra trong quá trình thực hiện yêu cầu của bạn!",\n    "details": null,\n    "data": null,\n    "validationErrors": null\n  }\n]		500	{}	a2a344a402be4a29aa63dcb9cbb52bf2
3a20f460-8e9e-ef23-90f8-dc086134efbd	KHHub.AuthServer	\N	\N	\N	\N	\N	\N	\N	\N	2026-05-01 13:22:13.129201	21	::1	\N	\N	de5be5ebcac84240b6970176dddb3c5b	\N	POST	/api/account/dynamic-claims/refresh	[\n  {\n    "code": "Volo.Authorization:010001",\n    "message": "Authorization failed! Given policy has not granted.",\n    "details": null,\n    "data": null,\n    "validationErrors": null\n  }\n]		401	{}	6592cddb89c24d8aa1839d2f23815dd1
3a20f460-9339-ba35-4be8-860b1042c0ea	KHHub.AuthServer	\N	\N	\N	\N	\N	\N	\N	\N	2026-05-01 13:22:14.103298	226	::1	\N	\N	a4d7ee5e80b64373961c5b52d50d873e	Microsoft ASP.NET Core OpenIdConnect handler	POST	/connect/token	\N		200	{}	c1aafd85b5dc441fae554b7f541ecd9f
3a20f46f-90ca-b480-6386-9ba333784116	KHHub.MasterDataService	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	admin	\N	\N	\N	\N	\N	\N	2026-05-01 13:38:36.627544	114	::1	\N	Web	bfaf3f6a2c6a407a8f7cf26a0c4bed12	\N	POST	/api/masterdata/article-tags	\N		200	{}	a26acdc27de141d2a13b98d22a73f6da
3a20f46f-cfef-06b4-afda-76c3a2eec4ac	KHHub.MasterDataService	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	admin	\N	\N	\N	\N	\N	\N	2026-05-01 13:38:52.856103	55	::1	\N	Web	bad5e46c4f174f36857bd09dc7300bfa	\N	POST	/api/masterdata/article-tag-mappings	\N		200	{}	98666f93529c4a5986efc3a45e59d9df
3a20f48b-8da5-c2ff-3c37-5f20547a92ff	KHHub.AuthServer	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	admin	\N	\N	\N	\N	\N	\N	2026-05-01 14:09:10.86148	86	::1	\N	Web	7ff59765965742f285311840c5706f49	\N	GET	/api/account/profile-picture-file/3a20e9c3-58f2-bbe9-d4ca-25656eaca802	[\n  {\n    "code": null,\n    "message": "Có một lỗi nội bộ xảy ra trong quá trình thực hiện yêu cầu của bạn!",\n    "details": null,\n    "data": null,\n    "validationErrors": null\n  }\n]		500	{}	de0b1435dfd04629a377a20ee5eb143d
3a20f492-9c05-bbac-ffc0-cb776418047e	KHHub.MasterDataService	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	admin	\N	\N	\N	\N	\N	\N	2026-05-01 14:16:53.318725	63	::1	\N	Web	f5681e3f096e49ebbe14bead25ff3359	\N	PUT	/api/masterdata/article-categories/3a20f3f1-702a-7f07-9196-92139196a7a2	\N		200	{}	d2b726a2cfe94b33b87d3c9b0e32a631
3a20f498-ef2e-9c2e-6981-0c74333db8d0	KHHub.AuthServer	\N	\N	\N	\N	\N	\N	\N	\N	2026-05-01 14:23:47.865444	21	::1	\N	\N	4ccb9f97ffe34630a2ddbf5956bab958	\N	POST	/api/account/dynamic-claims/refresh	[\n  {\n    "code": "Volo.Authorization:010001",\n    "message": "Authorization failed! Given policy has not granted.",\n    "details": null,\n    "data": null,\n    "validationErrors": null\n  }\n]		401	{}	f21a74c3a71f4cdf873adfe1ba1b6578
3a20f498-f394-0125-a6bf-fc75913a1724	KHHub.AuthServer	\N	\N	\N	\N	\N	\N	\N	\N	2026-05-01 14:23:48.805256	207	::1	\N	\N	e4d8461b33c64dd0994a537fa61b5027	Microsoft ASP.NET Core OpenIdConnect handler	POST	/connect/token	\N		200	{}	64839f29fd44411e8d0651f97e46b7fb
3a20f568-c18a-1ff8-c0b8-08bafd4dbbc5	KHHub.AuthServer	\N	\N	\N	\N	\N	\N	\N	\N	2026-05-01 18:10:46.874495	814	::1	\N	\N	0c6ff1e14935431f8dce2ef52bbd49db	Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36	POST	/Account/Login	\N		302	{}	1094dc20c1014bb7b4502b9e11cb2c43
3a20f568-c744-60bf-6640-f5b6362a62be	KHHub.AuthServer	\N	\N	\N	\N	\N	\N	\N	\N	2026-05-01 18:10:48.865618	291	::1	\N	\N	6410f9007c724146afd25ad00889fdba	Microsoft ASP.NET Core OpenIdConnect handler	POST	/connect/token	\N		200	{}	44a9fc00a6bc479d8c506dac6f376670
3a20f56f-a3e5-381c-4ba0-25b6d5fd7f25	KHHub.MasterDataService	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	admin	\N	\N	\N	\N	\N	\N	2026-05-01 18:18:18.741965	108	::1	\N	Web	2bf90155ff384ca58cd884f0c62b955f	\N	POST	/api/masterdata/articles	\N		200	{}	36c5c37485084acd8cb2c275f1d6da48
3a20f570-5cb1-2a77-9cd5-dfe4d982cdca	KHHub.MasterDataService	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	admin	\N	\N	\N	\N	\N	\N	2026-05-01 18:19:06.079033	82	::1	\N	Web	5097b45097ad4dfe9ad260183f13388f	\N	PUT	/api/masterdata/articles/3a20f56f-a3a5-1834-f0ee-bb939d7791d2	\N		200	{}	4a2101b44c334af8b70f2b0619ccbdfe
3a20f572-555d-b3b9-7630-78f5ee332188	KHHub.IdentityService	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	admin	\N	\N	\N	\N	\N	\N	2026-05-01 18:21:15.283177	72	::1	\N	Web	96d967859394464b9002d07a45aee740	\N	POST	/api/identity/roles	\N		200	{}	403363ffd13c415aafae1bb7a8e1025d
3a20f5a8-18a6-316e-3a08-0e29806c43f4	KHHub.AuthServer	\N	\N	\N	\N	\N	\N	\N	\N	2026-05-01 19:19:58.611606	139	::1	\N	\N	a24d93d24f3a4c449552f3f8dcc7a8fe	\N	POST	/api/account/dynamic-claims/refresh	[\n  {\n    "code": "Volo.Authorization:010001",\n    "message": "Authorization failed! Given policy has not granted.",\n    "details": null,\n    "data": null,\n    "validationErrors": null\n  }\n]		401	{}	3d60f1c0abbc4460aa08cd0275753f13
3a20f5a8-1e06-f71e-9e23-fc50127336a9	KHHub.AuthServer	\N	\N	\N	\N	\N	\N	\N	\N	2026-05-01 19:19:59.901097	233	::1	\N	\N	7e3b16b2486a4ed1944a03b85a94e3aa	Microsoft ASP.NET Core OpenIdConnect handler	POST	/connect/token	\N		200	{}	9a8639b529534f66b3cce63ef55afecb
\.


--
-- Data for Name: AbpEntityChanges; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpEntityChanges" ("Id", "AuditLogId", "TenantId", "ChangeTime", "ChangeType", "EntityTenantId", "EntityId", "EntityTypeFullName", "ExtraProperties") FROM stdin;
\.


--
-- Data for Name: AbpEntityPropertyChanges; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpEntityPropertyChanges" ("Id", "TenantId", "EntityChangeId", "NewValue", "OriginalValue", "PropertyName", "PropertyTypeFullName") FROM stdin;
\.


--
-- Data for Name: AbpEventInbox; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpEventInbox" ("Id", "ExtraProperties", "MessageId", "EventName", "EventData", "CreationTime", "Status", "HandledTime", "RetryCount", "NextRetryTime") FROM stdin;
\.


--
-- Data for Name: AbpEventOutbox; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpEventOutbox" ("Id", "ExtraProperties", "EventName", "EventData", "CreationTime") FROM stdin;
\.


--
-- Data for Name: __AuditLoggingService_Migrations; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."__AuditLoggingService_Migrations" ("MigrationId", "ProductVersion") FROM stdin;
20260429043429_Initial	10.0.2
\.


--
-- Name: AbpAuditLogActions PK_AbpAuditLogActions; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpAuditLogActions"
    ADD CONSTRAINT "PK_AbpAuditLogActions" PRIMARY KEY ("Id");


--
-- Name: AbpAuditLogExcelFiles PK_AbpAuditLogExcelFiles; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpAuditLogExcelFiles"
    ADD CONSTRAINT "PK_AbpAuditLogExcelFiles" PRIMARY KEY ("Id");


--
-- Name: AbpAuditLogs PK_AbpAuditLogs; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpAuditLogs"
    ADD CONSTRAINT "PK_AbpAuditLogs" PRIMARY KEY ("Id");


--
-- Name: AbpEntityChanges PK_AbpEntityChanges; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpEntityChanges"
    ADD CONSTRAINT "PK_AbpEntityChanges" PRIMARY KEY ("Id");


--
-- Name: AbpEntityPropertyChanges PK_AbpEntityPropertyChanges; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpEntityPropertyChanges"
    ADD CONSTRAINT "PK_AbpEntityPropertyChanges" PRIMARY KEY ("Id");


--
-- Name: AbpEventInbox PK_AbpEventInbox; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpEventInbox"
    ADD CONSTRAINT "PK_AbpEventInbox" PRIMARY KEY ("Id");


--
-- Name: AbpEventOutbox PK_AbpEventOutbox; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpEventOutbox"
    ADD CONSTRAINT "PK_AbpEventOutbox" PRIMARY KEY ("Id");


--
-- Name: __AuditLoggingService_Migrations PK___AuditLoggingService_Migrations; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."__AuditLoggingService_Migrations"
    ADD CONSTRAINT "PK___AuditLoggingService_Migrations" PRIMARY KEY ("MigrationId");


--
-- Name: IX_AbpAuditLogActions_AuditLogId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpAuditLogActions_AuditLogId" ON public."AbpAuditLogActions" USING btree ("AuditLogId");


--
-- Name: IX_AbpAuditLogActions_TenantId_ServiceName_MethodName_Executio~; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpAuditLogActions_TenantId_ServiceName_MethodName_Executio~" ON public."AbpAuditLogActions" USING btree ("TenantId", "ServiceName", "MethodName", "ExecutionTime");


--
-- Name: IX_AbpAuditLogs_TenantId_ExecutionTime; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpAuditLogs_TenantId_ExecutionTime" ON public."AbpAuditLogs" USING btree ("TenantId", "ExecutionTime");


--
-- Name: IX_AbpAuditLogs_TenantId_UserId_ExecutionTime; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpAuditLogs_TenantId_UserId_ExecutionTime" ON public."AbpAuditLogs" USING btree ("TenantId", "UserId", "ExecutionTime");


--
-- Name: IX_AbpEntityChanges_AuditLogId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpEntityChanges_AuditLogId" ON public."AbpEntityChanges" USING btree ("AuditLogId");


--
-- Name: IX_AbpEntityChanges_TenantId_EntityTypeFullName_EntityId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpEntityChanges_TenantId_EntityTypeFullName_EntityId" ON public."AbpEntityChanges" USING btree ("TenantId", "EntityTypeFullName", "EntityId");


--
-- Name: IX_AbpEntityPropertyChanges_EntityChangeId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpEntityPropertyChanges_EntityChangeId" ON public."AbpEntityPropertyChanges" USING btree ("EntityChangeId");


--
-- Name: IX_AbpEventInbox_MessageId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpEventInbox_MessageId" ON public."AbpEventInbox" USING btree ("MessageId");


--
-- Name: IX_AbpEventInbox_Status_CreationTime; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpEventInbox_Status_CreationTime" ON public."AbpEventInbox" USING btree ("Status", "CreationTime");


--
-- Name: IX_AbpEventOutbox_CreationTime; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpEventOutbox_CreationTime" ON public."AbpEventOutbox" USING btree ("CreationTime");


--
-- Name: AbpAuditLogActions FK_AbpAuditLogActions_AbpAuditLogs_AuditLogId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpAuditLogActions"
    ADD CONSTRAINT "FK_AbpAuditLogActions_AbpAuditLogs_AuditLogId" FOREIGN KEY ("AuditLogId") REFERENCES public."AbpAuditLogs"("Id") ON DELETE CASCADE;


--
-- Name: AbpEntityChanges FK_AbpEntityChanges_AbpAuditLogs_AuditLogId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpEntityChanges"
    ADD CONSTRAINT "FK_AbpEntityChanges_AbpAuditLogs_AuditLogId" FOREIGN KEY ("AuditLogId") REFERENCES public."AbpAuditLogs"("Id") ON DELETE CASCADE;


--
-- Name: AbpEntityPropertyChanges FK_AbpEntityPropertyChanges_AbpEntityChanges_EntityChangeId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpEntityPropertyChanges"
    ADD CONSTRAINT "FK_AbpEntityPropertyChanges_AbpEntityChanges_EntityChangeId" FOREIGN KEY ("EntityChangeId") REFERENCES public."AbpEntityChanges"("Id") ON DELETE CASCADE;


--
-- PostgreSQL database dump complete
--

\unrestrict 0W9d0yKbK5Rgq7CldbU4vSTCDbXaRDuRYBkTNMlJTqGogj0QR0z6J9kFhT2FNO0

--
-- Database "KHHub_BlobStoring" dump
--

--
-- PostgreSQL database dump
--

\restrict EORMlWq6rBGhdf9UhOot9w2bp5chvp4iGpSiUgg0oCWnSGLSNbShgcqagmIrZqd

-- Dumped from database version 14.22
-- Dumped by pg_dump version 14.22

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: KHHub_BlobStoring; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE "KHHub_BlobStoring" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'en_US.utf8';


ALTER DATABASE "KHHub_BlobStoring" OWNER TO postgres;

\unrestrict EORMlWq6rBGhdf9UhOot9w2bp5chvp4iGpSiUgg0oCWnSGLSNbShgcqagmIrZqd
\connect "KHHub_BlobStoring"
\restrict EORMlWq6rBGhdf9UhOot9w2bp5chvp4iGpSiUgg0oCWnSGLSNbShgcqagmIrZqd

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: AbpBlobContainers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpBlobContainers" (
    "Id" uuid NOT NULL,
    "TenantId" uuid,
    "Name" character varying(128) NOT NULL,
    "ExtraProperties" text NOT NULL,
    "ConcurrencyStamp" character varying(40) NOT NULL
);


ALTER TABLE public."AbpBlobContainers" OWNER TO postgres;

--
-- Name: AbpBlobs; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpBlobs" (
    "Id" uuid NOT NULL,
    "ContainerId" uuid NOT NULL,
    "TenantId" uuid,
    "Name" character varying(256) NOT NULL,
    "Content" bytea,
    "ExtraProperties" text NOT NULL,
    "ConcurrencyStamp" character varying(40) NOT NULL
);


ALTER TABLE public."AbpBlobs" OWNER TO postgres;

--
-- Name: __AbpBlobStoring_Migrations; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."__AbpBlobStoring_Migrations" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__AbpBlobStoring_Migrations" OWNER TO postgres;

--
-- Data for Name: AbpBlobContainers; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpBlobContainers" ("Id", "TenantId", "Name", "ExtraProperties", "ConcurrencyStamp") FROM stdin;
\.


--
-- Data for Name: AbpBlobs; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpBlobs" ("Id", "ContainerId", "TenantId", "Name", "Content", "ExtraProperties", "ConcurrencyStamp") FROM stdin;
\.


--
-- Data for Name: __AbpBlobStoring_Migrations; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."__AbpBlobStoring_Migrations" ("MigrationId", "ProductVersion") FROM stdin;
20260429043408_Initial	10.0.2
\.


--
-- Name: AbpBlobContainers PK_AbpBlobContainers; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpBlobContainers"
    ADD CONSTRAINT "PK_AbpBlobContainers" PRIMARY KEY ("Id");


--
-- Name: AbpBlobs PK_AbpBlobs; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpBlobs"
    ADD CONSTRAINT "PK_AbpBlobs" PRIMARY KEY ("Id");


--
-- Name: __AbpBlobStoring_Migrations PK___AbpBlobStoring_Migrations; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."__AbpBlobStoring_Migrations"
    ADD CONSTRAINT "PK___AbpBlobStoring_Migrations" PRIMARY KEY ("MigrationId");


--
-- Name: IX_AbpBlobContainers_TenantId_Name; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpBlobContainers_TenantId_Name" ON public."AbpBlobContainers" USING btree ("TenantId", "Name");


--
-- Name: IX_AbpBlobs_ContainerId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpBlobs_ContainerId" ON public."AbpBlobs" USING btree ("ContainerId");


--
-- Name: IX_AbpBlobs_TenantId_ContainerId_Name; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpBlobs_TenantId_ContainerId_Name" ON public."AbpBlobs" USING btree ("TenantId", "ContainerId", "Name");


--
-- Name: AbpBlobs FK_AbpBlobs_AbpBlobContainers_ContainerId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpBlobs"
    ADD CONSTRAINT "FK_AbpBlobs_AbpBlobContainers_ContainerId" FOREIGN KEY ("ContainerId") REFERENCES public."AbpBlobContainers"("Id") ON DELETE CASCADE;


--
-- PostgreSQL database dump complete
--

\unrestrict EORMlWq6rBGhdf9UhOot9w2bp5chvp4iGpSiUgg0oCWnSGLSNbShgcqagmIrZqd

--
-- Database "KHHub_Gdpr" dump
--

--
-- PostgreSQL database dump
--

\restrict oTsM1IecZKp8tJe89PVdyX3ctJvSmhikchSeMytBSM2zsJwRzSpYIuI6MYzfG7d

-- Dumped from database version 14.22
-- Dumped by pg_dump version 14.22

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: KHHub_Gdpr; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE "KHHub_Gdpr" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'en_US.utf8';


ALTER DATABASE "KHHub_Gdpr" OWNER TO postgres;

\unrestrict oTsM1IecZKp8tJe89PVdyX3ctJvSmhikchSeMytBSM2zsJwRzSpYIuI6MYzfG7d
\connect "KHHub_Gdpr"
\restrict oTsM1IecZKp8tJe89PVdyX3ctJvSmhikchSeMytBSM2zsJwRzSpYIuI6MYzfG7d

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: AbpEventInbox; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpEventInbox" (
    "Id" uuid NOT NULL,
    "ExtraProperties" text NOT NULL,
    "MessageId" text NOT NULL,
    "EventName" character varying(256) NOT NULL,
    "EventData" bytea NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL,
    "Status" integer NOT NULL,
    "HandledTime" timestamp without time zone,
    "RetryCount" integer NOT NULL,
    "NextRetryTime" timestamp without time zone
);


ALTER TABLE public."AbpEventInbox" OWNER TO postgres;

--
-- Name: AbpEventOutbox; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpEventOutbox" (
    "Id" uuid NOT NULL,
    "ExtraProperties" text NOT NULL,
    "EventName" character varying(256) NOT NULL,
    "EventData" bytea NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL
);


ALTER TABLE public."AbpEventOutbox" OWNER TO postgres;

--
-- Name: GdprInfo; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."GdprInfo" (
    "Id" uuid NOT NULL,
    "RequestId" uuid NOT NULL,
    "Data" text NOT NULL,
    "Provider" character varying(256) NOT NULL
);


ALTER TABLE public."GdprInfo" OWNER TO postgres;

--
-- Name: GdprRequests; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."GdprRequests" (
    "Id" uuid NOT NULL,
    "UserId" uuid NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL,
    "ReadyTime" timestamp without time zone NOT NULL,
    "ExtraProperties" text NOT NULL,
    "ConcurrencyStamp" character varying(40) NOT NULL
);


ALTER TABLE public."GdprRequests" OWNER TO postgres;

--
-- Name: __GdprService_Migrations; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."__GdprService_Migrations" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__GdprService_Migrations" OWNER TO postgres;

--
-- Data for Name: AbpEventInbox; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpEventInbox" ("Id", "ExtraProperties", "MessageId", "EventName", "EventData", "CreationTime", "Status", "HandledTime", "RetryCount", "NextRetryTime") FROM stdin;
\.


--
-- Data for Name: AbpEventOutbox; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpEventOutbox" ("Id", "ExtraProperties", "EventName", "EventData", "CreationTime") FROM stdin;
\.


--
-- Data for Name: GdprInfo; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."GdprInfo" ("Id", "RequestId", "Data", "Provider") FROM stdin;
\.


--
-- Data for Name: GdprRequests; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."GdprRequests" ("Id", "UserId", "CreationTime", "ReadyTime", "ExtraProperties", "ConcurrencyStamp") FROM stdin;
\.


--
-- Data for Name: __GdprService_Migrations; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."__GdprService_Migrations" ("MigrationId", "ProductVersion") FROM stdin;
20260429043437_Initial	10.0.2
\.


--
-- Name: AbpEventInbox PK_AbpEventInbox; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpEventInbox"
    ADD CONSTRAINT "PK_AbpEventInbox" PRIMARY KEY ("Id");


--
-- Name: AbpEventOutbox PK_AbpEventOutbox; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpEventOutbox"
    ADD CONSTRAINT "PK_AbpEventOutbox" PRIMARY KEY ("Id");


--
-- Name: GdprInfo PK_GdprInfo; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."GdprInfo"
    ADD CONSTRAINT "PK_GdprInfo" PRIMARY KEY ("Id");


--
-- Name: GdprRequests PK_GdprRequests; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."GdprRequests"
    ADD CONSTRAINT "PK_GdprRequests" PRIMARY KEY ("Id");


--
-- Name: __GdprService_Migrations PK___GdprService_Migrations; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."__GdprService_Migrations"
    ADD CONSTRAINT "PK___GdprService_Migrations" PRIMARY KEY ("MigrationId");


--
-- Name: IX_AbpEventInbox_MessageId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpEventInbox_MessageId" ON public."AbpEventInbox" USING btree ("MessageId");


--
-- Name: IX_AbpEventInbox_Status_CreationTime; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpEventInbox_Status_CreationTime" ON public."AbpEventInbox" USING btree ("Status", "CreationTime");


--
-- Name: IX_AbpEventOutbox_CreationTime; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpEventOutbox_CreationTime" ON public."AbpEventOutbox" USING btree ("CreationTime");


--
-- Name: IX_GdprInfo_RequestId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_GdprInfo_RequestId" ON public."GdprInfo" USING btree ("RequestId");


--
-- Name: IX_GdprRequests_UserId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_GdprRequests_UserId" ON public."GdprRequests" USING btree ("UserId");


--
-- Name: GdprInfo FK_GdprInfo_GdprRequests_RequestId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."GdprInfo"
    ADD CONSTRAINT "FK_GdprInfo_GdprRequests_RequestId" FOREIGN KEY ("RequestId") REFERENCES public."GdprRequests"("Id") ON DELETE CASCADE;


--
-- PostgreSQL database dump complete
--

\unrestrict oTsM1IecZKp8tJe89PVdyX3ctJvSmhikchSeMytBSM2zsJwRzSpYIuI6MYzfG7d

--
-- Database "KHHub_Identity" dump
--

--
-- PostgreSQL database dump
--

\restrict njwRDjtOcZBmWGNsu9aXuRDH3M72aiWKmOEGr97L4hQafb9OSgrpTD0LmOgLqIu

-- Dumped from database version 14.22
-- Dumped by pg_dump version 14.22

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: KHHub_Identity; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE "KHHub_Identity" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'en_US.utf8';


ALTER DATABASE "KHHub_Identity" OWNER TO postgres;

\unrestrict njwRDjtOcZBmWGNsu9aXuRDH3M72aiWKmOEGr97L4hQafb9OSgrpTD0LmOgLqIu
\connect "KHHub_Identity"
\restrict njwRDjtOcZBmWGNsu9aXuRDH3M72aiWKmOEGr97L4hQafb9OSgrpTD0LmOgLqIu

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: AbpClaimTypes; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpClaimTypes" (
    "Id" uuid NOT NULL,
    "Name" character varying(256) NOT NULL,
    "Required" boolean NOT NULL,
    "IsStatic" boolean NOT NULL,
    "Regex" character varying(512),
    "RegexDescription" character varying(128),
    "Description" character varying(256),
    "ValueType" integer NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL,
    "ExtraProperties" text NOT NULL,
    "ConcurrencyStamp" character varying(40) NOT NULL
);


ALTER TABLE public."AbpClaimTypes" OWNER TO postgres;

--
-- Name: AbpEventInbox; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpEventInbox" (
    "Id" uuid NOT NULL,
    "ExtraProperties" text NOT NULL,
    "MessageId" text NOT NULL,
    "EventName" character varying(256) NOT NULL,
    "EventData" bytea NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL,
    "Status" integer NOT NULL,
    "HandledTime" timestamp without time zone,
    "RetryCount" integer NOT NULL,
    "NextRetryTime" timestamp without time zone
);


ALTER TABLE public."AbpEventInbox" OWNER TO postgres;

--
-- Name: AbpEventOutbox; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpEventOutbox" (
    "Id" uuid NOT NULL,
    "ExtraProperties" text NOT NULL,
    "EventName" character varying(256) NOT NULL,
    "EventData" bytea NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL
);


ALTER TABLE public."AbpEventOutbox" OWNER TO postgres;

--
-- Name: AbpLinkUsers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpLinkUsers" (
    "Id" uuid NOT NULL,
    "SourceUserId" uuid NOT NULL,
    "SourceTenantId" uuid,
    "TargetUserId" uuid NOT NULL,
    "TargetTenantId" uuid
);


ALTER TABLE public."AbpLinkUsers" OWNER TO postgres;

--
-- Name: AbpOrganizationUnitRoles; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpOrganizationUnitRoles" (
    "RoleId" uuid NOT NULL,
    "OrganizationUnitId" uuid NOT NULL,
    "TenantId" uuid,
    "CreationTime" timestamp without time zone NOT NULL,
    "CreatorId" uuid
);


ALTER TABLE public."AbpOrganizationUnitRoles" OWNER TO postgres;

--
-- Name: AbpOrganizationUnits; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpOrganizationUnits" (
    "Id" uuid NOT NULL,
    "TenantId" uuid,
    "ParentId" uuid,
    "Code" character varying(95) NOT NULL,
    "DisplayName" character varying(128) NOT NULL,
    "EntityVersion" integer NOT NULL,
    "ExtraProperties" text NOT NULL,
    "ConcurrencyStamp" character varying(40) NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL,
    "CreatorId" uuid,
    "LastModificationTime" timestamp without time zone,
    "LastModifierId" uuid,
    "IsDeleted" boolean DEFAULT false NOT NULL,
    "DeleterId" uuid,
    "DeletionTime" timestamp without time zone
);


ALTER TABLE public."AbpOrganizationUnits" OWNER TO postgres;

--
-- Name: AbpRoleClaims; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpRoleClaims" (
    "Id" uuid NOT NULL,
    "RoleId" uuid NOT NULL,
    "TenantId" uuid,
    "ClaimType" character varying(256) NOT NULL,
    "ClaimValue" character varying(1024)
);


ALTER TABLE public."AbpRoleClaims" OWNER TO postgres;

--
-- Name: AbpRoles; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpRoles" (
    "Id" uuid NOT NULL,
    "TenantId" uuid,
    "Name" character varying(256) NOT NULL,
    "NormalizedName" character varying(256) NOT NULL,
    "IsDefault" boolean NOT NULL,
    "IsStatic" boolean NOT NULL,
    "IsPublic" boolean NOT NULL,
    "EntityVersion" integer NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL,
    "ExtraProperties" text NOT NULL,
    "ConcurrencyStamp" character varying(40) NOT NULL
);


ALTER TABLE public."AbpRoles" OWNER TO postgres;

--
-- Name: AbpSecurityLogs; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpSecurityLogs" (
    "Id" uuid NOT NULL,
    "TenantId" uuid,
    "ApplicationName" character varying(96),
    "Identity" character varying(96),
    "Action" character varying(96),
    "UserId" uuid,
    "UserName" character varying(256),
    "TenantName" character varying(64),
    "ClientId" character varying(64),
    "CorrelationId" character varying(64),
    "ClientIpAddress" character varying(64),
    "BrowserInfo" character varying(512),
    "CreationTime" timestamp without time zone NOT NULL,
    "ExtraProperties" text NOT NULL,
    "ConcurrencyStamp" character varying(40) NOT NULL
);


ALTER TABLE public."AbpSecurityLogs" OWNER TO postgres;

--
-- Name: AbpSessions; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpSessions" (
    "Id" uuid NOT NULL,
    "SessionId" character varying(128) NOT NULL,
    "Device" character varying(64) NOT NULL,
    "DeviceInfo" character varying(256),
    "TenantId" uuid,
    "UserId" uuid NOT NULL,
    "ClientId" character varying(64),
    "IpAddresses" character varying(2048),
    "SignedIn" timestamp without time zone NOT NULL,
    "LastAccessed" timestamp without time zone,
    "ExtraProperties" text
);


ALTER TABLE public."AbpSessions" OWNER TO postgres;

--
-- Name: AbpUserClaims; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpUserClaims" (
    "Id" uuid NOT NULL,
    "UserId" uuid NOT NULL,
    "TenantId" uuid,
    "ClaimType" character varying(256) NOT NULL,
    "ClaimValue" character varying(1024)
);


ALTER TABLE public."AbpUserClaims" OWNER TO postgres;

--
-- Name: AbpUserDelegations; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpUserDelegations" (
    "Id" uuid NOT NULL,
    "TenantId" uuid,
    "SourceUserId" uuid NOT NULL,
    "TargetUserId" uuid NOT NULL,
    "StartTime" timestamp without time zone NOT NULL,
    "EndTime" timestamp without time zone NOT NULL
);


ALTER TABLE public."AbpUserDelegations" OWNER TO postgres;

--
-- Name: AbpUserInvitations; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpUserInvitations" (
    "Id" uuid NOT NULL,
    "InviterTenantId" uuid,
    "InviteeEmail" text,
    "InvitationDate" timestamp with time zone NOT NULL,
    "AssignedRoles" text[],
    "Status" integer NOT NULL,
    "ExtraProperties" text NOT NULL,
    "ConcurrencyStamp" character varying(40) NOT NULL
);


ALTER TABLE public."AbpUserInvitations" OWNER TO postgres;

--
-- Name: AbpUserLogins; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpUserLogins" (
    "UserId" uuid NOT NULL,
    "LoginProvider" character varying(64) NOT NULL,
    "TenantId" uuid,
    "ProviderKey" character varying(196) NOT NULL,
    "ProviderDisplayName" character varying(128)
);


ALTER TABLE public."AbpUserLogins" OWNER TO postgres;

--
-- Name: AbpUserOrganizationUnits; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpUserOrganizationUnits" (
    "UserId" uuid NOT NULL,
    "OrganizationUnitId" uuid NOT NULL,
    "TenantId" uuid,
    "CreationTime" timestamp without time zone NOT NULL,
    "CreatorId" uuid
);


ALTER TABLE public."AbpUserOrganizationUnits" OWNER TO postgres;

--
-- Name: AbpUserPasskeys; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpUserPasskeys" (
    "CredentialId" bytea NOT NULL,
    "TenantId" uuid,
    "UserId" uuid NOT NULL,
    "Data" jsonb
);


ALTER TABLE public."AbpUserPasskeys" OWNER TO postgres;

--
-- Name: AbpUserPasswordHistories; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpUserPasswordHistories" (
    "UserId" uuid NOT NULL,
    "Password" character varying(256) NOT NULL,
    "TenantId" uuid,
    "CreatedAt" timestamp with time zone NOT NULL
);


ALTER TABLE public."AbpUserPasswordHistories" OWNER TO postgres;

--
-- Name: AbpUserRoles; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpUserRoles" (
    "UserId" uuid NOT NULL,
    "RoleId" uuid NOT NULL,
    "TenantId" uuid
);


ALTER TABLE public."AbpUserRoles" OWNER TO postgres;

--
-- Name: AbpUserTokens; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpUserTokens" (
    "UserId" uuid NOT NULL,
    "LoginProvider" character varying(64) NOT NULL,
    "Name" character varying(128) NOT NULL,
    "TenantId" uuid,
    "Value" text
);


ALTER TABLE public."AbpUserTokens" OWNER TO postgres;

--
-- Name: AbpUsers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpUsers" (
    "Id" uuid NOT NULL,
    "TenantId" uuid,
    "UserName" character varying(256) NOT NULL,
    "NormalizedUserName" character varying(256) NOT NULL,
    "Name" character varying(64),
    "Surname" character varying(64),
    "Email" character varying(256) NOT NULL,
    "NormalizedEmail" character varying(256) NOT NULL,
    "EmailConfirmed" boolean DEFAULT false NOT NULL,
    "PasswordHash" character varying(256),
    "SecurityStamp" character varying(256) NOT NULL,
    "IsExternal" boolean DEFAULT false NOT NULL,
    "PhoneNumber" character varying(16),
    "PhoneNumberConfirmed" boolean DEFAULT false NOT NULL,
    "IsActive" boolean NOT NULL,
    "TwoFactorEnabled" boolean DEFAULT false NOT NULL,
    "LockoutEnd" timestamp with time zone,
    "LockoutEnabled" boolean DEFAULT false NOT NULL,
    "AccessFailedCount" integer DEFAULT 0 NOT NULL,
    "ShouldChangePasswordOnNextLogin" boolean NOT NULL,
    "EntityVersion" integer NOT NULL,
    "LastPasswordChangeTime" timestamp with time zone,
    "LastSignInTime" timestamp with time zone,
    "Leaved" boolean DEFAULT false NOT NULL,
    "ExtraProperties" text NOT NULL,
    "ConcurrencyStamp" character varying(40) NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL,
    "CreatorId" uuid,
    "LastModificationTime" timestamp without time zone,
    "LastModifierId" uuid,
    "IsDeleted" boolean DEFAULT false NOT NULL,
    "DeleterId" uuid,
    "DeletionTime" timestamp without time zone
);


ALTER TABLE public."AbpUsers" OWNER TO postgres;

--
-- Name: OpenIddictApplications; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."OpenIddictApplications" (
    "Id" uuid NOT NULL,
    "ApplicationType" character varying(50),
    "ClientId" character varying(100),
    "ClientSecret" text,
    "ClientType" character varying(50),
    "ConsentType" character varying(50),
    "DisplayName" text,
    "DisplayNames" text,
    "JsonWebKeySet" text,
    "Permissions" text,
    "PostLogoutRedirectUris" text,
    "Properties" text,
    "RedirectUris" text,
    "Requirements" text,
    "Settings" text,
    "FrontChannelLogoutUri" text,
    "ClientUri" text,
    "LogoUri" text,
    "ExtraProperties" text NOT NULL,
    "ConcurrencyStamp" character varying(40) NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL,
    "CreatorId" uuid,
    "LastModificationTime" timestamp without time zone,
    "LastModifierId" uuid,
    "IsDeleted" boolean DEFAULT false NOT NULL,
    "DeleterId" uuid,
    "DeletionTime" timestamp without time zone
);


ALTER TABLE public."OpenIddictApplications" OWNER TO postgres;

--
-- Name: OpenIddictAuthorizations; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."OpenIddictAuthorizations" (
    "Id" uuid NOT NULL,
    "ApplicationId" uuid,
    "CreationDate" timestamp without time zone,
    "Properties" text,
    "Scopes" text,
    "Status" character varying(50),
    "Subject" character varying(400),
    "Type" character varying(50),
    "ExtraProperties" text NOT NULL,
    "ConcurrencyStamp" character varying(40) NOT NULL
);


ALTER TABLE public."OpenIddictAuthorizations" OWNER TO postgres;

--
-- Name: OpenIddictScopes; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."OpenIddictScopes" (
    "Id" uuid NOT NULL,
    "Description" text,
    "Descriptions" text,
    "DisplayName" text,
    "DisplayNames" text,
    "Name" character varying(200),
    "Properties" text,
    "Resources" text,
    "ExtraProperties" text NOT NULL,
    "ConcurrencyStamp" character varying(40) NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL,
    "CreatorId" uuid,
    "LastModificationTime" timestamp without time zone,
    "LastModifierId" uuid,
    "IsDeleted" boolean DEFAULT false NOT NULL,
    "DeleterId" uuid,
    "DeletionTime" timestamp without time zone
);


ALTER TABLE public."OpenIddictScopes" OWNER TO postgres;

--
-- Name: OpenIddictTokens; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."OpenIddictTokens" (
    "Id" uuid NOT NULL,
    "ApplicationId" uuid,
    "AuthorizationId" uuid,
    "CreationDate" timestamp without time zone,
    "ExpirationDate" timestamp without time zone,
    "Payload" text,
    "Properties" text,
    "RedemptionDate" timestamp without time zone,
    "ReferenceId" character varying(100),
    "Status" character varying(50),
    "Subject" character varying(400),
    "Type" character varying(150),
    "ExtraProperties" text NOT NULL,
    "ConcurrencyStamp" character varying(40) NOT NULL
);


ALTER TABLE public."OpenIddictTokens" OWNER TO postgres;

--
-- Name: __IdentityService_Migrations; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."__IdentityService_Migrations" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__IdentityService_Migrations" OWNER TO postgres;

--
-- Data for Name: AbpClaimTypes; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpClaimTypes" ("Id", "Name", "Required", "IsStatic", "Regex", "RegexDescription", "Description", "ValueType", "CreationTime", "ExtraProperties", "ConcurrencyStamp") FROM stdin;
\.


--
-- Data for Name: AbpEventInbox; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpEventInbox" ("Id", "ExtraProperties", "MessageId", "EventName", "EventData", "CreationTime", "Status", "HandledTime", "RetryCount", "NextRetryTime") FROM stdin;
3a20f568-1bff-8d22-4eaa-f2d7d02fabb4	{"X-Correlation-Id":null}	3a20f568162ce60f09dfe90fb348e6ec	abp.permission-management.dynamic-permission-definitions-changed	\\x7b227065726d697373696f6e73223a5b2241756469744c6f6767696e672e566965774368616e6765486973746f72793a566f6c6f2e4162702e4964656e746974792e4964656e74697479526f6c65222c2241756469744c6f6767696e672e566965774368616e6765486973746f72793a566f6c6f2e4162702e4964656e746974792e4964656e7469747955736572225d7d	2026-05-01 18:10:05.311729	2	2026-05-01 18:10:06.976585	0	\N
3a20f591-600e-5ad4-0826-d325225764c9	{"X-Correlation-Id":null}	3a20f5915ae5e159e6467b4afa941075	abp.permission-management.dynamic-permission-definitions-changed	\\x7b227065726d697373696f6e73223a5b224d617374657244617461536572766963652e4d6564696146696c6573222c224d617374657244617461536572766963652e4d6564696146696c65732e437265617465222c224d617374657244617461536572766963652e4d6564696146696c65732e45646974222c224d617374657244617461536572766963652e4d6564696146696c65732e44656c657465222c224d617374657244617461536572766963652e506c61636543617465676f72696573222c224d617374657244617461536572766963652e506c61636543617465676f726965732e437265617465222c224d617374657244617461536572766963652e506c61636543617465676f726965732e45646974222c224d617374657244617461536572766963652e506c61636543617465676f726965732e44656c657465222c224d617374657244617461536572766963652e506c61636554616773222c224d617374657244617461536572766963652e506c616365546167732e437265617465222c224d617374657244617461536572766963652e506c616365546167732e45646974222c224d617374657244617461536572766963652e506c616365546167732e44656c657465222c224d617374657244617461536572766963652e506c61636573222c224d617374657244617461536572766963652e506c616365732e437265617465222c224d617374657244617461536572766963652e506c616365732e45646974222c224d617374657244617461536572766963652e506c616365732e44656c657465222c224d617374657244617461536572766963652e506c6163655461674d617070696e6773222c224d617374657244617461536572766963652e506c6163655461674d617070696e67732e437265617465222c224d617374657244617461536572766963652e506c6163655461674d617070696e67732e45646974222c224d617374657244617461536572766963652e506c6163655461674d617070696e67732e44656c657465222c224d617374657244617461536572766963652e456e7469747946696c6573222c224d617374657244617461536572766963652e456e7469747946696c65732e437265617465222c224d617374657244617461536572766963652e456e7469747946696c65732e45646974222c224d617374657244617461536572766963652e456e7469747946696c65732e44656c657465222c224d617374657244617461536572766963652e506c61636552657669657773222c224d617374657244617461536572766963652e506c616365526576696577732e437265617465222c224d617374657244617461536572766963652e506c616365526576696577732e45646974222c224d617374657244617461536572766963652e506c616365526576696577732e44656c657465222c224d617374657244617461536572766963652e506c6163654661766f7269746573222c224d617374657244617461536572766963652e506c6163654661766f72697465732e437265617465222c224d617374657244617461536572766963652e506c6163654661766f72697465732e45646974222c224d617374657244617461536572766963652e506c6163654661766f72697465732e44656c657465222c224d617374657244617461536572766963652e506c6163655669657773222c224d617374657244617461536572766963652e506c61636556696577732e437265617465222c224d617374657244617461536572766963652e506c61636556696577732e45646974222c224d617374657244617461536572766963652e506c61636556696577732e44656c657465225d7d	2026-05-01 18:55:09.71273	2	2026-05-01 18:55:11.057907	0	\N
\.


--
-- Data for Name: AbpEventOutbox; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpEventOutbox" ("Id", "ExtraProperties", "EventName", "EventData", "CreationTime") FROM stdin;
\.


--
-- Data for Name: AbpLinkUsers; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpLinkUsers" ("Id", "SourceUserId", "SourceTenantId", "TargetUserId", "TargetTenantId") FROM stdin;
\.


--
-- Data for Name: AbpOrganizationUnitRoles; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpOrganizationUnitRoles" ("RoleId", "OrganizationUnitId", "TenantId", "CreationTime", "CreatorId") FROM stdin;
\.


--
-- Data for Name: AbpOrganizationUnits; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpOrganizationUnits" ("Id", "TenantId", "ParentId", "Code", "DisplayName", "EntityVersion", "ExtraProperties", "ConcurrencyStamp", "CreationTime", "CreatorId", "LastModificationTime", "LastModifierId", "IsDeleted", "DeleterId", "DeletionTime") FROM stdin;
\.


--
-- Data for Name: AbpRoleClaims; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpRoleClaims" ("Id", "RoleId", "TenantId", "ClaimType", "ClaimValue") FROM stdin;
\.


--
-- Data for Name: AbpRoles; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpRoles" ("Id", "TenantId", "Name", "NormalizedName", "IsDefault", "IsStatic", "IsPublic", "EntityVersion", "CreationTime", "ExtraProperties", "ConcurrencyStamp") FROM stdin;
3a20e9c3-5e55-c8a5-6d98-2c5c5927f057	\N	admin	ADMIN	f	t	t	2	2026-04-29 11:54:19.503862	{}	297714acfcc74a64aec770b3392f90ce
3a20f572-5532-401b-1e4c-60d8b781a6a3	\N	Người dùng	NGƯỜI DÙNG	t	f	t	0	2026-05-01 18:21:15.339613	{}	4b7eb144d1f44840a3a5639cff4b4b65
\.


--
-- Data for Name: AbpSecurityLogs; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpSecurityLogs" ("Id", "TenantId", "ApplicationName", "Identity", "Action", "UserId", "UserName", "TenantName", "ClientId", "CorrelationId", "ClientIpAddress", "BrowserInfo", "CreationTime", "ExtraProperties", "ConcurrencyStamp") FROM stdin;
3a20e9d0-3310-2062-fffe-69d58ec9198f	\N	KHHub.AuthServer	Identity	LoginSucceeded	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	admin	\N	\N	3c5f176bad3245688664ae46192b7e95	::1	Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36	2026-04-29 12:08:20.367584	{}	adedac35b6da4141b23d600485ce4ff6
3a20e9d3-5dd2-3f02-216d-9a52be46b79b	\N	KHHub.AuthServer	Identity	LoginSucceeded	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	admin	\N	\N	ef41dd71b475460980c3f8c7e42bbc6a	::1	Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36	2026-04-29 12:11:47.922856	{}	29b78f85e5284e2fb840d5ca307b9d75
3a20eef8-ad36-1836-f50b-4bc29f83ddd9	\N	KHHub.AuthServer	Identity	LoginSucceeded	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	admin	\N	\N	0685587333a84a9b92e82a2096be51d7	::1	Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36	2026-04-30 12:10:39.157929	{}	3a6f8616c69941a0837a0e474134b428
3a20f3f0-caaa-c0fa-48ba-27b41312bb97	\N	KHHub.AuthServer	Identity	LoginSucceeded	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	admin	\N	\N	d2d39d7826f0488d80f257aac24ce2c1	::1	Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36	2026-05-01 11:20:08.489904	{}	0c68e1fb03154a28bcafbdf46731bc79
3a20f568-c11a-99e5-0f65-6350ecc35682	\N	KHHub.AuthServer	Identity	LoginSucceeded	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	admin	\N	\N	0c6ff1e14935431f8dce2ef52bbd49db	::1	Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/147.0.0.0 Safari/537.36	2026-05-01 18:10:47.577411	{}	aa8a33113d534cc6bc29ec1d56bfdf86
\.


--
-- Data for Name: AbpSessions; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpSessions" ("Id", "SessionId", "Device", "DeviceInfo", "TenantId", "UserId", "ClientId", "IpAddresses", "SignedIn", "LastAccessed", "ExtraProperties") FROM stdin;
3a20f046-66ef-fd23-7849-7ef8d6982dc9	d3de6f32-5938-418d-b771-73887e4d0fc8	OAuth	Mac OS X Chrome	\N	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	Web	::1	2026-04-30 18:15:10.191573	2026-04-30 18:15:36.016861	{}
3a20f568-c389-3e87-b85d-9c9897de963b	69a1eafe-4622-4df5-b127-eb0697652fa7	OAuth	Mac OS X Chrome	\N	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	Web	::1	2026-05-01 18:10:48.20172	2026-05-01 19:06:27.570934	{}
3a20f3f0-cd1c-fef7-532b-98821296dbb3	b76b0858-19fd-44d9-b286-8f89369bf7ad	OAuth	Mac OS X Chrome	\N	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	Web	::1	2026-05-01 11:20:09.116363	2026-05-01 12:12:25.369753	{}
3a20f568-c0f4-f370-485b-5f3abec3a9bd	1807f4c5-10f8-408a-9f6c-963977e04ee5	Web	Mac OS X Chrome	\N	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	\N	::1	2026-05-01 18:10:47.540534	2026-05-01 19:19:59.443505	{}
3a20f5a8-1b7e-73f3-fa99-1b9458ea3928	5e33ec43-41ec-4f5a-b9a3-f5d56d8ee77c	OAuth	Mac OS X Chrome	\N	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	Web	::1	2026-05-01 19:19:59.486819	2026-05-01 20:18:05.698341	{}
3a20f428-8f5a-5a61-b4fb-7902ea9a0133	bc35b977-9ebe-4a85-9b7f-1b0955012bc2	OAuth	Mac OS X Chrome	\N	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	Web	::1	2026-05-01 12:21:03.3222	2026-05-01 13:13:44.643833	{}
3a20e9d3-5e92-d6d3-c319-d1ee844c7219	c9e28201-1f77-4e8c-af6a-927ec34825b7	OAuth	Mac OS X Chrome	\N	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	Web	::1	2026-04-29 12:11:48.114136	2026-04-29 12:11:48.46935	{}
3a20e9d3-5dcf-974b-2488-108810670cde	66362c3e-3104-4ed3-9e21-1e82cf6bce72	Web	Mac OS X Chrome	\N	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	\N	::1	2026-04-29 12:11:47.919089	2026-04-30 11:33:00.474406	{}
3a20f460-90c2-6603-6121-b9c042568d9b	9652c456-2923-4435-9887-4867abfcd7f6	OAuth	Mac OS X Chrome	\N	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	Web	::1	2026-05-01 13:22:13.698214	2026-05-01 14:20:33.90382	{}
3a20eef8-af0b-f30f-7623-96c1f5f3e280	410c1afc-b9f9-445b-bc14-d48dfbad5bc4	OAuth	Mac OS X Chrome	\N	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	Web	::1	2026-04-30 12:10:39.627686	2026-04-30 12:42:24.567708	{}
3a20efd5-5b39-ec6d-0b3b-ffe940ccabc7	5d5acdb4-b316-4d03-b5a0-95d7596cc2f6	OAuth	Mac OS X Chrome	\N	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	Web	::1	2026-04-30 16:11:41.625455	2026-04-30 16:11:41.984321	{}
3a20eef8-ad2d-0fba-8e8f-033c463d1e86	472a746a-1063-4575-bb9a-c98508751655	Web	Mac OS X Chrome	\N	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	\N	::1	2026-04-30 12:10:39.149267	2026-04-30 18:15:10.160468	{}
3a20f3f0-ca81-6b49-2adf-c0d0431fd744	6e825db9-880c-45a7-a9ed-b46534b4511a	Web	Mac OS X Chrome	\N	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	\N	::1	2026-05-01 11:20:08.449993	2026-05-01 14:23:48.370353	{}
3a20f498-f136-e98b-81b4-e3668043af6d	c41116f0-f6b3-4e71-b31b-56c92f522f20	OAuth	Mac OS X Chrome	\N	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	Web	::1	2026-05-01 14:23:48.40613	2026-05-01 14:41:23.125477	{}
\.


--
-- Data for Name: AbpUserClaims; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpUserClaims" ("Id", "UserId", "TenantId", "ClaimType", "ClaimValue") FROM stdin;
\.


--
-- Data for Name: AbpUserDelegations; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpUserDelegations" ("Id", "TenantId", "SourceUserId", "TargetUserId", "StartTime", "EndTime") FROM stdin;
\.


--
-- Data for Name: AbpUserInvitations; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpUserInvitations" ("Id", "InviterTenantId", "InviteeEmail", "InvitationDate", "AssignedRoles", "Status", "ExtraProperties", "ConcurrencyStamp") FROM stdin;
\.


--
-- Data for Name: AbpUserLogins; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpUserLogins" ("UserId", "LoginProvider", "TenantId", "ProviderKey", "ProviderDisplayName") FROM stdin;
\.


--
-- Data for Name: AbpUserOrganizationUnits; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpUserOrganizationUnits" ("UserId", "OrganizationUnitId", "TenantId", "CreationTime", "CreatorId") FROM stdin;
\.


--
-- Data for Name: AbpUserPasskeys; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpUserPasskeys" ("CredentialId", "TenantId", "UserId", "Data") FROM stdin;
\.


--
-- Data for Name: AbpUserPasswordHistories; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpUserPasswordHistories" ("UserId", "Password", "TenantId", "CreatedAt") FROM stdin;
\.


--
-- Data for Name: AbpUserRoles; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpUserRoles" ("UserId", "RoleId", "TenantId") FROM stdin;
3a20e9c3-58f2-bbe9-d4ca-25656eaca802	3a20e9c3-5e55-c8a5-6d98-2c5c5927f057	\N
\.


--
-- Data for Name: AbpUserTokens; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpUserTokens" ("UserId", "LoginProvider", "Name", "TenantId", "Value") FROM stdin;
\.


--
-- Data for Name: AbpUsers; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpUsers" ("Id", "TenantId", "UserName", "NormalizedUserName", "Name", "Surname", "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "SecurityStamp", "IsExternal", "PhoneNumber", "PhoneNumberConfirmed", "IsActive", "TwoFactorEnabled", "LockoutEnd", "LockoutEnabled", "AccessFailedCount", "ShouldChangePasswordOnNextLogin", "EntityVersion", "LastPasswordChangeTime", "LastSignInTime", "Leaved", "ExtraProperties", "ConcurrencyStamp", "CreationTime", "CreatorId", "LastModificationTime", "LastModifierId", "IsDeleted", "DeleterId", "DeletionTime") FROM stdin;
3a20e9c3-58f2-bbe9-d4ca-25656eaca802	\N	admin	ADMIN	admin	\N	admin@abp.io	ADMIN@ABP.IO	f	AQAAAAIAAYagAAAAEKd9mGk63AttgPLwkpM4hl4CUGCHTW7hpFvxNMXzZGx28UO3tlL3UOLNvql/j+efHQ==	YRPUEHJMFLLEY4IMSAF2IP4GK3IATP24	f	\N	f	t	f	\N	t	0	f	7	2026-04-29 04:54:18.283022+00	2026-05-01 11:10:47.628498+00	f	{}	a2e4e4a545b14aa983f7bd09240d2729	2026-04-29 11:54:19.139873	\N	2026-05-01 18:10:47.658222	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	f	\N	\N
\.


--
-- Data for Name: OpenIddictApplications; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."OpenIddictApplications" ("Id", "ApplicationType", "ClientId", "ClientSecret", "ClientType", "ConsentType", "DisplayName", "DisplayNames", "JsonWebKeySet", "Permissions", "PostLogoutRedirectUris", "Properties", "RedirectUris", "Requirements", "Settings", "FrontChannelLogoutUri", "ClientUri", "LogoUri", "ExtraProperties", "ConcurrencyStamp", "CreationTime", "CreatorId", "LastModificationTime", "LastModifierId", "IsDeleted", "DeleterId", "DeletionTime") FROM stdin;
3a20e9c3-6560-988d-e113-48c822c42f08	native	ReactNative	\N	public	implicit	ReactNative Mobile Application	\N	\N	["ept:end_session","gt:authorization_code","rst:code","ept:authorization","ept:token","ept:revocation","ept:introspection","gt:password","gt:client_credentials","gt:refresh_token","scp:address","scp:email","scp:phone","scp:profile","scp:roles","scp:AuthServer","scp:IdentityService","scp:AuditLoggingService","scp:GdprService","scp:LanguageService","scp:AIManagementService","scp:AdministrationService","scp:MasterDataService"]	["exp://localhost:19000/"]	\N	["exp://localhost:19000/"]	\N	\N	\N	\N	\N	{}	d7b14dc6b91f46dc9f2ba763ac8e0cde	2026-04-29 11:54:21.2847	\N	2026-04-30 11:46:34.111093	\N	f	\N	\N
3a20e9c3-6582-09b6-5856-bd9d5706108b	web	SwaggerTestUI	\N	public	implicit	Swagger Test Client	\N	\N	["ept:end_session","gt:authorization_code","rst:code","ept:authorization","ept:token","ept:revocation","ept:introspection","scp:address","scp:email","scp:phone","scp:profile","scp:roles","scp:AuthServer","scp:IdentityService","scp:AuditLoggingService","scp:GdprService","scp:AIManagementService","scp:LanguageService","scp:AdministrationService","scp:MasterDataService"]	\N	\N	["http://localhost:44394/swagger/oauth2-redirect.html","http://localhost:44369/swagger/oauth2-redirect.html","http://localhost:44315/swagger/oauth2-redirect.html","http://localhost:44333/swagger/oauth2-redirect.html","http://localhost:44390/swagger/oauth2-redirect.html","http://localhost:44355/swagger/oauth2-redirect.html","http://localhost:44362/swagger/oauth2-redirect.html","http://localhost:44350/swagger/oauth2-redirect.html","http://localhost:44396/swagger/oauth2-redirect.html","http://localhost:44381/swagger/oauth2-redirect.html"]	\N	\N	\N	http://localhost:44394	/images/clients/swagger.svg	{}	1128fbe73acb4d808bf3583fe5499306	2026-04-29 11:54:21.323096	\N	2026-04-30 11:46:34.298336	\N	f	\N	\N
3a20e9c3-64d3-3a2f-8837-2f81a4b56274	web	Web	AQAAAAEAACcQAAAAEFkAkgGzrbDBuysTiVyxNeh9jO8nQmRD3+FTvS6dRpRS3vxWC0TtDsr3qLQbdrgPfg==	confidential	implicit	Web Client	\N	\N	["rst:code id_token","ept:end_session","gt:authorization_code","rst:code","ept:authorization","ept:token","ept:revocation","ept:introspection","gt:implicit","rst:id_token","scp:address","scp:email","scp:phone","scp:profile","scp:roles","scp:AuthServer","scp:IdentityService","scp:AuditLoggingService","scp:GdprService","scp:LanguageService","scp:AIManagementService","scp:AdministrationService","scp:MasterDataService"]	["http://localhost:44347/signout-callback-oidc"]	\N	["http://localhost:44347/signin-oidc"]	\N	\N	\N	http://localhost:44347/	/images/clients/aspnetcore.svg	{}	a781d74c1ccf4c518fc16fb683ba0b63	2026-04-29 11:54:21.213547	\N	2026-05-01 18:09:59.988548	\N	f	\N	\N
\.


--
-- Data for Name: OpenIddictAuthorizations; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."OpenIddictAuthorizations" ("Id", "ApplicationId", "CreationDate", "Properties", "Scopes", "Status", "Subject", "Type", "ExtraProperties", "ConcurrencyStamp") FROM stdin;
3a20eef8-aed4-978c-4cf7-7e3be2a6ea06	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	2026-04-30 05:10:39.571936	\N	["openid","profile","roles","email","phone","AuthServer","IdentityService","AdministrationService","MasterDataService","AuditLoggingService","GdprService","AIManagementService","LanguageService"]	valid	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	permanent	{}	dbd0b43c68a641268422dedaf9463a9f
3a20e9d0-3539-f24a-9b74-eb447e49a1e7	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	2026-04-29 05:08:20.921086	\N	["openid","profile","roles","email","phone","AuthServer","IdentityService","AdministrationService","AuditLoggingService","GdprService","AIManagementService","LanguageService"]	valid	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	permanent	{}	7fd9fa2607e64029b25a9c0afad500e7
\.


--
-- Data for Name: OpenIddictScopes; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."OpenIddictScopes" ("Id", "Description", "Descriptions", "DisplayName", "DisplayNames", "Name", "Properties", "Resources", "ExtraProperties", "ConcurrencyStamp", "CreationTime", "CreatorId", "LastModificationTime", "LastModifierId", "IsDeleted", "DeleterId", "DeletionTime") FROM stdin;
3a20e9c3-6250-9363-fda4-fdab0088c346	\N	\N	AuthServer API	\N	AuthServer	\N	["AuthServer"]	{}	b70287c432dc4751aa74ded406df0a45	2026-04-29 11:54:20.55893	\N	\N	\N	f	\N	\N
3a20e9c3-62f7-bba2-7e35-96100029b9ef	\N	\N	IdentityService API	\N	IdentityService	\N	["IdentityService"]	{}	92ee26fb7d854b7f8751acdcfb716908	2026-04-29 11:54:20.752348	\N	\N	\N	f	\N	\N
3a20e9c3-6375-883a-1261-54fbf82edc48	\N	\N	AdministrationService API	\N	AdministrationService	\N	["AdministrationService"]	{}	1ba9cc93f8364016aea40321a318ad63	2026-04-29 11:54:20.791257	\N	\N	\N	f	\N	\N
3a20e9c3-63ab-a41c-e35d-2905525ece3c	\N	\N	AuditLoggingService API	\N	AuditLoggingService	\N	["AuditLoggingService"]	{}	aa250268f0b94171b8ae88771cc20b85	2026-04-29 11:54:20.849552	\N	\N	\N	f	\N	\N
3a20e9c3-6407-4cae-76dd-702566df4200	\N	\N	GdprService API	\N	GdprService	\N	["GdprService"]	{}	721b82ce959b4b86a82e184333c803b6	2026-04-29 11:54:20.951932	\N	\N	\N	f	\N	\N
3a20e9c3-6463-3c0e-7345-9c1773c599e9	\N	\N	AIManagementService API	\N	AIManagementService	\N	["AIManagementService"]	{}	1b6a39266fa44a768698279b06440e69	2026-04-29 11:54:21.029945	\N	\N	\N	f	\N	\N
3a20e9c3-648e-5241-d3ff-b9b630d6fb82	\N	\N	LanguageService API	\N	LanguageService	\N	["LanguageService"]	{}	055ec59cc0ae46a5b0b8511208100f04	2026-04-29 11:54:21.074612	\N	\N	\N	f	\N	\N
3a20eee2-9ccc-3ee0-57b9-d756380d589d	\N	\N	MasterDataService API	\N	MasterDataService	\N	["MasterDataService"]	{}	8b63a0ce86f447c5bb2d0874cfe76fbc	2026-04-30 11:46:33.213496	\N	\N	\N	f	\N	\N
\.


--
-- Data for Name: OpenIddictTokens; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."OpenIddictTokens" ("Id", "ApplicationId", "AuthorizationId", "CreationDate", "ExpirationDate", "Payload", "Properties", "RedemptionDate", "ReferenceId", "Status", "Subject", "Type", "ExtraProperties", "ConcurrencyStamp") FROM stdin;
3a20e9d0-366e-5131-e5e2-44b5275706bc	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20e9d0-3539-f24a-9b74-eb447e49a1e7	2026-04-29 05:08:21	2026-04-29 05:28:21	\N	\N	\N	\N	valid	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:ietf:params:oauth:token-type:id_token	{}	fa5c72243b1e4a1bb7c34b83de196fa2
3a20e9d0-359f-a46b-1d2c-c315324f21c8	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20e9d0-3539-f24a-9b74-eb447e49a1e7	2026-04-29 05:08:21	2026-04-29 05:13:21	eyJhbGciOiJSU0EtT0FFUCIsImVuYyI6IkEyNTZDQkMtSFM1MTIiLCJraWQiOiIwMzEwM0QyNEVERDExRjNBOTY1NzFCRUMxQTlBRDUzMUUwMkJDNkQ4IiwidHlwIjoib2lfYXVjK2p3dCIsImN0eSI6IkpXVCJ9.D97zmM2cxv_EgBibuCWdPHsWoqhBM8RAlEKi-5uwntPJX6DrqZ-L7GsjMCrdNJ__WoTVk4W1kZ4e5s_63mI1tst2Rg9p0MRlgXZVFxsdxyQOEvArC3WdSPVUnP0nZgX7rjH9-C1hGZOp4s0Cm6dWG_mwb-vb46xz-1DLBHTBLfP3lNLSy8EDiSXRZpFxU-nHhEHxss9C2cSn2IIzrstbgP4HnSYUbvExNC8MOQqAOL2FhrLs2M5kOmM4fjrwA1rsM3DUvREKYWcjBppH45j_-qoWw4lI7Kc6bUdZ0p5VzHaFvOHgE-zrmHPjhkl2_T_j39n7KMf9bZunSXwlU2hPrg74bLWGonC7tvcHUoHAyjn9CVjRtKrOD4pV5MpN7iphMnHFcthcLH8fYLrIReG9rvgtqKNA49JhotRUj4ToZxEBrjQM6Hh9yLiTllfP4FlJKGWFBmp-HCOHiTIYCVxSeqaSwAAwcXebKMJTCfhC-UKQc8NuJvv6NU9PgOdv0Ig9SLquU37rATq_mGaZXVPV1ZY8biO-qdn0qMk-uQmMp7BukfbVgAAm-gglW6oT7faoOkkfnhPeymO_GzKkNxDeXwhME6jVpLM67v5O4hKOYdWlbJjAek6MyMXrxj3ynFMi_r9ZFv4ojs2GWE5eDGRWoO1gL2rYkoE1pIlnssNw8Sw.P1N37c3zO9LwXlUJRAIQnw.iYkMSL52mlM9XLS4MmWxjYo7Lj5EdsKHjcpQpkRLFW7TqKFWCiHC3mW2d7fSKK0f1K4W3JcALR3Dthe0xc7TS5x5xbzMmeAUPs25c7Im8AZQXslpKk0Cllup0xymD7yTu4OHqRgntDF4zTlkm49JfJ0xcaS06-mnQmXXQK5HpW-FjD67nVEm4TpZesn2kF3etCIw1GZ7fpV-FelvXvJG77CVZKH31gjMxIKNFFurRdVZbDJkm6_R0PzuvPCXjDLyTyEkGr_xPCH5X1Sxh_fUPs0x2-J7TBENMAwH0rCY5BoduJ9bDWNwfXcDqOTnQi_AE0x7VJHyjerGFiEVDcX5CqcU2-LjP8podtv6ZbGig2fT-3wqPDXD6nVqmiC9tL76AsVG9RgLRrQ0TX2av9uYuJ4I-j7AgRrjqTrDhm2ogSBZvAWsZV5btEdX0DjDAaqXrhpMZ_RnxEumpfgFe6P40yLDyfctCCk0wUiLRzezauJRmk1EwV7oqiyAvl5Vr7WqpHwTpNSM9rvdeXdNMxtY6-vM_9-JxpZG_6EqJqr4F_pVzBGrvNKJkhJNCEEYDJDgDQa9__9Y58tHI_BVrqKQTcB_xuj6rUpj7gJ9bkQyL1L2O2i7Olp-_Crj5sHwzHcpgpaSUU9VFVXMeL54dYkPXfGnjGa0zovXx_SKlVmxl0RUCQPy9trGconT72_HiX8fLfiYE6kxMurImvdr2jV0Z7biwndXXlx0Hs0HtxdgoECKajUzFaBl-66TNPdcP71x9ruYJ8qpqRm76RYfzbF81sw1MWJGPrdT37JSf1g3DMbh0k3XVO2c2kErepoIZCM7ePXW1kqKUZbQO-rGj0p-Dmj4ggyqAptl37N5SSCpGC-OY0oRiIgiiAbdNV9EkEmYZ9norM9G2kuyZvDZlwW_j3JYmPku8i8okb9mNnt93ILBGcOwZYHI1Cih3WFL7ramSZG-Xe7smTj91GXV55hDw-zLjTN-_UmC96Brj9GCnAT5AYg__C94I53tvBmLG-QKkoBmCmb5SrsDdPgN7jejFFvOmq66XtwQwb_QLGoNtOkPwvRvJPWnUu3R_LB01Lt6OlEYsBaIGXM9I3mSJGyY4uv0B6xT-wzwHeHIpDiwOAK5fhrXJuUGCBfRlyq16NTTaoVc5AiL60G0h3Ne-ybiz3ucWLYNsekI3jk2ysWT9xnE7PsDP5g0fQAuSZJuRM8OJcZkh4NxSNPklF18EKjChpP_pNfRkCsyjdLI9u2bqPPxH4gFqcHWzUcHIVPOy5P1FzqQkY43IUz_DprtRaPH3MpfNx2-KpuYKfxNnTb3uLMpvsInnwwSv4bqh6o1UAtOOGhPPaf_cXUlWmbltzbjVxwtWIGk_85rm1H9uHZe13qU5DdxUBHp6qRQs_3Ux4vovQmlJhHjmQfFNCcpKrbLa3-Ho6T2-DG_2VXrpJ2yKOF1M-kv2uOZ2fjhrf16SnWtyMBqxGvWabxCkyRV5z5g2SlaDcxkmIUA3fGphHGYHkNuMeBJRMYXeCDUenEH38hgngcpWvZI5rrbDFFllR5aq-CzD0pk3g4Yq8ncJb1CiGKpA4WbzSnG8RnXhwHPte5umjnW-khZAkeG-f-eGjlInjp8flbI96f3IOcga-eZAxHxtXqBK4etEkYAYBi4hwyHHrqlgdj03fnZCN2LxCRrK4dLzQCH5FHt5stNSWIKnjgYJTIkt2DeaRdFw6kM7QcBKptKK98WnW3_Sh8TlzED9TXwI5R1EcjxDRpU2bl-zCQAx6oJVPKhpxkzYEMB9dhgyo-C-gQswBdIFyogXDTlWpjg7HvcJbsLbN_L6uln4abkg0W45ipTC6o4dzeszrdyBdTERJn3qwff7kVEh7Zif0MfPpiKCR2boA6rD_dX3E1kfh0Da5sizrV7g9BxhruypZtaENKpEzjjt-4S7da9y_sfmHSDeAuvECOE456NmUZh-aUhGdI8NfZraC2_fiDR_Qti75BkITtCIGgRMoMjh3zcksAaHDBEViAd8QHjfZg8UqiH-9rREm13vj7u6I5C1G7377daD7kvb_NqBogYT5SW7B5Eck2WomcMWkLf6S1d9oYKIFmx7GIOZi4YTjGIYXDWHRQKN86KbQEIg1aPu371DYtQ7mqD8t1pZ5fT00mikb177w-w1MyJY-AOLWAdK4uIxOCztOvJiFdJACqhhN7YlQSR-iedDOdBUYkG3-xKv54jVi--xtGMHsf0IzDkjikhGezP8n-uhKbI_aoqsAIV3zKj5x7UEs8PFY4KJgfxuzlp4pmqEEDsQdWAV5uj-UTk4u_-FqJ7oLABoiw6c36SxVOzibmGFIUz49sihNwCbyB4CUvp_4OdUbN8U7avKYQU3iEyRj-ge_hLAwqBYUC3mi5X2RL0-mccNcix3444OlgciGgdMMspLoDQIx9KNIQHfCUka02FRGO7RNLLIjJeRoExjkSrDc1vEsl7oGAoHvWU_zxWI3fdj-mU-2b2kt2vrZKI5F9OWzRXE16bX-c3xodpJyM0Me3qPOqcCn8D1pByiZO8dpXcL_FFP0ruN0zDJ_u1z7O4hEwQKMiTK6VI75K__75XnDxw0uY2f1wyum32fWwJFhgV33ac_IK_u9MAiGc_SeFfmlSLkJu2ozJHE62PFAFLgrvMTuca0dWLYcvEn5HavEuCNmjc6q6XZ5Nm7gkyTKN3vTTAXvoT8N34S7EoEzkISHMoKyoW8B3OryGNdWMzpErvp2z5nFdWQZyb_vRpM4BR2qaDYW7a3VxRGk-yxth2eM1PANAZGl16No6hv5ctWC1eaHxOzCAvN2UWoctwHIWiF-L_KUpAaB5QCjrOVWHCUXUHGu-Iujt-6Y-dfw5BqJRuoQrum3k0t-Lw7sdK0m0Yj6WilKrRnID6Aeq8w13llx7a5lJR-xnJ8heYvICYrbNYhJFChp7hJTaRinjJfzGs1WL2YKrDIUZW_SRsy2NbGZZIiVSL1oHKDS-DidUOybZRJBnS2cDnuSpxkYeDPKvsOkPpmwfJcVM746LUbdu5PZQA7YMoxE2jaJWPb0X3yS68IWLmMnuSZl67u_n5qE7_csP1nkBtTwjrF6JtycB5b4gMumfwIpm_96B4CBUCUrb5ITOBGnTUvfR6FZgnCgQYpNQ2T6Iae8L5_LGCv1iB0YeFjFc4L__ST9nS_oSSYZRDQoQaHLvLowx5Vnw9gj8jAFlWFdz5qvWBSmCqUfHEBcKDceLG290zyleFH-ibeyBTaOmZLY0DHoJivlry7hRd_SwKixkWFrXDqbg9EH20sKP0qwEbVdwNRSb9Z2GuVhHLPQ6OYXwZwjeFe34MYPlViGSn_N325HaeM83tsLG7fXlN2aZ1yMe4NDw9rkJ4IyCWJuo1RWojTbm3LmoHi33uRb4ZS0II91NUvsX8tO6mMCQnCE2kBIb_8d0wKIwS-aQH7cppYGsLESJnBuU8q9IHBSPFZ8s1ZIGlKCzMdX5fN3CxCvHkyB0RlHBylbOzYtDnuUBtkPU7LEQA6deNuIT-BCPU6SPevujQX7eIG7VjX2sOCzNZYULr-W1CaBXQfNk43OWK6SviUXIC8JdFXKLywNyipZYgmUD62KR9oU7kIKtsd61URgZGxriCjwrP0l6TVVJ1EedHEAg4AnWvHfWq7xiIMM332EwrSWA-7csbTeJRB0iAl2B5rsEweabpKDUCuin-6ZUgyC-HsO4ifxpfVFlsHsbBGcdTid8RER7SfHrl3dORKbt8sb3Ly1m5e3WMd0oyzc1z6mP3WBB0YUnmj9XD0WTBVXmY0q3j2LOiECWE-W_8Q5poSMy-CSxTnhgP7YpwscTTc870on26bDSp587wmkEZ4jS4I8LpbCF8f_J3s3593M1PMiWX4v3m6vacLahuGpFS9BneviHf9Io3IIQmYH7wlQ.LK3q41UnssJXAgMB5OZi2VScCJ1pyCatDLsOds5U4E4	\N	2026-04-29 05:08:21.66224	nJn6gBlONf+MkiHPyqWXnGS6gOE3JU3P2+xlSpm7kFU=	redeemed	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:openiddict:params:oauth:token-type:authorization_code	{}	f4998d9cb65f47828c24ac51548e717d
3a20e9d0-382c-bacb-6a85-b02456cc6708	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20e9d0-3539-f24a-9b74-eb447e49a1e7	2026-04-29 05:08:21	2026-04-29 06:08:21	\N	\N	\N	\N	valid	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:ietf:params:oauth:token-type:access_token	{}	d3b63c29b79642fdaa2501e92c3a5eb7
3a20e9d0-3883-4df5-9cd8-301610de5593	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20e9d0-3539-f24a-9b74-eb447e49a1e7	2026-04-29 05:08:21	2026-04-29 05:28:21	\N	\N	\N	\N	valid	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:ietf:params:oauth:token-type:id_token	{}	980d4b0a99644bccbfda8890c4b2d90e
3a20e9d3-5f0d-672c-088e-1bfa74f85315	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20e9d0-3539-f24a-9b74-eb447e49a1e7	2026-04-29 05:11:48	2026-04-29 05:31:48	\N	\N	\N	\N	valid	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:ietf:params:oauth:token-type:id_token	{}	2503337dd57044958f8ec7fe8a5cd340
3a20e9d3-5e94-69c3-ea8d-879e89402f7e	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20e9d0-3539-f24a-9b74-eb447e49a1e7	2026-04-29 05:11:48	2026-04-29 05:16:48	eyJhbGciOiJSU0EtT0FFUCIsImVuYyI6IkEyNTZDQkMtSFM1MTIiLCJraWQiOiIwMzEwM0QyNEVERDExRjNBOTY1NzFCRUMxQTlBRDUzMUUwMkJDNkQ4IiwidHlwIjoib2lfYXVjK2p3dCIsImN0eSI6IkpXVCJ9.IDL_FMoRZFchxci6aCtJOH7-KoPOhShDV1s-GcnrnhFNbbJWStp82-Hq7zeEv3utxTKZtkJEOCRpe5GqRcTKMFMOdEWeSdzc2Dt_Ng9SOWbwJnNyDFSiPHRKVsHjU_ikZfAGrYzKC1eYsmqoOP_4dg4dUpReU2IeWr2QvhWgh80eEovRZKWTNH0E7FU73CsmMFzfC7uMUoYoOEZKnaUMbxTQQLb00xBHql6NulsJ4VNzSbyojNNUskLwzAiCtKjD-NnDbz7mnoHqjr2pVzdsQiRYGw3k8NU0xEch5JJqRmhajyRFz4tjNNMhUvxJXBbtAKwANobrBjAZ3k8B6QRqgXhSg4GuM-m1dg-emWB3Sze_dRG3_DxwulcKhLfxnxFADTZEv_nw40ujA_c4Jki-xzaNzTCnd8dda7YQRLKZgaTLVEuv5S22Vgqx2ZsmNfEZMCxRAqDOsuQznRCyjYUCzNZ98OmJCToBcff5mtfoy4b4tuIN3sFhoxvanvdAvpJzDluj4EpNk3Vw7qqsH_8MNgA4oXSFEQmSucKjKZljoV5Ke8MLiYJXWFBRYcEhYA3GFaPFHA2A-Nrvq4IhKMR8e5TWEDN17ZzPxRNYTxnFxemOQpMKkNLWIFj1t2sJ3V2WVI7DhCwRXDMGXAuCNAozEbVYZ0q5AS8moFI656pgRbg.BdZz77Kmz--FQpTAY9FESA.QhXWldLVFCUEZ9Csh5FZMn8DpGZE8834b-tFyWCFWD6Hikwm3ykUUhZyfn_ZAyPODys8HVH_zRpPS1EWozqJymtlnlAJWRgOu6IPeX6_NhWqTB88FG6qWwEdS1SyUDMJ4_BDMQNf7L6K4ozhPTHOWkQx5-fCH3p7fwXPCvwpBHUVpVDeUz51epwKE0UbIDaRlQOja4mdQQ51WxPFZ7EWUKx3c5LmwYPJX3RwtCrfykB0wzpm3bqCrxvwbIqkLMYmrzkYs_6DiC325Rbh-JnNOCXZyp5EEaG_1nI16JXiX2Wi9sNzl1ooVH0Cuwk9KTzIcf2OFRVwYf1g_zcOPVDxWf4CxBuwHt9mogao5c1VGW2yLknZo0aekXFAHWMLxIadCYmMgXK8vstj_mCoPzo780AENMWgexF8f_rgANysQATpRPiQ_IBxN1efSnFafI4g06HSQ3LW8Dk1TA8Po2Re-70CLWEdB52ja_8yZ-kW9mcw2kKAffDATPrKJy6wifLrjTt-N-cFWkQExVXI4qigZtluL25y6afylNZEDJaDyDhyk_hhwJ4QdpOzrysHjXv2Rt0Mzha4vJ00fKlNqM_89eRs2MOriLmejCbWqA9mJMSLZ1G_IRl2gpu4Ba5u4MLn9NrnfD1Qork1aCAuxYDvs1TXgduYa-6C0jUtj6joJKTihVw1tdbC4JNTd3xRLn4-MyAR9C0DcQSvoLd9DboPqF7YEC8MhRSv9Xztfv8AWF-dZLfPSYygMTksdK055z5TlYgwfNWcna6v3GGXVL2hLxmAHCFyMD9e6gR1aEHNxTI6F3md6b21G4cEeSjDbdPLLbrWzwdpDXs1kzq4DGEb59i5hfzKW9U4F00edzOmnm85G5A66-Xxjl5Fp5vdOTn4bggKQUaFb8A28Gvx2wxToxn1JPwc12g3aPGeaemvcuxmKF09U84I_twhmS8iIOVKMoXSJpeK0un04AwnfQ2wK20fnwuQD-2hHP1WjKH5nxZf2oCyIk-ZweYmqkGMKYiJSIkIuDqrJ5Zo57CULYbOhmmwttHA5-4sYX1182Lj1olJJjC0aa14A-v3iw77_1c0-25uuF9mear1QepM5S6_-LDM3iL3uxghyu0Y474QB39pvdq9llCxyGjmqK1N6kZHorl5Vz0g7jZs9LSfdMZtnuITeE2DtE6w0uysDghGaLMwitW_AL0FCfCiXUEqdIefUTtUOkHo-cyFBZ9Nphazh-n8NBRqAqibmRzpJlTwRkU9u-n5bQdevdzqXFM6m9Ro8baarmObyKFT56pNTsnerdTRhd3JnXvycDXYGv_Ge5t0a2JkbPOP1ptZ-9xnS1pZV5keZQBazhfaiLQJsPZukwrUvAcZv22ZAfna3UvxStE40VwzEn0eu_dImCRJd7hxI6DRqklmp-fxVFc7BKhTaOAAMmsYFZDE8v0fTokimesNDlWT0KIJnCSjetEn7TSQM030V9MdEBchcpF0mYgxQ2ZQ3pi1RK-EMd4HWgT6T1Je-WgNUcfwd5GwDXYv2emrlo4wi2F-XnqJzp_GpcZ0h_WarXHXWYTJ_NNSMUJfOPv9VPvLTIeIybDjVeIHp7juZspha1qQsxvlkaw3AJYXS9JiiTenkvN72x6to9eRxFWIjEMZ5n3nC4ARSvXivR1FFdS9UlGbRgwlRFFhoscGIikvR7B3WHBfoQs87dw_bobTTAsecg6GJqWyuRbmk3WW7qA2F1bUcCPdOh1GARWWaHt0r6idt3N9fI89Xd21Sl09ZqKYVYM2cT7y9cJjNiDUSmoIVoJ2d0DEnnFGmnKKRFliShL1LxaKRY-d45ivooZPwgzZm9wXouo3tDVhfPa_uyquPLyNWuFuCBJ-mGAiaAr962ayXeT3D_w9Jrw2Sxbhub1u3Z0-PX3LkzA8ssKnNbhc3ByvBVTU9OMLPuZlGIPUeIJmrtDUUWAAmzND6Lp4qWE7_ARmzNciFExKoK3kPWQxoC4ZBkIUmg6QHNKaksMn7eoghXlmw5KtIIThzyJ8WlpBokXpexgY8JTn9UIWxm-6SbotqEox9rbV9TDOLn1w0F4AlSpR_YrPIF9ELdNC3cZ116Ij_6_zdplj7i4U5haV7mo2w5s-Na2L1vPzneLRACQ3dSP2axJLoYDwAlNMNhEme8AczpAKPQ-fDaRDgH831k283MtYQLs7Vz2djmwjAO9FWYJ7M_aphFoZ7LCY8VTpfEIITzhoHjmEZ4ytt8HH6ZBqprevsAEVAvidohepgBiEYb1ks6fZfsVPKbgavG1BOhYS6ZXkUhY2fp78eDTFuOlthYHJ304QTfyXsu52tonw7qLBZ2a5pdNma64uoXf6iOWyKLuDxoVULI6qSqND88CuYqUMxgVRVUhEscQcXvt3JAe61M6kJ0s0gtwNxWchHTxCwL4uEA8mXWkNHVbFoYuwMlHVHg49aDecU-iYSFzOCJK7DyggxgZK7newogx1XQM6o5WpShltDwQOjTD91IFYOjW2rqX2UAB0Rmtn8U_4NmMfK39syUtp8uZohkQ3S-yqhVJyvyPDIvdpHmYNVKhAZdBLqUwV4dUjbMOl588aXnO2Lu_xPNUhfF8HjT5oidOR50Fe0y6RDMra3d6RigkBt14jW9w1mDwbBM1DnZnExGzNIZbC-H7R_u14ppuh4MgxKMHmleVG5MT3udw54HykimiYCJjIC5DgbVa52YN5SjghZoiBUa_GNyAIXxi0mI0Yo7q6jjLRVLWtMkTAH9PSoAKHw3TH_xPtX4PqTZznGM3dbKa9zxN1CJ8gxDHzvbWIFr1fc5NbICyDLELparHTyr4Vc39jRzYGBMoYi5zfsQ95lR7yFOi0iXkP-Oa_4veMR3-uevWmalM4mOS5yUv7uXy7bPAA7pEFOvSlJZEThtGDRZimNTvz6xG-zHrsaQqxaCDa7_IN9U6SKpK6Hdc-H0jcuXdCfh_mnpHRiN3rYO5drsehu1js5sBHsPKSxatrUA6b8eqj7Sh4G2C0zgJOEj4F18GK8mL71oPfG3AnSLupsvTzC5j5maaBUqekl1C7q6VeFjKpNM_gumxxJwUOamrp6qqtij-Jgiva6QuswpCvpV9oEekAFtPaggkZvieePrbHueQflhmfEylqauFjMqYyjHECgRptF30j3ANcWQ5lMOM-BP1v0n6CmUjfQyWSz5Nh_VGro0lHHGRA0KXgRdngZMIiLuLOvJopfl2ujYjY1vAk3mK0pqorU3JRHapFuYR6g0gGhEXY-xhhj5qIWmxe0St9lpH3mEJKvLcWieJmqhtnYIpv6SiYdxwDfqjI8IjD1r-_up9gO89lms43XRxE5lo_S_8R2swDr5vGwvPu5_1Mx90UCldr8Am61k8iVnKnTaQzbyhspJfsfCoPHwO9GQF6EtDZ-Fj9t5KOWocAt-hGzi3U7GDVIoFxnO3k4zXtwTgV2UgHNvOOrdXMF3EQVxbZVHp-ozPOa8OoJNS8clnE7QTKfzwkfpX4viQMKylCel8IPkfnF0ydnw91_Me6G5yuEdySmHVKxAm7Qiri0og__PttLENJRx2b1E1Sp2TxxO_YkmCkwurMvVGIkaNqxH_pmg5bsJNLQ3sTuTD04de5VwcicXKy2FEuVmJiLSMpqO7EK-YQ785UGldcea0s902wFZNiAsH-8puRzXl2Ba9RJhwIizntHYDft1A52A_-R3dvwRQ3Mc02HairEmEJVpDt8CtHdFj80vPJE9KTw1wjmV57LqqIoOivDMSQn9M9Qemi5IjyBw-QzQurdITr1n0hmDJkjzchduQFSmYx4ux_z8II3ia_wAjya5D-SvS5BJwuvDowvZJnckXMiSYeACEdu9ihuteXevnI_EAaPF2RJ5EUJ9ORPbPzovm8LX9cLRtEna82r31m5ZiJeaXEkX3zuiNlKA.P-xmxPyabB2OS6DFlcXQQUdf4mh-DSDkOdFUTh3mDZ0	\N	2026-04-29 05:11:48.523298	DmBobzqF5LDsjwKgw0Is5OkSfVZx19fvQ+otlFCKFME=	redeemed	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:openiddict:params:oauth:token-type:authorization_code	{}	5049dd6e02784bf1aefad2d465013d85
3a20e9d3-6041-4c84-fe04-cda116a08be4	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20e9d0-3539-f24a-9b74-eb447e49a1e7	2026-04-29 05:11:48	2026-04-29 06:11:48	\N	\N	\N	\N	valid	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:ietf:params:oauth:token-type:access_token	{}	e3fc4e48c3d34fc2a82df4cfb9395454
3a20e9d3-60a4-679d-1102-fc3e52611bab	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20e9d0-3539-f24a-9b74-eb447e49a1e7	2026-04-29 05:11:48	2026-04-29 05:31:48	\N	\N	\N	\N	valid	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:ietf:params:oauth:token-type:id_token	{}	ef8944218a45433bb5301f89f127e58d
3a20eed6-3729-a692-86bb-094685f89558	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20e9d0-3539-f24a-9b74-eb447e49a1e7	2026-04-30 04:33:00	2026-04-30 04:53:00	\N	\N	\N	\N	valid	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:ietf:params:oauth:token-type:id_token	{}	07b515ec20f84fa9a18d715aebb452e1
3a20eed6-368a-66ce-90b9-c6e91fb75b4a	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20e9d0-3539-f24a-9b74-eb447e49a1e7	2026-04-30 04:33:00	2026-04-30 04:38:00	eyJhbGciOiJSU0EtT0FFUCIsImVuYyI6IkEyNTZDQkMtSFM1MTIiLCJraWQiOiIwMzEwM0QyNEVERDExRjNBOTY1NzFCRUMxQTlBRDUzMUUwMkJDNkQ4IiwidHlwIjoib2lfYXVjK2p3dCIsImN0eSI6IkpXVCJ9.jRSI1zVPOSuWgOxxwzoa_1NJadG2gEiwsQNTxNP2psOX1Cr_6HIxO9ayDf6hq615IhADyEay9KA2IPrpyLWzmE5teiimrNOQr9_41ziJN5Eycj5hizYU0CQ2BIkNnFmIabyWDzU4pDrTCGlDl1MnRvjGrI_cvF_2qX3YIKIDfPTAQwYG7INW40CU0WQYLsJDjFcztKQsUrnlDx9YkOJKBT2aO3ZCCN5UTjrMcoglYbzEGqpOrv-u1qnSwjhWP-oRz8VRSkr2m-WD3p6VkbdnFkXLq0LyUnaRYmFeQUH7jd1ogFfo3hb_-Zp8U3zu1kHhJ_h8hvKBra5OBTjMABnGc4blPs6eWGjCwFav5BoNd_hMxxdwt1kJS41bhnu6Yr740x1TmPVY6plUJRXo3X2-5mBvQAJg1qoBBrkdwa0kOq3zoIh2uwbYPGyLLeWgPb00Ec_r5viz4AowRW-hosLpFB-7tLg6eou08O8YDXUmgF_ot9T68D1bbn6T9JCYYjLWxuk52sbQjRRDZvn-RGKiHwJDByAEud83DNZ5NyunPB4hf449GPwGRx5CQpolhhLz5u1aiJ2daX0wo1JGHskk9spM4DKMIugcLBOnJ1vbIP6vgl5QwPgbPj3_g-Q4rDzqmdNJ3o6X_CbvaRDYxV6ugbt7P5S2F9SQhdjNMEp6Ewk.C4Gd5Qn2lYJ42cfi7t2RiA.veFS-tmv-T87u5KWmf-Ld8Crz6OJTedvEYsXCOSnPWuoDU2J6F8iIEeeNCxKcfAk-8vBiagaMzdsfHJf63o77RoaZPcunapPNcEh0-2p6TGdkfTiJmA6yqcWn6baehzMvXfXvNrYsx1-nwpeaKuRRdCWKnRYPpJdZyOdMxBN0ozrqqXiz3oNahgzcCMnc3ZJa9zk-VAkqPwJ_yDXvLhIWIkGY3mP9h98LwtrzzkeAvVgdRQ5-UGuvVfujG5-4JXslCZ5w3LkG5U-bPWCIOpzgCCsWr3oMS4RqZ13nIEbIFIlGSfi8WOnli2oAXVT71FR605k8llb4VGBfSYOh-3YP02MbRGqTuagpr64NLo1zlSRxoBMu__l4VfDhE_gnv9j4s8uCMcOCnl5veEzUuRNU07EiIAP9_FfpzzJlm0vp92Dcu1b62jOhXobaJDOLJrKYQewtBHSWyoSzPow654_VHS2I_TZi1LU_q5zxNsKu3T28BfOjg6Na1ojS8BRoUspYBLCCzs9_A7PViiFoq-8DYfQGWMygpu8L0PukTM57TmD3gjitwcbkeX2ICPJWBHILdCldukrx5-5UzMmKUo-HTnAC9q3d1SCKK5Naz6vIxitIDQM2m6ly5qixodFqYz1P5i-mCrpgnA1Mn9pAiCdePdUr0idI76FQzVFohJJQ570FgKiG-ycR6z0xLE1RkHW3f7meZySocIRhqTS9d7F-Ue5SDRA-DOIlP8SOCIDLeNBXRR9zDeOA5WBygwkNfUhhxyX6v4egmV4_etdNZuPpU_ZrgT8ujDhJnazcIVnkqCmHcDunc3IojMfGIDur5sHwnnLXJfY2YJUfQsUtCZ3itgItYtwSQPQrZjBv9DaP5C9QYZjEZMGovqYTAt8UNlapkH4fHPfzBXWsdcpAjC_SAykNhlYmDE_jjEZCtZnmLnYsFUJCKRUBAmgt6yHAGAQoKlwjxqxnkl9Y3x9wDGweryo9Hl0UOra0ox0IOLHXZSCBbIdtRwAfq2a0cVJ9BrQcOuY6LI32rYGBXVck-4XKp6GnzGStPLQbHuNkcGaUCfUM2kXdY5ZGvdSiylEsqbfb2pues6uE01MI7R0ln9HWo9Pv0P6YihA-qayXVr9VnHEU402JqFeUPfOe13iq3irRNXJLFfhNz6urAeb9XAgFTMifqMng8D_T-KFQ_N8cqrzKo5lxOS8lucjjNU__Iao5t0mze-egh1bRf0cZRpWTxSjRgp_AyWN80ryxg6NvMqjAp53pXjDC3XBVs35Gv2y982sR67kkNWNebMlE_TDxdBOBRTfG1Iykvc61f_C0sm7uwVS80EO4eWtVpwJVDKz4IlIvl8QagCDYpmiSCqkpVfOrfXMh-z9IYBiX-XaRlsxmdvKSgoHn_cMfjx1wsIJH46UHtcvV3dtie6BZlrtn5b-fWzbbDsXbI8n6zfWKaifV5qk29iSPGuDcs0bLztbt_iIi_wN7FxvFD27wYfAe6C7U6_pC1iLnu__qROlrGpKRwGzB3QwtHeQTRHziuUFHvoko5zzNQLs0rC8wOxQr1jAHKxLld9Q_vusFblIRIcPtG43ag52CQ4fhAf9je8V0BkEMiuqyKJyGnUI3QzhoqRpdVhRYx236PRAjdKqaEGpVmoJ-FygAvNdPMt9AbfD6ME_tX3yD8QscjguOiZSoQBYA6l7MQ3jwy__v1SbeMNe9vndUb1kK94ZUB2PzmkHMjmrqHALRcAgQd7wg8zxSCiLUAoQxsyB88zobABuo9qbEqzVGbh45-TWZ0WB3_z1h7Z2Eft93Cj-48r8xsB99udh41sw9YQjvHICUa4Wjg-Ozho8n2MngWZ6aAFA1LTdo4WAFOLxF6w1IhixqhynsWLyS9IwI1Zkdx5r2O8xgshpv9r9voMulxbYTKMLXgdVKQbbVwL-J6OdI18hukghk6_WABVf348rem2iY_5ajluo1Ncse5nj45ydKqK0shrPDOKlVL4VLDbziWstmGDHFsVxK8GwbJp-aM-DWyrvbIT6qjrPjVo_6dSNFkUq4Lxyb_Lss71PO5X1PT7f_f2DeoARWdfmz_vcLM0ul8d7CHiikyqbg7ndeNiCKFhyVvTHuJgmWszmSTz4H0OHUCtYWRP_6r1MJdGxvkoobjHAceVy1AVH3EKQn9KzETKLUE3KG-Fcsiztt1eWfL2oiCURpZUODxxFhtqrx9uWksXutPs4FsIQ1ooBEkNryAMG8lh2Oe89QedTesxA3o3HDFRhsBZP8k56lzzn6WF7xbxgAWskznSB5PDEzyHaJv7U_ULvVmXT2dwMgj6rIQphO32ANQ6SfNYQ8zIWTM6hVwCgaelhH8Z7w0DqPTmY0zrbIAAPpzo7b6lWeoObE6VrZaRTlAxrh66YJnLUlsNOxDcc-8QW9vmqk_zUR5Xv6tCrmmQNUN7XNaxlgqKGgxXH9oB9M4ujylZBRAP6Q86i-hpp088K5AoTFS5pfhZMVFHpDIbehjhY5DhAQ5HQTlYNSYj1xtER4vs8c5HJmdRMNUSSGywkKguMyn1sUQzsRGexmlL0Zi5Zb1pvMT5808fe62lTK6dYb8xt_VW_myS0xWBJVTm5ZGS4SRc7DbuVK0SXXE0A8W2nuuALOVaQuhfva6PaxZ9KfKC3y_hZAm41pCWbBZI7LpvcvKEO6UrHTsHQPjTo6wLfkbe2uOTkMP7dlpqgR0sS7p5nUOiWzHzn1o9SMPAZ4BduTbiP5Y0Jp400yIkEoDTmLcC8D6g0rlTimzhavIakf6J0zxaaT-P7KKOhX7KQlFx0AG1o6DXghO4IfMNbjkFWtFciIb7l-i3AqpVl-lXICY13i3mAita63Z16VJbGTWVGJwI3hSwaud0LJIFbEA7FYMGNIfXYdT8R8se6a04bngfBnh9R-JNNtb4D2DeamOI_mmqWdStodCXecZbhjg8YXjoDVJDJzDa6IQIMIJ5bJ3v2T9pGSHtbSKdnVPz43IvaXcb8CN98L0NtuoWtCPCFDYGFZeVWcrng5dl0psWQDnu4Zhk7eztiv6YshFYnak3BGky3ny1V3RG9ScX7F_KlXWmlyEn6ZQlMQIpTmVJg-a6rslOJukKAWrFVZtlGlPT2Gl2yWnt_7lkvrRKl4CyaI22kggYVvSij69q4PuxqZ_NJ2arc0eucIjLBfD-pkZoy_jaV9W8NJGuj-y9kwnk-oTbhgUcFY05qagPzFzgKADz1eTUgZlYrwIWhjGiYxq2AqoKwAxWJtHlNnJR2D5QwbE0C9n7ieooUbxudZWb-4hGGoPMf8u5U55dDMzA0F_KkwFcH_2Jv7QxJsvdNZh-nQBW6jmaIeVZKweompT3h4D3ZuAtU4j02pqOuMzZSs2p_8AesqyQ6FhN-7xeSmStr6sHhQakeedPfclvnVOOtX12Y_aL8OFsvnYzYQ4RGRZ4V4mgN4H6P0vGtEkDhZ0pgqvLcyuR0fO4-UitqC8-PkUkEKIK6Ko_I9lgtGVoRzxGOSybsKBUB9FAVfoK1zAJIcyL4uc2GQ9k_prFwwuUfNFs-hDjxqrkhf9qIy5TQo8wsBEgpeuLJUS4Tl2-_gbScsCivmUPJCB4nAdMntvyPUHRfnkRybaXFETyMIFsKTb6xQ7NsP29TqXpu5HV_5DGbcpVzofPfzvlg4WaadWeuCJlcDzIioUEsM6L9l49n5OuoMbX1fxtFLX-WpbEJ4lJzikrTI0CAy2AHt1kPkjZ_9ltznI0yyGdmhle0Aebypsnu_Lp9GxxMd2dzEcEGnKeYjoSFIIIU8ecWvEjc2Ut9r_aAR3jpWx0Mh36s847V5TzzBnogVSWybR8PihI5WFook59k6cK2gJdAl55gPFOiD0-XMy3e5nn4dISKpdghrdy7_9B7xdxma_-lDRXz7t3CtHFH_yVmVUtGg-Ta5Q._OBv3GjwtKaZhUJAOz9NQ2AZGUgbMsETz9CLOYVQQ38	\N	2026-04-30 04:33:00.99201	4BkyxAn1lBhGF5Tb9XY4AjjECvbxYMSS4uAArOYHQEQ=	redeemed	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:openiddict:params:oauth:token-type:authorization_code	{}	d8109a6fab3844beafc94bfaa34b256b
3a20eed6-384d-aaf9-9ea5-4508fbb54fba	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20e9d0-3539-f24a-9b74-eb447e49a1e7	2026-04-30 04:33:01	2026-04-30 05:33:01	\N	\N	\N	\N	valid	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:ietf:params:oauth:token-type:access_token	{}	b373284859eb4507bde4d3dcd1296498
3a20eed6-38a6-0b55-5766-24d4585494da	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20e9d0-3539-f24a-9b74-eb447e49a1e7	2026-04-30 04:33:01	2026-04-30 04:53:01	\N	\N	\N	\N	valid	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:ietf:params:oauth:token-type:id_token	{}	842d4d822d3d44fb97ce7ee0e6a285bc
3a20eef8-afdd-c53c-6bff-9f18f6b3724c	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20eef8-aed4-978c-4cf7-7e3be2a6ea06	2026-04-30 05:10:39	2026-04-30 05:30:39	\N	\N	\N	\N	valid	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:ietf:params:oauth:token-type:id_token	{}	b4179486c6e0447c869dc37496560196
3a20eef8-af14-6437-192d-c72f833bfb04	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20eef8-aed4-978c-4cf7-7e3be2a6ea06	2026-04-30 05:10:39	2026-04-30 05:15:39	eyJhbGciOiJSU0EtT0FFUCIsImVuYyI6IkEyNTZDQkMtSFM1MTIiLCJraWQiOiIwMzEwM0QyNEVERDExRjNBOTY1NzFCRUMxQTlBRDUzMUUwMkJDNkQ4IiwidHlwIjoib2lfYXVjK2p3dCIsImN0eSI6IkpXVCJ9.VksP6GLJMEmAR-IKV214bFKKM_SugOTTcwLYf8-PFwCZBW1nxIK_r_YV93-ggs1DFT51ibRsJkKRFZ9uCybeH3AtobB4BdymPLOewvPaJFTzkQ1b6sNZIuV-w7KdqwBc4kxGJByd06B3BbRy3MiRTOuDmedDAye0dS9q2M573kV5BYF5ksWFxJ46EsTLaVE9zUssa94X3rg8x4yslDip8WuhYzBilCQN_fx_L4dVKsWcAIy8GlschXoSgIo1MqqOQ10Mtn0lsd1YVr8a-5W2hcvoVyEPXMaLvapBDdtPl1lUWfRetnsdl-SQWeSR2B4CabFhx2lInRSNaysDhtdbn5Kv4LgyjfRu2FKclc4nxqM6ESFVGZfp_22uBvRDrmNTsHzi0TEPfsKtMrcvvK-srTl1kA_yYVHY3GOelA2l56d5iqJMmqZIGIinDwR-Cc2Sb-sgS_hv7YE8cTNxA94-PAQBz4IzfFUkf0-hCjbKAPNXGtzXk0BZZ8rIu0ZkQ7YsZjpMS100Kl6qOqE05WgmGc7NbHTwFk-G2rbwimuOKw5mLojYDS6roxk7FF_4CczHnF-gwxfpeHa9r5O5PchkjXNKckTt0RaJ0ERS5JUvnjb_-dFUA85PtVbXxVVyJYZwPsm8bWNRNV86wDLMZoVsYJQUq4sP2WnsbRKYs45R110.f-NRepee6YbXkydILIhTww.puzFdkdkUw5lRllmONYJpaV_F2XxUr4d-NgQ7lOfLXpNno0kY8lgHCsUk92qWtx9lwnOjE6N_CRsdP_r5N9ADGs9Ru6MhSqqlyTTDF70fPK-t22CQkwQZNNupPiGg6ZgKoHtidHIlVrQRWISgRiLXBny2Xd2vPU6YGVTpTmrnTgOzMEGBd9-qW3ylfZGUbvskUiaH5YBaMLzt-exN-ZTb0xEFz_ms_9J7k4OYRqkgOZktNY2KwV6BDRpWbxbif0zf7TqNeEuNniTH8gyIL4znzGJDBhLq1i87J3wwfWt2HjxI6PecuCNkngg2mKDLp1wyCJjdOjDPdlLv7eALoM9_35f_pWzwUKsuprFxIOi9XVhLkbVh6Pkttzsh75I1DVrFD6PrGbVjFJEEzKBkZcPIGQmiTw9KU9S9ay7S4OiFlLtzsSRXNiit9yDKM4qCswUi9DGysES4ZsEpj7x3tMpMboNmTL5fmtglDdRMkjS_LTkX-by0eCQ6zXRU1wLsZAs9EduJkCT6kVTcMiPS9FvH4snShRqtzJWYP_QylaN3ZYZ9D9xjMDRCFjdt0dC-OBIpZ5yvqFQMaSIVG5uWzCTPC3E47KnzJiD6MdQrj-E-IS29mLZHMy-QhVlH8CFhTMJZoXRh8Bx0fNBEPEnIyZlElViQnI7aiA-21tbMkCZ2Bx4ec4t8nXJ8PtbOb9b8A1mUQBWWawTi9TFsfQwjf2_8L2ZMxd7pY1urvBmNIxAJXhjqjHcYqqP7sTb4Njt8eBLJYeUPohoNwz8pA8LwJxiD-0y0ZQ26WO-tjUyjPLYiWv4f2GqOFOoN9vnJ4vXTXwcqJ3hAi8LjD8axrEC3M81z3i4b1spqVKcbt3j1Bc8xkpRBtXue2o-qefsIvkTtrXsBBHQkEKklmhkNz2zGsNzBVQzPaSC7mR8GMa2i6-PLtm-psmlTblHJrNIyCHjztGfoG4njR_SJlAurkTMgKyUDnE4O2vCryuFZXzVLTAunQA-U9Pk64x3dEYBOvCbI48PYJkib8w_hoFtKr8hvLKz075dlgK3vYqVK85d7wTUdjWBqoBS5rVf8A1mPbJ2bYEfQZEgJuE-xqLlMQccsqQ3dFUnIw3Il6YzZcKwTDGpqqbvzl2FzKHrlu3Qfqxiu70G6S8w2qF__zmcEhMsSTwoKRIG9XfZAnyvNHdU6-YeOwuRwrSRzIUTwljcpCOiKl4Ga-OWJv162zqUbZW7gSlS4DM7LejZ89l6nTGp7SFnLNY94yGTCBZ1buWa2tDwc30ts2plx6PIwA-rgwYlGhTMcBl5_ot_y2F_Od2ckJtbmSIgiWgwolJ9nTJKfoIa-bvTyG8tW21Guicvpkx4NvxEnAC6-SGzeQQl7f0HzPoNwcJqDFXXmUCx58Hqu9QbZ7W9QNsYXc8wlhtU4oMlWY1KAJ-pjqMYaIMkO_m5COZgRLNEaxdg_RD389EGKgvqpBOQVoOW5Pmi_LvcO6VE0hQ6AnhFzVAN30AhBseedtANhRLh2eqPRsSnKmEUCwvqGDpizIgAvJh6fqlVy0sHQoQrojuvKfVnQjJOMLVJWrHiDxO8XISWl4Odj5YZ94MG8GjKnaT24x6wvuZjef2ikD5HQzY6BWNfHU0NGXZwGMefbD18A6vblwZLWDm-bn5p1ZIGdlqWLv9hO0J6NsMGfeCLUmZe0uQm5XurpRtw4b4hLD5tlk29USGR7OcFiokZP9DJfk88edjyzDQXxgQJI5bRUVfhWMVPhyjwrJSwPwdFyFVGaZT-KHpJHoHqdiLuoZAdew2lgYYWGwsGegBCrOZhpoWqUluMCOWud6REZ5CMWvgW8gO3oU_kK3iDe1A3SLoNmOMjceWk9eO230UNeNs5MdR5BVzNtvoHbkIbtLqsSlHyZsAgS23FPqxJ7VLK0t_6dTBIXV2EBJBqWG6xUU0nOCez7_p-HtKmxl-NmHpNDazmJ89SnR_fm7PwOfB8WM_QX4n0G6G2wRblAB5eVnFykVkSkmYw12R8Ji0UwVRzjFhjGPdnnRoWzQOrRFy-E36OmV1yCKQmRbf309DNn2_4tJ0GjyS6lCj8Sa7eSrnVQfK1cJAjxjbvJQwvBGQ5ilOhEWvoXaQ0vNQhZlp8JLtdtEbFB6fA3QTx6uN0pljVEqbg2GXR1tCz0vvQELinbVaKHDOov5q4T3w603MrURfXOXzVMpY0xAUMFMa-_rntkIZObgJsJKQxzo-idBZEczLNnpKJkCg2sOUq0g4_0LZlZ2o5U61eaxkvPzzCR2tRR08PBp15Oz1qYzoq5Z1aqZeuoOB2DCRVU0E3exf8af4ur7JKIMIOst7H0fKG4rxEn_n0kJfWBRrrd4KnQjWQs0WIpAEVn-B8HKa-yEIZZLfNrZY_VXp0vAV9PsBhPazdo3gCXJ5rbtCIgAzstiwpO3USjCWAzQdquTcEBhuKEhc56rwXXk95RImnz18ONMdPFLARadv32754huN2642sSmF6F5HYpfsU2ZEu-hf4XMkMHNHXcuXW039MozXk49rz-Bbkf7Ht5CdiysAnqdoCKOgo6_8tCeTBEj0V2eKA52beI6ZA-ohAabgtGXjTvbmxfx_iCA6eja-GD4jDXSFJ10anTxn-JYpEoVxD3okjf2Mhk4IFdCEmBmdPCs5MaZQUrcwyv8O7HDzwOjtponx6Vp4QWyophPK5qYZeH5V-1RH3mLaZ_UfiDAXrO-226zzKFEPEdpKqbLvFm3dkFsWO-M02lrWrCuwwVr7y30Vg3YZLj5wz-Uj4WRqDWfOE_3ERmqYumRJ6XmrxbAZWxwEBTMfqnoCAzOx5I4JBg0UT2GxBxqTW29E3PzE1652N7JHNGzGCJh-08UP_P691NQuKFaSZSbUtwn8SfjGkMlFV_wiTvLJTm-heOGf7fl0BP-a8iJPGN2Qfr0rpS0ATveVQxfGoiEgZvn6pPu3A7PbdSfFg91Va-niIZ624523o0Tj_0v4yevZLt46G_YpW5NfnligPxxqJqm_8nzXeAtg1fWRGcV5_Ez_YcE46I12d4eTIXKk_-KAX96VbTv3-jXmEW_Y6mMG7xb5fBkCRYAtW76_nN57_kzB299wYa9an5L6hBGAM77vx4XwaRF3TSA-55VmXvbAr-7ECkwLutNpCoYSQElFKTbKkvNFTc6bSAmq4hjoIYf2KqqM_wo9rD75wqhp2oaPCTY3z-wQSUyCMneIVi-ltmQFTXpyF2J_uXYcmBhPoB-1YSxe9-I-D88D8FHA9Q6a8G3aWmvxgj4Ogj6N_kSEtoxPrOp6Jpctey5ohepVSqXWlMXVrdKxZ6EvMUZEqiAWPcZFN7-zaLBwIfCX677cc5ICwBMSHx9Qlf56wjFf-tNBuiM6tfWTo5Rj1jJjTbqwp5NbxZJJlfp1Zm5PxHCl74H_uHDfZmN4FJaVJcPNyfdLJEFAhxpA_JeHq77_sTrPaQOxKNqCu7afv5LP7sbyMYsFwDzh0YQPTmg3Lz8f9CLFIz9SiKVnQUMwvC5i5G6xWucSuZ3i2EvwjnHzxzwU8I0R0AcGjJ8UvP9jknRx8RBcvDZTrwYLKxD5IGPJrR52L8cQq4FXwTXAIdsCAfTpPsDtXf1e70jF0ey8rdzR2ycYI6trb7-GokcYy6cWvNGdJisEkjOXHPUJefHv0NRfsIY2tLRPIFJhepY3hjxSvszadSUim2895zkZaKxwfgENhM6ZzbZNe9p3AiHuWMJd9iazJRrfL_XjHjq2lOm5FR7-8YZaa3NDlVKp-SHALjTGMeX2Jpm8SSe8PSTPqHtni-zN_ef65yD0WOMOQ-v480_8z9L6BigW939u3sjhN50h2kFxdfekqjQF6Oz2mJeKdzgjvqeMNGMIgwohhFgg1g19TGvvLvqXfdeYa7auA5cN0GMrI5uG2lKcyQBUGKi8liracdXONknkqWf_lbl2vKeJqyiJzer56_e3Y-8XXcar1C2t9vSqtao1mFcvu4Q.agKv42mYuANDzKSe7OQuyTRpaXFz4jfHLqakOGzXSes	\N	2026-04-30 05:10:40.218535	rWLJHPWZbq9hT8nA9WiZEaQaATxUPm+Y9IQjh5IKtSs=	redeemed	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:openiddict:params:oauth:token-type:authorization_code	{}	e3c3c6525306463090b20fa44b206358
3a20eef8-b174-831b-eae9-d94b7a710d0a	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20eef8-aed4-978c-4cf7-7e3be2a6ea06	2026-04-30 05:10:40	2026-04-30 06:10:40	\N	\N	\N	\N	valid	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:ietf:params:oauth:token-type:access_token	{}	815f7cda66d54e9c95a3bae3474bd06d
3a20eef8-b1da-8d72-d974-b64149d7503c	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20eef8-aed4-978c-4cf7-7e3be2a6ea06	2026-04-30 05:10:40	2026-04-30 05:30:40	\N	\N	\N	\N	valid	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:ietf:params:oauth:token-type:id_token	{}	54f0493591fe41a4a76f59d68bd3033c
3a20efd5-5a52-f53a-df3d-c75b7c944bdf	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20eef8-aed4-978c-4cf7-7e3be2a6ea06	2026-04-30 09:11:41	2026-04-30 09:16:41	eyJhbGciOiJSU0EtT0FFUCIsImVuYyI6IkEyNTZDQkMtSFM1MTIiLCJraWQiOiIwMzEwM0QyNEVERDExRjNBOTY1NzFCRUMxQTlBRDUzMUUwMkJDNkQ4IiwidHlwIjoib2lfYXVjK2p3dCIsImN0eSI6IkpXVCJ9.ARReV16EBS7zLp790p-SwSqH4XASxR-Gs9_XC5Anav-npPIlOhFo_PPEEfUir0Sjxkxl9KzYkxfYWmb1se6TxjTpmrY5GdYcJ7xCLw0JNQr4XdxUscy4ho1wkiFhlvyEzI9Ioqkvgk7rxLfkYl2ib2NFjIyni6Pb_LtMXUVdL7hLeEZAi9sRYKIZyljbaD655oEk4d8VfhZKjs2p4tobJD-2CVHRF_RSE4FZLk9RBYFnZCLDec0PKFsiOgCFeQY4cd99nE7GEAm0KLrVTg8Ol4O_X4yNjDTUcOQItSoRwTRqKOLIqZjIbr3-v_IbtOnOpifxYE6SGS7Fy9reWt-joosfetrUNgh0B1RJ8PdsZyUVKSggWMk5ev2LyszWZ2nIqM3COaUhlYodmNj8C2dU66ZRp-rKQ4qOlhMr6qsRTw5tlYRti6_SkRrOStBM5rkkWj8G1fI4o6XPs3WLWQv2Yn_FSFr4oo6M9pD_DlCr6Rvv8YxB8wmRbetkovx63vVTxwPqtbsr7ZJJGAeKJa3C3pb1wYxBc-D4ujBnCTRmYlRZcqWR2ZUoMPU5LBgPnDwsyDdfySkf7pzOjoFOTr3T1wgyo8B77Ury5MPPJepcwdiC8ZUVMjt46YQkcG8u1LhrqL8mo_xESYuE9zNzkKrtel_YbUjb_eA8QENg70LX4Tk.O8O-yoCxv2dquKw52tEmkg.kfa_47syWUOzQB_FVh2sZ2RF8cPJAOxxKpomXQ6Ecjf3ETmatQverNcSoUKNa1pKt1bMpNiaD0zmv8c3UARJzSOtmdJ7_hu8UDMd-F8hweMBeDczbrWZU5Ec295bcr9WNJuYUAi5nEgo_wgpFGJHQMZQ6p8b0AxViwxkwPPsuajVfwrxfyddZi7I7T9BQabjffkzzm3NGvxLMFSes2sZ0Q1Ker9RgA4PmM3YueOSbdU-5VbIHIMPnQhnwzjkDRcwUf1NytubJYpzCNZwZEOvhbpMawhb5xpUu0iy0uWvulMJ5kafYvZW6KGL41TpcTLZsiFgrC2f6wmzsx_ijihlApN1232ygB8xLB0fBGdqgZmGbwAS3c_1bL81BMy7IoBqJqMMJ1-v8VEshASQK08-ScNK-x349Flt43siaa8Rz2AwrYHEZojuut7X5-UUaTm2PcnpsWd1QfBZqtsn3EJsTZnj3eG6WMH_QBhT70hoYRiMikHGagQt9VSc3Ij9XX4V6YhHa1p06W9PvPbH5jLEjNpZRSy4ucp6hGQRTckz-mn4gLov5zQMnk3xQALqPkhtluJBFL1oMzlC5-3gDQi_NBEX5ZCOd1zeo0as7HV191g4UEDOwyaBwzG2s7YzBxiu-JJVc2cpxPt-IWtlabm8DPnPMdbVaRbZnexA0m-OCsVILvqE3bUFke494oYaoXlCLoWi7JM66QUujE80JuAq_9PQ0sZyzJ6EZgkgjHrQdp2qNHe5FjlKpcFsBaoERs7tW0C-q1x30J384z1bqWs_ZgeZSvTt-yk-bZTESyQ18UzztMIEmW6wI0BQWB0NpEVlHsNVVSpoEbv7Qu3CFA8PWANbUSao1TqtL94lFb-5QLIb5TIjB_JLqnmt658KPHhCI_WQ8b8ttBmUUDOH3_7CPLAmGuoEeZ6H9vyKnhdakGWC_Pi6iswzpm5o2DdQlSHO6Ab-lPH9dg3VWnKJy91nHPlioUkkz14hyjyOtr-TrKCQWhco4b-ZV1VB64ELrVGvV-94kO3TiTXHNIF5RNBWXLYHPfv4bubmPFl9mn0AA9RZP22Ebo-uTa1FcuQTCp4TgLnlvQyYVWmBCRKhoNixLgwII7HAUo8ee91Fx2lLnKQLiuyyRvvkMpJZWNEFwuKMoQyrxENr2etrFDn_KhqmIsqlJL6_CG6OSFzX8BydtX6n5bfdlcQzKzQJwvVHJS0z3bcMTNTPtGLSnOcnqzwrK7YCzR9RZBAC6n2sN2m36judb1KjeK3Z3e7zM3ObsAL9wDei2JPDj3w0WTYfCCFfN8VhjGlqKCyuf4QJBEAHW40OrKZa1UNpsmsfon3Ttc5W8-8cTtEwbAtmAs60OJ12go_AwUo4SihQrt0gARcz97rwMhtgoE1AbiXC6ezJTAxqCKeljxXi8oto7gGdbUWXg0tOMUP1FrtlVfv3VCODjl6-Qucin7BCEqu2Kom8BbXot3WPB-1mXKmfJN5B8Lf0Dl6nbpXGR46h9mE01alBEBxAXsEIRXrEtZSPvs3JVBg_pH8MIOis1_7Xu7dWPjPWY2WkqiVYO7Z3EVZCaQkSV9n-u0p2CL0BzSZMNkJEXC7agylnUcfPCVETm4fbgk0hq623wLuT_gEVXmeItlmfQpkYT6eNi8jGjwuVwqtCG04yHhQhXi8B1cTdKm6divu4sf0PGCf99AbY9E-V52G_ijqryJvWYNnnTTnrDlR18lsrz_3jSgJGnRVlE2BFL6xbdqwaitFH8WT9ULbzOkROKg3FnI5RcKUPAfkv8EevsQ_Ac-ik0vBiQN0-GnNjRqlbg9F5Elcaam97uvNt__uVR_CTrZ6pB5R21n_-2iaOxBJSfC8qOq5b2G6VDbbO8j1oRomuVarptC7-2J-bCVs-7fVdQitksldksCfMHvoGfZR0WDFaTp400V5aK-mHwFFvG7rpgrGu_hHivUb1GMI5vEwbaVFncb37dCS9dAiJ_5grE3EghNx4lJTPrl8XHEQnCuSF8k_H8AvTNlk4rCtriMDjElvJdGZFZK64ZXq5t3V6otLShvvSJbNV6hBQREl8ZRK_izfWbQSqB8qxBDigubtXsGtTQH8k9gtC0LG6YKpY_a9elhnFfNcEzPg3RdE4AnK-k7y04EwB301je3zis96IVICmGtdrgMQM7Z_oLrSiFdTwyXUW7Ql_snn0UEHwa72jM4dI-i1l4kNkNPi5b-c8qHOrAtxacRcBchjtwRg4w5Le2KIU5FjkMtnYs6vpPq4t_G7a-SEirsk0_5fnbsKPpOj_QJjCzc_IwYm70C7MhRQFmyIUrxbjWjZKTtUpd-yy7s6n22HsLlV15pZpg5FZaVi8ONHoX6DgnhqWvjrgLUnEidvrLCMvRYLg6qtPUDJrnMngf5YTRVh_NF6oKl9mcbKkbJLeQ7FgMxMMHpHdBjO8FC6wjPrBuoHxGK4u5_E-QAqgSyDdqGYlLUD161IVpa6g0MjXzjyFeNE-IdYvAmjaolfbKBjpN38t8UqxmGPMmy0Ivz85OulWta_K0qL400FUSxbAUMj6PuTD3MY5BtWZ2r2NM8aktheBxynfA5K1O7eRLpTS0TUCxGcTnDDKYh5kCAgRwm9XGCEJRdIl3a4JRIw5mv80y-PEDljkbNLKXlGjS4aVjTGgfrz-OxeHbbPrjZcDo7xPl4_DNDA0XXQwKMTxP_1OkBnsq7zEbHYdmHiBj7oNKpjB4nN480lNGI6TSTWjv1lcMAYt0V6azF4E7WfBbkFdhQMHT7CoOwYxU3XOrBRDlqIJL_T36EwbK68ta9JK-VEvLlvQMmBBoe4t-hXkV9rjdba-jUM4DY3czE_6RPWXtx6UicTko33PfDiUshGTpfJk0UXBGrzRcxwjYsrwpkBSdMUOTDvKiHKOstNWRkVpfs3C6c1FNkFldAlSOUvN01v10IQ-U2bBsNwsww-teddhy0dkoeF8K68n4r6xPxuXJaJJyuIyhj6FdfAoXNAGHLmUt1r7SnudENxH00abcG2YVBfBug9olo32c3CiGYxOEJh5LiftWr08M6A61dYOIjEsAzYabz1hLRnAYIl7O_ugzDDt038rjAbbabZk6Ulakh3VbmqtkJ0J_O3mewHZtrq3PfcDiOphkm1IbymRBUk9aKf0ypPk7jjDFIBdMQsdsEKrypgBiNIubmqdUlG3zbk5DQz4CAuNRDzgjJBhjhL97mejEbJ96FtlMyAhd4vo3jk7hbqUjK8TplK_VZ_m1Ral8md6l52Q-yx27RFhrjhAFsqfgmMQcgiKpltGPgH68iFN0n7-HVVqdMS2wVujgOrVjn4ya3DMtxjXTaqbUSfVREkAnFm0uVsMw0cw4X-L3dkC_3e-m6YGBJiMVqvXSKUKJIvLpHztgl0iV6mSfn2Ew1tAel7o8M1NbL3iiiEkyTxqsTCmMRSmuX8imcwl8FzuEUIDimiiJ0qFV9_mg06LeTjBCm5tC9ukqpFewP2nQFrtGVhGQPjNy8giRDdr5NoXY_P1YvQAnedjzWIEe-6itQp0KfOCYFX8PWLG-w6GhpQiGt8LtHqVIe_yap_O4a3JX6xpIvGT3SffNGPEkXcPWSs0u3p3tnPzr1YaZJ_2t_DrAzsghlFjJxXKgtsogt30TabP1eGI2s53ON0N0TJVUf_kFj-ACpn1z0EGoYliIQNxy8bVBP7_p_2smaMXFQngmu66TA3o_tVC8msWosSBkY7d1YIMQ4Den3pVajWLKEwYvW-9UbLPwTsWN0IApEQiyG2Jo6-QLi_AEUGov4cn12WYfLkDLAkY5aX47RpaJ_r6wP_o_7eu4ljU2v-r43MaP2zT5LtKcnRcYVzuoaqCyFJqPZexx_evVYQxHJyLy9z9H8HkoiD_ZrZVqkkRIHKSjjdCcO09waoNoAlcY0fwxmBGA8vG_R3fJKj9FB9YMrEPOo9FnROsYgY-WkxNOx6rC0K3QKn4dVV9M5SLkp-1qj5MAUnqHg.XhzbF40u1S40SoQnpm7SQdhfKpGMA1BBpMPtR-b1Xyg	\N	\N	yCqXFZfgCrXLuIqxgVSebj0M0/cXq4tXHGYVGH2b72A=	valid	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:openiddict:params:oauth:token-type:authorization_code	{}	dcab76eb85b14323b3608e6fb38593e9
3a20efd5-5ac4-7e32-b47a-1a99eaca814d	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20eef8-aed4-978c-4cf7-7e3be2a6ea06	2026-04-30 09:11:41	2026-04-30 09:31:41	\N	\N	\N	\N	valid	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:ietf:params:oauth:token-type:id_token	{}	aac43fcc4bc4479fae2a5369901e9df6
3a20efd5-5b99-57a3-60c1-5675f41c3a63	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20eef8-aed4-978c-4cf7-7e3be2a6ea06	2026-04-30 09:11:41	2026-04-30 09:31:41	\N	\N	\N	\N	valid	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:ietf:params:oauth:token-type:id_token	{}	97c70c783e4d4853bb11573e4bbd7990
3a20efd5-5b3c-a53e-4fb7-4c13a9f663dd	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20eef8-aed4-978c-4cf7-7e3be2a6ea06	2026-04-30 09:11:41	2026-04-30 09:16:41	eyJhbGciOiJSU0EtT0FFUCIsImVuYyI6IkEyNTZDQkMtSFM1MTIiLCJraWQiOiIwMzEwM0QyNEVERDExRjNBOTY1NzFCRUMxQTlBRDUzMUUwMkJDNkQ4IiwidHlwIjoib2lfYXVjK2p3dCIsImN0eSI6IkpXVCJ9.NDy2H7f-aQTdE4o0MiHIgO4Oj_Bh0Q_jrXhKhdoz3gleYyiwu1X2eaAK8uikTPWpp5QwvY8qshi-xmdH1PsiyT3CTd_gOpai3QRb0o3EQrUelv65Ch2OLZRbUPqkGtCznFz9cb_DNGPUAp3TWPFGdGm6Y1lnprkwKNjJ6QbIfb9bNphURP5OlhEpSvTONp4X64ahELwbHNBTsl16chQZaBWNeWIjJ3ZQbME0ZcbfLpmD6YzUDZHvgmReshCmKMGF991jYhnpvnSkR3dJ_j1IdqCN1oHg72GmsmlXnxJsExtAycVgekivfmooz6H6FGje11zumKTWaBvPg5idcvRScuvgbYw_lQIX0y97I3-_6gR9IOwb-w2YSU8flX0AdKQnr44wroZ_Bunla3PJGf7DCS8yv85Z9z3Hck0NVbJb5NAfIsDZj_d1rvjKI1yquA_y5zCWo5ZtmLU9D6WKOVi0cB4AdtmdrjsErx7lLyuuog3mfOtMMXV1IB30IDdx1vLdlSLNbpaXKeRUbJzXsYsQEKkXydA_H9BwbIk9YC3z9UDSmNf-sPnPso6hRRsNOM3c6A0qn3xPKt2oEJ0VXfzhdZqJl1C8imzt8MX_AiWjQ4wdiKnKBriv45pwFDmfSfH3ZFhppkvrtgOWyIIKsKIHKo23Fqhq0RBxcqcD7GtxNP0._t0bZVqrX1755X42nPvung.nLnbwxY5rJ5gdWAdFF4Mh5x0wESm9ZToQ4RKQQwT5uGmx1JWWaTVZEfnuU9LFyiXdchB04ZLZe8FyTt9B-aQwU1piMIkeFbT0pVsnKYABqIPk5pVjIyr-JMRTjeXntq3OhMRIBxFw_2vqkN5ngP0M98Ec1MH1ZqEXKNIk5eBz4ct3ctc8dNXoh0shpEgTsPgcXmAV440Z4X7JXgtCwQJNlpqirqW0w4UEDCf7E6tt-Q6_xsCUhx8G_j-_6Rquohr_VlLFCaoadmoYhoyWdUpgS9oNW4raCWBI8bp-VLZxVVFuTqcy95HvbBf_LU2ZUwB4U-60uo7m2O-IAaQlbJRbGQ3vAKqptWIbfKAs6wQpdl1BXvhwBa8CvKeCtAAU9vJrMFUcg6WIWjR3lhArJ0Nh9zEGh1kqqLW-igSCN7rlXTW_ISKSvHWzOY9j47zydUgUtOto-z6Gy73lnO5o8vsooLxt8qwc58oxYltOgwV0ZO_Plj3PRZM6i_xAr2KoTnMxkMRH7EYFWadv4pZZmr-bM_Qq4WZ1ljq9vfix520QlJXg4f_we7vN2UPHW_7-JKgVAgWoZGZOk9EypKsLS5DJGDtF-aH_f7mCpXjz2jyUXxTXO1fJ3MPlzLlnDaZMbhYBxAEC90cXXd3rIUYhnOI4fBMFw9n-SQg-bXVC3LKzOvc2wguHsOoJzHBvnD18vSy21hVFO12yBDvcFiL7aOuTTOsz8Pd98kuGzXqpuZKwEuTWIRA8ockyulwa2M391-367xMqR-ehq2gN4wRdVkpwLtGNVKTkp1OUPXJEJ137YqNWWFW3JLhTkrBeXnSbfsF2GhxBf1b4OoOcb_kru4gbEpDZ9C4oNoo7lXlaLrXyRh3NK64zHEZbjjekvWgI8TV9tlpKRQASJI88iUSUVJdk9_mS84ocx7HKfe4PuaF3UT5Dxun2yPjZMRfxRTxUmMtTjPqN4R8CAO-CYkKd2SBTB1THE9CCLdi5wRhVQAznoE4lL9im9qBeEPpAE67Hu6yvR53VGysph5EEktJN9tY5XTz7976oUr5fz7ZWeXzdHo3Rdf_f32Lo8B4MltUCgt19ITcS3dJ_kC8O78prkoL64rLyWY_98U8Y-GyRoQIR0EC6_wELdWR5LfHu9h4N8DPo6ft1Yt-smgFkjomKJ3Vo-liegRWs10fthF1NQG6LfnufHZ7qW2GDAN5CyUPGN14I7fyha1EuJOZ_zXTKlO3P1zH9sqJq_hO5MVlvL_6q1hIKCmi-m5MdM-TPxosOOZgJ-Z91fQ5tDOiuxfadMu_ybc2Sg1lGz5po_xNx3UXYw3dasrFhdmJmYFTI0XAO_wswqkbS9cUznoHcqnaxBNce4Q6Jet4TWC5y8OVanjhZGVkXxtFRvCn8XuKskM357edeUAIntl30NlrV4mwg-5Lz1_9NKkTIzvenBeF-Qjwcy-QgWVFr-g5lRmktIyXE_ynKHNR5-g7mdGeOPHwtbaloA96kMXzxkBgqdGkWKG1dmHuhAhvJFsnGlUQd6mdqxCiFEF8uMu0Lc_aXYlOJsZXEiGnW4UXjBbXryHka0e9VfeVwMybSg8aISuM2QITef4V5etJCIiCQl3lu92pcsx5lbXS11b0Rn5UExUgbpGGmN26DMoT3csmbAGQKpVH_SHYrCsp28Qq-Cer64wVhbSzfL0SlaY0sIoyjgbjKo2CWTNf2UfvzbEb_aIQeZnLc6Nb4WUShXvJMKYYcR8xhX9UY33krrsT_3kArfbqxkJbImOboD4xkxECjnGNUw95JwczQv0wwx3-M3V-lY3JDdwT9IZNwWQwKkbkT_L6-3Uc9Fw-ycbeZQKQipb7QtOzF9bHscPI8z44Y-_rfFqmMflZUch5QVpy8g_ESxAB-QY6YPrWNWbf8Rn67U-vrCPD59fLOAcVs3MnkN29VBWXk7z61lFSN4m9xNwUsk-Gd8sWSsOfrBPQQMmClyuEk-YP1mi2bMAzzpUl4fRRsiyjy-VqjhjIKl81RILVL1ZbRTnd85f_pfq66uzHj-9hbpIro_AAyyf0uyv7ptGJzChdAfjk8XuJ5haeIrJhtyZOKO7IUsSu2NzoqDSYbCTV8KemjGkbm_oCTU2kkQdLBbdcBXGgS9L74mUcRh9pqk7rq3Xaj-SEqnJ2wkUYURtUqREkJcGanEwmnlL1grE6gQM_TWEq48jv_vMsYm7UvTOJpQRrgek2GvIDyDciw1WOmGtGUwaMQa60DtTz7uKEOUMCTI_PAVgvv1ruJFxY2G0csOSiLBU1Tf7vGMYfrk6r_RoY7TyYBwZYxXzO7bFE1FcDipaQcJdstlmh9c3_iyRIpQIWOE_Wezx395FSv8nPSRVE1DAWfCZgZ8RmyqG89BLl1LjQiUBquagZuKkXQ8JLloFYeG8PyAbZXukLDFuKqpNl7jiK0WtTWcyZijKwnIT7Txqclii57g8zU3vxK3W_NzgebX9uJ889WdTKJXiW6dPJnGh0gMvGIwOHP3BIwu6xNRGCKLwGFNoYF30Th_bOhml7BdJ89iNktkKFf9LQ9YiiWfUUZHlSJ8QL1RnO36LGqvZVnWWUIHsPBLpLbkw7tQ_lfHbk2vc5-BFBGCyntlF8gDF0TREFSV1hI_OVqIDDSUrkl7PQUv6C6Kzr3jjkMhLd-XaJ_Gm8UIoBjypRXXgaEc-TCwQql8-EEclKxrTJrxKNDCQ_39pxmHAZgUOjADXLE1cOXQbXZKWLcWWsVGcZPZboIPd-Bo-JUkRAyz4RBcBotR2VJ_YLxynetA_uhJ6DEjuTPsZDDb1bv7HZmWhP72uY5cYYKXlS0uNEIT0BzH0xoYWonIL_myY0GVvMDm3_Ievxaxlru-GDXWxPXMTj7zxRIxptz-cl09dyNVYVoxJDhYFCZy0FYCWEVCbQruJRugrZqgmTqeEsrzYW7lI6gN19e7CVUC7Ra2CcI6Y_fmh2cywL-DuiNlOWUO7vScgWdrnvxoq4WZ7LIHYTdPFyO93PHA_mOwEN5aUPuvgjtWn6nzj2M0MdqbpMjVX9nRhiFHqBf7KTkqgfLdy_Sfm0A4atYC1ezjpfXsRwuZlh5-kC7fiHJnamxJo-OioqY2FAp8EAppx8mFSWpiWw5GOhpHbvsBgZMSQ3t77oWflyD38Shb1xp_pJQnVIz5KcxXfL6mtmgj0JXgx-1JlyrOeQ-HQr931_sofHXpKcz7NtU41i2dYAfXUdsXmCCEaPIaPy7DJjxf_i6-Lr2YTiL4gBerFVoYhLqXASaWLO3EE2UfGKqtKGu1_uSTsuh-QPzcwUXylafwW69b-zvW_CsREZolIPjSQ1GFTYlEweGXMaqUgTLzfVf2qx3femAVogccFH6DFWak1T_5nxePlkaNlGAoBalxHW0WerVJp4AsFh03q-tQ4lGtwly1atrYVVs8rWzdd54nVf3jr97vwOcMNrF48tTao4LKd7gqCpVmFAIvLaU7jMoeP2dyQctnDahQaB4ZrSvb9yaFcmVKJ1DTSKYCmvxznc_EU0XAEwqTMDdABRjKf_GQrlcYRYjLjtj9Nn_E0yGWjY2V2l06zIsTZVFGLpZbbo-Iu6gBIk191LgD5guuXuh5O78v2PtlztUSklXRnmbxcq_iJYtNaoBU77M5ao09MDVVT2Nl3UFxBWf8J4CJ8PExmQSPrNKU-EfnhzchX8AmLB_kV8LYI5icu-0vK9SV6qgqEkMiMKbnyUR8iCDQ0zYoZ-a4SYaRXNvsgln3x6BAnb_L6jFru2IaWOe4tqRj2b59QqFPj0-pk0bPAq6j2KNZFm-KYX2zjsXyBubAb59GaHo1XD95i2VcgMHXpEgDeCxhkZogz2b6xYO1q4_LYV6AqjGPhHhtlTRr1-rSvdVnGElvdASa5zbqemdGgx9YiEPBa5eKV0O9P0M3SKxi_RlxEWD3NnItByFGtuXT1gcvfCaVTK09ylxKOCuuVFEonbItwQftQsOD8HcCaHtQ.4X2gocHzVPhEEugY_AWvU72VUPcvBxqE0X8_9TCbqM0	\N	2026-04-30 09:11:42.011129	Zf/pzHNWXbvZMAqisTR3CeEi8DjiteVAk25UmNdC6bs=	redeemed	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:openiddict:params:oauth:token-type:authorization_code	{}	ac226283930f4323868484254b71a061
3a20efd5-5cc5-a71e-83ff-e0de4bf7c446	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20eef8-aed4-978c-4cf7-7e3be2a6ea06	2026-04-30 09:11:42	2026-04-30 10:11:42	\N	\N	\N	\N	valid	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:ietf:params:oauth:token-type:access_token	{}	a37fe58e342f4d1798497941d938ce63
3a20efd5-5d1a-fccb-6138-9e92e775367d	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20eef8-aed4-978c-4cf7-7e3be2a6ea06	2026-04-30 09:11:42	2026-04-30 09:31:42	\N	\N	\N	\N	valid	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:ietf:params:oauth:token-type:id_token	{}	5f1df198b4a54980834b126107fc9b0e
3a20f046-6770-0609-f75f-5b503bf3f971	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20eef8-aed4-978c-4cf7-7e3be2a6ea06	2026-04-30 11:15:10	2026-04-30 11:35:10	\N	\N	\N	\N	valid	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:ietf:params:oauth:token-type:id_token	{}	b546e107665e4b04be9f1b60702292dd
3a20f046-66f5-de5d-7e75-1c569583a46e	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20eef8-aed4-978c-4cf7-7e3be2a6ea06	2026-04-30 11:15:10	2026-04-30 11:20:10	eyJhbGciOiJSU0EtT0FFUCIsImVuYyI6IkEyNTZDQkMtSFM1MTIiLCJraWQiOiIwMzEwM0QyNEVERDExRjNBOTY1NzFCRUMxQTlBRDUzMUUwMkJDNkQ4IiwidHlwIjoib2lfYXVjK2p3dCIsImN0eSI6IkpXVCJ9.h33tUn4NV7tmtEzGcUokklMEKDhYnOFehMa1rXZaLXz43OqRpxdQEFDN1ZALkg8_wcAIN0pq16a25MdQ4s-Wg9GvFE-GZtoxy69bMYyBx44iB3aecER2O627uAI5woB0N8_x5totp2u8znoWrsQCfzn-BwEDSTOp7foa0UjWY02lFiP8H33jerqbzsUANM63Q_VI7rw0Z_3Hjx-Tat9FCxTg7UcC20xzV9yvkkuJdGkqF6FZ7ZCUYRZHjuyiI3w_EWBmUDZgQ5j9CklKpAYkhNjjltHQ6YjjnGUbxUvuoOROXavxU05pUhVpX8fn1zpAt14q8QzA80-3D7AFRHygigbkEavW-WX9jf4GtYvwd_bgvHMH924006xRJWZz6njymrTFYaMC-3y8XCbsX-z0QscwLQNYxTtuHXNxVv6fGUNb80mokxwos7EdQaBvQYeZFDceFVaHDbKCGNIfTdZq2ngZ5iqGKFdo0nHImaKCo6cYsWBmGgcuIKrw0-A7y6f2tuacsyB0cUbZ71oWpY_11L4jKTUvqmEyYCYCkWZi8qoGeEuNIg5wSG0N1OC4lEFi9VqkTRn4aUvmdDEL6g-5lNxpwo7HS2FFonWV-qA4toBa_s9mFkICvRLe-b3Kcfo-fJRBthTi8W2FOSBrQFlT_PPZOP3U4x1sJxFOBtjJdPw.SmdnkJ8vQOo-Bd1fSO2FFA.sLALaBg9To5woI8ZEveEjW31IZMHL_EwalESBEjZJ0elohUk_4qjZVedEzTAjh_2s5PCHUxDef0RWwQj_OY4_pn5CYT2RaRYAt8SZdQEDke6UwGQGVj-8PeNYNoTZGYK2jWtAEU9B4cgOyFyj2aC01D0VXQ8DozFZ-xZ9fr7-xTUphkZUwt9FEsqhbB7ocWc96RvSJdv2zgKrQ-Nbfv1t7zJfjda__V_05548y4XCtHkE8wGdG5-iRKk9Nc9vaij9rUUT4ZdEXGwS_evSlldDYwhvrAafsg6pICDFMfA4gaAKeJMhTSOQQSvXXP6fFlvZbHMczkq8l2xRsPzjYNSrJveiPFPd0NqanBM7E1Q7wRlhOqGWvMROogJUZUqgrSwgLKIZbwUaiaq7Sv0D3rmXTlNp-2YI6qgFZPlCHUq7hHRQzVmqpbFnMQebmd1n64EOyvDN4qkoh6RHmzdnJLuqV7_ZZR0I_IV9rTujFsyS455OrMgXqK53nU9yrg9KGLNYOV7ba5jxJfoSgMORK3QlZYpsYTQc90S-2uB-jaoOLqCzCWOq-39whcTR6_ZomDcjILIcCTbTwxn9Y1PcEoaPg2JyHyXQaydQsxKt50oWmHy29C40q_aTbTZThJSKmNt7xlbs4G5eFu2ieepInmhtJIiuvQyIqlUv_KNUW_vx977iWBxNovULwD8_mGqTcDi6E1Q7FBbuQ4tUrtX0vHk9LHjHd5h5gOwp7xvkR3FO6XBSAw3Z9fGlLY1DcHjO3fkG05zeGIxHaN9Fyt20NpsIr04YyxZNHNFn8PjivzY0DD90PczC7frFX7cfVowaydW1nTW_jhu_f2f4JgbxGb-TCGaZavap7xmKtJ9hYYu9li1yUC_KItkMmthdd5OrnNTsNi3YYpsGOSATw2xokjp9KI94iZD049Qh0zfuoOBFlpGwtWalg5UmNOvYTqN5fOqfvRqoj3O615kixy8pI6-3XPEaxFWZzYgWRJBe7g84KBZvNNQOR-LMrKf5Om_pX3J1s4Hjv18PJD7OEIZzAHHbErXlogrw0DbBc3Hp0EC1C9EsKOHz7QPpHt9bMY8oDEPs8wSC1BJl6ecJKCNgKAl480nnbE2uK0RHJs8suSqnSuj2Dn0hMUw_WrexZzQPw8jqNwir5rq3yve7vaC7INjXXPeYbpOmCbiLxorAja3tsb2Fbs5KV5qDOd_KbkilwGvX6FRJmMGDv1JYorjDutraDOq_pdC0_rznjTfv1RHX2LOg-Euyqwx-Wx4Ra6spQW7fhRqC6XNgN6Lh8EmVyN0syJ9B2B2Q6hd8voDtETxy9Gnh4I7K3h-MxmeeacuU14yq2u7-660YRr5KYhayAKraf5LKH9J3fpqbx_epBnqIFI0__8jF_S16mNKci6g6gvLPbXW2nRebXsoAAm6p1SbDrKJ5EnQGTTdmlPJovYsgFm_Lrp4kSg6LGFt6Q1U3VbqUG5Fd1xrvfATZ6vJGaKIesOe1xGFGCdVCXz1OYrdlTzBJWkyuoeTZoY79SLnMFnBjJyH3LpiXNcL2xKTNcWdgDxci2weNjUirL2GEdTg3Jtxi_2PTVQPWoWxYqUAppN3oOzDfBN9XgIWbJ6Gb7gWj7mnP50xbUXmrdRCSSeGBWxzKMEzmMH6jFQCxDX6iXvkdjbfgIpKkIm70fuj2QnEN2UYFhKPlu0GSVoRQNpQKThZvItf19VIM4sLfcTeNDT7WPikZefQ_Cz0ZqjPZprAcvX5cwcHd4pp3qdQBqe8pfTZlpe22e5ggqlgodpm12YTzXNAFnEdRCbd9yYv5lvHNe9hTiTRCMv66XP7ZWUhvRZdlwvyrozuTwDf6wOaLNhUobqWEjGFNDDydwwyMUFJTznYAoi2Y3sF7Z1MdSgqZL35Zwk_ejIVwehPDQGMaqqDyktCgAuC6z_op0LkPGZygI0h6Rzc_fclkDi54m5Sy4l7im7ANKPTEujqqdSsFmtKj0urDjKNw7KidPPTna9OUSTKLLaLW30Ft90t57ehL7fTAgGDIfo12i9PMzUxnpyZFuZa5soVUojZ_9IafKVj5Rz9GqEvAmcItF_U3nz015mXjvbajK5ItxCMKtBAqfpvJ0fC5P9xMlzWpdb665rxl7gZNmzQEg-TGgGPBpOz0atKTXBa0F1oq36ZTDdHE8vrc3tmxvInsMNUthGM7grCl7PgWeNgGX-Zj6EKvnLAai2VUEbWThvJfmVXgiKsDlJQX6sRXhvHVdJf-v-usjiLXoJE_FtrR38SoPQ4L6UQXEr6bqBSsdW2VDEj6P1qvkEaq57fY9qMZEn3DiCyvtCCXBV60MajOpqspjmFGC3KtlJrpezbAlkzoJjZByPWl9g2tog80v8-VSi1-qK8poTmqj8JDlBGhaOAc69uqENxi-v5cBwWkaSwDNEPrbptq04mmkLzCpCRoolMcUjpEl4NgYfZZ_eYb7qKyEK_JHIoZcvFQBlAKIPvZ5uQ_xodCXlapogVUC7nFIVJRa5Q_iyOzBkf3NP1XiaFjNWjHHquCWPvkGTOCYhpL7_A_b94ka94WCj2zdHOONAEvdSY4omNmheF_qUF4LcJdfHQGO4t1Cvs_syCaHFzHd_NLludaU1BOlkvE0sehrJru3IrBxXM_BhcioEdgNxDgBPnKEUSbPiGTATUiikXvDcnWzmSomFUFAQoEd3Jzk9xq1pUoa_zqXjXJsyQ7ZZUXMeBdTVPP6zXRHuthCjjS3a6d_Oo2orsSJFSJolX9nVk_5O2VR55msEyWM6mNTLtUPGhe16nGZf63SP-QdXx7bkoZMteE9KdpsS5SV0spzVo5-E8p0Vg0nqV514G9eL1gsuWOPUrzuDvF0Jf7RmalgRpEbHcEPwi4rH_Q4t-XQGyJUMk5R13AyvyhyPRvYv2UVBzOh9NP715fosZzYg3iO1p3iy1BU9qeO8sB3IdGkRmj6kVE0MkFW7zcaFKBi3VH_uGb9n_6p904zhJcp6WoU89MQl2MiUs-nplAKL7LFbTwoBNKn3M9k0wBNeXAV6AUZHXn4Rg9Lu4xP7yyw67ROWuvniLN_HMn52vxLWoCD_oFMxtah8I08eWhOiEUQ9uU3rS3wKaIeNs3HclscS_l8VYF-pXKOxNOkpFsLzzpxXi51LcWjG-KZrU30HKHC7rJHgNDoIqMt_1irnAs0L_r8Y5mUG_X79qMTJBz5FwhcZ4RRIVRzvuo0OJT6l9MYvT-vH-v6RRtTlDq9DnsfTlxQhc_KwVJf1EjFGrhBISz_80hVjK_h9_T7gGUFWscCrqNQMNedmza__scxPDDp9eRGx_myxAe2FRUpD3OHC_9HS-Inisu3UDxUwz9KdiqnbmxAMt4mBaSfzaNB3liQIFWy47sfno_pSr0fXRdL_Tcu1_dOu9eHmiGpnZxk3i6XZ-tqhWPY_MVg_RwvDFMT4mt_GJJqUPZyXZ9IdzSzv-uDv9if2dJggYc2G4hfU_pfSkrRDSx4V16-5r3k6tO2LHZGV5gtDOrmtc4W2VurpbXa8wbq2Tjnp7yj7qdjcIT3ge8VVOnVK80Rf1QzpQC7G8UxJncapcecmF2fWbONZylMHx5g12TRwh5IArEgVvs3XBjd_MxAyZqYlUhCpkFuzCdCWhZpGXzlbfXgJWDInF0hNg2kTdEawJ7rVCzgaFU6lNc2pAPcEyzHrhyeZqIFbeKuWqkptFaKbUV2I0Q-luC59ADzv4WvOoxy7nBt--ZqydnqGbapa9j4QSRoKvI1EDxZQAKk6OwEL1-CHdrS9t11yGL911vFb9FHmDOSRBq__wH018IhQS93SsPCI_B6aTtTETnzcoajvEbWiQrKzgLv2IgTz_kg0mel1xNe07eA47JXG5__Qn8Olooro1JpXiI0i90wzz53cwQdrhn7pw8VnweE337bJjMtLfISt_qn1tsdU949l1ICRvTWazNoKnvG-Wlbu_gu2yNz3Q6vus3BIhBOutjKn6Ww.9AJTALmgfKRR3ZfwDdjJ2tvV7hZYYT9afqIZaWV0Px0	\N	2026-04-30 11:15:10.589748	yrhwkNCxn46PuyDSiSACDrAJgqU7p6kb8WLrr1BL0e4=	redeemed	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:openiddict:params:oauth:token-type:authorization_code	{}	f8608e68deda4bd78d53d8b0ea03ee57
3a20f046-6886-9de6-889e-9c772ee9243a	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20eef8-aed4-978c-4cf7-7e3be2a6ea06	2026-04-30 11:15:10	2026-04-30 12:15:10	\N	\N	\N	\N	valid	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:ietf:params:oauth:token-type:access_token	{}	aa1b6bdfa28e40598f051ba3aa4c89f3
3a20f046-68e3-eec7-3b58-05da26b37216	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20eef8-aed4-978c-4cf7-7e3be2a6ea06	2026-04-30 11:15:10	2026-04-30 11:35:10	\N	\N	\N	\N	valid	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:ietf:params:oauth:token-type:id_token	{}	1ec1ae2e2be246a8a3278ba8e8dd155e
3a20f3f0-ce23-f077-36fc-606a67ccaaf9	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20eef8-aed4-978c-4cf7-7e3be2a6ea06	2026-05-01 04:20:09	2026-05-01 04:40:09	\N	\N	\N	\N	valid	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:ietf:params:oauth:token-type:id_token	{}	e2d35749a4d841c4a35c3449ecf99ddd
3a20f3f0-cd24-19be-8481-8182ca0da4bb	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20eef8-aed4-978c-4cf7-7e3be2a6ea06	2026-05-01 04:20:09	2026-05-01 04:25:09	eyJhbGciOiJSU0EtT0FFUCIsImVuYyI6IkEyNTZDQkMtSFM1MTIiLCJraWQiOiIwMzEwM0QyNEVERDExRjNBOTY1NzFCRUMxQTlBRDUzMUUwMkJDNkQ4IiwidHlwIjoib2lfYXVjK2p3dCIsImN0eSI6IkpXVCJ9.aE8HMZooQM3Z9rRsClyNRtoHdqefUJlm0K3Y9tbIrWBvFF5f3S2YCJOHzCzdZmKdUGi0ZUePVEjrMn6QJSJ5DUoiKHUV0Bo0NdqS9-Z1CJNQVF40RhDW6GEaWkmFWAM2VsqBsBAmyVeYREv7Tyvz1eZ9O-y2Ud7J6JC1Wm8o9PGBzoLm3PQfnhlL73fNtyApIfs0Y3VS2gx7-a6M8hZ9cfLx6gmgLqVCeZnuwmc5yWBL371pzGtecU80DBYZyhYDwJHE9btVye9OpB2Xy30_guacbnFnjuho0Vn3bVGoF6JYIn7L5p3tr5oF2ccgSZwtlGUHTCAQks2qwACSFJ58fYRijmVqFt-1kMJ5U2h-ldsSBir6wRG4oyxlFZT1dSv0MmlAhabSdy-IbnuPSROgF_u45ikOpsLZ7P-iJP9daNlVJo5r24VJuf2PRuouXS-gaajmV2elvX0pnvslmHM7D4PA7PovHnUZbYrj_yyDmaxMwxTh5b0Z7WMBieW1zHsOx-ONYLNlPBqBZpkQ32Y0UiphjbEjxgwRT7yLc0w5XgmTklJ6fYcBQ8Y7xS4Ort_2Yy21xshFsz3EqZ6ZdumBr9RlVQYFYm6UGLB1PNlRTVWccg1_kfgQMuVgZK6b46GF8Bww-_O21cfdN0WP_lGeAvOO6ciBRRWMUx3KdYdOWnE.LIafbD5nfsxRD62bV5CGnw.sDrGUKGrbSJbxMFfu1UVU2idARRIy4nSku4IF5e3YMGUTgfBAD1pWBsFNJtG2b67uRQkB1blJLeRK2CuFY9OMVSDZ23eN60fpAmrzSXlekO3AM_inyrFJwtKvysvlvKYugaVxwnfUnf_dMfVCMrtIHmfbvElGzjaMoLoKf0VpjaDXt_P5BVVQczXqtWZg4cLgQxFeaKHwzy8Zw6vMJ6U_Hid50fG6mujoWg8hf5yDsSbbchSPkn15GP56Gr4IVsCzhVZZvzSnOtkZfV87qtmltXlTSssjupfzgWbTrvFdrN_6N3qFpecCbRch2N7P1yiJksAs0U3m9oZ1zBXPF4ULkQJ6ZzYi5SHaqDgBHwnELy_VAY6YWC7f9nSZeU3LjItLak0iFH7A0iL4ecBY-aSsQ0MpuQnMB2ZBELOAatOTrEQlLieDU6GM-pUBPy2QpslBhWnwfQcEjai4PaYqh7XpOxByoJWzvr7d5aRAFyuTD87wYUknn6N2EBKGgMz50I2KZSOYnVeaFeg8h3tKUCffZ9lyO0oCE-JNBh8wYndgw2J7fEQjCFbC7tRMZGsNsv9Iwrt4h2gFA1lqKEkVuBx-v6iLwJpJ54QqgkLA8ltos5PT-4ZRtVXhbKbAtxqZEUl4GuNekxyEd0Oi8-DQB1oOx9SKlaksTr3yWsQ4mUx21VZ3659qB1BN96lHIiWZ9x4WsfvnTEN-mLr_BLC9XTV32c502WmPAjWqit9H59pG-WgvuOjTKLuJXS8Bh2JxyPrvTFZRIBHqxnvHAMy0aiYX5jXThR2cUlbZMm0l0_K5A5Q6WvRBeM1ziQUMjHpKwQb-WIlUban0tkPMkc0FLgY_iuwAnPJrpa22Vi5RxCpVsuwzYKzxjUALRgwqhc7HV8Bz0WC651sI5tdg0UBY_sz0OT0FZwHwP412wHbmdhlEQwDIUywRjU-uCv1km4wLfyFtunxEF4dFwH7h5MQBiMkhiPam22JMwXBX8RYKT0zb-XOlOaOiyC_hHv5gqPusO62iCuZNl3AFZp4bXNCKyRwMMj90mGsMV06IobX0TvoAwNG3XtgNBUx6GO8CpKnBXTTjWqqpqjzzZ9H5qmetK3nP8rxoRaBkK506oB1Ohsp5qvWQwwiJHEVdnHXyfjS40Jgd7-HGDF8v4W_8rTU-z5JmRNqPPHWfUVEEV7eP3Af610FiLLLY_gBeKTCiPD9tKXe2JuH-bJdJ5oJ68JDFQcY3Y7k6RQM1Cm0v07UQr8pVv9iBO5cl_BSty4GM6q-eDjZDzibyQmWMGftuT8cyQ5EQpBhr3X4T-g3cTpxbwG-_YH_fbgXT6ukZqUulD5Vzpr6imxJd6e3tXeR-FGhWdnRBs5ci7jZltmAR6rXif0KR_1NaxorOG22tKH4EoPGSuS5lDK6HvvY0db6AY_tRwJp2YBwuB0irYFI2c3M1XWKJg2VuClZptCi7WnjPNh-H7PfXH-ohYNCVAwlnumkv8HXOzmi2Gvq4QRpHHaIM3UQwAlSOPSIxr0wtuS5-nwiangmGuN9GpQzdfjeuDFkbVSoXPkywUMn2mib1KXef-2wdymP8vBRg6wlCsq57-YiiUvXjEvBd5IJRVWWX8PWNTRXupoLMKfiUqcvcc1ZGdPb7kWzG2xd7brtqob0A-Rb8UKrwG8UgUmNRm-diNQKhu-u4aofJX7hthNJ4poQ8BpfFpyU12jEPB29EU4RRsP6abaiigKilXbCVCSOWU6lmVm-hrOScCCXK96u7HePasjjSRR_XUF7jysVROeDZuK9FNaQqywSGemFTl2VRZHuRGz5g-q2P8J6lRKHRpp5BtpMJqb05oowdr6OdqKcrfdqBlNXz8mqGh-La_8R2T17vv6XEFiE1C5GSio7N3epk4g40QMR8rSzA1SSQJwuUy8emgESbKs7hnlXArqeAEMVgGa6rwHC7_I_HD9mdKcKoa4uK3Jptsuhuxz1050eHsg4eY6t8I-yufI6dC8vhE-4wHxkpZeyOn_JiQegNL0GHqtRL-yKNgGrU7ULYT6pt_Q2dSfa4T6DxGfxgS-YWWqjEegKrJooSXxEjBoFEwjk2VcOfjq0POHBYBQJ4xbPDDPCmbfw5gJZTST9862_wtjyHocQfAgdbdYRygnWlriSqmu7xhlnU3mzrR1KNF99pJJ7EkKZYrX6qk-slR5mTR5Gf9Uk510bNlj9A-odqzSANspivAUXf4IYg_UXCnhRe5TtYfwVPMPy3huQghhsLdltZaEocSv5WY_KOWJUh7CamDwRGXDmPN1Hi6SHnlbvYa3Yx0_FL8unkQnm7CMa2b_xIoxH5MPEwo1IjgyOn5inEs3fJQtu8sn3QEWUCBUXp_BZpTwtvMioMSzH417avw3UwzrNjNYtqfeggM7x4b_t62EqMdKifLXKozmTkKASq7KBrqtRJFOo2Yfb-VOR7qp4eVgcI2Zsf1tSIM9HUyD-MZ5bd-Dr-BWUYWzECycyLf1RJ_T5Lo_DFEqxCUxDeNVZanaylMAjwZBGzV6c1cY2ryLT6QJ33iiGfX61EWXDKyHFwB6N0RRRM9OiKTSy5wvPczymUGUjy9BXtYyTDvRh7e6v5yCw4djgfPoNbBpaEhP-8IthzHbjfzsBZ6scu301T2ftFU1dsNNtURbunJnsKd6fh_ta-51xLiv59evxTNqBvjqkEqLLkpAPmJhUVDXLHOFpIiQhLF-AS9EIhB---yprws9smFtY3Yag2oafXPwVEOw2yFoDDBqHunRqbnLbLvnXrhywu62GnO-AJaRFFvto_Dk2sBvSX5Yw1KBT3o4hcYn8HMlufdiVwp9B5paC6xbGBiflJQT_SYNmyzr41CpmTSylEvjlYi7AIbwd9aLp8jW0NsCndq6YxU87LqM6DPhkiTI3GnnSv2ttP93567MWJn6FnaNn4sMHLEdFPqRJT6tNlr8T6gW60eZueQGkBgydW4-AW3dgYPelEAZDbjuUMcYP0OxQLOzA097-Z6zVKzKfHKC0Kyqwjk0BdhOgqs8LC9zqlnBwGyoMCVjR0zubi_9c2f-jcNBP058DECLKnCtvxeyZM-Px0x7fiaSIdjfQ75zUx1SvoOXE9Sb9wDP9Rnnwta_PDnTl3T2aFxG3BmAOsCp1-2hQLDQkDpQ1T2tmW0mJGht7iBXEEQ7gCnzfDI5p093T6us2-YZLt7y_ht_K6aEW24BPGUO1kj1eJ7sZB7bT8aaAoszoJFtu7AhyhGOqqhhAS9QbdRJRdCcqNWMcKMdzmtAKpa1rnkC5hgrFbjWgH7AFUgTQY0eRHiy1ZGl-8WCbLEQwvZyvdXWqlFpiXj4wZ7DqlYOuFtUhM_p48QK0qvlP05rbN6cTDp9OduNa_wVb4RzBxtHRiqoMh5BV9jW7bfSg6IhjSSVTPevkrgnimPIfhgjSKqo19YlEyWbF9JB0wBHFrcYlQoxCLSbw5p7hJ-d4-wYbX9AMpuXg-lGgXsnyWpu3aNE1mNgQdURfrmK1NkN5EWPPsH9k29PQZ8OA-TaT67IKcg3G-1LlYEUYQ94cpVifgS6LPTUjqtM7MRk_LVF_gTviOTIuP30mr_RWWuus-KpYnYE9esrf-rPsmOf1bb8y7LIl85xqCWC4dKaaPnSrGfNRwPxNPYMOyKD7nQFA-0z4KA_J6Q0jmqdyemSQi0jeybzvCIJjqOISYTnZjpptMiGe8DgdAtSWp0mTxQSP9fRM7GlYyxXagWcxrVpmZMfEitrLz_7obV8nM9KYeo2pBG0yPRnoq3-JOTtdjvhI1ovXXvI9Y5o2xoAJC6Qwmnz0wT5y15POLTGtWsoonSE2TZuaUGTvSLnDS16_Oft3hp8uKCEmFu72hNuFN7AKYiyiVvtPd1Ia6NRww9hogvlG3j2WcATsiVcJxqZC-q7IDO3qi1ZgeK8cuHGrsbbBenn-fd07y97NE6qPiGDjIWymWPIVgPK7gGgaIeyqAjALQSiwrYxpf1_jfA.fT_tEv68g-GpAbylWkY8wERq5c9WDZ0rQqenjTmm65Y	\N	2026-05-01 04:20:09.75512	iCvfFKIVWvI8XGj5j0J70U4hvwXn2+FuoBYbWW75o+0=	redeemed	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:openiddict:params:oauth:token-type:authorization_code	{}	dab27d6dda1140e0bbf3a7a8def251d4
3a20f3f0-cfad-4d17-687e-5347d762712e	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20eef8-aed4-978c-4cf7-7e3be2a6ea06	2026-05-01 04:20:09	2026-05-01 05:20:09	\N	\N	\N	\N	valid	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:ietf:params:oauth:token-type:access_token	{}	013bbbe0b5734c83bc6c2a5bc8bd7cd4
3a20f3f0-d00f-7709-bcf0-4f27b2298ea9	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20eef8-aed4-978c-4cf7-7e3be2a6ea06	2026-05-01 04:20:09	2026-05-01 04:40:09	\N	\N	\N	\N	valid	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:ietf:params:oauth:token-type:id_token	{}	126efc06176b4373a4ffe8dcb560d6fb
3a20f428-8fd5-d6d7-7a8f-1080f5433a08	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20eef8-aed4-978c-4cf7-7e3be2a6ea06	2026-05-01 05:21:03	2026-05-01 05:41:03	\N	\N	\N	\N	valid	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:ietf:params:oauth:token-type:id_token	{}	02d46e034c4648bc82d4dcb9324aec5c
3a20f428-8f5d-b5fe-2b02-d4734a48ca6d	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20eef8-aed4-978c-4cf7-7e3be2a6ea06	2026-05-01 05:21:03	2026-05-01 05:26:03	eyJhbGciOiJSU0EtT0FFUCIsImVuYyI6IkEyNTZDQkMtSFM1MTIiLCJraWQiOiIwMzEwM0QyNEVERDExRjNBOTY1NzFCRUMxQTlBRDUzMUUwMkJDNkQ4IiwidHlwIjoib2lfYXVjK2p3dCIsImN0eSI6IkpXVCJ9.kLo-FB0POKejTHYMLQVuH78qpC7v4B7H7f0IYe4eyDaiAC_EOI6zbj7XHyR1BjmYDM-cNxfcaB5RDCIhyDugWQuviCbL-NGB7vSVoN0iBc5wjKeF6EMG0jeNdJc97GeXN9iQw56aXDTgUk1Bzo3ujIVkldtEe5NIQHDRnQ_xOqXoa8oRq-OvDSxiwcL41Lcpd_5rpXQnBTeNvlC_OnmPvdQ3uRFL6JRH6WAQKawEYwI3WeNmZuzQAfgorX_WOIc82BLeKd4IHynQ-SQV9e80vPvY3bQXpaPbHxQF71Oky_oPcAjZdVMzuRUlzV7GF6ukqydfLJb4U-Kh8E1Kh6y82bzzuFB_k5CMNnR-buwV-1cGC3SCXAl6JqjIcdFZjkYgUr2pc5n8Id-Pw7TfbkRyDo3RQFMPKamDvgM3z2_3F3lh3AYJSzqV7XlrGvWunXXnkMBh8MQtRUzKoivW97GP1OtmtnThiIeXBMCWbKSVOkpg3LSWMnMzOqfwPPLgQz7QAwtqeKocwebANURs_MTgMS5KwTeP8MgrxZFBqTtYj-B0OH5rnLzfdjWRDZFQSf1kdVPAwLYheZT-WmFagSaE7V61Acc9S7ElYZC4c8HMQm6uzpy8BJHPsFRsEMp_jrrfAhgvLZP9j-RhfUGDJVGEzEDTvBhxjssGbyXB9WO3QyQ.GOert113vvGjUFHC_v7Hpw.GZGxxJ_oen_3gh0k2igv3Md6SQoLwATYORD44tnK1izAKgmg1103iPRGmva6j_S6EkOA8vhyHQuQJIE2Hf5Yb88diMj30WdLJLcP7Uh6lBgEDnrLZJz0wx4dBNDvL2KymeAW2HOxmUFym9pyMV5B086cYryocwCzt0K7AZiDdmtk_OE4chzhedkT4uDqxsdDRIMviaqSwheV6segT2IjEeWdZCUuvU1kQztl384tqaNYQqEvvR75V9k3FG9OViLETzIBGLVhrnNWqVBcmdGJ7-ys_ol1awRgPAJDMSV3TyE0Wf05iwY2CXa7I9jzlGDEnzrX65ed23B1WFyBQoQ-6H7nSddHsCeeq2ZX5pHD1s8tO3FBnV8-kW4phVcrXn9KT284e3YfO4IgjPxMwT_oidq9TegDgXOR6NYUKGHyRPr8yPZM11-rtPraUw4cvhywN0nVyXUbuIQhCpvYLEzR5WFrkUYxZsiUUPSYMU63dT7LF87BxBHBCJPJFF0FN9uWJijtNgLF8JN-BF3dR62EaRIZMUL6cDoSaZgSkXuWPaRhITdd8cm_K1i6CEwOTIymRG3GB7HSeFlvCkwbpQi_lrXVJ4y80WRO0iJW23Nw7VI7bH-pL8OPDkMNTOtlPAr14_P62DX4zt7CX3usgGVFlfDOOIeLpw0nwLbA0pSH5J9HbMGWSHA1CCjRsFGulS84Ff2pNwoXwyT1KSjfE8z2KyXY8jxJ4dxFXHa0MlF_PZGb8ML9bMlw_bITN6iz5pXSFshZNRTWfuE2ewQVFh5OAvCY1JjII1vwlB7aNyhtkMSMpZLF_w1kLrtLcA6lg7NFlRHbx4f007MNvAVoTvJbpfKuZ_nkZC7YQI4WNmMBqMVLRfwRWrXLPYRfXQyMUlPyPFbScdHqvRkMcf-n__nROfiDe8KzNMpA1lzYOov7utt9iU9U_4j4w8zsWdt_G0WkcRKudcvKXcURGvwuOd9vO9CdZmd5BWty-RnyzDbl9dNxWiPM7R1EmfwyqpMplziBCh33icJ21qgeDjQZ-e1bXG_WDvljLGXAGGIEvj8KWcd9BmzZpZpGQq4DdChaN8wFLxfQYkmoOLa-49STiRjocv7LARZlmSMl2p2PKJ9doquGCeb1GyCFYpXADNMrS3f8rG9_WYmknnsHxwSgrwVrY4f1cJXgWEAIXD-mmvcIDkE0LdWb0g2graxFbR9iRDVZ9a53h7la9hS8J3_7a3lHDhIr4FuSMj3rYHiFLjahdU8yufdfqjB0FzKwo1Tv21gGmG2eLpoCs23x1qMu6-qtfaDZ87Q2dNKbxlljXTPaQvax_y_9zHoGFi2UQWk0y6wCPF-yhQZid9ye145Q_-XDnpSBVeVNZb8Rp3eT02uVC2_28G_TQJGfIx1OhT1BX4IUF_cwppbpL2E72bwko2otNHjqexGbRdEGuAkm6fcqwAREW6MGWsO2KhW0E3vAV03XV4JOsekhP8pKRrh_yrMak4-FALaFgxU2TsV9G6ZfznSvw3PL8lVlJnSsUIhAUXgM__hetpiG7oFyg4MzpIJj1zJo-qFeB1lfucSghpCmTJyPFXoq-5dNZJz9rKFV6NK3RPWrRk35DMSHOZe_aJgoNquKq3OR_xubTocZATUwqCojmThV3oqK3AZnP43Ljlfvx1Q2djIYYl-IbedL-ta2FEf0egErsYzfos7U9o07LGFh5e3j0TV6mpEN2gEX-Frf6DhdAGtEZCQn5BXCxoR9vrIvO5IEp7Azs1IuvSbkvL32G9xXIJhAxC_QsYl1StD-p9NqDaogQAxFbJ6yN8dxGSJlbrCRKUWGd4RUoNRpYfbEnVrVsDBBME80iN8WTs2ppOIBWlWOx34c57mjqxdTvMeNr6h2HVYrCSwto9ljVF0oWsXAi5-tFSJPlLIM03F7xWodJzTW5fa44T2C0oN13jLVx947z7DjsYQF5jxXWAW-S2lLx7OYkS6MFyD7dBUx2ieuPqZ2e03SkvXfICpIPcLWu-iuFY0TthY9pDkKdpD_1pZS_XmGZGot4wbcVFZzeDGNj3BfXAztxgCXu0gcreni6jL9BB6bmEUNVq93wIDnz58w15lPZc5r_j3TyaL1uLerhotgcibLYYFuNhp3En6wM5WQAeBWMRBfQkwQSFh8Qg1ymLyxxpfvc2rFvvHnIdEGPKNpsNdik7IlMGAnNwdtBsmNjptsnrtXz9XdSoOZXbbvjqaKT2mQUMFZrh7Dm1XBJvP7bLIWgl6u2xzX_xTLrMzydLCeiK8BLeQHeFgg8PPUJLfjT-7CvELQtNpzOr6Ovx3OPNWjhRhgmvXDRWQL5wPW9kzaPXxSIE7CgjEjJOUTZI5ioixahKURpUYZwdegIgbwnLMiO1NWe14ebrJFk5_mwqJmAxmdB4kMP8eXsxpzpAXg9rIT5gIqRAVscqWe4Fwqgba-gZlfxaU-XjruJp3Y2dlJpo6dI2OSAjfz1FWu-RKaEWjDE7-9skjtJWEp5TeOuCvbBPfbb2p4lkriCB1HfgIBUcvH5sNLP-w-Rt9kt_2VAsgCeGxOXZpY2YTb52NdwY6jKSqZ-GMj7TlT9zt1U05KnHB1XfOZ8SpjLTA-xPA1xDTOTaFCbrsGgoIsoFTH7FcTl0YYLyDgawDK9uceMSe4xBzXv9YpUk8SENGwhNUyVNSclktCYZ9gQt7XYcBuETVeYdlmLxsmfmWZRFjWTtDYCb-C29vwT9uDPVg8MDvXV6Inpt0BWoidEBQpvl2qKlTeuvgjRrrCrKscDrGPIC8am0fm_8HQB6LLa3PZdsg63lil7u1RnH-HUAsVnRXaCkln-UC4KKrPjNarG8K0j2FL8gdeCK1EZrkuGAgzVy3xs2TZJsz_3jGuO5Nupj-uhySGXHjdF6bMIobRonPFFlLNrQcJ8DwO_vKu-gfrFPIDCvqWM6kBhHfYn5ZJOkITppcIApEb2OZrlnOgwDEAX55JJL8c2CBng-nVtR6tGthj2qq_uFtP3sZ1P6CaiIpcaXZn2aRDgsrTVGlcZV6ZO5ViSjAFcgXs5BvPVBtl9JDtgFEgFVzKJMprcjszwYW3ra1j5r6_HUDnZVoMbb6wOumh4HDkhVcMKkJW8RyM8OVUMURnWm5DVGmKvYVlbT35sMP2QGMT2G2wYIu0j66xwuOPCNXBvuZV00L1vxXkV_km0mqVjF2nZiIVZUZhlQl0I8kQ2s5_PBxV2euq_RKfNrhkqunW4KglnqDsRhjWmZw0AhfWS5V8pQTh-rZ3gVY0ZOFP-Ms9quwd9hRrKDoHCawS8ntPtwPWqNIy3H_4LWQnIo9noa7Wl3spjJxMt-Q-BpQjUh-ACisNk7K5dQtgZ_Q_Bap-muP9q1IuUmSrN4szOUZtZU_HFgb8AW2bQfu0w91Bb13fh9Hy-LLFqm1ZoOHyiynXMDVeGAjRgQATve0hw0bfMBaBwWGz1OA91TgpmBOTSUAIRQM-PqsPguUFVIJM-ywkc9e91CdFR93VpMinHn3WitgRtsB1ACl2qhpP6tbKBe7SmfaMjoR8-vN39wfIzk7qJI52He6cLFRBdOW60l_fuv1TEZ1PjbHctelf3GxfAew3E33tvUI71wkURj4KqH_7sE8vfcaH42OJYKT8pdS3sWaeA3dTokvBP5vpVKuZw4xiX8zzB7lH8V_9o0S0Uuyj9kEGjZ5bmT-LHbdAU_1hAhSFEDwWk90g-kj6zhgOFf4btKc3zZiSma97jDcFayF5oWTqedtBaIkt6MjfZDlneNkZibzi9NurgtOpURqWJt69pCd6IWnbTFly1IoCPxvttUmD9sm97EUpQryMINjhKei3FwBUAGprlQUKmGCo8k7cv4RWVCo8MnpXfD8_NPXdA2HbohcUfBeMyAghhtX31kwZmerBA0U76VSPHkz2KTP9E4Tl8bO-rsANX_yQOdibZuN-XnznTVEOHrb-657IiNg__OonhtRM_9kvOcIHYzcay7O1pA.FTAqLy7ID9tsAfkctfNMqWzYspfP1gk5jeh9sKGYMMI	\N	2026-05-01 05:21:03.750914	mXh/0libw7BwI0WEsBgiUvenWxcBET8pojQJpgIfVfk=	redeemed	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:openiddict:params:oauth:token-type:authorization_code	{}	83df3e0dac2c4f57a9c77845aec88a71
3a20f428-9116-7f94-fc65-cb3728c56880	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20eef8-aed4-978c-4cf7-7e3be2a6ea06	2026-05-01 05:21:03	2026-05-01 06:21:03	\N	\N	\N	\N	valid	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:ietf:params:oauth:token-type:access_token	{}	be6443087269429dafd093d4d32abfa7
3a20f428-9171-0b03-bfdc-2e45f6b4a96c	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20eef8-aed4-978c-4cf7-7e3be2a6ea06	2026-05-01 05:21:03	2026-05-01 05:41:03	\N	\N	\N	\N	valid	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:ietf:params:oauth:token-type:id_token	{}	e24fc904ea784542bed00402f0934089
3a20f460-913a-31c4-ed3b-378d570db4bb	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20eef8-aed4-978c-4cf7-7e3be2a6ea06	2026-05-01 06:22:13	2026-05-01 06:42:13	\N	\N	\N	\N	valid	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:ietf:params:oauth:token-type:id_token	{}	cac3f3d4ce0f43f39b2dd02f611b1394
3a20f460-90c7-cdf2-b0d2-219c4af0257a	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20eef8-aed4-978c-4cf7-7e3be2a6ea06	2026-05-01 06:22:13	2026-05-01 06:27:13	eyJhbGciOiJSU0EtT0FFUCIsImVuYyI6IkEyNTZDQkMtSFM1MTIiLCJraWQiOiIwMzEwM0QyNEVERDExRjNBOTY1NzFCRUMxQTlBRDUzMUUwMkJDNkQ4IiwidHlwIjoib2lfYXVjK2p3dCIsImN0eSI6IkpXVCJ9.SgLeL60s2p0vnhGanLpcUQKEtdMHvsOHsLRpIGydpIhD48FyKyqXjip-z9-XVAhiuCH_4BFDuySTgQi-kcMEM3d9MPImN3ynphiRKgxk8Y7iq5hTaMFa5jaaFojrKAufhrQFwkrwRB1mLPGwBhKR9v5oFUMNbswd7k1zVQd4W33pjEHNKHl3UADeEcWxSGotZC7tgUyd-Hl-DRjxw1SJuvnqCoxx3Nv-aT0Ew_QltsBZFPYFeP0ZAEryt8b-pj_rs3YcTj_Ijl5exKH5obkexxc8kyJinCvuhGEfh2j0fX64w_e4-vPy2GDOXOvJzAOkI44EUaI0xBFO7JnsXCOIC0Fh356tBcKgJCKTuqoaKIwXen1jvO16tnZLAUQ9d_x1OjtoCkAndqfYdDMservPVjEEDMy55x2xeUwhH9hmV2km9X77Ls1e9medXK5ItHZe8-zt9l0kvT2qIFEaHDzicRItFM-IH6mxxflTJV7PNiWaq9M-HHe5f68GuffpVQ1rAIGAX6G0MrMYxdmEHjtGAmeom5xozaK1dn0Fa_j9obFhYMcSxXN_s_FzB85h0gHwPNxs_h7fo0kU_XnWrvfJ7qtWkxcuypMyvead0hpvH1ZynstZTRNNEKjHXZQrzqnNV-Fab7bEivtKbnl05j4yIc20o9Jw2i90CRF40Ezs6zE.j0vU8bhmYl4y_S_vHr1DxA.3ceR0LHdCdHSlKuGy_Rz4qOqNXT2_oiHVMIyWPRTz9ntmgXE7-EukrNoiVtHlLoJR1TO7vR9v16F8CXgQke61Nmo9ly8mT15TDjWl3fLjKTLYaHRSiBCnz0peyO-55hFv3mRJ4IkwBl_IstdCm77DW3zAFgHvpsJFHWxK76NPAAG_JWhxjpwcZEJLrk1-cLeb_r8kc1u_D2DakyrMrrsugPgUfBtDrOq_qN1eSVt_VNHMEwXepzr8G3v8ADVDEYDafqfrrUAkvgYYgqcCrjmtxVRPbuFxbBUntcVJVmn3iFFQl9LdYq5PsotO9nhLyco_NLcPsMYuAA8BoRf9uekj4oN2-TlEXqJllhxrtxipVnkmJQorFaT10yGV96NUEJc-Cfs7g6TZM3C3tc-bJtk8jz0RE-B4DTfdpxTGc9pYS4L2M7uRhEkUuj_HfENKnIqcml0zh-nv0KAcwg4fvtD29oTZgqHXEXdUj7OXeng-hvnyhPXvaFC93exfuNTGdCk8v5m9KL48sQruYHJthuCvFaix0JY1AR1YEzkkHpyZn_8klt2fVJTHWUjl0SvY42YE9D-_DbMBmfpYowF5CH7GRUvdP0G0w0Hpn2i464KGdFnMcVQIERNoPxBqP7haMCz7Ell89zX5m7wGEQhKJ9mLUX5xLoCWBJUPp50aa9FuczYXGH0LVnz7znWPtJboPjRYqtptqD3Zsaxz3WpO6qGetjVyuw-CtL4C5LIw5hg0-vURCpJeHWAxR6irhsbDOOP0peEjpQWZISK4-iJOtbWLzRCygv6NAmDuylE13wUks4iDuIdiPrEj35tpkdwtcqC0gQrJDmmTCWsJQlFWfPC-a5xPyjsNMZJMqJcQE0tjhf0EBOioFWF2obOu5X7-KEkDeddzd5VyOYWItMyPGmbD-jnzyihTtVNt0kIUUHtXDBfZCPxs5oSyocSNWN98nbR6ErCEaSvCmqYyO6WlvPC9XWMVuLs_aVckoGazxemq2fZk6wE-j2ZEqGmtNIxVBAankbsJamb_j062KsBB1LUZcHGEFYaOpirXgr0E-CG5yzBcDayYlQV8fvPtxcA7WJtadVzi4C0hL_t3zP32VrRiXnpw4e9iA-9n_uhZTWK1e-W3mZFh5fT6VfNsw43PdrwKz_PV9ESOV3BcxcJ_HPiMy0zYL0T1CuAiOv-8-Qdzw1NZf000xsjGnn-haBM-dLPALTbKyvJ6zIXQuEMIBF2Ve1q7rmKPLGylE65dqrIwqk_1tPTrKa68ssdo_yULfSvaJkRTrXJ46O4oHmLdBr3vCY1PQ1Bq2izbNcr0uQjpiRg4qvs_se_-IyDV8c8jZeMKax5CuYYMRgJfS39uWOuPZ8Cy4oaE7bvq2IpFHFYqY7PclmBCjixdlRYCKzXi8VVnfdu_Njbco5KHmDFyhdUvYbnfKOq1-R1R54IfnExjUntuwIfnGu08ZF1yd6-cPJwbs4fvA5jNfgaBpALQNNLT_B8kGCbCOYEGSdJxkMrBekfwcbSegFpNJLZtgR7Mkoc3EhRR-emSLsW2XZB8BTL-zQmCLig9bQrRynU1h0Vu516mKEUjv6jspXryiXlzjs_xKfmJ_UfSbC0JnoBXLCvn81GsSo1a0m_XJLQj5e9-AOnQxaiBYOxNxxcHcE910OETbgPZrb7e_n_nZBMKAh7VYZDiMy0HpBRWe0DCy5BQBfDRURSEnRsgTaZ1ypyKNH65xRhWBleao3y7_lZDQs6TO6iv6T2DgkPc33dR2YDMhlTU5D1aI3zCfrwJzgrwKVmZw9bGKdDPMxgpOM4cTJoejoeaeNYG5diAxveg3xUUUwWeNKrcOiojlCbfNwkFC-JlxpKT1bU-AEBzh6P5ctHsvxlqQVsJzIJDSbC0DucWbknBfwQroDqySIyaICzauULTvqqpEq3A4CbCsnRLksG9m-nA5W1uBovXpcJq1wVnNiY2go6cPdmibGpZzD8MyTaxCb7xr-URwKICaMJvKgpmdoOGJUmijw_5rTkdUOvcomWcHk2uYyU0bQVbuTxNZeNSeqGDATczwh3Lh7ciNOOrdDbuLR7-_BURDds5UXpG0PtnEqtHjfmQN8X7sRQFExjUiIr6spyAiPlH5y4fD0X1crsAL2BxmCQ4EPdMemTT8Yw4dTDS7-7dD_s_LzH4_saywMb0jVFl6h36Zztf_NsGUKfreH6M8qXFsfVUr_ANogQ-Cbch2ON4xMNSqvrcFoBrxPC0tA4LQdYT5PYaGe6H9Dr3rkm2YHqPNU_kWZTuhtntJuogYZgB6QXzGsfejcEJhrKmjoQJ8P06dUZEDaNQuk9eRIXYVaHHE5CHeyzfxlZ0lvTf8gSqm1iyAbyzkwBzHrNhQMkkpXAf_loKRHrZp1jsrh0datWi-pUzofkNn57aBn5wDemsXQqOQAOL0aU0ENQW-lZvGTy6DpuwVd2Em_6eX5ZPcWnYzhFrnEaqhTdwtMTPB5aHe-9DzzhDoILi6MqZ-aIpzBlEWmJa7feNJzmJU5JjHUCUkaocZ6XFYuivzwXemJEhNVgI-MpD_zwnICF325juFGTiL2kMZrC9n_wT_P1b8lCFSoqRrozeO510mrnndFBDkVcklxVwrfGl6Ig-J4srtmYLpKjx-5ntjyID2chmbKa9F9y4hYmK-chqHo4bLPrOg7Li0xYlEm9ScibSijUC-84xAEZQO284OMZZ8xADUIudDQ86aUjVK24mtLzAJ7M3YZHlesBIJsHnmI77hi6xlmfJTE-V80KRAEQOoHQ49av6SEGA1RAm8sQKUSdxHRwvq6UXAW8Jc1v4qmZY2aIT0Umc5WDFBynSQgqnFsP4mhIMYJe-4Apf9Bt7kjkTS5MsRJFaIZekIockahflGUVylqZbAcYIzF1IyZXUeU7wp1TKHs9aelQE-HjCgkm7iRkCXaEksnr02R_egjiodKZ5zwPE7RDQGvN75iqVsue_g1KjN4bu3JRkyXzK9ve5N2yr8bolGCoxV-vR1mpaPeQ6KU8L2XqD-zsmLiipcwWrJXAIDwTVcdcM63_W6USxsWyF1S84_vkqw5pADFmJfPPCHCdWA5YLrteP894uPipqJfEAEX7n7kjWpoSZnIB_wu7cq8OFwjZmeIQuNkQ7ynRlnXBJttjWqNhBgaUiRX44Hvj8CimQi0AAkqaQPhZKrZsxVZYWV15wy56e3vyfKwjc8bPVi2BZJrHVqgsYUNmu4sjTA2umLpFTJxSnj4SU-BzYUHVnpX9neHWZ8GlT4ZKTPq9G2txXNtk08FRsz2cMkNe4l7ovHxVMRljPkDQwMEqKn2pClaV-_2A0S2v_2JYHid2C8MIbP3hF2AmK_ies0pLt-8q9iNuyNxbY9BS_t5DGjIOFtko6Tuu2VzHwzWSz7FsIFBHz9uS2RQIHpc3x8MnwJL2k_RTHqz2bVFtwDhXYEWxgBnqLJtrck4XLEww8jn5NsRn6iI4pl9kgLrrheLHg42cYN6B8gMSPUcRaxIeUa2c3AWsLMpnrnJ5yBnq6IzlBYE3FM_MQWm4S8a1QGefC4gPIRThAKiUKrgmXAlqRH7sXwwbFo8VfQ6jVF0sQwjnLhleb6huH4r491tM_PnnZHKNhK8Py2oRUkrjz4hn1VHyS4IoQkcyFePWDLEmOrtUgrV6XIjoUOqTgme4wjG1Rr-zBRr6cB4aj1FxhR--DdUkuUNtmDezowPKA-mfGZoIdO0fN0a6s4LgNgxMM3JZxlOblfC1Vciql_DHp3YoIeM9dZAt4a81CCRhoYgFWupWIF2ONgW-YR0p59KcoO9Qm29-NrUhpS-zgXKT7gam7Ta3T7profqhJRQam9i5DYbuZ7KCVx4WBuRgI9FU6RLHzGGqdG-qek2en6Am2KznyJ3xEt4kyVUwCOPkzDPTPgYzTi4ub97FdYeSEf0Rtn64ztexN2COBdITs8Hm9Ls4EBgrqtQwFEGjIE77D1ZWX6eKYRf4_kOQow.EdJJ6A1iu7xgcOI81eOuT9V-8Tl4jW3XSv6ckd5okRg	\N	2026-05-01 06:22:14.119207	JrWaInES5+7Cdc8ZIAm1hnyfxcov7yAZNcft8Sf2wcQ=	redeemed	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:openiddict:params:oauth:token-type:authorization_code	{}	591845e8f3064c9093ef9fb8d61577b5
3a20f460-927b-6c01-dc78-a84774e436eb	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20eef8-aed4-978c-4cf7-7e3be2a6ea06	2026-05-01 06:22:14	2026-05-01 07:22:14	\N	\N	\N	\N	valid	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:ietf:params:oauth:token-type:access_token	{}	337c9781af6a489782b32983d558bff5
3a20f460-92dd-221f-422b-9ea9621bf1f6	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20eef8-aed4-978c-4cf7-7e3be2a6ea06	2026-05-01 06:22:14	2026-05-01 06:42:14	\N	\N	\N	\N	valid	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:ietf:params:oauth:token-type:id_token	{}	16aa4ca30b5b4e1ebfa3d8076b6627e4
3a20f498-f1b6-68be-49e0-4fe2c1c53500	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20eef8-aed4-978c-4cf7-7e3be2a6ea06	2026-05-01 07:23:48	2026-05-01 07:43:48	\N	\N	\N	\N	valid	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:ietf:params:oauth:token-type:id_token	{}	2607be3fc5424c67886624ec0651dcd1
3a20f498-f139-0439-f14b-377fa01e0ecf	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20eef8-aed4-978c-4cf7-7e3be2a6ea06	2026-05-01 07:23:48	2026-05-01 07:28:48	eyJhbGciOiJSU0EtT0FFUCIsImVuYyI6IkEyNTZDQkMtSFM1MTIiLCJraWQiOiIwMzEwM0QyNEVERDExRjNBOTY1NzFCRUMxQTlBRDUzMUUwMkJDNkQ4IiwidHlwIjoib2lfYXVjK2p3dCIsImN0eSI6IkpXVCJ9.DvUYCcVlEzNFj9yKFbgKhUaqwQOzLWKO5YLh5Axko7Z3M3aeMCTP6xlL0OXQGm2VFFmbQwM7MRc31NczO3nxcD8vKTu6FRTQdz9RldJahVCI2e5fOI80Ch3wYWKjLZWUbZ61n7S3jMA75iiU83QIHozCBQo6E8Ym34xcQ1tvpgKQhr-7Dr5dOrOcwEjFY_5YcMUDxZxvyMy8W52570YrgrKlSah4Ltv6PP-LRXNNR2NnsWM-BlCl5Pn3OUXhTvlS5ztIjH44hEDuGm9R-FShZqwWlYxks3U_1t3DKyq8CdYk9xQ7VMJi6GTOXDzS1rKL5UuAqD30B4czwdo1PUkavOkNtBwMD-KOuGOEo93g8W8EI1pKjAmgl1mRzN7nqrDNHpGi7CTefPA3bcJ8ek64aMWWdT_xQB9qrlofNELlYQ_2KBebanIn6B5hDARZx_DboVAXYw-8Gh54Xvt-duwsJAS2aIoPzaTeHFuzdWgeVlV0-XiR9-jaFcTzzhnCpbXdCHBOH7_tjquK7G5GuEC-b2EM6feZIrWUBLlo4H0xXDFUPlASGU_6wOf2oWUVHfTLJzIlXGcpVzkkxpI5f5hmNUE8w9bfF_cUjMShwFlLRHLHpNpbeV79lg56Kv3ates51DgxFo4VRmceoExpM2IPVRBSjf1QKtFsWq3DC9qR7aU.IbfAvx1gZ9cpAygJPqKzSA.lVcpLOrljkHu58UHorxVnUpl8pTm_Vvt1-sKEkaPzMUDVGxSmbbOOxGuYNFggCM9MVhvAO2G86HTeiDLFICa5M7getrsR9NoP1QN7zxLB_3LcuOLMncvVOLtJcFIl91OM8Z-Jl9_JreFnbws9Y_XakHF0xp4ZiJwn2z95zBBkM9cpVpw4j4MGYx1SX67KwpXf7leFb78h9-xrvw6vPwvvRchdVliDokOV4GSk7y7DrZCZA4DZPxoPb2pvzQxq4qDQK8nm2sVzmlH0pZzrwVQIhWAJYPM3AvTkbqrHcTf5uZ1VHktVs9qBqFJszggmwZ-6XoXhWrIMDdCVevlfj9-Hv4lEMIlEWv3gJZB-t-N4Ajv0M7wL0rc-exZ4SfnqtIfUUn7juCBhz1Tmi1oRq1732a1Y-Gf1SMp4byBKUi9LChC8zGaou4qXnF0i5j0NH27vBSQ23j_rH6gI6pHE-2L5mPVpzxIWte_30jpIrxjW3sVXFudklsH1z1HTmVysxGlea09jyXuP-K6PStbbX5_aC2Sju7iklogiHxWcnXMYZ7bDIIFH7tAGwj3IZBe1_kz2ixqe9aMmvRoWa_ha0pCbPoYB0-MUapSvu0td57qqb83ixYOHhCDZP0dQ2dFH9O2W3zkyiXubCxPGv2fIdbRqAdpAfiM1-QxyiK50CWI78AHkQo9zzlyINcWh0njGS4LuYB3zaDFQSHIoGgdtaHtnnL12nrfRcw50sfYO4BglaJYbPUMzILWmczif1ysXG83qrzfs8vzA6QqFcpbzBzQYbBvhibGpmMJtUgNnuzoUxicJRcLOafy5Zm8hxSB329ehQABfUrVVdPfo6mZBwwiqvMsoTgK_MBU5HdOlyxqpOVsgeMXhJWfr5CTC4aXZvNuLocnR7nqb0tIIjnKQAjIDBDIl681l_2bfi9OuXbb0Zw4l2LSezQSSe_oQrwqNFot0c8l4-Nt1MNKwwZOiChNsy93h0JNz6bziYcsXnUWQLg37IeZV0IScumMytk-IifLA25Sbvn6N0i7MxV4DmcYJf0ipUJhuD1fvN3twgbLQW0Wk6KpZu1lWfuhhqytLKCO6QSZesbxuz3yVbv05u5z05T2tuKQrnMjkcFnaz3tH8UXxNreKauSMqebGErPab43ewjXwGkak-yuihkwdLh4gYEnpbSoY0xyST6fS6Xr-wVUfhfpgZFfWWiLvmpqZaSw_0CzTEmmG1N3q2l5abFsTA53XM0rBJPrtaAFNuAd7PQp2W3XsLACeI7tnnxkgIuS0V6SL0FdkEiD0mW4qmvnOMnneCH77kUKizbp3pz1-coOYET-1eTkZGL0NmXEFdhlm6GSghnRwBtx27VVHTDBqFDA5aMBfXbNjud1BA-9-q7Bhy-MY_rlK7AvBiny_oEneZt_Giy4fbl02bMqr0F8y4bys8uTs0nU_XAXKR43VWK-TEMvmYbFowpg5oj-pvEPzgKVPz3GZIUcVSZBtWSPszuUp0ZuGq1zhk3_Jbxz9wq17uX5oG2uC4MBl-LhtnwCCAgk8Uvqxz7kyNz4p_Q22gOZ25BS8AQ13Bk_zJ5VIYQQD1atrrD4M91FkKRcoUivHFwpymWnVpXbxW6cnctlH6YUA1QpFk2HoYDPHf615Ex4q85PsKNLEJkmwDSwMRYI9hiE1lNap1ZN132f1JHk_vGEq9-F5tsbVJU7NZPj4qCTqW26FJrgZZ1wQTxy4aRea9c3q9owQLXdJ0zN-kBUBm57yj7mJcLnCUy2ANqnIgDEr9vd531UrKp9Qb6wi2KbzSlOLdaftzP60PRjFyXK-x6RvUGV9fhyoXZiDIN2jM0wXZwjFC6Go1SX9EbmnvqTn1Kof9wfHWzUss_jLeMHd0mvXfmTcdCnU-ulH15AJW-7cOzXNmR4Xhh8Y8Aq2DW6oevFMD2VnXMzmW7GWiUkNf2ehAUaWwDcReHXACZ7d5tfoUFscjSNKKZ6JmrdVGBvThdt3h4b2XVlCBuKCns4O0M0pjq1YI1H7zcMieBply-9jToD8W4HDSLQFJ2J6S2QiFO5CAqf0YPLWMCithA9Io6D-7XYTiMMJWpJQWvNE89eb9O7-mmtgcdHE9DFFuw-6XCl9Ww6PxU0OH3o4xQH-zQMwclsPspRwnacxQoO7P-F3dHCHMuePVBy9eyuLgmRR2MF4whl8vOUQwk6Q6PvDFopmJZOgkgH9H09ArsNKKZVGrk7KJJEtF4edzD_MjtHvJiYo3E09l6ErahE-mbl0ZNTtVByZGHkjXZcnzvot1fJnMhy-OMKKJATVqduaDGAWXsAP3KFrcBi2G7IDW3blMnXUo6vawcUPy0tuR4qosovSQr5iGvvHq0bJC9YQX9rvAGyyCPJpfUo0rGDMPmy8Eu8wrunTOk8Ks-EXeZhLNIGeMa2WJe5bSwpV-fbFfRfRrlXT13j6-EJwAjMpZsQxE1SyJqaXCmWfwh_sDb2sAy2n2YJurD92YluIXtj5nXUPLUWXpyM0Fkfh9L5tqq6cw9YRwgNHdvUJ65QYbkhE-zF5ig1IFAYZpXkDVxVehW4ErVcIp8cA3iHIiaJsVRcJDucNkcyxCgJVDr7HT3vXUUqy-Ln2jNDSkZx0njOtMT8R7ZVkKrDojovYxe4YogZjwUM0ntlHfxHU1CppkfaxYdWx_Bpd_cNDcG8NrFtHOjB6_uiFdbbp_9SbRcb9QYr_i_lX-92yMSviLcY51dz4VUToo0x6qd1el6zZeZIaVNugL8PU5xJQrr_YTOP6mXJZsRrSvh0UTLCnoF6OjEaVQ-F7GRYJvBZutN7vu1t7g8XtFF-szjPeoPwkEAnlbonlgHiDSZEim_czpkEryDp05gKssYRpACb3qcy5QIFw5Ll3JQmwGYA-8qLTPJ2jEV2KZscrSsm-yXt5bkEzPm79Ymm9NDD7JDrKlqngu-Gq33BZDJmq48iOjox4u4b4vyq-uCkUE4UtTHofCn5HTUb--CTi3VRY1U_369m471PEUh8PbHhwoj3D5dbwQ6piaD2lHLtHuMebs0_Vob-zmg4CGw6zLBmplKi9_XOaABtiN247Vd_leTjlgezO0DkCcnUP6DJnyjhCZYBBQtNO5uTlTyy-ij0Rx4Uo0IE-Nx-fSTZ9hlfMhlf6kUJbHj4qXLAQ5q4RigNwe7pho7O0FArXFAeMjjiDr-l8Ur36K002lQyZtijyhw633UGt7HNnH-ycPIMYcUpKcW-3QfLVwv2CuhS6Z8Tud_P0MOmfHGj9CLXJaWp6fZ66Be6h4B_4uf5f9sAL16teLeN8Rohwzhax8m40ZJZwifjQCiIC4Tg5RWmJG9rUP3cQudKMtAjlont4HhI9Wa5EjeZRZjwi7WNymRFTEEHTBCEtLgMjDhmw5LN2w2u20YK_IOEJXmHP0N5XSD3bBYqThhYjBFnTn5SfNbUioAXzyxjeKN-cyKm1FX-G0gOewYm3kfXYcADuSW11ZcYGvA_nQz_anjyRvCP4UNEdrco41l4E49Zfp3mz439iWO1moM5q2XiCEQUml-TxO9zXunhnQnGT4ThIkKN85kbkj0sZvurUM1W1ZPSE1TxxUJm33d1M5sxt8hGPlvxYH2hHqQ-BEnahK242aAu28lLym--mJGXAQaGYetp0fBh03P8bWbhP59dq5MsXGOUdHUw9lgOlwChg-MxYeegcRZR64zBON-l-7hR3fZQ1-ibdfjW9EyqHa-nf5KqtXGDjM0HrYBwuuS4vTcMA6rw39cnvzS2vwmPpzJsVkcrHZHY7f7SokESkDv_SjqdsVAFY6fThSw9uz-yBW9qvMBcnI5r74Edcs6BAg_VGeUPXFyYNjuTOKoSGpEjrAdYS7gNno30ntfFPVUO833xYwBJAbCMXJgiACD8OmzJf9yPuwWUN0kSs1m2OGvtYZcSwLZOEnlIYA96u1-R4UtdNjQ5s07xJ28CSIYjthxrM7GteX8aeSNgk67kmkGyXjlBNi_3Jg.EDW1m762Rac-rEKEoCEMqikz_kdzFe67hL4tVgnKNWM	\N	2026-05-01 07:23:48.819213	ORv1YKxFbpGmrthTSX/f8df5Rakh3H1B1MEeSmketcM=	redeemed	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:openiddict:params:oauth:token-type:authorization_code	{}	0ca5d8fa1a0c4a5cbc8d294c0ba86b4c
3a20f498-f2df-da08-e343-c48f36ae754e	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20eef8-aed4-978c-4cf7-7e3be2a6ea06	2026-05-01 07:23:48	2026-05-01 08:23:48	\N	\N	\N	\N	valid	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:ietf:params:oauth:token-type:access_token	{}	0d916cd979c041a9828382f512f3323a
3a20f498-f33d-fa7f-7092-4e8d1a8928b8	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20eef8-aed4-978c-4cf7-7e3be2a6ea06	2026-05-01 07:23:48	2026-05-01 07:43:48	\N	\N	\N	\N	valid	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:ietf:params:oauth:token-type:id_token	{}	66984b9960dc47a39b83a347ea5f268e
3a20f568-c493-4a93-b6cb-7e596a0dbb7d	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20eef8-aed4-978c-4cf7-7e3be2a6ea06	2026-05-01 11:10:48	2026-05-01 11:30:48	\N	\N	\N	\N	valid	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:ietf:params:oauth:token-type:id_token	{}	e2fa6f327c5940cd9d15e99a5e3c3e62
3a20f568-c396-f7a1-0be2-ba299eab930d	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20eef8-aed4-978c-4cf7-7e3be2a6ea06	2026-05-01 11:10:48	2026-05-01 11:15:48	eyJhbGciOiJSU0EtT0FFUCIsImVuYyI6IkEyNTZDQkMtSFM1MTIiLCJraWQiOiIwMzEwM0QyNEVERDExRjNBOTY1NzFCRUMxQTlBRDUzMUUwMkJDNkQ4IiwidHlwIjoib2lfYXVjK2p3dCIsImN0eSI6IkpXVCJ9.R4T5Id1OQPdb-lMl-sY6NTpGUAVxv45v16MAUMdPOFJcn3wms4pgBEt7mToeAo7EwoMN3zgXnC1l4dzUyOLxv3Hj1NDXx6nsWbh_TAuDjSxUY-7RkkZDw_0r7fgN4dMZB2iDf00oaLOTF6yZho0X3dnlJEigA9LW1d-eA2BJ8dMwAX32bpTFj16SXHW_ECxhffww8VVJu1VRYUVPeA5OSN1KqQRttRlSzPD3t8MueqdoJjI8yEUc50vcfWpVLi7LH0yMNz-JpT1I7loO1zmOHh0Jza8KwcWkpmxE0GyqehQ9tXiP-X-9BaPQ24LzFdqvy6Q2f6lSVTk5aPFhGXPNI4aFeIM97S5Py7tw0plNy9WdB_pgOZHRc8Qtujy0JFrgVyaAVCufibqY95c1CQ4joN5rWssgDqHD8HiLrWo_gYC6v6MdaBrsopgEYA5p5S7j3Gbm9nQhbGEzrZCcadN8BPeNWjtHi_Z20HEjuSIIRXhsnVJDUhZmGvvFGgH0_AQNLAsEye8-sILsiogNFyvH2zlzwhpJ_CdpGilLg78_ZTOFf_h-X6DCWNL_FMChfMqkPEzmKBQyaWoQTEaDjdykZwrwG6tI8hb7omZTD0OcFGwwal4R918NcE_C7XVzF99Zw7D0rru8ahOBmf6ennqZX_pv_Y_32TQocJ8goI9ssug.Om_0Vzvrd37fuTy00ZCOcQ.VU_0y6OFnwNm6Apryx5stzZIjTca5DJ1uS19L9BHMrnriYs057Jy7t2zZFrdQ2cb2GjWm3PBgQm0Yx1i9LAL53jcB_qcOk5onFCAwBhO3MBBveYW6eFkg50SSjbnxGKhVe6hS87dDxRMKdAPhzWIM48tKk55jE-oT2rk6WhMZ2OmF5wUQfsDlznzZ46q1iFlIPZ4wgyVKk8MzU_-VULi04UxNRcI8n_TUEfLeJ757_2I5cJbVMxUFUVNea1DmPDaL7d7YATExmnJawkYNK6XOsP3gs-qjloLhhPGztmQKvsqZDzzewdF3NmwlhOLcRGBcHpQe1bLsbdSd6omIMaPJaLSvROq8drAFF3cGW4g2vEy-YpfLGB5s4fUW7JiBJk7yLqOHpbDT_Rt5KjO1sTvJddCO7fzSmKXhEoAZ3eNMC5Gjw6NAS3T50b54d_Q2xujDKysd1CoqQ4XVqDVACEz-vKgpAnvK6Rptit_xECVYwZMQpTe-rW6Wo1hkT1w8dnKnfrBh4ZteVK6PzuMzZkCreAHlE4KR7_OP79oADvAxNHk-dxU9qRrG0oXvnfU-RikV8_Cz1vhq-rTSPRiBv07IGS04rvuBfl4egqfgS2ZlRSLh0FoFXB9_FX12H-igI3PF6DZvoae9u77yyF1kqOwTwFbS6Jcvfia8lGY8S8edUi0idaS7kpquWg7hxosfGYe5-pdE-L9u1OAhahFSrRZsjbAzlgVm7hzxCvGJDl1j_3lJk1_0iyLnJ3Gjij3ztz0EoMZK2-UExpeSHa5OOwigJHIl6UtsAmhzJ0bJri75YTn6QwdZ3WYFcqON26lD93uGK8Bc8ki18CNrTFZA67cDKpXqlyhhMoGqgHHU5Tyq1j3wyx2PkORhN3LnQlv2F5eZ8PDisY9Aky4TrBHRnjf3SYcvtBut7SC3lEQURWfrCyl2PPQ3xVQrYtaI1R5Sxo20bUaDilSlns9OXl5mJqvF4lkNZ8TpcE7jHj1E9waSNu76nu717ZOcwiTKmXltrWuQ16jSNk9VJsxyz1gDSXCWEaYjVC0I16NE73yHATyfCLCq8H6ZvpIg4uzcFqrFbkhoie7Fv8N-ZfbVqvq5iLfLPVpZlqQNWD3euSNwbfFC7Li3haxnNwpBFAu-_NAqL506Chi9QnC4UzXdsatGC7JG9FN7LQNleYjoY6QA-dEKmP5vpvPk4GdiIjtAm1vyL9ACS5BmvHil1cfU8ohvw18UTadASo_T7pjmFaRYyU-jmcsbnyNT2FBhfPJChMjA_XtCOGuJ7ruCN8GjbQGfl_UQrQux82-02BvfgyawcG0VSi7Vgj1h8Q9CXJ59-On1EG-aP3eNEVGncmul30KE7YAqjkQoVW3PYivzimg5AmdrXoaWU_JZ7jHg-1zdJxSObIhU4H1j8KoqkJaNdrMfkkrn_iZwqRyG7tkXDg-vlqUejq9TLcoVFqpuEXwKFdvjwmaCO0QvtB0Td_KoHBxWUssenPSSPWUn73PywxwCdO2ZgvUXGgaLHrpQRoCGSZBPR8qS15DAnx245buEesJcCosC2vODFDB2uw9vs32wUx7iJpfu4efa55QPx4gwQu0njyTIzSPWQpnfUAxXSbCgQN8vobg6JIlikwmc2H_8RRL9Uow8YrDnp-xZDDqjqo1cYmDbosqFbDcHtp4BIdT8FmmA8RpWZtJuV4wukVT_eYXVbXxwqhei7ak8OySM9QmDas7wyrrEe9obzYcrKDMJPeicxfCQf_9wZusgnlL0h3mITn75vN6ScOPWoJctZgRFopST1Uz_EMv1Vh-86lJqHyoFTYp4fPXiK5J-KG8txF9kkn9dj-5YUmVi-LTfKhBFq8nHjcbPKXWjNnldw3SFAuRBuveLq05k2pz5tTWrSp5ySiO3O0RWjIZvz5YSssx0QnYgKUSvDomUrfUEuhXJW6OojC-kNBE1jAA9QuX0z4OTzFS8uY7d-I1AACiCJuCU_7ptELfoYiui4noayduK3ZlzSbmrK78OGjuwIMEF5QDdsgqhlcz7CDQ_j74QBPMIyXZWmfUJ0pSEbt6Q5g4MC6cwkSW9hhJWmWYrSPDeXgoUth-DeH_ZxIqt-bYYPkrxANjM1ghfrH0O8F2wZ3BgCJc1ncyK-oxnaAvmVfKPP3cGrn0i9bA7iiWNmqihJqul8pD6scErfhNTWzKkHhsldgUuECoMe8YpaMHUpTn-OI4ETxehC-uT_sPjnpIG27PlMvHMuskfvvE99f4yCNJn016ZHCaowHOLf9WrnJ0fBAEO5rOez2pqSBxEYzDS-NWejasEtSMZYLxheo32NdTo7TBhuhc2az7TB5EdZuxxKSaGUP3MgqRItnlXIMuR4BLnGYPZRFnKuMl8RG6wXk6-ONZH2JI5YdoBg49CEp9kSK-FtMY5MC9ULAbKgWXU7E-SYj7udYFAKMuQ4QXCyjRl4XGevU6pnB1gAXVI7-x49UCOrV9RmezqVwgw4wu9ebn4HOPnca1Uoc9tdgkM0Ai2YqrSz37F6I_Ot_lspmWIunIOVhsxw674HqvANKaQPArXGscQCeNUG-NFYd69_FekGc2BvID2fdM0ybr92vbg1vi6VAfobgUc8ox6fCSU1IuKRpgtskfnaJJ1TqXYvKSWWvPz3-OKKUWj5zwjMwOTRzU4GnJyp3M5DJxe-zZF7la5p82rlAB0xD6i6IOP0kiJjg_qfzVKTG-m7Y1Kjyvxis6KWB6JWAGjJCzA_zj3ZT4J1NnM0zNbjRKnAzqPKsSa_WAvnvNCW4JkdOkHnvTRbW5pS9zOAhk725xZP-5Lo2PAIL1n0uNK3kAKz1NS4M4K-zASq6MbNt8QInJdEa5JK6OMgkhAb4bf-bzaZmwagB3v2_zhQ1mCW0fkyILi8tkwYlF2QEZ_OAzYZo2vfF9-0vTUviP2XOmVAsYRC9iK-SP5SDn-5GAKgJkClWyV3XVXpzXCFiCHrwLE8N34tHqUE6z_wLsUCxn6mcxK-KB7Gms2flSv0Np7IjwO-KbzZ8V83ymNCDuK4UsUP0LeVQADD8Jyv1ySnHkkImaUKX6JQPmI4QBBQObHubLMN1SYoy-E4xEx8u8rUow2-Yt90ypRLT9Mtq17UFQJunyG8G376-HjqMIGJrILXiQbqvE4fJlyB2Er_4LnH8m-9IWqMQ1pmKM3ToVaC9VA1fkUgJHdNQ8ehsMQ24srv-LHy2cTc0yiVDa_r8aAiAg7WTvMqjXT1N1Aue5SDmtvH39Y8zCIs3S0B7tcN_ok_WdH9r_xwRxriGzi6jWSMr0tAuXNXUkXbjTLdRF5rAM3RhcAB6rbWiGxy9ZvrXIxed4y0b2ubyJJMlqsQTHBLXge6tGeZNTfQ84lquQPOruMi0EI9e1ojDpG8OzCysWRUE6RS10B3DQAn00dbrvpLM0mPTQPhcSz8BFhjiWqvWfskUYrs66t-F3nUzDhq671ogvXFbX6A-aeTo8VA2v3G7BRmohamL0uiaQGr6Q-3FLNW4I8PuJC9fDyXxZjjlCsYDasozShYikg2OygCDiChfAGPkssqDE6vaqolH8Te4iHcS1M-Pp2WQiRjshJsB9MOMge5HfdbXL__YRC2eSf6nM9_UJQNp-93-LH8-EHwsqjOeumIDW1oRy0xdAhtpnEZdXSAgX4MnxO55V19xmXn8P7y0YG5V23Ylis0fChNN1zY7fWlb3CILXMslo-fgX7T8jPiaq2KkwjWM-Q864xwizlAlCn5XVZy6xK3inYsymrcQHcXgN_0PVkEtDLoWcd8qEJrzYBBVZIrr6TliwSuZXaiXHNd9xa1K16BGKPPIgd2rZqd_rPUZmpm_zpmrVFjqgbk367zUyIP2w4ueqA4qQyvwwJB218WUGjHAdXYtQt8GIcd_bV2jdcT6XJLCoJ1QmwJLTTbYwowmKvEjANx-KVCkVJhAqL4LQaqZNiAoYmnf6qy6oJKc8clyWhPrlTk154YAUzztmlRSRmw.T40eznXlT2k2NaLLCMJgPMPHu_Prxk2HyJ-z2-CTbU4	\N	2026-05-01 11:10:48.903186	K3XkSG+Za+7YmpvXimD1tj+MiNfNXJDl+MxDH3kVTDo=	redeemed	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:openiddict:params:oauth:token-type:authorization_code	{}	15b2f4121c2940878bc3a625c4a4b3f0
3a20f568-c672-5091-8b8c-3f4017d1dbb4	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20eef8-aed4-978c-4cf7-7e3be2a6ea06	2026-05-01 11:10:48	2026-05-01 12:10:48	\N	\N	\N	\N	valid	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:ietf:params:oauth:token-type:access_token	{}	d5db76ccf425428aa7cfb3a53c7aba6d
3a20f568-c6dc-97d4-3061-3d690b040952	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20eef8-aed4-978c-4cf7-7e3be2a6ea06	2026-05-01 11:10:48	2026-05-01 11:30:48	\N	\N	\N	\N	valid	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:ietf:params:oauth:token-type:id_token	{}	5b4f777ee92744449edf934716513739
3a20f5a8-1c06-cacc-32af-1f8533f57ad6	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20eef8-aed4-978c-4cf7-7e3be2a6ea06	2026-05-01 12:19:59	2026-05-01 12:39:59	\N	\N	\N	\N	valid	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:ietf:params:oauth:token-type:id_token	{}	4538bc9a67f34927b91809a1eee6fe0f
3a20f5a8-1b82-3e68-7b00-4f1973376e22	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20eef8-aed4-978c-4cf7-7e3be2a6ea06	2026-05-01 12:19:59	2026-05-01 12:24:59	eyJhbGciOiJSU0EtT0FFUCIsImVuYyI6IkEyNTZDQkMtSFM1MTIiLCJraWQiOiIwMzEwM0QyNEVERDExRjNBOTY1NzFCRUMxQTlBRDUzMUUwMkJDNkQ4IiwidHlwIjoib2lfYXVjK2p3dCIsImN0eSI6IkpXVCJ9.NGHAQ2jMZpmR2LE5iR11nejeYcktoN-QAIrYEABRgRfuML-E8k1TfAiK_2T6OMaIzBD_uXK_nEyMC9wL7vmUCx8pGrLyspbFhD8kXw_vjfPJi7cnpWRrATa1n3L-lByWyB-up5fHsTj91-dZTr0W126HHJs4tCP5ESJNiyfoLF5S3fqGhm6pB5tzBM-iWV5RVMVyqFrXO4AH1dyHtwG3jIxuKhRwEsE2IKe0m9-C4pwCw2RXOUhsKuqADEmDGDtuczvQAQGM3MmaGnhLYO-VdyVKuCnrYCz3HWJbM3Y9iE7Htas7GlvFzhxhiTXFOV79qSNp8deeVv72hbumCDD8IAdynk2jb7lgPsBbyQ0zILN1qu7IvI_mIu-npxAQX3eK37moTISU0Wf8Ma8EIxcTqqohrNhfBtWkGVaxLAnQkL_nGfV3VvB6WndfmSm29kOlnI9caR5yxj_y1SqFCnuqFVxG_JUbnF7pTcDrhRWlTPTuwn5KJF6EVp5hJJCjGQxFkDluwDF_NFUuZkecQhnBVwSIUs8bnma_ggsgsBpFVrQddYvTFSq_0rgM7iHayu5UP2qFTaNmS4j3CHLb9hXgrGXBBCUhobFfPDfYAQZ2f1HmUcUbtnHmb-lWdb9jT9OZli0eQu3DyiEk8GpUCCthLRLPQjlHW2DmfMxwU4rCiiU.eTcHB-CIyOvPd2nIjRYoNA.F4zK7oDKtw7abLKe_PXFxnqZGNdTJUAktkfqpNoopyVxgrNgM_r6X3cbCv9efYk0-LvkElWHq7av8WvNMnrqXPQPR69jkeRGr5zJANar1Z-4fBEJM3r0A5eDL8GB1rfrxgHtCemF4n26_CLWjRTsPpD4x636p35cm55gvkJi7PZU1X1FNVp6rtRVH3-QEdZOO3J3hK1t7U5bps53gJOGBG8lpIMu2LIGtZHLWUGUIs2DJhmBHuL5doBhJiU85QIcdLbnINmpMGSNLbStrZ09uWfxHIOAG-mXtNnMpNnSqxct1V2JV3ireh3wWJBPxYZY20jdaWqFkUrwnCbCSPvEUbelwDnUJWFhWMv7uamxdEVsZ27QhXXFAEFGF-vC_zp7JTqTIJwVfXJlazLmuHdGdf1E1LlcBAHFuPTmHQxNgW5Z8kqHlQZyqhHBIogJ2iACtypA909Lg2KQLo2qnTZN-yNk_X1yZFXLMaf61tHKyEjoeFt7qz1L4mntL2IiH_11T8V4qYRKdh3KnwazTT4lsGUQGQNHCvjcBEfMOn6Iq4B_OJHYFhB3UFtavBFBUyCR40imOQPxPmVIhAitBNdWgpG-bQgml4AR4M-Z_9r-wntwrZUb40EyUOEKYwV8kRzAU-rmnXQD24l8hsAF_ECEhvZRsxVXZS2utOw_A_y54uq9gzoOckpqhZ2N88MSlwoIkKhNjmfMWdol7jHklxf9nUJUQ6BmvvOkbiC69dRiLm8ubs4fgUapQ8BV0DLo3BtneZnW8wAwaqT436Gu04IfziibM8H-3S_sLlpWYy6BsPX02tgGvmorW0e5XhXRyS_ZSOZ2NDruuI4yFdhbfQtgwyVBDgOSvG-0-TTcubAbVff4hOLyfB6WbqMt2zlj6WSh_kx3vaOIVynMEHFLsKPN5TtSo8N_zAsmeTtZieUwp25e8_Ho2DoFLGjYIhzf4jnUx1QPgQVR4vGPOxL6mqrOKAWYlAfddk9WWXjaytWiTDV88yj4lG3gvAC91JPcWg8OlV8N-P4yl3x1a8-sXE7VsmaOxqCO_ljLsgjhNGMDI63Vm4_UIAKEw2A56_YlTr3sms5y2aKmDLRgYFPaVZSnHlc-tv01g3r9TgNryRsQY325sInv1nEuALqeZf01D2ONCV3mtF6NmB9GQBjXipZcAymmDDripsMY-gt2-yR00duEc_f1_IGneZrOxsJvSr24oketapP2aCI4fmDBv70-V4lHLsJYBhWBFO2ki1MqZVKIEyz7JYhoeVreMBbSnOQxDDnTsKOsdKTnNwzkpiuEwkJ5v69uRaBsLhW9e3Msw_jW6As7Ok5xGLeFFcym2JzRZugqdRyrGlz_wazclMTMjDojH1bkTQXPeBNgO4xW82ovoi9yYoswbcycX0gbYFfucwd_2rb_UOxVlzlld3LW8kXs4bFf7a5T3XrkRWWYkX6FV8LxYNYJS5qvdEFZ1T8_HtqmxNfeTTwTJ0DlECynPRusyGIkTWPwGI-ZY8y4rRoEItUj9BoF990TaOx2kAxC_064SN-f0_Om_P3Ynn4l9SBGkGNRfDR7g3DfNgs67rgD5OIAsjnJkvfbgbMht2Ae60IzanrNyTI0Ss09KXkihEGlPSO3FbbVMHLQt4UTInQEb9-wMIwcZTMbWtdejvBVulPifOFCaY9Q8vk1IJuphHSc0oJKM__3y0NW0NQ8LrxXBAZivZIq1Lh9uwG1ky_Bnl6yX7_l5-4EFseRNMnx42o2RuFj9aymRDLXSuwgIMtZUfEunZIVkSTifh59oDVZqyKhh9q2ok4zIYxQSRhmqqCBBQgrGSPD7qK31cDSh9vMD0nwkME56lN8Mif5eTH21o_Yqn7IxEP4jA_Mrm77fGF85DJg-54GLV3s8J3GMjZQsYdjbRcRhrKmzAkjcc40Qjahi2Fhes_2xvnaBvfyb5lnxyqy0hQr9l_NrkmUOaVyOHDC1DZRuh6wHULj-8h_WNR1a_sgy2KstErLeT_Pr_XXqpSJXbLxFrvY0ohU7wR014jlUw28l55pbPPsN9rgUkC5i6uBL-O1bFgipJe-OdP0oYUykJKqFtUeWDwr8N0D7KSSK0K9BVmDH_puf9wyXgiB66MKFPGBXgCIA1wCIZeduhOeNJJwZEDBvTIb3kfksY0I_ubIjQU9AdP9YQg0CBZvdxT5RP-0O18HR9ccY88Lvcop7IZSz_2fqsGP5PlTko2L_zH2sMcIu8_RcZbhTCggmF4k4tdf5EPk18sllv3iUsqbCxfxiQb-8US1iz5eM5dmWv0jzggSB0rqN1AahYs0t8S9jNxEfzzamrroCY4vRT55GR6Aq81GyOGHOBbRkKoN6jFpDbYdJq8Z95-GCEZLwFx4ZcSrXJRHFz3IIUfbIEwbMru5WYCiFdKCprA699L2oPDAow6GpOC-8WSSPDj7t4cbNGYjlP3GozMSqTFlac_uV-k2O0YHzQbahvWQmAS3oICMMWGZUtTFefRrb3ESDN8BqlgQCf8DHB3zEH9J2hEsjhLPDTJgLkgft_nnpHpV0a9n1Ck-fitCxIw9OkrY0Tmm8l9t8oLF2QuIaG7wFlXTPd4gDqwAk0oxjEa5i3a_hg2orXhXupYIxagcLT1otmlIiDZUIbANR6k3vIJAMO93NFivyLxUXdG_OJy5W_Aoxvr7jNVFjQoIWMg-ngxlSS9PVtWtrzW6uuXDEfD1Nlufh_0bBzojPCy5yY7dOSTZtYm3NfGlvuIB3NL_EiZ10tPgyr_62V7Fh9HEZmqXVchZKnMybX2nIIuNF8gNti1DyfcYaS2XR703prL_QNt-ka_H95Pu3ilZZ5ZwK9qM-1tskvmB3WFyy0_utHamBZ0txUeMRxfE1iIRZWng73pd0LQ0zy3-uhob2ZzQsgpe06M3zRFpMVg2b0gqIi3jmyqzg0Y8D9UNWNYPI0i0JM_FBDCp2IVI4DLiP58kblJGYUqmcGVNQX6qh8t6qDvQjxJzsmtjgoTf230udLgPmRR22xMY0SCvfJUCIlgSBdvSXVF6dbcOjp4bJQkj-i2VRkA33Ka7PBBXftg09Ss0UJPtMn-fDtba5g0H3kXjQz-esnfozlNZnfEJXxb5UsiZxlsShDhK0S9H6KgmWW68kL6K31ocqhLo7llNFtUmsTm-8KuEEWnXhnZWQaNGfGGmWv0sDA-gOV5JXgsVXR48Afpsg8HvDUpYJnDaTrIabcg7nIUbjNbxyzNkA17kaskMxjsZ364TSxs3lG9EckM1i7f4YX9PHy_GgLXznOsH3NV2wSibjjacXdfh2MWRYLJzRNH1gTnUvUI6cakHI8xNUVOz1dbIVhsxpj2OAwIhAKsO0DqlBf_YTScg_f8jbYFpjT68Ea2iag4TDL13viHxC9xAqYaZd_ajZoRV-WER65-MbL8ozn5ZZZC3FJcpwYL-WjdfI_WTm0jer48SlRKsJhpKXeWiYmcVo0VIZnmD4d_2WgJWOakYYo14iQ1SJ1E8cVKrn3_h7ewBA2JlaMoJTW_FHfjIxCd77ZTcBbye0CdACYE-_4iAMOqRWVpwkfiiHV0isnZMnWpAcortlH3r7MjPxwOygzuF2B9kU7Q3h7bIHFJ13NGHitLbjekwrzcQvIVVMkcM7fBY4tYfQIf1iD--PABBPrHu7UZjNr0k1ylMChH44lqYf42nlPees1RFM__DqRvcwhO7tmtvSt0H7YlLwxhgtYa-0pW5HrXwkYK0tOe4u6DHfCfdsSVYTsyZnprqa1OxTBjwtMnalnnK7ZUSHG82cdPP2c3CH2fCjsF6j1CkifZ7LjXJ2tJhYTbHw8hanugXvfSlnG72u6d1vvANLT5okHY2Hc_62Y1Tz8v_8ssy52m0D793CER2JBe__gpfQSy9gT8rQBx2s_Wm5aNpywG0BnQMI9CWYz3lESo1U_WaHVRYlPFQ_vz0i5wgzGlFeHYqbqZlCndc6n1xHuR-jlrz5u-NCxyLD8OfoA.3iOab3n8jVJEpfdWUyLrH1NwggFxb6qYLJIaIhIHI2s	\N	2026-05-01 12:19:59.921163	NyiwRkCBzEgyHqxMDl6cDtE8SBn3lTmrPX6AGhX9x5M=	redeemed	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:openiddict:params:oauth:token-type:authorization_code	{}	ba5c05bc7da24ea5af16e0171b872ea7
3a20f5a8-1d41-c5dc-fc3a-9ba037f66fba	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20eef8-aed4-978c-4cf7-7e3be2a6ea06	2026-05-01 12:19:59	2026-05-01 13:19:59	\N	\N	\N	\N	valid	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:ietf:params:oauth:token-type:access_token	{}	eb668321f02b402ebe3a320556761b8f
3a20f5a8-1da9-3b82-4c8a-22602c4b1c68	3a20e9c3-64d3-3a2f-8837-2f81a4b56274	3a20eef8-aed4-978c-4cf7-7e3be2a6ea06	2026-05-01 12:19:59	2026-05-01 12:39:59	\N	\N	\N	\N	valid	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	urn:ietf:params:oauth:token-type:id_token	{}	29430d2ae5624982aa6de8674b9fb05c
\.


--
-- Data for Name: __IdentityService_Migrations; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."__IdentityService_Migrations" ("MigrationId", "ProductVersion") FROM stdin;
20260429043421_Initial	10.0.2
\.


--
-- Name: AbpClaimTypes PK_AbpClaimTypes; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpClaimTypes"
    ADD CONSTRAINT "PK_AbpClaimTypes" PRIMARY KEY ("Id");


--
-- Name: AbpEventInbox PK_AbpEventInbox; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpEventInbox"
    ADD CONSTRAINT "PK_AbpEventInbox" PRIMARY KEY ("Id");


--
-- Name: AbpEventOutbox PK_AbpEventOutbox; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpEventOutbox"
    ADD CONSTRAINT "PK_AbpEventOutbox" PRIMARY KEY ("Id");


--
-- Name: AbpLinkUsers PK_AbpLinkUsers; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpLinkUsers"
    ADD CONSTRAINT "PK_AbpLinkUsers" PRIMARY KEY ("Id");


--
-- Name: AbpOrganizationUnitRoles PK_AbpOrganizationUnitRoles; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpOrganizationUnitRoles"
    ADD CONSTRAINT "PK_AbpOrganizationUnitRoles" PRIMARY KEY ("OrganizationUnitId", "RoleId");


--
-- Name: AbpOrganizationUnits PK_AbpOrganizationUnits; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpOrganizationUnits"
    ADD CONSTRAINT "PK_AbpOrganizationUnits" PRIMARY KEY ("Id");


--
-- Name: AbpRoleClaims PK_AbpRoleClaims; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpRoleClaims"
    ADD CONSTRAINT "PK_AbpRoleClaims" PRIMARY KEY ("Id");


--
-- Name: AbpRoles PK_AbpRoles; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpRoles"
    ADD CONSTRAINT "PK_AbpRoles" PRIMARY KEY ("Id");


--
-- Name: AbpSecurityLogs PK_AbpSecurityLogs; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpSecurityLogs"
    ADD CONSTRAINT "PK_AbpSecurityLogs" PRIMARY KEY ("Id");


--
-- Name: AbpSessions PK_AbpSessions; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpSessions"
    ADD CONSTRAINT "PK_AbpSessions" PRIMARY KEY ("Id");


--
-- Name: AbpUserClaims PK_AbpUserClaims; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpUserClaims"
    ADD CONSTRAINT "PK_AbpUserClaims" PRIMARY KEY ("Id");


--
-- Name: AbpUserDelegations PK_AbpUserDelegations; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpUserDelegations"
    ADD CONSTRAINT "PK_AbpUserDelegations" PRIMARY KEY ("Id");


--
-- Name: AbpUserInvitations PK_AbpUserInvitations; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpUserInvitations"
    ADD CONSTRAINT "PK_AbpUserInvitations" PRIMARY KEY ("Id");


--
-- Name: AbpUserLogins PK_AbpUserLogins; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpUserLogins"
    ADD CONSTRAINT "PK_AbpUserLogins" PRIMARY KEY ("UserId", "LoginProvider");


--
-- Name: AbpUserOrganizationUnits PK_AbpUserOrganizationUnits; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpUserOrganizationUnits"
    ADD CONSTRAINT "PK_AbpUserOrganizationUnits" PRIMARY KEY ("OrganizationUnitId", "UserId");


--
-- Name: AbpUserPasskeys PK_AbpUserPasskeys; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpUserPasskeys"
    ADD CONSTRAINT "PK_AbpUserPasskeys" PRIMARY KEY ("CredentialId");


--
-- Name: AbpUserPasswordHistories PK_AbpUserPasswordHistories; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpUserPasswordHistories"
    ADD CONSTRAINT "PK_AbpUserPasswordHistories" PRIMARY KEY ("UserId", "Password");


--
-- Name: AbpUserRoles PK_AbpUserRoles; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpUserRoles"
    ADD CONSTRAINT "PK_AbpUserRoles" PRIMARY KEY ("UserId", "RoleId");


--
-- Name: AbpUserTokens PK_AbpUserTokens; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpUserTokens"
    ADD CONSTRAINT "PK_AbpUserTokens" PRIMARY KEY ("UserId", "LoginProvider", "Name");


--
-- Name: AbpUsers PK_AbpUsers; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpUsers"
    ADD CONSTRAINT "PK_AbpUsers" PRIMARY KEY ("Id");


--
-- Name: OpenIddictApplications PK_OpenIddictApplications; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."OpenIddictApplications"
    ADD CONSTRAINT "PK_OpenIddictApplications" PRIMARY KEY ("Id");


--
-- Name: OpenIddictAuthorizations PK_OpenIddictAuthorizations; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."OpenIddictAuthorizations"
    ADD CONSTRAINT "PK_OpenIddictAuthorizations" PRIMARY KEY ("Id");


--
-- Name: OpenIddictScopes PK_OpenIddictScopes; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."OpenIddictScopes"
    ADD CONSTRAINT "PK_OpenIddictScopes" PRIMARY KEY ("Id");


--
-- Name: OpenIddictTokens PK_OpenIddictTokens; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."OpenIddictTokens"
    ADD CONSTRAINT "PK_OpenIddictTokens" PRIMARY KEY ("Id");


--
-- Name: __IdentityService_Migrations PK___IdentityService_Migrations; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."__IdentityService_Migrations"
    ADD CONSTRAINT "PK___IdentityService_Migrations" PRIMARY KEY ("MigrationId");


--
-- Name: IX_AbpEventInbox_MessageId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpEventInbox_MessageId" ON public."AbpEventInbox" USING btree ("MessageId");


--
-- Name: IX_AbpEventInbox_Status_CreationTime; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpEventInbox_Status_CreationTime" ON public."AbpEventInbox" USING btree ("Status", "CreationTime");


--
-- Name: IX_AbpEventOutbox_CreationTime; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpEventOutbox_CreationTime" ON public."AbpEventOutbox" USING btree ("CreationTime");


--
-- Name: IX_AbpLinkUsers_SourceUserId_SourceTenantId_TargetUserId_Targe~; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "IX_AbpLinkUsers_SourceUserId_SourceTenantId_TargetUserId_Targe~" ON public."AbpLinkUsers" USING btree ("SourceUserId", "SourceTenantId", "TargetUserId", "TargetTenantId");


--
-- Name: IX_AbpOrganizationUnitRoles_RoleId_OrganizationUnitId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpOrganizationUnitRoles_RoleId_OrganizationUnitId" ON public."AbpOrganizationUnitRoles" USING btree ("RoleId", "OrganizationUnitId");


--
-- Name: IX_AbpOrganizationUnits_Code; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpOrganizationUnits_Code" ON public."AbpOrganizationUnits" USING btree ("Code");


--
-- Name: IX_AbpOrganizationUnits_ParentId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpOrganizationUnits_ParentId" ON public."AbpOrganizationUnits" USING btree ("ParentId");


--
-- Name: IX_AbpRoleClaims_RoleId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpRoleClaims_RoleId" ON public."AbpRoleClaims" USING btree ("RoleId");


--
-- Name: IX_AbpRoles_NormalizedName; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpRoles_NormalizedName" ON public."AbpRoles" USING btree ("NormalizedName");


--
-- Name: IX_AbpSecurityLogs_TenantId_Action; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpSecurityLogs_TenantId_Action" ON public."AbpSecurityLogs" USING btree ("TenantId", "Action");


--
-- Name: IX_AbpSecurityLogs_TenantId_ApplicationName; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpSecurityLogs_TenantId_ApplicationName" ON public."AbpSecurityLogs" USING btree ("TenantId", "ApplicationName");


--
-- Name: IX_AbpSecurityLogs_TenantId_Identity; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpSecurityLogs_TenantId_Identity" ON public."AbpSecurityLogs" USING btree ("TenantId", "Identity");


--
-- Name: IX_AbpSecurityLogs_TenantId_UserId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpSecurityLogs_TenantId_UserId" ON public."AbpSecurityLogs" USING btree ("TenantId", "UserId");


--
-- Name: IX_AbpSessions_Device; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpSessions_Device" ON public."AbpSessions" USING btree ("Device");


--
-- Name: IX_AbpSessions_SessionId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpSessions_SessionId" ON public."AbpSessions" USING btree ("SessionId");


--
-- Name: IX_AbpSessions_TenantId_UserId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpSessions_TenantId_UserId" ON public."AbpSessions" USING btree ("TenantId", "UserId");


--
-- Name: IX_AbpUserClaims_UserId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpUserClaims_UserId" ON public."AbpUserClaims" USING btree ("UserId");


--
-- Name: IX_AbpUserLogins_LoginProvider_ProviderKey; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpUserLogins_LoginProvider_ProviderKey" ON public."AbpUserLogins" USING btree ("LoginProvider", "ProviderKey");


--
-- Name: IX_AbpUserOrganizationUnits_UserId_OrganizationUnitId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpUserOrganizationUnits_UserId_OrganizationUnitId" ON public."AbpUserOrganizationUnits" USING btree ("UserId", "OrganizationUnitId");


--
-- Name: IX_AbpUserPasskeys_UserId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpUserPasskeys_UserId" ON public."AbpUserPasskeys" USING btree ("UserId");


--
-- Name: IX_AbpUserRoles_RoleId_UserId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpUserRoles_RoleId_UserId" ON public."AbpUserRoles" USING btree ("RoleId", "UserId");


--
-- Name: IX_AbpUsers_Email; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpUsers_Email" ON public."AbpUsers" USING btree ("Email");


--
-- Name: IX_AbpUsers_NormalizedEmail; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpUsers_NormalizedEmail" ON public."AbpUsers" USING btree ("NormalizedEmail");


--
-- Name: IX_AbpUsers_NormalizedUserName; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpUsers_NormalizedUserName" ON public."AbpUsers" USING btree ("NormalizedUserName");


--
-- Name: IX_AbpUsers_UserName; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpUsers_UserName" ON public."AbpUsers" USING btree ("UserName");


--
-- Name: IX_OpenIddictApplications_ClientId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_OpenIddictApplications_ClientId" ON public."OpenIddictApplications" USING btree ("ClientId");


--
-- Name: IX_OpenIddictAuthorizations_ApplicationId_Status_Subject_Type; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_OpenIddictAuthorizations_ApplicationId_Status_Subject_Type" ON public."OpenIddictAuthorizations" USING btree ("ApplicationId", "Status", "Subject", "Type");


--
-- Name: IX_OpenIddictScopes_Name; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_OpenIddictScopes_Name" ON public."OpenIddictScopes" USING btree ("Name");


--
-- Name: IX_OpenIddictTokens_ApplicationId_Status_Subject_Type; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_OpenIddictTokens_ApplicationId_Status_Subject_Type" ON public."OpenIddictTokens" USING btree ("ApplicationId", "Status", "Subject", "Type");


--
-- Name: IX_OpenIddictTokens_AuthorizationId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_OpenIddictTokens_AuthorizationId" ON public."OpenIddictTokens" USING btree ("AuthorizationId");


--
-- Name: IX_OpenIddictTokens_ReferenceId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_OpenIddictTokens_ReferenceId" ON public."OpenIddictTokens" USING btree ("ReferenceId");


--
-- Name: AbpOrganizationUnitRoles FK_AbpOrganizationUnitRoles_AbpOrganizationUnits_OrganizationU~; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpOrganizationUnitRoles"
    ADD CONSTRAINT "FK_AbpOrganizationUnitRoles_AbpOrganizationUnits_OrganizationU~" FOREIGN KEY ("OrganizationUnitId") REFERENCES public."AbpOrganizationUnits"("Id") ON DELETE CASCADE;


--
-- Name: AbpOrganizationUnitRoles FK_AbpOrganizationUnitRoles_AbpRoles_RoleId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpOrganizationUnitRoles"
    ADD CONSTRAINT "FK_AbpOrganizationUnitRoles_AbpRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES public."AbpRoles"("Id") ON DELETE CASCADE;


--
-- Name: AbpOrganizationUnits FK_AbpOrganizationUnits_AbpOrganizationUnits_ParentId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpOrganizationUnits"
    ADD CONSTRAINT "FK_AbpOrganizationUnits_AbpOrganizationUnits_ParentId" FOREIGN KEY ("ParentId") REFERENCES public."AbpOrganizationUnits"("Id");


--
-- Name: AbpRoleClaims FK_AbpRoleClaims_AbpRoles_RoleId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpRoleClaims"
    ADD CONSTRAINT "FK_AbpRoleClaims_AbpRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES public."AbpRoles"("Id") ON DELETE CASCADE;


--
-- Name: AbpUserClaims FK_AbpUserClaims_AbpUsers_UserId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpUserClaims"
    ADD CONSTRAINT "FK_AbpUserClaims_AbpUsers_UserId" FOREIGN KEY ("UserId") REFERENCES public."AbpUsers"("Id") ON DELETE CASCADE;


--
-- Name: AbpUserLogins FK_AbpUserLogins_AbpUsers_UserId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpUserLogins"
    ADD CONSTRAINT "FK_AbpUserLogins_AbpUsers_UserId" FOREIGN KEY ("UserId") REFERENCES public."AbpUsers"("Id") ON DELETE CASCADE;


--
-- Name: AbpUserOrganizationUnits FK_AbpUserOrganizationUnits_AbpOrganizationUnits_OrganizationU~; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpUserOrganizationUnits"
    ADD CONSTRAINT "FK_AbpUserOrganizationUnits_AbpOrganizationUnits_OrganizationU~" FOREIGN KEY ("OrganizationUnitId") REFERENCES public."AbpOrganizationUnits"("Id") ON DELETE CASCADE;


--
-- Name: AbpUserOrganizationUnits FK_AbpUserOrganizationUnits_AbpUsers_UserId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpUserOrganizationUnits"
    ADD CONSTRAINT "FK_AbpUserOrganizationUnits_AbpUsers_UserId" FOREIGN KEY ("UserId") REFERENCES public."AbpUsers"("Id") ON DELETE CASCADE;


--
-- Name: AbpUserPasskeys FK_AbpUserPasskeys_AbpUsers_UserId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpUserPasskeys"
    ADD CONSTRAINT "FK_AbpUserPasskeys_AbpUsers_UserId" FOREIGN KEY ("UserId") REFERENCES public."AbpUsers"("Id") ON DELETE CASCADE;


--
-- Name: AbpUserPasswordHistories FK_AbpUserPasswordHistories_AbpUsers_UserId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpUserPasswordHistories"
    ADD CONSTRAINT "FK_AbpUserPasswordHistories_AbpUsers_UserId" FOREIGN KEY ("UserId") REFERENCES public."AbpUsers"("Id") ON DELETE CASCADE;


--
-- Name: AbpUserRoles FK_AbpUserRoles_AbpRoles_RoleId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpUserRoles"
    ADD CONSTRAINT "FK_AbpUserRoles_AbpRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES public."AbpRoles"("Id") ON DELETE CASCADE;


--
-- Name: AbpUserRoles FK_AbpUserRoles_AbpUsers_UserId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpUserRoles"
    ADD CONSTRAINT "FK_AbpUserRoles_AbpUsers_UserId" FOREIGN KEY ("UserId") REFERENCES public."AbpUsers"("Id") ON DELETE CASCADE;


--
-- Name: AbpUserTokens FK_AbpUserTokens_AbpUsers_UserId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpUserTokens"
    ADD CONSTRAINT "FK_AbpUserTokens_AbpUsers_UserId" FOREIGN KEY ("UserId") REFERENCES public."AbpUsers"("Id") ON DELETE CASCADE;


--
-- Name: OpenIddictAuthorizations FK_OpenIddictAuthorizations_OpenIddictApplications_Application~; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."OpenIddictAuthorizations"
    ADD CONSTRAINT "FK_OpenIddictAuthorizations_OpenIddictApplications_Application~" FOREIGN KEY ("ApplicationId") REFERENCES public."OpenIddictApplications"("Id");


--
-- Name: OpenIddictTokens FK_OpenIddictTokens_OpenIddictApplications_ApplicationId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."OpenIddictTokens"
    ADD CONSTRAINT "FK_OpenIddictTokens_OpenIddictApplications_ApplicationId" FOREIGN KEY ("ApplicationId") REFERENCES public."OpenIddictApplications"("Id");


--
-- Name: OpenIddictTokens FK_OpenIddictTokens_OpenIddictAuthorizations_AuthorizationId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."OpenIddictTokens"
    ADD CONSTRAINT "FK_OpenIddictTokens_OpenIddictAuthorizations_AuthorizationId" FOREIGN KEY ("AuthorizationId") REFERENCES public."OpenIddictAuthorizations"("Id");


--
-- PostgreSQL database dump complete
--

\unrestrict njwRDjtOcZBmWGNsu9aXuRDH3M72aiWKmOEGr97L4hQafb9OSgrpTD0LmOgLqIu

--
-- Database "KHHub_Language" dump
--

--
-- PostgreSQL database dump
--

\restrict b5bLrd3HAD8gkgYacR4lfFDNwzKJ0g5UXlGD02koe3Dg1s1kUqzZnMvoubutKvr

-- Dumped from database version 14.22
-- Dumped by pg_dump version 14.22

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: KHHub_Language; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE "KHHub_Language" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'en_US.utf8';


ALTER DATABASE "KHHub_Language" OWNER TO postgres;

\unrestrict b5bLrd3HAD8gkgYacR4lfFDNwzKJ0g5UXlGD02koe3Dg1s1kUqzZnMvoubutKvr
\connect "KHHub_Language"
\restrict b5bLrd3HAD8gkgYacR4lfFDNwzKJ0g5UXlGD02koe3Dg1s1kUqzZnMvoubutKvr

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: AbpEventInbox; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpEventInbox" (
    "Id" uuid NOT NULL,
    "ExtraProperties" text NOT NULL,
    "MessageId" text NOT NULL,
    "EventName" character varying(256) NOT NULL,
    "EventData" bytea NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL,
    "Status" integer NOT NULL,
    "HandledTime" timestamp without time zone,
    "RetryCount" integer NOT NULL,
    "NextRetryTime" timestamp without time zone
);


ALTER TABLE public."AbpEventInbox" OWNER TO postgres;

--
-- Name: AbpEventOutbox; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpEventOutbox" (
    "Id" uuid NOT NULL,
    "ExtraProperties" text NOT NULL,
    "EventName" character varying(256) NOT NULL,
    "EventData" bytea NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL
);


ALTER TABLE public."AbpEventOutbox" OWNER TO postgres;

--
-- Name: AbpLanguageTexts; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpLanguageTexts" (
    "Id" uuid NOT NULL,
    "TenantId" uuid,
    "ResourceName" character varying(128) NOT NULL,
    "CultureName" character varying(10) NOT NULL,
    "Name" character varying(512) NOT NULL,
    "Value" character varying(65536) NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL,
    "CreatorId" uuid,
    "LastModificationTime" timestamp without time zone,
    "LastModifierId" uuid
);


ALTER TABLE public."AbpLanguageTexts" OWNER TO postgres;

--
-- Name: AbpLanguages; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpLanguages" (
    "Id" uuid NOT NULL,
    "CultureName" character varying(10) NOT NULL,
    "UiCultureName" character varying(10) NOT NULL,
    "DisplayName" character varying(32) NOT NULL,
    "IsEnabled" boolean NOT NULL,
    "ExtraProperties" text NOT NULL,
    "ConcurrencyStamp" character varying(40) NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL,
    "CreatorId" uuid,
    "LastModificationTime" timestamp without time zone,
    "LastModifierId" uuid,
    "IsDeleted" boolean DEFAULT false NOT NULL,
    "DeleterId" uuid,
    "DeletionTime" timestamp without time zone
);


ALTER TABLE public."AbpLanguages" OWNER TO postgres;

--
-- Name: AbpLocalizationResources; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpLocalizationResources" (
    "Id" uuid NOT NULL,
    "Name" character varying(128) NOT NULL,
    "DefaultCulture" character varying(10),
    "BaseResources" character varying(1280),
    "SupportedCultures" character varying(640),
    "CreationTime" timestamp without time zone NOT NULL,
    "LastModificationTime" timestamp without time zone
);


ALTER TABLE public."AbpLocalizationResources" OWNER TO postgres;

--
-- Name: AbpLocalizationTexts; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpLocalizationTexts" (
    "Id" uuid NOT NULL,
    "ResourceName" character varying(128) NOT NULL,
    "CultureName" character varying(10) NOT NULL,
    "Value" character varying(1048576) NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL,
    "LastModificationTime" timestamp without time zone
);


ALTER TABLE public."AbpLocalizationTexts" OWNER TO postgres;

--
-- Name: __LanguageService_Migrations; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."__LanguageService_Migrations" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__LanguageService_Migrations" OWNER TO postgres;

--
-- Data for Name: AbpEventInbox; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpEventInbox" ("Id", "ExtraProperties", "MessageId", "EventName", "EventData", "CreationTime", "Status", "HandledTime", "RetryCount", "NextRetryTime") FROM stdin;
\.


--
-- Data for Name: AbpEventOutbox; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpEventOutbox" ("Id", "ExtraProperties", "EventName", "EventData", "CreationTime") FROM stdin;
\.


--
-- Data for Name: AbpLanguageTexts; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpLanguageTexts" ("Id", "TenantId", "ResourceName", "CultureName", "Name", "Value", "CreationTime", "CreatorId", "LastModificationTime", "LastModifierId") FROM stdin;
\.


--
-- Data for Name: AbpLanguages; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpLanguages" ("Id", "CultureName", "UiCultureName", "DisplayName", "IsEnabled", "ExtraProperties", "ConcurrencyStamp", "CreationTime", "CreatorId", "LastModificationTime", "LastModifierId", "IsDeleted", "DeleterId", "DeletionTime") FROM stdin;
3a20e9c3-50c2-68ce-cdb7-e1c8086aa762	vi	vi	VN	t	{}	4597c9ebba2747f6b656366f645a7d95	2026-04-29 11:54:16.075635	\N	\N	\N	f	\N	\N
3a20e9c3-5114-dd30-cd52-02be066b172a	zh-Hans	zh-Hans	Chinese (Simplified)	t	{}	f5d54083bc334a68b35f89608683572a	2026-04-29 11:54:16.085411	\N	\N	\N	f	\N	\N
3a20e9c3-5116-2824-2f31-7676b507fe77	en	en	English	t	{}	f88188a1e3b6432baf3bdfbd728989d0	2026-04-29 11:54:16.086937	\N	\N	\N	f	\N	\N
3a20e9c3-5117-e5fb-b31c-e0ad1d461cc1	ru	ru	Russian	t	{}	060a6aa13e614263ae88d2e83726469a	2026-04-29 11:54:16.08773	\N	\N	\N	f	\N	\N
3a20e9c3-5118-c6b9-5a57-3a86c8bd8e3d	ko	ko	Korean	t	{}	2006c309fc704c3d9cf908461a7f8e13	2026-04-29 11:54:16.088544	\N	\N	\N	f	\N	\N
\.


--
-- Data for Name: AbpLocalizationResources; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpLocalizationResources" ("Id", "Name", "DefaultCulture", "BaseResources", "SupportedCultures", "CreationTime", "LastModificationTime") FROM stdin;
3a20e9c3-528c-5e21-90b6-ea57d0f0db3b	Default	en	\N	\N	2026-04-29 11:54:16.473287	\N
3a20e9c3-528c-afaf-d9f8-b96ef69f2c6f	AbpTiming	en	\N	ar,cs,de-DE,de,el,en-GB,en,es,fa,fi,fr,hi,hr,hu,is,it,nl,pl-PL,pt-BR,ro-RO,ru,sk,sl,sv,tr,vi,zh-Hans,zh-Hant	2026-04-29 11:54:16.473846	\N
3a20e9c3-528c-de96-e45e-f6e12a0c1a09	AbpLocalization	en	\N	ar,cs,de,el,en-GB,en,es,fa,fi,fr,hi,hr,hu,is,it,nl,pl-PL,pt-BR,ro-RO,ru,sk,sl,sv,tr,vi,zh-Hans,zh-Hant	2026-04-29 11:54:16.473643	\N
3a20e9c3-528d-1184-4abc-2a8b5ae548a4	GdprService	en	AbpValidation,AbpUi	en,ko,ru,vi,zh-Hans	2026-04-29 11:54:16.474091	\N
3a20e9c3-528d-172d-9323-cea757129c91	AbpUiNavigation	en	\N	ar,cs,de-DE,de,el,en-GB,en,es,fa,fi,fr,hi,hr,hu,is,it,nl,pl-PL,pt-BR,ro-RO,ru,sk,sl,sv,tr,vi,zh-Hans,zh-Hant	2026-04-29 11:54:16.474084	\N
3a20e9c3-528d-5fbd-a3d4-08cc3627ded7	AbpGdpr	en	AbpUi	ar,cs,de-DE,en,es,fi,fr,hi,hr,hu,it,pt-BR,ru,sk,sl,sv,tr,zh-Hans	2026-04-29 11:54:16.473988	\N
3a20e9c3-528d-6f68-c5c6-f723ee401e03	AbpFeature	en	\N	ar,cs,de-DE,de,el,en-GB,en,es,fa,fi,fr,hi,hr,hu,is,it,nl,pl-PL,pt-BR,ro-RO,ru,sk,sl,sv,tr,vi,zh-Hans,zh-Hant	2026-04-29 11:54:16.474008	\N
3a20e9c3-528d-97ba-350f-4c21ea83d169	AbpValidation	en	\N	ar,cs,de,el,en-GB,en,es,fa,fi,fr,hi,hr,hu,is,it,nl,pl-PL,pt-BR,ro-RO,ru,sk,sl,sv,tr,vi,zh-Hans,zh-Hant	2026-04-29 11:54:16.473963	\N
3a20e9c3-528d-a6b3-afc2-813ba9891259	AbpUi	en	AbpExceptionHandling	ar,cs,de,el,en-GB,en,es,fa,fi,fr,hi,hr,hu,is,it,nl,pl-PL,pt-BR,ro-RO,ru,sk,sl,sv,tr,vi,zh-Hans,zh-Hant	2026-04-29 11:54:16.474077	\N
3a20e9c3-528d-a846-f376-d734f5ad8fd7	AbpAuthorization	en	\N	ar,cs,de-DE,de,el,en-GB,en,es,fa,fi,fr,hi,hr,hu,is,it,nl,pl-PL,pt-BR,ro-RO,ru,sk,sl,sv,tr,vi,zh-Hans,zh-Hant	2026-04-29 11:54:16.474033	\N
3a20e9c3-528d-b66d-259e-2b713ea494c3	BlobStoringDatabase	en	AbpValidation	ar,cs,de-DE,de,el,en-GB,en,es,fa,fi,fr,hi,hr,hu,is,it,nl,pl-PL,pt-BR,ro-RO,ru,sk,sl,sv,tr,vi,zh-Hans,zh-Hant	2026-04-29 11:54:16.474	\N
3a20e9c3-528d-ca00-2546-a43fb8e32c02	AbpDddApplicationContracts	en	\N	ar,cs,de,el,en-GB,en,es,fa,fi,fr,hi,hr,hu,is,it,nl,pl-PL,pt-BR,ro-RO,ru,sk,sl,sv,tr,vi,zh-Hans,zh-Hant	2026-04-29 11:54:16.474058	\N
3a20e9c3-528d-d948-0251-0ea982d2bda1	AbpExceptionHandling	en	\N	ar,cs,de,el,en-GB,en,es,fa,fi,fr,hi,hr,hu,is,it,nl,pl-PL,pt-BR,ro-RO,ru,sk,sl,sv,tr,vi,zh-Hans,zh-Hant	2026-04-29 11:54:16.473952	\N
3a20e9c3-528d-e6e9-b981-81519bd887d7	AbpMultiTenancy	en	\N	ar,cs,de,en-GB,en,es,fi,fr,hi,hr,hu,is,it,nl,pl-PL,pt-BR,ro-RO,ru,sk,sl,sv,tr,vi,zh-Hans,zh-Hant	2026-04-29 11:54:16.473936	\N
3a20e9c3-528d-ee9d-5daa-421c0edf07a1	AbpGlobalFeature	en	\N	ar,cs,de,el,en,es,fa,fi,fr,hi,hr,hu,is,it,nl,pl-PL,pt-BR,ro-RO,ru,sk,sl,sv,tr,vi,zh-Hans,zh-Hant	2026-04-29 11:54:16.474065	\N
3a20ee93-7a02-8838-b542-4fecd8d9ddfd	MasterDataService	en	AbpValidation,AbpUi	en,ko,ru,vi,zh-Hans	2026-04-30 10:20:06.916984	\N
3a20e9c3-5880-ddb2-c6f3-79492c82f941	LanguageService	vi	AbpValidation,AbpUi	ru,en,zh-Hans,ko,vi	2026-04-29 11:54:17.98612	\N
3a20e9c3-6b7e-8286-a429-cf0d8dee46c5	AbpIdentity	en	AbpValidation,AbpLdap,AbpUi	ar,cs,de,el,en-GB,en,es,fa,fi,fr,hi,hr,hu,is,it,nl,pl-PL,pt-BR,ro-RO,ru,sk,sl,sv,tr,vi,zh-Hans,zh-Hant,ar,cs,de-DE,en,es,fi,fr,hi,hr,hu,it,pt-BR,ru,sk,sl,sv,tr,zh-Hans,zh-Hant,ar,cs,de-DE,en,es,fi,fr,hi,hr,hu,it,pt-BR,ru,sk,sl,sv,tr,zh-Hans,zh-Hant	2026-04-29 11:54:22.848881	2026-05-01 18:10:05.828579
3a20e9c3-528d-7dd1-b31b-6ccf514a4c0f	AbpSettingManagement	en	AbpValidation	ar,cs,de-DE,de,el,en-GB,en,es,fa,fi,fr,hi,hr,hu,is,it,nl,pl-PL,pt-BR,ro-RO,ru,sk,sl,sv,tr,vi,zh-Hans,zh-Hant	2026-04-29 11:54:16.474018	2026-05-01 18:10:00.343285
3a20e9c3-648d-7bfb-200d-bb36db79b5bc	AbpEmailing	en	\N	ar,cs,de-DE,de,el,en-GB,en,es,fa,fi,fr,hi,hr,hu,is,it,nl,pl-PL,pt-BR,ro-RO,ru,sk,sl,sv,tr,vi,zh-Hans,zh-Hant	2026-04-29 11:54:21.075384	\N
3a20e9c3-648d-c1eb-826b-929a3e53008f	AuditLoggingService	en	AbpValidation,AbpUi	en,ko,ru,vi,zh-Hans	2026-04-29 11:54:21.074359	\N
3a20e9c3-7b23-6929-d318-6d38c5964538	IdentityService	en	AbpValidation,AbpUi	en,ko,ru,vi,zh-Hans	2026-04-29 11:54:26.852115	\N
3a20e9c3-528d-8cd2-1cae-bdb7c750f8f8	AbpPermissionManagement	en	AbpValidation	ar,cs,de,el,en-GB,en,es,fa,fi,fr,hi,hr,hu,is,it,nl,pl-PL,pt-BR,ro-RO,ru,sk,sl,sv,tr,vi,zh-Hans,zh-Hant	2026-04-29 11:54:16.474043	2026-05-01 18:10:00.35445
3a20e9c3-6b7e-d5f6-56f1-c77abb044ce2	AbpOpenIddict	en	AbpValidation,AbpUi	ar,cs,de,el,en,es,fa,fi,fr,hi,hr,hu,is,it,nl,pl-PL,pt-BR,ro-RO,ru,sk,sl,sv,tr,vi,zh-Hans,zh-Hant,ar,cs,de-DE,en,es,fi,fr,hi,hr,hu,it,pt-BR,ru,sk,sl,sv,tr,zh-Hans,zh-Hant	2026-04-29 11:54:22.848648	2026-05-01 18:10:05.828434
3a20e9c3-6b7d-23f6-3766-c13c511cc3d3	TextTemplateManagement	en	AbpValidation,AbpUi	ar,cs,de-DE,en,es,fi,fr,hi,hr,hu,it,pl,pt-BR,ru,sk,sl,sv,tr,vi,zh-Hans,zh-Hant	2026-04-29 11:54:22.84842	\N
3a20e9c3-6b7e-9fe0-a886-68883887fdaf	AdministrationService	vi	AbpValidation,AbpUi	en,ko,ru,vi,zh-Hans	2026-04-29 11:54:22.848813	\N
3a20e9c3-6b7e-f473-8aef-bf0b982881ed	AbpLdap	en	\N	ar,cs,de-DE,de,el,en-GB,en,es,fa,fi,fr,hi,hr,hu,is,it,nl,pl-PL,pt-BR,ro-RO,ru,sk,sl,sv,tr,vi,zh-Hans,zh-Hant	2026-04-29 11:54:22.848893	\N
3a20e9c3-528d-9d80-8004-650efe9ac9a8	AbpFeatureManagement	en	AbpValidation	ar,cs,de-DE,de,el,en-GB,en,es,fa,fi,fr,hi,hr,hu,is,it,nl,pl-PL,pt-BR,ro-RO,ru,sk,sl,sv,tr,vi,zh-Hans,zh-Hant	2026-04-29 11:54:16.474051	2026-05-01 18:10:00.354875
3a20e9c3-528d-c8f4-abbc-1aedeb095059	AbpAuditLogging	en	AbpValidation	ar,cs,de-DE,de,en,es,fi,fr,hi,hr,hu,is,it,nl,pl-PL,pt-BR,ro-RO,ru,sk,sl,sv,tr,vi,zh-Hans,zh-Hant	2026-04-29 11:54:16.473974	2026-05-01 18:09:53.882044
3a20e9c3-7477-1fbc-4595-e59eb99d68aa	AIManagementService	en	AbpValidation,AbpUi	en,ko,ru,vi,zh-Hans	2026-04-29 11:54:25.148185	\N
3a20e9c3-7477-ecfd-28f3-81c9b1a3bf25	AIManagement	en	AbpValidation,AbpUi	ar,cs,de,en-GB,en,es,fi,fr,hi,hr,hu,is,it,nl,pl-PL,pt-BR,ro-RO,ru,sk,sl,sv,tr,vi,zh-Hans,zh-Hant	2026-04-29 11:54:25.147602	\N
3a20e9c3-a2f4-1609-6374-30856f083f20	AbpAccount	en	AbpValidation,AbpIdentity,AbpUi	ar,cs,de-DE,en,es,fi,fr,hi,hr,hu,it,pt-BR,ru,sk,sl,sv,tr,zh-Hans,zh-Hant	2026-04-29 11:54:37.046948	\N
3a20e9c3-a2f4-1dc3-fca0-2fbd6cdd25a7	LeptonX	en	\N	ar,cs,de-DE,de,el,en,es,fa,fi,fr,hi,hu,is,it,nl,pl-PL,pt-BR,ro-RO,ru,sk,sl,tr,vi,zh-Hans,zh-Hant	2026-04-29 11:54:37.047396	\N
3a20e9c3-a2f4-52d2-f6a4-6d4c4c331db0	AbpOperationRateLimiting	en	\N	ar,cs,de,el,en-GB,en,es,fa,fi,fr,hi,hr,hu,is,it,nl,pl-PL,pt-BR,ro-RO,ru,sk,sl,sv,tr,vi,zh-Hans	2026-04-29 11:54:37.04719	\N
3a20e9c3-a2f4-72de-3a4e-440d95e78974	AbpUiMultiTenancy	en	\N	ar,cs,de,el,en-GB,en,es-MX,es,fa,fi,fr,hi,hr,hu,is,it,nl,pl-PL,pt-BR,ro-RO,ru,sk,sl,sv,tr,vi,zh-Hans,zh-Hant	2026-04-29 11:54:37.047476	\N
3a20e9c3-528d-a671-66d2-2921a5647e56	LanguageManagement	en	AbpValidation	ar,cs,de-DE,en,es,fi,fr,hi,hr,hu,it,pt-BR,ru,sk,sl,sv,tr,zh-Hans,zh-Hant,ar,cs,de-DE,en,es,fi,fr,hi,hr,hu,it,pt-BR,ru,sk,sl,sv,tr,zh-Hans	2026-04-29 11:54:16.474025	2026-05-01 18:09:55.65341
\.


--
-- Data for Name: AbpLocalizationTexts; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpLocalizationTexts" ("Id", "ResourceName", "CultureName", "Value", "CreationTime", "LastModificationTime") FROM stdin;
3a20e9c3-5215-d3e7-83eb-1e11e99015de	AbpLocalization	en	{"DisplayName:Abp.Localization.DefaultLanguage":"Default language","Description:Abp.Localization.DefaultLanguage":"The default language of the application."}	2026-04-29 11:54:16.477995	\N
3a20e9c3-521f-bb9a-e442-bd6804a21c30	AbpTiming	en	{"DisplayName:Abp.Timing.Timezone":"Timezone","Description:Abp.Timing.Timezone":"Application time zone"}	2026-04-29 11:54:16.478192	\N
3a20e9c3-5223-0d80-ec84-b4f29fe643fc	AbpMultiTenancy	en	{"TenantNotFoundMessage":"Tenant not found!","TenantNotFoundDetails":"There is no tenant with the tenant id or name: {0}","TenantNotActiveMessage":"Tenant is not active!","TenantNotActiveDetails":"The tenant is not active with the tenant id or name: {0}"}	2026-04-29 11:54:16.478305	\N
3a20e9c3-5228-8861-05e0-cc4e57a039a7	AbpExceptionHandling	en	{"InternalServerErrorMessage":"An internal error occurred during your request!","ValidationErrorMessage":"Your request is not valid!","ValidationNarrativeErrorMessageTitle":"The following errors were detected during validation.","DefaultErrorMessage":"An error has occurred!","DefaultErrorMessageDetail":"Error detail not sent by the server.","DefaultErrorMessage401":"You are not authenticated!","DefaultErrorMessage401Detail":"You should sign in to perform this operation.","DefaultErrorMessage403":"You are not authorized!","DefaultErrorMessage403Detail":"You are not allowed to perform this operation!","DefaultErrorMessage404":"Resource not found!","DefaultErrorMessage404Detail":"The resource requested could not be found on the server!","EntityNotFoundErrorMessage":"There is no entity {0} with id = {1}!","EntityNotFoundErrorMessageWithoutId":"There is no entity {0}!","AbpDbConcurrencyErrorMessage":"The data you have submitted has already been changed by another user. Discard your changes and try again.","Error":"Error","UnhandledException":"Unhandled exception!","Authorizing":"Authorizing…","401Message":"Unauthorized","403Message":"Forbidden","404Message":"Page not found","500Message":"Internal Server Error","403MessageDetail":"You are not authorized to perform this operation!","404MessageDetail":"Sorry, there\\u0027s nothing at this address.","Unauthorized":"Unauthorized","invalid_token":"Invalid token","SessionExpired":"Your session has expired. Please login again to continue in the application."}	2026-04-29 11:54:16.478366	\N
3a20e9c3-522d-9543-9d91-dd2d582559af	AbpValidation	en	{"\\u0027{0}\\u0027 and \\u0027{1}\\u0027 do not match.":"\\u0027{0}\\u0027 and \\u0027{1}\\u0027 do not match.","The {0} field is not a valid credit card number.":"The {0} field is not a valid credit card number.","{0} is not valid.":"{0} is not valid.","The {0} field is not a valid e-mail address.":"The {0} field is not a valid e-mail address.","The {0} field only accepts files with the following extensions: {1}":"The {0} field only accepts files with the following extensions: {1}","The field {0} must be a string or array type with a maximum length of \\u0027{1}\\u0027.":"The field {0} must be a string or array type with a maximum length of \\u0027{1}\\u0027.","The field {0} must be a string or array type with a minimum length of \\u0027{1}\\u0027.":"The field {0} must be a string or array type with a minimum length of \\u0027{1}\\u0027.","The {0} field is not a valid phone number.":"The {0} field is not a valid phone number.","The field {0} must be between {1} and {2}.":"The field {0} must be between {1} and {2}.","The field {0} must match the regular expression \\u0027{1}\\u0027.":"The field {0} does not match the requested format.","The {0} field is required.":"The {0} field is required.","The field {0} must be a string with a maximum length of {1}.":"The field {0} must be a string with a maximum length of {1}.","The field {0} must be a string with a minimum length of {2} and a maximum length of {1}.":"The field {0} must be a string with a minimum length of {2} and a maximum length of {1}.","The {0} field is not a valid fully-qualified http, https, or ftp URL.":"The {0} field is not a valid fully-qualified http, https, or ftp URL.","The field {0} is invalid.":"The field {0} is invalid.","The value \\u0027{0}\\u0027 is invalid.":"The value \\u0027{0}\\u0027 is invalid.","The field {0} must be a number.":"The field {0} must be a number.","The field must be a number.":"The field must be a number.","ThisFieldIsNotAValidCreditCardNumber.":"This field is not a valid credit card number.","ThisFieldIsNotValid.":"This field is not valid.","ThisFieldIsNotAValidEmailAddress.":"This field is not a valid e-mail address.","ThisFieldOnlyAcceptsFilesWithTheFollowingExtensions:{0}":"This field only accepts files with the following extensions: {0}","ThisFieldMustBeAStringOrArrayTypeWithAMaximumLengthOf{0}":"This field must be at most \\u0027{0}\\u0027 characters long.","ThisFieldMustBeAStringOrArrayTypeWithAMinimumLengthOf{0}":"This field must be at least \\u0027{0}\\u0027 characters long.","ThisFieldIsNotAValidPhoneNumber.":"This field is not a valid phone number.","ThisFieldMustBeBetween{0}And{1}":"This field must be between {0} and {1}.","ThisFieldMustBeGreaterThanOrEqual{0}":"This field must be greater than or equal to {0}.","ThisFieldMustBeLessOrEqual{0}":"This field must be less than or equal to {0}.","ThisFieldMustMatchTheRegularExpression{0}":"This field must match the regular expression \\u0027{0}\\u0027.","ThisFieldIsRequired.":"This field is required.","ThisFieldMustBeAStringWithAMaximumLengthOf{0}":"This field must be at most \\u0027{0}\\u0027 characters long.","ThisFieldMustBeAStringWithAMinimumLengthOf{1}AndAMaximumLengthOf{0}":"This field must be at least {1} and at most {0} characters long.","ThisFieldIsNotAValidFullyQualifiedHttpHttpsOrFtpUrl":"This field is not a valid fully-qualified http, https, or ftp URL.","ThisFieldIsInvalid.":"This field is invalid."}	2026-04-29 11:54:16.47838	\N
3a20e9c3-5234-74fc-d2a2-2110036235f5	AbpAuditLogging	en	{"Permission:AuditLogging":"Audit logging","Permission:AuditLogs":"Audit logs","Menu:AuditLogging":"Audit logs","AuditLogs":"Audit logs","HttpStatus":"HTTP status","HttpMethod":"HTTP method","HttpMethodFilter":"HTTP method filter","HttpRequest":"HTTP Request","User":"User","UserNameFilter":"User filter","HasException":"Has exception","IpAddress":"IP address","Time":"Time","Date":"Date","Duration":"Duration","Detail":"Detail","Overall":"Overall","Actions":"Actions","ClientIpAddress":"Client IP Address","ClientName":"Client Name","BrowserInfo":"Browser Info","Url":"URL","UserName":"User name","TenantImpersonator":"Tenant Impersonator","UserImpersonator":"User Impersonator","UrlFilter":"URL filter","Exceptions":"Exceptions","Comments":"Comments","HttpStatusCode":"HTTP status code","HttpStatusCodeFilter":"HTTP status code filter","ServiceName":"Service","MethodName":"Method","CorrelationId":"Correlation Id","ApplicationName":"Application name","ExecutionDuration":"Duration","ExtraProperties":"Extra properties","MaxDuration":"Max. duration","MinDuration":"Min. duration","MinMaxDuration":"Duration (Min. - Max.)","{0}Milliseconds":"{0} milliseconds","ExecutionTime":"Time","Parameters":"Parameters","EntityTypeFullName":"Entity Type Full Name","Entity":"Entity","ChangeType":"Change type","ChangeTime":"Time","NewValue":"New value","OriginalValue":"Old value","PropertyName":"Property name","PropertyTypeFullName":"Property Type Full Name","Yes":"Yes","No":"No","Changes":"Changes","AverageExecutionDurationInLogsPerDay":"Average execution duration","AverageExecutionDurationInMilliseconds":"Average execution duration in milliseconds","ErrorRateInLogs":"Error rate in logs","Success":"Success","Fault":"Fault","NoChanges":"No change(s)","EntityChanges":"Entity changes","EntityId":"Entity Id","EntityChangeStartTime":"Min change date","EntityChangeEndTime":"Max change date","EntityHistory":"Entity history","DaysAgoTitle":"{0} {1}.","DaysAgoWithUserTitle":"{0} {1} by {2}.","MinutesAgo":"{0} minutes ago","HoursAgo":"{0} hours ago","DaysAgo":"{0} days ago","Created":"Created","Updated":"Updated","Deleted":"Deleted","ChangeHistory":"Change history","FullChangeHistory":"Full change history","ChangeDetails":"Change details","DurationMs":"Duration (ms)","StartDate":"Start date","EndDate":"End date","Feature:AuditLoggingGroup":"Audit logging","Feature:AuditLoggingEnable":"Enable audit logging page","Feature:AuditLoggingEnableDescription":"Enable audit logging page in the application.","Feature:AuditLoggingSettingManagementEnable":"Enable audit log setting management","Feature:AuditLoggingSettingManagementEnableDescription":"Enable the configuration of the audit log setting management within the application.","InvalidAuditLogDeletionSettings":"Invalid audit log deletion settings. If deletion is enable, the period should be greater than 0 days.","AuditLogSettingsGeneral":"General","AuditLogSettingsGlobal":"Global","DisplayName:IsPeriodicDeleterEnabled":"Enable clean up service system wide","Description:IsPeriodicDeleterEnabled":"If this option is disabled the periodic deleter won\\u0027t work. Audit logs won\\u0027t be deleted automatically.","DisplayName:GlobalIsExpiredDeleterEnabled":"Enable clean up service for all tenants and host","Description:GlobalIsExpiredDeleterEnabled":"If this option is enabled all the tenants and the host expired items will be deleted automatically unless it has a specific setting.","DisplayName:IsExpiredDeleterEnabled":"Enable clean up service","Description:IsExpiredDeleterEnabled":"If this option is enabled the expired items will be deleted automatically.","DisplayName:ExpiredDeleterPeriod":"Expired item deletion period","Description:ExpiredDeleterPeriod":"Set the number of days after which expired items will be automatically deleted.","ExpiredDeleterPeriodUnit":"day(s)","AuditLogsBeforeXWillBeDeleted":"Audit logs before {0} will be deleted.","TenantId":"Tenant Id","Permission:Export":"Export audit logs","ExportToExcel":"Export to Excel","Exporting":"Exporting","ExportCompleted":"Export completed successfully","ExportFailed":"Export failed","ThereWereNoRecordsToExport":"There were no records to export.","ExportJobQueued":"Export job has been queued for {0} records. You will receive an email when the export is completed.","ExportReady":"Export completed for {0} records and is ready for download.","FileName":"File name","TotalRecords":"Total records","ExcelFileAttachedMessage":"The Excel file is attached to this email.","EntityChangeExportCompletedSubject":"Entity change export completed","AuditLogExportCompletedSubject":"Audit log export completed","DownloadLinkExplanation":"You can download the file using the link below until {0} (UTC)","DownloadNow":"Download now","TryAgainMessage":"Please try again. If the problem persists, please contact support."}	2026-04-29 11:54:16.47839	\N
3a20e9c3-5238-77fc-95d6-db60fc1a50ef	AbpGdpr	en	{"Volo.Abp.Gdpr:010001":"You have previously requested to download personal data. Once the given request time period has passed, you can create a new one.","Volo.Abp.Gdpr:010002":"Your personal data is still being prepared. You can download it at {GdprDataReadyTime}.","PersonalData":"Personal Data","Menu:PersonalData":"Personal data","PersonalDataDescription":"Your account contains the personal data that you have given us. This page allows you to download or delete that data.","RequestPersonalData":"Request Personal Data","DeletePersonalData":"Delete Personal Data","CreationTime":"Creation Time","Action":"Action","Preparing":"Preparing","Download":"Download","ReadyTime":"Ready Time","DeletePersonalDataWarning":"Deleting this data will remove your account and you will no longer log in to the application! Are you sure you want to proceed?","PersonalDataDeleteRequestReceived":"Your personal data delete request is being processed... At the end of the data deletion process, your account will be deleted and you will no longer be able to use it.","PersonalDataPrepareRequestReceived":"Your personal data request is being processed. You can download it from this page, once it\\u0027s ready!","NoDataAvailable":"No data is available.","Accept":"Accept","CookiePolicy":"Cookie Policy","PrivacyPolicy":"Privacy Policy","ThisWebsiteUsesCookie":"This website uses cookies to ensure you get the best experience on the website.","CookieConsentAgreePolicies":"If you continue to browse, then you agree to our {0} and {1}.","CookieConsentAgreePolicy":"If you continue to browse, then you agree to our {0}.","CanNotGetDownloadToken":"You can\\u0027t get a download token for this request!"}	2026-04-29 11:54:16.478398	\N
3a20e9c3-523c-613e-0cc1-5cb91e4cecbb	BlobStoringDatabase	en	{"MyAccount":"My account"}	2026-04-29 11:54:16.478409	\N
3a20e9c3-5241-dd07-c1ec-d55ca3e7261d	AbpFeature	en	{"Volo.Feature:010001":"Feature is not enabled: {FeatureName}","Volo.Feature:010002":"Required features are not enabled. All of these features must be enabled: {FeatureNames}","Volo.Feature:010003":"Required features are not enabled. At least one of these features must be enabled: {FeatureNames}"}	2026-04-29 11:54:16.478418	\N
3a20e9c3-524d-329a-9403-4ad28f454f23	AbpSettingManagement	en	{"Settings":"Settings","SavedSuccessfully":"Saved successfully","Permission:SettingManagement":"Setting management","Permission:Emailing":"Emailing","Permission:EmailingTest":"Emailing test","Permission:TimeZone":"Time zone","SendTestEmail":"Send test email","SenderEmailAddress":"Sender email address","TargetEmailAddress":"Target email address","Subject":"Subject","Body":"Body","TestEmailSubject":"Test email {0}","TestEmailBody":"Test email body message here","SentSuccessfully":"Sent successfully","MailSendingFailed":"Mail sending failed, please check your email configuration and try again.","Send":"Send","Menu:Settings":"Settings","Menu:Emailing":"Emailing","Menu:TimeZone":"Time Zone","DisplayName:Timezone":"Time zone","TimezoneHelpText":"This feature allows you to set a default time zone for your server, while users can select their own. If a user\\u0027s time zone differs from the server\\u0027s, all times will adjust accordingly. For example, with the server set to Europe/London(00:00) and a user in Europe/Paris(\\u002B01:00), times will be adjusted by 1 hour for that user. Selecting \\u0027Default Timezone\\u0027 will automatically use the server\\u0027s or browser\\u0027s time zone.","DefaultTimeZone":"Default time zone","SmtpHost":"Host","SmtpPort":"Port","SmtpUserName":"User name","SmtpPassword":"Password","SmtpDomain":"Domain","SmtpEnableSsl":"Enable ssl","SmtpUseDefaultCredentials":"Use default credentials","DefaultFromAddress":"Default from address","DefaultFromDisplayName":"Default from display name","Feature:SettingManagementGroup":"Setting management","Feature:SettingManagementEnable":"Enable setting management","Feature:SettingManagementEnableDescription":"Enable setting management system in the application.","Feature:AllowChangingEmailSettings":"Allow changing email settings","Feature:AllowChangingEmailSettingsDescription":"Allow changing email settings.","SmtpPasswordPlaceholder":"Enter a value to update password"}	2026-04-29 11:54:16.478428	\N
3a20e9c3-57d3-5418-6fa4-9499bed3871d	AbpAuthorization	vi	{"Volo.Authorization:010001":"Ủy quyển thất bại! Đã đưa ra chính sách đã không được cấp.","Volo.Authorization:010002":"Ủy quyển thất bại! Đã đưa ra chính sách chưa được cấp: {PolicyName}","Volo.Authorization:010003":"Ủy quyển thất bại! Chính sách nhất định đã không được cấp cho tài nguyên cụ thể: {ResourceName}","Volo.Authorization:010004":"Ủy quyển thất bại! Yêu cầu nhất định chưa được cấp cho tài nguyên cụ thể: {ResourceName}","Volo.Authorization:010005":"Ủy quyển thất bại! Các yêu cầu nhất định chưa được cấp cho tài nguyên cụ thể: {ResourceName}"}	2026-04-29 11:54:17.990146	\N
3a20e9c3-57d5-b2dd-51e8-6a872a86158e	AbpAuthorization	zh-Hans	{"Volo.Authorization:010001":"授权失败！提供的策略尚未授予。","Volo.Authorization:010002":"授权失败！提供的策略尚未授予： {PolicyName}","Volo.Authorization:010003":"授权失败！提供的策略未授予提供的资源：{ResourceName}","Volo.Authorization:010004":"授权失败！提供的要求未授予提供的资源：{ResourceName}","Volo.Authorization:010005":"授权失败！提供的要求未授予提供的资源：{ResourceName}"}	2026-04-29 11:54:17.990153	\N
3a20e9c3-5252-4103-0861-7eacd367fe20	LanguageManagement	en	{"LanguageManagement":"Language management","Languages":"Languages","OnlyEmptyValues":"Only empty values","All":"All","BaseValue":"Base value","LanguageTexts":"Language texts","BaseCultureName":"Base culture name","BaseCultureValue":"Base culture value","TargetCultureValue":"Target culture value","TargetCultureName":"Target culture name","CultureName":"Culture name","UiCultureName":"UI culture name","ResourceName":"Resource name","RestoreToDefault":"Restore to default","SaveAndNext":"Save And Next","TargetValue":"Target value","Value":"Value","Key":"Key","Edit":"Edit","CreateNewLanguage":"Create new language","Delete":"Delete","DisplayName":"Display name","IsEnabled":"Enabled","SetAsDefaultLanguage":"Set as default language","DefaultLanguage":"Default language","Menu:Languages":"Language management","LanguageDeletionConfirmationMessage":"\\u0027{0}\\u0027 language will be deleted. Do you confirm that?","DefaultLanguageDeletionConfirmationMessage":"Default language \\u0027{0}\\u0027 will be deleted. Do you confirm that?","Filter":"Filter","Feature:LanguageManagementGroup":"Language management","Feature:LanguageManagementEnable":"Enable language management","Feature:LanguageManagementEnableDescription":"Enable language management system in the application.","Volo.Abp.LanguageManagement:010001":"Culture name {CultureName} already exists.","LanguageDisplayNameInfoText":"if the display name is not specified then the culture name will be used instead","Permission:LanguageManagement":"Language management","Permission:LanguageTexts":"Language texts","Permission:LanguageTextsEdit":"Edit language texts","Permission:Languages":"Languages","Permission:LanguagesEdit":"Edit language","Permission:LanguagesCreate":"Create language","Permission:LanguagesChangeDefault":"Change default language","Permission:LanguagesDelete":"Delete language"}	2026-04-29 11:54:16.478436	\N
3a20e9c3-5256-e074-066d-5374e7049ac6	AbpAuthorization	en	{"Volo.Authorization:010001":"Authorization failed! Given policy has not granted.","Volo.Authorization:010002":"Authorization failed! Given policy has not granted: {PolicyName}","Volo.Authorization:010003":"Authorization failed! Given policy has not granted for given resource: {ResourceName}","Volo.Authorization:010004":"Authorization failed! Given requirement has not granted for given resource: {ResourceName}","Volo.Authorization:010005":"Authorization failed! Given requirements has not granted for given resource: {ResourceName}"}	2026-04-29 11:54:16.478444	\N
3a20e9c3-5259-9d77-55f1-4d5dcd5dcdb8	AbpPermissionManagement	en	{"Permissions":"Permissions","OnlyProviderPermissons":"Only this provider","All":"All","SelectAllInAllTabs":"Grant all permissions","SelectAllInThisTab":"Select all","SaveWithoutAnyPermissionsWarningMessage":"Are you sure you want to save without any permissions?","PermissionGroup":"Permission Group","Filter":"Filter","ResourcePermissions":"Permissions","ResourcePermissionTarget":"Target","ResourcePermissionPermissions":"Permissions","AddResourcePermission":"Add permission","ResourcePermissionDeletionConfirmationMessage":"Are you sure you want to delete all permissions?","UpdateResourcePermission":"Update permission","GrantAllResourcePermissions":"Grant all","NoResourceProviderKeyLookupServiceFound":"There is no provider key lookup service was found","NoResourcePermissionFound":"There is no permission defined.","UpdatePermission":"Update permission","NoPermissionsAssigned":"No permissions assigned","SelectProvider":"Select provider","SearchProviderKey":"Search provider key","Provider":"Provider","ErrorLoadingPermissions":"Error loading permissions","PleaseSelectProviderAndPermissions":"Please select provider and permissions"}	2026-04-29 11:54:16.478451	\N
3a20e9c3-525e-bf36-d190-8a6fec78352a	AbpFeatureManagement	en	{"Features":"Features","NoFeatureFoundMessage":"There isn\\u0027t any available feature.","ManageHostFeatures":"Manage host features","ManageHostFeaturesText":"You can manage the host side features by clicking the following button.","Permission:FeatureManagement":"Feature management","Permission:FeatureManagement.ManageHostFeatures":"Manage host features","Volo.Abp.FeatureManagement:InvalidFeatureValue":"{0} feature value is not valid!","Menu:FeatureManagement":"Feature management","ResetToDefault":"Reset to default","ResetedToDefault":"Reseted to default","AreYouSure":"Are you sure?","AreYouSureToResetToDefault":"Are you sure to reset to default?"}	2026-04-29 11:54:16.47846	\N
3a20e9c3-5269-80da-d7b0-537c59af161f	AbpDddApplicationContracts	en	{"MaxResultCountExceededExceptionMessage":"{0} can not be more than {1}! Increase {2}.{3} on the server side to allow more results."}	2026-04-29 11:54:16.47847	\N
3a20e9c3-5276-6ece-67c0-083c95d096d0	AbpGlobalFeature	en	{"Volo.GlobalFeature:010001":"The \\u0027{ServiceName}\\u0027 service needs to enable \\u0027{GlobalFeatureName}\\u0027 feature."}	2026-04-29 11:54:16.478477	\N
3a20e9c3-527d-7e05-6fac-ccb017c3ce9a	AbpUi	en	{"Languages":"Languages","AreYouSure":"Are you sure?","Cancel":"Cancel","Clear":"Clear","Yes":"Yes","No":"No","Ok":"Ok","Close":"Close","Save":"Save","SavingWithThreeDot":"Saving...","Actions":"Actions","Delete":"Delete","CreatedSuccessfully":"Created successfully","SavedSuccessfully":"Saved successfully","DeletedSuccessfully":"Deleted successfully","Edit":"Edit","Refresh":"Refresh","Language":"Language","LoadMore":"Load more","ProcessingWithThreeDot":"Processing...","LoadingWithThreeDot":"Loading...","Welcome":"Welcome","Login":"Login","Register":"Register","Logout":"Log out","Submit":"Submit","Back":"Back","PagerSearch":"Search","PagerNext":"Next","PagerPrevious":"Previous","PagerFirst":"First","PagerLast":"Last","PagerInfo":"Showing _START_ to _END_ of _TOTAL_ entries","PagerInfo{0}{1}{2}":"Showing {0} to {1} of {2} entries","PagerInfoEmpty":"Showing 0 to 0 of 0 entries","PagerInfoFiltered":"(filtered from _MAX_ total entries)","NoDataAvailableInDatatable":"No data available","ErrorLoadingDatatable":"An error occurred during the request. Check the message for details.","Total":"total","Selected":"selected","PagerShowMenuEntries":"Show _MENU_ entries","DatatableActionDropdownDefaultText":"Actions","ChangePassword":"Change password","PersonalInfo":"My profile","AreYouSureYouWantToCancelEditingWarningMessage":"You have unsaved changes.","GoHomePage":"Go to the homepage","GoBack":"Go back","Search":"Search","ItemWillBeDeletedMessageWithFormat":"{0} will be deleted!","ItemWillBeDeletedMessage":"This item will be deleted!","ManageYourAccount":"Manage your account","OthersGroup":"Other","Today":"Today","Apply":"Apply","InternetConnectionInfo":"The operation could not be performed. Your internet connection is not available at the moment.","CopiedToTheClipboard":"Copied to the clipboard","AddNew":"Add new","ProfilePicture":"Profile picture","Theme":"Theme","NotAssigned":"Not Assigned","EntityActionsDisabledTooltip":"You do not have permission to perform any action.","ResourcePermissions":"Permissions"}	2026-04-29 11:54:16.478484	\N
3a20e9c3-5283-457d-b1ff-65564a0a1217	AbpUiNavigation	en	{"Menu:Administration":"Administration"}	2026-04-29 11:54:16.478492	\N
3a20e9c3-5627-0e73-9fcd-59f07048aaa6	AbpLocalization	ru	{"DisplayName:Abp.Localization.DefaultLanguage":"Язык по умолчанию","Description:Abp.Localization.DefaultLanguage":"Язык приложения по умолчанию."}	2026-04-29 11:54:17.989516	\N
3a20e9c3-5631-3439-0260-00be233cb3d3	AbpLocalization	vi	{"DisplayName:Abp.Localization.DefaultLanguage":"Ngôn ngữ mặc định","Description:Abp.Localization.DefaultLanguage":"Ngôn ngữ mặc định của ứng dụng."}	2026-04-29 11:54:17.989652	\N
3a20e9c3-56b2-11f9-ef26-ed4c9e00dcbd	AbpLocalization	zh-Hans	{"DisplayName:Abp.Localization.DefaultLanguage":"默认语言","Description:Abp.Localization.DefaultLanguage":"应用程序的默认语言。"}	2026-04-29 11:54:17.98983	\N
3a20e9c3-5874-6a58-48b2-fce4544cae6b	AbpUiNavigation	ru	{"Menu:Administration":"Администрирование"}	2026-04-29 11:54:17.990339	\N
3a20e9c3-5877-14eb-db7c-4edd13b987b4	AbpUiNavigation	vi	{"Menu:Administration":"Quản trị"}	2026-04-29 11:54:17.990345	\N
3a20e9c3-587a-62bc-6888-9df76073ca1f	AbpUiNavigation	zh-Hans	{"Menu:Administration":"管理"}	2026-04-29 11:54:17.990359	\N
3a20e9c3-56c4-0880-d461-c898f6c9d049	AbpValidation	ru	{"\\u0027{0}\\u0027 and \\u0027{1}\\u0027 do not match.":"\\u0027{0}\\u0027 и \\u0027{1}\\u0027 не совпадают.","The {0} field is not a valid credit card number.":"Поле {0} не содержит действительный номер кредитной карты.","{0} is not valid.":"Значение {0} недействительно.","The {0} field is not a valid e-mail address.":"Поле {0} не содержит действительный адрес электронной почты.","The {0} field only accepts files with the following extensions: {1}":"В поле {0} вы можете загрузить файлы следующих форматов: {1}","The field {0} must be a string or array type with a maximum length of \\u0027{1}\\u0027.":"Поле {0} должно иметь тип строки или массива с максимальной длиной \\u0027{1}\\u0027.","The field {0} must be a string or array type with a minimum length of \\u0027{1}\\u0027.":"Поле {0} должно иметь тип строки или массива с минимальной длиной \\u0027{1}\\u0027.","The {0} field is not a valid phone number.":"Поле {0} не содержит действительный номер телефона.","The field {0} must be between {1} and {2}.":"Поле {0} должно находиться между {1} и {2}.","The field {0} must match the regular expression \\u0027{1}\\u0027.":"Поле {0} не соответствует запрошенному формату.","The {0} field is required.":"Поле {0} необходимо заполнить.","The field {0} must be a string with a maximum length of {1}.":"Поле {0} должно быть строкой с максимальной длиной {1}.","The field {0} must be a string with a minimum length of {2} and a maximum length of {1}.":"Поле {0} должно быть строкой с минимальной длиной {2} и максимальной длиной {1}.","The {0} field is not a valid fully-qualified http, https, or ftp URL.":"Поле {0} не является действительным полным http, https или ftp адресом.","The field {0} is invalid.":"Значение в поле {0} недопустимо.","The value \\u0027{0}\\u0027 is invalid.":"Значение \\u0027{0}\\u0027 недопустимо.","The field {0} must be a number.":"Поле {0} должно быть числом.","The field must be a number.":"Поле должно быть числом.","ThisFieldIsNotAValidCreditCardNumber.":"Это поле не содержит действительный номер кредитной карты.","ThisFieldIsNotValid.":"Значение в этом поле недействительно.","ThisFieldIsNotAValidEmailAddress.":"Это поле не содержит действительный адрес электронной почты.","ThisFieldOnlyAcceptsFilesWithTheFollowingExtensions:{0}":"Вы можете загрузить файлы только следующих форматов: {0}","ThisFieldMustBeAStringOrArrayTypeWithAMaximumLengthOf{0}":"Это поле должно содержать не более \\u0027{0}\\u0027 символов.","ThisFieldMustBeAStringOrArrayTypeWithAMinimumLengthOf{0}":"Это поле должно содержать не менее \\u0027{0}\\u0027 символов.","ThisFieldIsNotAValidPhoneNumber.":"Это поле не содержит действительный номер телефона.","ThisFieldMustBeBetween{0}And{1}":"Это поле должно быть между {0} и {1}.","ThisFieldMustBeGreaterThanOrEqual{0}":"Это поле должно быть больше или равно {0}.","ThisFieldMustBeLessOrEqual{0}":"Это поле должно быть меньше или равно {0}.","ThisFieldMustMatchTheRegularExpression{0}":"Это поле должно соответствовать регулярному выражению \\u0027{0}\\u0027.","ThisFieldIsRequired.":"Это обязательное поле.","ThisFieldMustBeAStringWithAMaximumLengthOf{0}":"Это поле должно содержать не более \\u0027{0}\\u0027 символов.","ThisFieldMustBeAStringWithAMinimumLengthOf{1}AndAMaximumLengthOf{0}":"Это поле должно содержать от {1} до {0} символов.","ThisFieldIsNotAValidFullyQualifiedHttpHttpsOrFtpUrl":"Значение в поле не является действительным полным http, https или ftp адресом.","ThisFieldIsInvalid.":"Значение в этом поле недопустимо."}	2026-04-29 11:54:17.989907	\N
3a20e9c3-56cb-6da5-4487-c0439fd3d857	AbpValidation	vi	{"\\u0027{0}\\u0027 and \\u0027{1}\\u0027 do not match.":"\\u0027{0}\\u0027 và \\u0027{1}\\u0027 không khớp.","The {0} field is not a valid credit card number.":"Trường {0} không phải là một số thẻ tín dụng hợp lệ.","{0} is not valid.":"{0} không hợp lệ.","The {0} field is not a valid e-mail address.":"Trường {0} không phải là một địa chỉ email hợp lệ.","The {0} field only accepts files with the following extensions: {1}":"Trường {0} chỉ chấp nhận các tập tin có phần mở rộng như sau: {1}","The field {0} must be a string or array type with a maximum length of \\u0027{1}\\u0027.":"Trường {0} phải là một chuỗi hoặc một mảng với độ dài tối đa là \\u0027{1}\\u0027.","The field {0} must be a string or array type with a minimum length of \\u0027{1}\\u0027.":"Trường {0} phải là một kiểu chuỗi hoặc mảng có độ dài tối thiểu là \\u0027{1}\\u0027.","The {0} field is not a valid phone number.":"Trường {0} không phải là một số điện thoại hợp lệ","The field {0} must be between {1} and {2}.":"Trường {0} phải ở giữa {1} and {2}.","The field {0} must match the regular expression \\u0027{1}\\u0027.":"Trường {0} không khớp với định dạng được yêu cầu.","The {0} field is required.":"Trường {0} là bắt buộc.","The field {0} must be a string with a maximum length of {1}.":"Trường {0} phải là một chuỗi với độ dài tối đa là {1}.","The field {0} must be a string with a minimum length of {2} and a maximum length of {1}.":"Trường {0} phải là một chuỗi với độ dài tối thiểu {2} và tối đa là {1}.","The {0} field is not a valid fully-qualified http, https, or ftp URL.":"Trường {0} không phải là một http, https, hoặc ftp URL đủ điều kiện hợp lệ.","The field {0} is invalid.":"Trường {0} không có hiệu lực","The value \\u0027{0}\\u0027 is invalid.":"Giá trị \\u0027{0}\\u0027 không hợp lệ.","The field {0} must be a number.":"Trường {0} phải là một số.","The field must be a number.":"Trường phải là một số.","ThisFieldIsNotAValidCreditCardNumber.":"Trường này không phải là số thẻ tín dụng hợp lệ.","ThisFieldIsNotValid.":"Trường này không hợp lệ.","ThisFieldIsNotAValidEmailAddress.":"Trường này không phải là địa chỉ e-mail hợp lệ.","ThisFieldOnlyAcceptsFilesWithTheFollowingExtensions:{0}":"Trường này chỉ chấp nhận các tệp có các phần mở rộng sau: {0}","ThisFieldMustBeAStringOrArrayTypeWithAMaximumLengthOf{0}":"Trường này phải có tối đa \\u0027{0}\\u0027 ký tự.","ThisFieldMustBeAStringOrArrayTypeWithAMinimumLengthOf{0}":"Trường này phải có ít nhất \\u0027{0}\\u0027 ký tự.","ThisFieldIsNotAValidPhoneNumber.":"Trường này không phải là số điện thoại hợp lệ.","ThisFieldMustBeBetween{0}And{1}":"Trường này phải nằm trong khoảng từ {0} đến {1}.","ThisFieldMustBeGreaterThanOrEqual{0}":"Trường này phải lớn hơn hoặc bằng {0}.","ThisFieldMustBeLessOrEqual{0}":"Trường này phải nhỏ hơn hoặc bằng {0}.","ThisFieldMustMatchTheRegularExpression{0}":"Trường này phải khớp với biểu thức chính quy \\u0027{0}\\u0027.","ThisFieldIsRequired.":"Trường này là bắt buộc.","ThisFieldMustBeAStringWithAMaximumLengthOf{0}":"Trường này phải có tối đa \\u0027{0}\\u0027 ký tự.","ThisFieldMustBeAStringWithAMinimumLengthOf{1}AndAMaximumLengthOf{0}":"Trường này phải có ít nhất {1} và tối đa {0} ký tự.","ThisFieldIsNotAValidFullyQualifiedHttpHttpsOrFtpUrl":"Trường này không phải là URL http, https hoặc ftp đủ điều kiện hợp lệ.","ThisFieldIsInvalid.":"Trường này không hợp lệ."}	2026-04-29 11:54:17.989919	\N
3a20e9c3-56db-c3f9-c83d-3185384f24fd	AbpValidation	zh-Hans	{"\\u0027{0}\\u0027 and \\u0027{1}\\u0027 do not match.":"\\u0027{0}\\u0027与\\u0027{1}\\u0027不匹配.","The {0} field is not a valid credit card number.":"字段{0}不是有效的信用卡号码.","{0} is not valid.":"{0}验证未通过.","The {0} field is not a valid e-mail address.":"字段{0}不是有效的邮箱地址.","The {0} field only accepts files with the following extensions: {1}":"{0}字段只允许以下扩展名的文件: {1}","The field {0} must be a string or array type with a maximum length of \\u0027{1}\\u0027.":"字段{0}必须是最大长度为\\u0027{1}\\u0027的字符串或数组.","The field {0} must be a string or array type with a minimum length of \\u0027{1}\\u0027.":"字段{0}必须是最小长度为\\u0027{1}\\u0027的字符串或数组.","The {0} field is not a valid phone number.":"字段{0}不是有效的手机号码.","The field {0} must be between {1} and {2}.":"字段{0}值必须在{1}和{2}范围内.","The field {0} must match the regular expression \\u0027{1}\\u0027.":"字段{0}与请求的格式不匹配。","The {0} field is required.":"字段{0}不可为空.","The field {0} must be a string with a maximum length of {1}.":"字段{0}必须是最大长度为{1}的字符串.","The field {0} must be a string with a minimum length of {2} and a maximum length of {1}.":"字段{0}必须是最小长度为{2}并且最大长度{1}的字符串.","The {0} field is not a valid fully-qualified http, https, or ftp URL.":"字段{0}不是有效的完全限定的http,https或ftp URL.","The field {0} is invalid.":"字段{0}是无效值.","The value \\u0027{0}\\u0027 is invalid.":"\\u0027{0}\\u0027是无效值","The field {0} must be a number.":"字段{0}必须是数字.","The field must be a number.":"该字段必须是数字.","ThisFieldIsNotAValidCreditCardNumber.":"字段不是有效的信用卡号码.","ThisFieldIsNotValid.":"验证未通过.","ThisFieldIsNotAValidEmailAddress.":"字段不是有效的邮箱地址.","ThisFieldOnlyAcceptsFilesWithTheFollowingExtensions:{0}":"字段只允许以下扩展名的文件: {0}","ThisFieldMustBeAStringOrArrayTypeWithAMaximumLengthOf{0}":"该字段的长度不能超过\\u0027{0}\\u0027个字符.","ThisFieldMustBeAStringOrArrayTypeWithAMinimumLengthOf{0}":"字段必须是最小长度为\\u0027{0}\\u0027的字符串或数组.","ThisFieldIsNotAValidPhoneNumber.":"字段不是有效的手机号码.","ThisFieldMustBeBetween{0}And{1}":"字段值必须在{0}和{1}范围内.","ThisFieldMustBeGreaterThanOrEqual{0}":"该字段必须大于或等于 {0}。","ThisFieldMustBeLessOrEqual{0}":"该字段必须小于或等于 {0}。","ThisFieldMustMatchTheRegularExpression{0}":"字段必须匹配正则表达式\\u0027{0}\\u0027.","ThisFieldIsRequired.":"字段不可为空.","ThisFieldMustBeAStringWithAMaximumLengthOf{0}":"该字段的长度不能超过\\u0027{0}\\u0027个字符.","ThisFieldMustBeAStringWithAMinimumLengthOf{1}AndAMaximumLengthOf{0}":"该字段的长度应在{1}到{0}个字符之间.","ThisFieldIsNotAValidFullyQualifiedHttpHttpsOrFtpUrl":"字段{0}不是有效的完全限定的http,https或ftp URL.","ThisFieldIsInvalid.":"该字段无效."}	2026-04-29 11:54:17.989934	\N
3a20e9c3-5701-c86c-c5bd-d9da0938cc38	AbpMultiTenancy	ru	{"TenantNotFoundMessage":"Арендатор не найден!","TenantNotFoundDetails":"Нет клиента с идентификатором или именем клиента: {0}","TenantNotActiveMessage":"Арендатор не активен!","TenantNotActiveDetails":"Арендатор неактивен с идентификатором или именем арендатора: {0}"}	2026-04-29 11:54:17.989943	\N
3a20e9c3-570f-3520-3d92-c409b6de7722	AbpMultiTenancy	vi	{"TenantNotFoundMessage":"Không tìm thấy người thuê nhà!","TenantNotFoundDetails":"Không có đối tượng thuê nào có id hoặc tên đối tượng thuê: {0}","TenantNotActiveMessage":"Người thuê nhà không hoạt động!","TenantNotActiveDetails":"Đối tượng thuê không hoạt động với id hoặc tên đối tượng thuê: {0}"}	2026-04-29 11:54:17.989951	\N
3a20e9c3-5718-b033-317f-d70e7108fd69	AbpMultiTenancy	zh-Hans	{"TenantNotFoundMessage":"未找到租户！","TenantNotFoundDetails":"没有租户的 ID 或名称为：{0}的租户。","TenantNotActiveMessage":"租户未启用！","TenantNotActiveDetails":"租户未启用，租户 ID 或名称为：{0}。"}	2026-04-29 11:54:17.989959	\N
3a20e9c3-572d-3d0e-8056-6c0c8bc22556	AbpFeature	ru	{"Volo.Feature:010001":"Функция не включена: {FeatureName}","Volo.Feature:010002":"Необходимые функции не включены. Все эти функции должны быть включены: {FeatureNames}","Volo.Feature:010003":"Необходимые функции не включены. Должна быть включена хотя бы одна из этих функций: {FeatureNames}"}	2026-04-29 11:54:17.989967	\N
3a20e9c3-573b-cf8e-087c-e7f518a3a447	AbpFeature	vi	{"Volo.Feature:010001":"Tính năng chưa được bật: {FeatureName}","Volo.Feature:010002":"Các tính năng bắt buộc không được kích hoạt. Tất cả các tính năng này phải được bật: {FeatureNames}","Volo.Feature:010003":"Các tính năng bắt buộc không được kích hoạt. Ít nhất một trong các tính năng này phải được bật: {FeatureNames}"}	2026-04-29 11:54:17.989978	\N
3a20e9c3-574d-4d64-24b6-72b38725f611	AbpFeature	zh-Hans	{"Volo.Feature:010001":"功能未启用: {FeatureName}","Volo.Feature:010002":"必要的功能未启用. 这些功能需要启用: {FeatureNames}","Volo.Feature:010003":"必要的功能未启用. 需要启用这些功能中的一项：{FeatureNames}"}	2026-04-29 11:54:17.989986	\N
3a20e9c3-5753-33db-7df0-462c772e6344	LanguageManagement	ru	{"LanguageManagement":"Управление языками","Languages":"Языки","OnlyEmptyValues":"Только пустые значения","All":"Все","BaseValue":"Базовая стоимость","LanguageTexts":"Языковые тексты","BaseCultureName":"Название базовой культуры","BaseCultureValue":"Базовая ценность культуры","TargetCultureValue":"Ценность целевой культуры","TargetCultureName":"Название целевой культуры","CultureName":"Название культуры","UiCultureName":"Имя культуры пользовательского интерфейса","ResourceName":"Имя ресурса","RestoreToDefault":"Восстановить по умолчанию","SaveAndNext":"Сохранить и далее","TargetValue":"Целевое значение","Value":"Стоимость","Key":"Ключ","Edit":"Редактировать","CreateNewLanguage":"Создать новый язык","Delete":"Удалить","DisplayName":"Показать имя","IsEnabled":"Включен","SetAsDefaultLanguage":"Сделать языком по умолчанию","DefaultLanguage":"Язык по умолчанию","Menu:Languages":"Управление языками","LanguageDeletionConfirmationMessage":"\\u0027{0}\\u0027 Язык будет удален. Вы это подтверждаете?","DefaultLanguageDeletionConfirmationMessage":"Язык по умолчанию «{0}» будет удален. Вы это подтверждаете?","Filter":"Фильтр","Feature:LanguageManagementGroup":"Управление языками","Feature:LanguageManagementEnable":"Включить управление языками","Feature:LanguageManagementEnableDescription":"Включите систему управления языками в приложении.","Volo.Abp.LanguageManagement:010001":"Название культуры {CultureName} уже существует.","Permission:LanguageManagement":"Управление языками","Permission:LanguageTexts":"Языковые тексты","Permission:LanguageTextsEdit":"Редактировать языковые тексты","Permission:Languages":"Языки","Permission:LanguagesEdit":"Изменить язык","Permission:LanguagesCreate":"Создать язык","Permission:LanguagesChangeDefault":"Изменить язык по умолчанию","Permission:LanguagesDelete":"Удалить язык"}	2026-04-29 11:54:17.989994	\N
3a20e9c3-5754-c792-e2c2-06944a71c6e8	LanguageManagement	zh-Hans	{"LanguageManagement":"语言管理","Languages":"语言","OnlyEmptyValues":"只有空值","All":"全部","BaseValue":"基础值","LanguageTexts":"语言文本","BaseCultureName":"基础文化名称","BaseCultureValue":"基础文化值","TargetCultureValue":"目标文化值","TargetCultureName":"目标文化名称","CultureName":"文化名称","UiCultureName":"界面文化名称","ResourceName":"资源名称","RestoreToDefault":"恢复到默认值","SaveAndNext":"保存并下一步","TargetValue":"目标值","Value":"值","Key":"键","Edit":"编辑","CreateNewLanguage":"创建新语言","Delete":"删除","DisplayName":"显示名称","IsEnabled":"已启用","SetAsDefaultLanguage":"设置为默认语言","DefaultLanguage":"默认语言","Menu:Languages":"语言管理","LanguageDeletionConfirmationMessage":"{0}\\u0022语言将被删除。您确认吗？","DefaultLanguageDeletionConfirmationMessage":"默认语言\\u0022{0}\\u0022将被删除。您确认吗？","Filter":"筛选","Feature:LanguageManagementGroup":"语言管理","Feature:LanguageManagementEnable":"启用语言管理","Feature:LanguageManagementEnableDescription":"在应用程序中启用语言管理系统。","Volo.Abp.LanguageManagement:010001":"文化名称 {CultureName} 已经存在。","Permission:LanguageManagement":"语言管理","Permission:LanguageTexts":"语言文本","Permission:LanguageTextsEdit":"编辑语言文本","Permission:Languages":"语言","Permission:LanguagesEdit":"编辑语言","Permission:LanguagesCreate":"创建语言","Permission:LanguagesChangeDefault":"更改默认语言","Permission:LanguagesDelete":"删除语言"}	2026-04-29 11:54:17.99	\N
3a20e9c3-5768-ee19-a37a-fe62e1258a7d	AbpTiming	ru	{"DisplayName:Abp.Timing.Timezone":"Часовой пояс","Description:Abp.Timing.Timezone":"Часовой пояс приложения"}	2026-04-29 11:54:17.990007	\N
3a20e9c3-576f-5eaf-b6cd-abc1b3d98f79	AbpTiming	vi	{"DisplayName:Abp.Timing.Timezone":"Múi giờ","Description:Abp.Timing.Timezone":"Múi giờ ứng dụng"}	2026-04-29 11:54:17.990016	\N
3a20e9c3-5778-5390-045c-e248137ab488	AbpTiming	zh-Hans	{"DisplayName:Abp.Timing.Timezone":"时区","Description:Abp.Timing.Timezone":"应用时区"}	2026-04-29 11:54:17.990023	\N
3a20e9c3-5780-4cb1-37ba-f7bfa4c488db	AbpExceptionHandling	ru	{"InternalServerErrorMessage":"Во время запроса произошла внутренняя ошибка!","ValidationErrorMessage":"Ваш запрос недействителен!","ValidationNarrativeErrorMessageTitle":"При проверке были обнаружены следующие ошибки.","DefaultErrorMessage":"Произошла ошибка!","DefaultErrorMessageDetail":"Сведения об ошибке не были предоставлены сервером.","DefaultErrorMessage401":"Вы не авторизированы!","DefaultErrorMessage401Detail":"Вы должны Войти, чтобы выполнить эту операцию.","DefaultErrorMessage403":"Вы не авторизованы!","DefaultErrorMessage403Detail":"У вас нет доступа к выполнению этой операции!","DefaultErrorMessage404":"Ресурс не найден!","DefaultErrorMessage404Detail":"Запрошенный ресурс не удалось найти на сервере!","EntityNotFoundErrorMessage":"Нет объекта {0} с id = {1}!","EntityNotFoundErrorMessageWithoutId":"Нет объекта {0}!","AbpDbConcurrencyErrorMessage":"Отправленные вами данные уже были изменены другим пользователем/клиентом. Отмените внесенные вами изменения и попробуйте с самого начала.","Error":"Ошибка","UnhandledException":"Непредвиденная ошибка!","Authorizing":"Авторизация…","401Message":"Неавторизованный","403Message":"В доступе отказано","404Message":"Страница не найдена","500Message":"Внутренняя ошибка сервера","403MessageDetail":"У вас нет прав на выполнение этой операции!","404MessageDetail":"Извините, по этому адресу ничего нет.","Unauthorized":"Неавторизованный","invalid_token":"Неверный токен","SessionExpired":"Ваша сессия истекла. Пожалуйста, войдите снова, чтобы продолжить работу в приложении."}	2026-04-29 11:54:17.990044	\N
3a20e9c3-5781-6ac6-c5ac-7e61b18f8c50	AbpExceptionHandling	vi	{"InternalServerErrorMessage":"Có một lỗi nội bộ xảy ra trong quá trình thực hiện yêu cầu của bạn!","ValidationErrorMessage":"Yêu cầu của bạn không hợp lệ!","ValidationNarrativeErrorMessageTitle":"Các lỗi sau đây đã được phát hiện trong quá trình xác nhận","DefaultErrorMessage":"Một lỗi đã xảy ra","DefaultErrorMessageDetail":"Chi tiết lỗi không được gửi bởi máy chủ","DefaultErrorMessage401":"Bạn chưa được xác thực","DefaultErrorMessage401Detail":"Bạn cần đăng nhập để thực hiện thao tác này.","DefaultErrorMessage403":"Bạn không được phép!","DefaultErrorMessage403Detail":"Bạn không được phép thực hiện thao tác này!","DefaultErrorMessage404":"Tài nguyên không tìm thấy!","DefaultErrorMessage404Detail":"Tài nguyên được yêu cầu không được tìm thấy trên máy chủ!","EntityNotFoundErrorMessage":"Không có thực thể nào {0} với id = {1}!","EntityNotFoundErrorMessageWithoutId":"Không có thực thể nào {0}!","AbpDbConcurrencyErrorMessage":"Dữ liệu bạn gửi đã bị người dùng/khách hàng khác thay đổi. Vui lòng hủy các thay đổi bạn đã thực hiện và thử lại từ đầu.","Error":"Lỗi","UnhandledException":"Tình huống ngoại lệ không thể xử lí được!","Authorizing":"Đang ủy quyền…","401Message":"Không được phép","403Message":"Cấm","404Message":"Không tìm thấy trang","500Message":"Lỗi máy chủ nội bộ","403MessageDetail":"Bạn không được phép thực hiện thao tác này!","404MessageDetail":"Xin lỗi, không có gì ở địa chỉ này.","Unauthorized":"Không được phép","invalid_token":"Token không hợp lệ","SessionExpired":"Phiên của bạn đã hết hạn. Vui lòng đăng nhập lại để tiếp tục trong ứng dụng."}	2026-04-29 11:54:17.990051	\N
3a20e9c3-5783-4302-0d3e-bc4e7d2562ed	AbpExceptionHandling	zh-Hans	{"InternalServerErrorMessage":"对不起，在处理您的请求期间产生了一个服务器内部错误！！","ValidationErrorMessage":"您的请求无效！","ValidationNarrativeErrorMessageTitle":"验证过程中检测到以下错误。","DefaultErrorMessage":"发生错误！","DefaultErrorMessageDetail":"服务器未发送错误详细信息。","DefaultErrorMessage401":"您未通过身份验证！","DefaultErrorMessage401Detail":"您需要进行身份认证(登录)后再执行此操作。","DefaultErrorMessage403":"您未获得授权！","DefaultErrorMessage403Detail":"不允许执行此操作！","DefaultErrorMessage404":"未找到资源！","DefaultErrorMessage404Detail":"服务器上找不到所请求的资源！","EntityNotFoundErrorMessage":"不存在 id = {1} 的实体 {0}！","EntityNotFoundErrorMessageWithoutId":"不存在实体 {0}！","AbpDbConcurrencyErrorMessage":"您提交的数据已被其他用户/客户更改。请放弃您所做的更改并从头开始尝试。","Error":"错误","UnhandledException":"未处理异常！","Authorizing":"正在授权…","401Message":"未经授权","403Message":"禁止","404Message":"未找到页面","500Message":"内部服务器错误","403MessageDetail":"您无权执行此操作！","404MessageDetail":"抱歉，这个地址没有任何信息。","Unauthorized":"未经授权","invalid_token":"无效的令牌","SessionExpired":"您的会话已过期。请重新登录以继续应用程序。"}	2026-04-29 11:54:17.990059	\N
3a20e9c3-5789-6035-133e-64996f47486c	BlobStoringDatabase	ru	{"MyAccount":"Мой аккаунт"}	2026-04-29 11:54:17.990066	\N
3a20e9c3-5793-cfb1-82a4-0b1e54156562	BlobStoringDatabase	vi	{"MyAccount":"Tài khoản của tôi"}	2026-04-29 11:54:17.990075	\N
3a20e9c3-5797-d747-a54d-4cadbb80b668	BlobStoringDatabase	zh-Hans	{"MyAccount":"我的账户"}	2026-04-29 11:54:17.990084	\N
3a20e9c3-6b75-7e32-dc22-a46903aabbbf	AbpLdap	vi	{"DisplayName:Abp.Ldap.Ldaps":"LDAP qua SSL","Description:Abp.Ldap.Ldaps":"LDAP qua SSL","DisplayName:Abp.Ldap.ServerHost":"Máy chủ lưu trữ","Description:Abp.Ldap.ServerHost":"Máy chủ lưu trữ","DisplayName:Abp.Ldap.ServerPort":"Cổng máy chủ","Description:Abp.Ldap.ServerPort":"Cổng máy chủ","DisplayName:Abp.Ldap.BaseDc":"Thành phần miền cơ sở","Description:Abp.Ldap.BaseDc":"Thành phần miền cơ sở","DisplayName:Abp.Ldap.Domain":"Lãnh địa","Description:Abp.Ldap.Domain":"Lãnh địa","DisplayName:Abp.Ldap.UserName":"tên tài khoản","Description:Abp.Ldap.UserName":"tên tài khoản","DisplayName:Abp.Ldap.Password":"Mật khẩu","Description:Abp.Ldap.Password":"Mật khẩu"}	2026-04-29 11:54:22.858936	\N
3a20e9c3-579c-eacc-95a9-4e19b4dc422a	AbpSettingManagement	ru	{"Settings":"Настройки","SavedSuccessfully":"Успешно сохранено","Permission:SettingManagement":"Управление настройками","Permission:Emailing":"Отправка по электронной почте","Permission:EmailingTest":"Тестовая рассылка по электронной почте","Permission:TimeZone":"Часовой пояс","SendTestEmail":"Отправить тестовое письмо","SenderEmailAddress":"Адрес электронной почты отправителя","TargetEmailAddress":"Целевой адрес электронной почты","Subject":"Предмет","Body":"Тело","TestEmailSubject":"Тестовое письмо {0}","TestEmailBody":"Проверьте текст сообщения электронной почты здесь","SentSuccessfully":"отправлено успешно","MailSendingFailed":"Не удалось отправить письмо. Пожалуйста, проверьте настройки электронной почты и повторите попытку.","Send":"Отправлять","Menu:Settings":"Настройки","Menu:Emailing":"Отправка по электронной почте","Menu:TimeZone":"Часовой пояс","DisplayName:Timezone":"Часовой пояс","TimezoneHelpText":"Эта функция позволяет вам установить часовой пояс по умолчанию для вашего сервера, в то время как пользователи могут выбрать свой собственный. Если часовой пояс пользователя отличается от часового пояса сервера, все время будет соответствующим образом скорректировано. Например, если сервер установлен на Европа/Лондон(00:00), а пользователь находится в Европа/Париж(\\u002B01:00), время будет скорректировано на 1 час для этого пользователя. При выборе \\u0027Часовой пояс по умолчанию\\u0027 будет автоматически использоваться часовой пояс сервера или браузера.","DefaultTimeZone":"Часовой пояс по умолчанию","SmtpHost":"Хозяин","SmtpPort":"Порт","SmtpUserName":"Имя пользователя","SmtpPassword":"Пароль","SmtpDomain":"Домен","SmtpEnableSsl":"Включить ssl","SmtpUseDefaultCredentials":"Использовать учетные данные по умолчанию","DefaultFromAddress":"По умолчанию с адреса","DefaultFromDisplayName":"По умолчанию из отображаемого имени","Feature:SettingManagementGroup":"Управление настройками","Feature:SettingManagementEnable":"Включить управление настройками","Feature:SettingManagementEnableDescription":"Включите систему управления настройками в приложении.","Feature:AllowChangingEmailSettings":"Разрешить изменение настроек электронной почты","Feature:AllowChangingEmailSettingsDescription":"Разрешить изменение настроек электронной почты.","SmtpPasswordPlaceholder":"Введите значение для обновления пароля"}	2026-04-29 11:54:17.990091	\N
3a20e9c3-579f-6398-b43e-146a3a1793f9	AbpSettingManagement	vi	{"Settings":"Cài đặt","SavedSuccessfully":"Lưu thành công","Permission:SettingManagement":"Cài đặt quản lý","Permission:Emailing":"Gửi email","Permission:EmailingTest":"Kiểm tra gửi email","Permission:TimeZone":"Múi giờ","SendTestEmail":"Gửi email kiểm tra","SenderEmailAddress":"Địa chỉ email người gửi","TargetEmailAddress":"Địa chỉ email mục tiêu","Subject":"Chủ thể","Body":"Thân hình","TestEmailSubject":"Kiểm tra email {0}","TestEmailBody":"Kiểm tra nội dung email tại đây","SentSuccessfully":"Gửi thành công","MailSendingFailed":"Gửi email thất bại. Vui lòng kiểm tra cấu hình email của bạn và thử lại.","Send":"Gửi","Menu:Settings":"Cài đặt","Menu:Emailing":"Gửi email","Menu:TimeZone":"Múi Giờ","DisplayName:Timezone":"Múi giờ","TimezoneHelpText":"Tính năng này cho phép bạn đặt múi giờ mặc định cho máy chủ của mình, trong khi người dùng có thể chọn múi giờ của riêng họ. Nếu múi giờ của người dùng khác với múi giờ của máy chủ, tất cả thời gian sẽ được điều chỉnh tương ứng. Ví dụ: với máy chủ được đặt ở Châu Âu/Luân Đôn(00:00) và người dùng ở Châu Âu/Paris(\\u002B01:00), thời gian sẽ được điều chỉnh 1 giờ cho người dùng đó. Chọn \\u0027Múi giờ mặc định\\u0027 sẽ tự động sử dụng múi giờ của máy chủ hoặc trình duyệt.","DefaultTimeZone":"Múi giờ mặc định","SmtpHost":"Tổ chức","SmtpPort":"Hải cảng","SmtpUserName":"Tên tài khoản","SmtpPassword":"Mật khẩu","SmtpDomain":"Lãnh địa","SmtpEnableSsl":"Bật ssl","SmtpUseDefaultCredentials":"Sử dụng thông tin đăng nhập mặc định","DefaultFromAddress":"Mặc định từ địa chỉ","DefaultFromDisplayName":"Mặc định từ tên hiển thị","Feature:SettingManagementGroup":"Cài đặt quản lý","Feature:SettingManagementEnable":"Bật quản lý cài đặt","Feature:SettingManagementEnableDescription":"Bật cài đặt hệ thống quản lý trong ứng dụng.","Feature:AllowChangingEmailSettings":"Cho phép thay đổi cài đặt email","Feature:AllowChangingEmailSettingsDescription":"Cho phép thay đổi cài đặt email.","SmtpPasswordPlaceholder":"Nhập một giá trị để cập nhật mật khẩu"}	2026-04-29 11:54:17.990098	\N
3a20e9c3-57a2-e3f5-3637-97e6acf1ac80	AbpSettingManagement	zh-Hans	{"Settings":"设置","SavedSuccessfully":"保存成功","Permission:SettingManagement":"设置管理","Permission:Emailing":"邮件","Permission:EmailingTest":"邮件测试","Permission:TimeZone":"时区","SendTestEmail":"发送测试邮件","SenderEmailAddress":"发件人邮箱地址","TargetEmailAddress":"收件人邮箱地址","Subject":"主题","Body":"正文","TestEmailSubject":"测试邮件 {0}","TestEmailBody":"测试邮件内容","SentSuccessfully":"发送成功","MailSendingFailed":"邮件发送失败，请检查您的电子邮件配置并重试。","Send":"发送","Menu:Settings":"设置","Menu:Emailing":"邮件","Menu:TimeZone":"时区","DisplayName:Timezone":"时区","TimezoneHelpText":"此功能允许您为服务器设置默认时区，同时用户可以选择自己的时区。如果用户的时区与服务器的时区不同，所有时间将相应调整。例如，如果服务器设置为欧洲/伦敦(00:00)，而用户在欧洲/巴黎(\\u002B01:00)，则该用户的时间将调整1小时。选择\\u0027默认时区\\u0027将自动使用服务器或浏览器的时区。","DefaultTimeZone":"默认时区","SmtpHost":"主机","SmtpPort":"端口","SmtpUserName":"用户名","SmtpPassword":"密码","SmtpDomain":"域","SmtpEnableSsl":"启用ssl","SmtpUseDefaultCredentials":"使用默认凭据","DefaultFromAddress":"默认发件人","DefaultFromDisplayName":"默认显示名称","Feature:SettingManagementGroup":"设置管理","Feature:SettingManagementEnable":"启用设置管理","Feature:SettingManagementEnableDescription":"在应用程序中启用设置管理系统。","Feature:AllowChangingEmailSettings":"允许更改邮件设置","Feature:AllowChangingEmailSettingsDescription":"允许更改邮件设置。","SmtpPasswordPlaceholder":"输入一个值以更新密码"}	2026-04-29 11:54:17.990109	\N
3a20e9c3-57a9-e792-9d5f-742165e82de1	AbpAuditLogging	ru	{"Permission:AuditLogging":"Ведение журнала аудита","Permission:AuditLogs":"Журналы аудита","Menu:AuditLogging":"Журналы аудита","AuditLogs":"Журналы аудита","HttpStatus":"HTTP-статус","HttpMethod":"HTTP-метод","HttpMethodFilter":"Фильтр HTTP-методов","HttpRequest":"HTTP-запрос","User":"Пользователь","UserNameFilter":"Пользовательский фильтр","HasException":"Имеет исключение","IpAddress":"Айпи адрес","Time":"Время","Date":"Дата","Duration":"Продолжительность","Detail":"Деталь","Overall":"В общем и целом","Actions":"Действия","ClientIpAddress":"IP-адрес клиента","ClientName":"имя клиента","BrowserInfo":"Информация о браузере","Url":"URL","UserName":"Имя пользователя","TenantImpersonator":"Имитатор арендатора","UserImpersonator":"Имитатор пользователя","UrlFilter":"URL-фильтр","Exceptions":"Исключения","Comments":"Комментарии","HttpStatusCode":"Код состояния HTTP","HttpStatusCodeFilter":"Фильтр кода состояния HTTP","ServiceName":"Оказание услуг","MethodName":"Метод","CorrelationId":"Идентификатор корреляции","ApplicationName":"Имя приложения","ExecutionDuration":"Продолжительность","ExtraProperties":"Дополнительные свойства","MaxDuration":"Максимум. Продолжительность","MinDuration":"Мин. Продолжительность","MinMaxDuration":"Продолжительность (Мин.-Макс.)","{0}Milliseconds":"{0} миллисекунд","ExecutionTime":"Время","Parameters":"Параметры","EntityTypeFullName":"Тип объекта Полное имя","Entity":"Сущность","ChangeType":"Тип изменения","ChangeTime":"Время","NewValue":"Новое значение","OriginalValue":"Исходное значение","PropertyName":"Имя свойства","PropertyTypeFullName":"Тип недвижимости Полное имя","Yes":"да","No":"Нет","Changes":"Изменения","AverageExecutionDurationInLogsPerDay":"Средняя продолжительность выполнения","AverageExecutionDurationInMilliseconds":"Средняя продолжительность выполнения в миллисекундах","ErrorRateInLogs":"Частота ошибок в логах","Success":"Успех","Fault":"Вина","NoChanges":"Без изменений)","EntityChanges":"Изменения сущности","EntityId":"Идентификатор объекта","EntityChangeStartTime":"Минимальная дата изменения","EntityChangeEndTime":"Максимальная дата изменения","EntityHistory":"История объекта","DaysAgoTitle":"{0} {1}.","DaysAgoWithUserTitle":"{0} {1} от {2}.","MinutesAgo":"{0} минут назад","HoursAgo":"{0} часов назад","DaysAgo":"{0} дней назад","Created":"Созданный","Updated":"Обновлено","Deleted":"Удалено","ChangeHistory":"История изменений","FullChangeHistory":"Полная история изменений","ChangeDetails":"Изменить детали","DurationMs":"Продолжительность (мс)","StartDate":"Дата начала","EndDate":"Дата окончания","Feature:AuditLoggingGroup":"Ведение журнала аудита","Feature:AuditLoggingEnable":"Включенная страница ведения журнала аудита","Feature:AuditLoggingEnableDescription":"Включить страницу ведения журнала аудита в приложении.","Feature:AuditLoggingSettingManagementEnable":"Включить управление настройками журнала аудита","Feature:AuditLoggingSettingManagementEnableDescription":"Включить конфигурацию управления настройками журнала аудита в приложении.","InvalidAuditLogDeletionSettings":"Недопустимые настройки удаления журнала аудита. Если удаление включено, период должен быть больше 0 дней.","AuditLogSettingsGeneral":"Общие","AuditLogSettingsGlobal":"Глобальные","DisplayName:IsPeriodicDeleterEnabled":"Включить службу очистки в масштабе системы","Description:IsPeriodicDeleterEnabled":"Если эта опция отключена, периодический удалитель не будет работать. Журналы аудита не будут удаляться автоматически.","DisplayName:GlobalIsExpiredDeleterEnabled":"Включить службу очистки для всех арендаторов и хоста","Description:GlobalIsExpiredDeleterEnabled":"Если эта опция включена, все просроченные элементы арендаторов и хоста будут удаляться автоматически, если у них нет специальной настройки.","DisplayName:IsExpiredDeleterEnabled":"Включить службу очистки","Description:IsExpiredDeleterEnabled":"Если эта опция включена, просроченные элементы будут удаляться автоматически.","DisplayName:ExpiredDeleterPeriod":"Период удаления просроченных элементов","Description:ExpiredDeleterPeriod":"Установите количество дней, после которых просроченные элементы будут автоматически удалены.","ExpiredDeleterPeriodUnit":"день(дни)","AuditLogsBeforeXWillBeDeleted":"Журналы аудита до {0} будут удалены.","TenantId":"Идентификатор арендатора","Permission:Export":"Экспорт журналов аудита","ExportToExcel":"Экспорт в Excel","Exporting":"Экспорт","ExportCompleted":"Экспорт успешно завершен","ExportFailed":"Экспорт не удался","ThereWereNoRecordsToExport":"Не было записей для экспорта.","ExportJobQueued":"Задача экспорта поставлена в очередь для {0} записей. Вы получите электронное письмо, когда экспорт будет завершен.","ExportReady":"Экспорт завершен для {0} записей и готов к загрузке.","FileName":"Имя файла","TotalRecords":"Всего записей","ExcelFileAttachedMessage":"Файл Excel прикреплен к этому электронному письму.","EntityChangeExportCompletedSubject":"Экспорт изменений сущности завершен","AuditLogExportCompletedSubject":"Экспорт журнала аудита завершен","DownloadLinkExplanation":"Вы можете скачать файл по ссылке ниже до {0} (UTC)","DownloadNow":"Скачать сейчас","TryAgainMessage":"Пожалуйста, попробуйте еще раз. Если проблема не исчезнет, обратитесь в службу поддержки."}	2026-04-29 11:54:17.990116	\N
3a20e9c3-57b2-b144-034b-00e081426847	AbpAuditLogging	vi	{"Permission:AuditLogging":"Ghi nhật ký kiểm tra","Permission:AuditLogs":"Nhật ký kiểm tra","Menu:AuditLogging":"Nhật ký kiểm tra","AuditLogs":"Nhật ký kiểm tra","HttpStatus":"Trạng thái HTTP","HttpMethod":"Phương thức HTTP","HttpMethodFilter":"Bộ lọc phương thức HTTP","HttpRequest":"Yêu cầu HTTP","User":"Người dùng","UserNameFilter":"Bộ lọc người dùng","HasException":"Có ngoại lệ","IpAddress":"Địa chỉ IP","Time":"Thời gian","Date":"Ngày","Duration":"Khoảng thời gian","Detail":"Chi tiết","Overall":"Tổng thể","Actions":"hành động","ClientIpAddress":"Địa chỉ IP của khách hàng","ClientName":"Tên khách hàng","BrowserInfo":"Thông tin trình duyệt","Url":"URL","UserName":"Tên tài khoản","TenantImpersonator":"Người mạo danh người thuê nhà","UserImpersonator":"Người mạo danh người dùng","UrlFilter":"Bộ lọc URL","Exceptions":"Ngoại lệ","Comments":"Bình luận","HttpStatusCode":"Mã trạng thái HTTP","HttpStatusCodeFilter":"Bộ lọc mã trạng thái HTTP","ServiceName":"Dịch vụ","MethodName":"Phương pháp","CorrelationId":"Id tương quan","ApplicationName":"Tên ứng dụng","ExecutionDuration":"Khoảng thời gian","ExtraProperties":"Thuộc tính bổ sung","MaxDuration":"Tối đa. Khoảng thời gian","MinDuration":"Tối thiểu. Khoảng thời gian","MinMaxDuration":"Thời lượng (Tối thiểu - Tối đa)","{0}Milliseconds":"{0} mili giây","ExecutionTime":"Thời gian","Parameters":"Thông số","EntityTypeFullName":"Loại thực thể Tên đầy đủ","Entity":"Thực thể","ChangeType":"Đổi loại","ChangeTime":"Thời gian","NewValue":"Giá trị mới","OriginalValue":"Giá trị gốc","PropertyName":"Tên tài sản","PropertyTypeFullName":"Loại tài sản Tên đầy đủ","Yes":"Đúng","No":"KHÔNG","Changes":"Thay đổi","AverageExecutionDurationInLogsPerDay":"Thời gian thực hiện trung bình","AverageExecutionDurationInMilliseconds":"Thời gian thực hiện trung bình tính bằng mili giây","ErrorRateInLogs":"Tỷ lệ lỗi trong nhật ký","Success":"Thành công","Fault":"Lỗi","NoChanges":"Không thay đổi)","EntityChanges":"Thay đổi thực thể","EntityId":"ID phap nhân","EntityChangeStartTime":"Ngày thay đổi tối thiểu","EntityChangeEndTime":"Ngày thay đổi tối đa","EntityHistory":"Lịch sử thực thể","DaysAgoTitle":"{0} {1}.","DaysAgoWithUserTitle":"{0} {1} của {2}.","MinutesAgo":"{0} phút trước","HoursAgo":"{0} giờ trước","DaysAgo":"{0} ngày trước","Created":"Tạo","Updated":"Đã cập nhật","Deleted":"Đã xóa","ChangeHistory":"thay đổi lịch sử","FullChangeHistory":"Lịch sử thay đổi hoàn toàn","ChangeDetails":"Thay đổi chi tiết","DurationMs":"Thời lượng (ms)","StartDate":"Ngày bắt đầu","EndDate":"Ngày cuối","Feature:AuditLoggingGroup":"Ghi nhật ký kiểm tra","Feature:AuditLoggingEnable":"Đã bật trang ghi nhật ký kiểm tra","Feature:AuditLoggingEnableDescription":"Kích hoạt trang ghi nhật ký kiểm tra trong ứng dụng.","TenantId":"ID người thuê","DownloadLinkExplanation":"Bạn có thể tải xuống tệp bằng liên kết bên dưới cho đến {0} (UTC)","DownloadNow":"Tải xuống ngay","TryAgainMessage":"Vui lòng thử lại. Nếu sự cố vẫn tiếp diễn, vui lòng liên hệ bộ phận hỗ trợ."}	2026-04-29 11:54:17.990123	\N
3a20e9c3-57bb-8dc7-0d5c-ce53b2982b79	AbpAuditLogging	zh-Hans	{"Permission:AuditLogging":"审计日志","Permission:AuditLogs":"审计日志","Menu:AuditLogging":"审计日志","AuditLogs":"审计日志","HttpStatus":"HTTP 状态","HttpMethod":"HTTP 方法","HttpMethodFilter":"HTTP 方法过滤器","HttpRequest":"HTTP 请求","User":"用户","UserNameFilter":"用户过滤器","HasException":"有例外情况","IpAddress":"IP 地址","Time":"时间","Date":"日期","Duration":"持续时间","Detail":"详细信息","Overall":"总体","Actions":"操作","ClientIpAddress":"客户端 IP 地址","ClientName":"客户名称","BrowserInfo":"浏览器信息","Url":"网址","UserName":"用户名","TenantImpersonator":"租户模拟","UserImpersonator":"用户模拟","UrlFilter":"URL 过滤器","Exceptions":"异常","Comments":"评论","HttpStatusCode":"HTTP 状态代码","HttpStatusCodeFilter":"HTTP 状态代码过滤器","ServiceName":"服务","MethodName":"方法","CorrelationId":"相关标识","ApplicationName":"应用名称","ExecutionDuration":"持续时间","ExtraProperties":"额外属性","MaxDuration":"最大持续时间","MinDuration":"分钟持续时间","MinMaxDuration":"持续时间（最短 - 最长）","{0}Milliseconds":"{0}毫秒","ExecutionTime":"时间","Parameters":"参数","EntityTypeFullName":"实体类型 全名","Entity":"实体","ChangeType":"更改类型","ChangeTime":"时间","NewValue":"新值","OriginalValue":"原始值","PropertyName":"属性名称","PropertyTypeFullName":"属性类型全名","Yes":"是","No":"没有","Changes":"变化","AverageExecutionDurationInLogsPerDay":"平均执行时间","AverageExecutionDurationInMilliseconds":"平均执行时间（毫秒）","ErrorRateInLogs":"日志中的错误率","Success":"成功","Fault":"故障","NoChanges":"无变化","EntityChanges":"实体变化","EntityId":"实体 ID","EntityChangeStartTime":"最小更改日期","EntityChangeEndTime":"最大更改日期","EntityHistory":"实体历史","DaysAgoTitle":"{0} {1}.","DaysAgoWithUserTitle":"{0} {1} by {2}。","MinutesAgo":"{0}分钟前","HoursAgo":"{0}小时前","DaysAgo":"{0}天前","Created":"创建","Updated":"已更新","Deleted":"已删除","ChangeHistory":"变更记录","FullChangeHistory":"全部变更记录","ChangeDetails":"变更详情","DurationMs":"持续时间（毫秒）","StartDate":"开始日期","EndDate":"结束日期","Feature:AuditLoggingGroup":"审计日志","Feature:AuditLoggingEnable":"启用审计日志页面","Feature:AuditLoggingEnableDescription":"在应用程序中启用审计日志页面。","Feature:AuditLoggingSettingManagementEnable":"启用审计日志设置管理","Feature:AuditLoggingSettingManagementEnableDescription":"在应用程序中启用审计日志设置管理的配置。","InvalidAuditLogDeletionSettings":"无效的审计日志删除设置。如果启用删除，期间应大于0天。","AuditLogSettingsGeneral":"常规","AuditLogSettingsGlobal":"全局","DisplayName:IsPeriodicDeleterEnabled":"启用系统范围的清理服务","Description:IsPeriodicDeleterEnabled":"如果禁用此选项，定期删除器将不工作。审计日志不会自动删除。","DisplayName:GlobalIsExpiredDeleterEnabled":"为所有租户和主机启用清理服务","Description:GlobalIsExpiredDeleterEnabled":"如果启用此选项，所有租户和主机的过期项目将自动删除，除非有特定设置。","DisplayName:IsExpiredDeleterEnabled":"启用清理服务","Description:IsExpiredDeleterEnabled":"如果启用此选项，过期项目将自动删除。","DisplayName:ExpiredDeleterPeriod":"过期项目删除期间","Description:ExpiredDeleterPeriod":"设置过期项目自动删除的天数。","ExpiredDeleterPeriodUnit":"天","AuditLogsBeforeXWillBeDeleted":"{0}之前的审计日志将被删除。","TenantId":"租户ID","Permission:Export":"导出审计日志","ExportToExcel":"导出到Excel","Exporting":"导出中","ExportCompleted":"导出成功完成","ExportFailed":"导出失败","ThereWereNoRecordsToExport":"没有可导出的记录。","ExportJobQueued":"导出作业已为{0}条记录排队。导出完成后您将收到电子邮件。","ExportReady":"{0}条记录的导出已完成，可以下载。","FileName":"文件名","TotalRecords":"总记录数","ExcelFileAttachedMessage":"Excel文件已附加到此电子邮件。","EntityChangeExportCompletedSubject":"实体变更导出已完成","AuditLogExportCompletedSubject":"审计日志导出已完成","DownloadLinkExplanation":"您可以使用下面的链接下载文件，直到{0}（UTC）","DownloadNow":"立即下载","TryAgainMessage":"请再试一次。如果问题仍然存在，请联系支持人员。"}	2026-04-29 11:54:17.99013	\N
3a20e9c3-57d1-a77e-84a1-58f5c1e158c7	AbpAuthorization	ru	{"Volo.Authorization:010001":"Авторизация не удалась! Данная политика не предоставлена.","Volo.Authorization:010002":"Авторизация не удалась! Данная политика не предоставила: {PolicyName}","Volo.Authorization:010003":"Авторизация не удалась! Данная политика не предоставлена для данного ресурса: {ResourceName}","Volo.Authorization:010004":"Авторизация не удалась! Данное требование не выполнено для данного ресурса: {ResourceName}","Volo.Authorization:010005":"Авторизация не удалась! Указанные требования не удовлетворены для данного ресурса: {ResourceName}"}	2026-04-29 11:54:17.990136	\N
3a20e9c3-57e8-aba7-0624-6f43833fe549	AbpPermissionManagement	ru	{"Permissions":"Разрешения","OnlyProviderPermissons":"Только этот провайдер","All":"Все","SelectAllInAllTabs":"Предоставить все разрешения","SelectAllInThisTab":"Выбрать все","SaveWithoutAnyPermissionsWarningMessage":"Вы уверены, что хотите сохранить без каких-либо разрешений?","PermissionGroup":"Группа разрешений","Filter":"Фильтр","ResourcePermissions":"Разрешения","ResourcePermissionTarget":"Цель","ResourcePermissionPermissions":"Разрешения","AddResourcePermission":"Добавить разрешение","ResourcePermissionDeletionConfirmationMessage":"Вы уверены, что хотите удалить все разрешения?","UpdateResourcePermission":"Обновить разрешение","GrantAllResourcePermissions":"Предоставить все","NoResourceProviderKeyLookupServiceFound":"Служба поиска ключа поставщика не найдена","NoResourcePermissionFound":"Не определено ни одного разрешения.","UpdatePermission":"Обновить разрешение","NoPermissionsAssigned":"Нет назначенных разрешений","SelectProvider":"Выбрать провайдера","SearchProviderKey":"Поиск ключа провайдера","Provider":"Провайдер","ErrorLoadingPermissions":"Ошибка при загрузке разрешений","PleaseSelectProviderAndPermissions":"Пожалуйста, выберите провайдера и разрешения"}	2026-04-29 11:54:17.99016	\N
3a20e9c3-57ea-8178-6d9f-a3fd1fe4df30	AbpPermissionManagement	vi	{"Permissions":"Quyền","OnlyProviderPermissons":"Chỉ nhà cung cấp này","All":"Tất cả","SelectAllInAllTabs":"Cấp tất cả các quyền","SelectAllInThisTab":"Chọn tất cả","SaveWithoutAnyPermissionsWarningMessage":"Bạn có chắc chắn muốn lưu mà không có bất kỳ quyền nào không?","PermissionGroup":"Nhóm quyền","Filter":"Lọc","ResourcePermissions":"Quyền","ResourcePermissionTarget":"Mục tiêu","ResourcePermissionPermissions":"Quyền","AddResourcePermission":"Thêm quyền","ResourcePermissionDeletionConfirmationMessage":"Bạn có chắc chắn muốn xóa tất cả quyền không?","UpdateResourcePermission":"Cập nhật quyền","GrantAllResourcePermissions":"Cấp tất cả","NoResourceProviderKeyLookupServiceFound":"Không tìm thấy dịch vụ tra cứu khóa nhà cung cấp","NoResourcePermissionFound":"Không có quyền nào được định nghĩa.","UpdatePermission":"Cập nhật quyền","NoPermissionsAssigned":"Chưa được cấp quyền","SelectProvider":"Chọn nhà cung cấp","SearchProviderKey":"Tìm kiếm khóa nhà cung cấp","Provider":"Nhà cung cấp","ErrorLoadingPermissions":"Lỗi khi tải quyền","PleaseSelectProviderAndPermissions":"Vui lòng chọn nhà cung cấp và quyền"}	2026-04-29 11:54:17.990167	\N
3a20e9c3-57f3-a83e-67d6-301a7888aeac	AbpPermissionManagement	zh-Hans	{"Permissions":"权限","OnlyProviderPermissons":"只有这个提供者","All":"所有","SelectAllInAllTabs":"授予所有权限","SelectAllInThisTab":"全选","SaveWithoutAnyPermissionsWarningMessage":"您确定要在没有任何权限的情况下保存吗？","PermissionGroup":"权限组","Filter":"过滤","ResourcePermissions":"权限","ResourcePermissionTarget":"目标","ResourcePermissionPermissions":"权限","AddResourcePermission":"添加权限","ResourcePermissionDeletionConfirmationMessage":"您确定要删除所有权限吗？","UpdateResourcePermission":"更新权限","GrantAllResourcePermissions":"授予所有","NoResourceProviderKeyLookupServiceFound":"未找到提供者键查找服务","NoResourcePermissionFound":"未定义任何权限。","UpdatePermission":"更新权限","NoPermissionsAssigned":"未分配权限","SelectProvider":"选择提供程序","SearchProviderKey":"搜索提供程序密钥","Provider":"提供程序","ErrorLoadingPermissions":"加载权限时出错","PleaseSelectProviderAndPermissions":"请选择提供程序和权限"}	2026-04-29 11:54:17.990173	\N
3a20e9c3-57ff-7d0d-b72f-aee78767bd62	AbpFeatureManagement	ru	{"Features":"Функциональные возможности","NoFeatureFoundMessage":"Нет доступных функциональных возможностей.","ManageHostFeatures":"Управление функциями хоста","ManageHostFeaturesText":"Вы можете управлять функциями хоста, нажав следующую кнопку.","Permission:FeatureManagement":"Управление функциями","Permission:FeatureManagement.ManageHostFeatures":"Управление функциями хоста","Volo.Abp.FeatureManagement:InvalidFeatureValue":"Недопустимое значение функции {0}!","Menu:FeatureManagement":"Управление функциями","ResetToDefault":"Восстановление значений по умолчанию","ResetedToDefault":"Сброс по умолчанию","AreYouSure":"Вы уверены?","AreYouSureToResetToDefault":"Вы уверены, что сбросите настройки по умолчанию?"}	2026-04-29 11:54:17.990183	\N
3a20e9c3-5809-017b-3322-3b4f5372fe31	AbpFeatureManagement	vi	{"Features":"Tính năng","NoFeatureFoundMessage":"Không có bất kỳ tính năng khả dụng nào.","ManageHostFeatures":"Quản lý các tính năng của Máy chủ lưu trữ","ManageHostFeaturesText":"Bạn có thể quản lý các tính năng phía máy chủ bằng cách nhấp vào nút sau.","Permission:FeatureManagement":"Quản lý tính năng","Permission:FeatureManagement.ManageHostFeatures":"Quản lý các tính năng của Máy chủ lưu trữ","Volo.Abp.FeatureManagement:InvalidFeatureValue":"Giá trị tính năng {0} không hợp lệ!","Menu:FeatureManagement":"Quản lý tính năng","ResetToDefault":"Đặt lại về mặc định","ResetedToDefault":"Đã đặt lại về mặc định","AreYouSure":"Bạn có chắc không?","AreYouSureToResetToDefault":"Bạn có chắc chắn đặt lại về mặc định không?"}	2026-04-29 11:54:17.990191	\N
3a20e9c3-5810-b756-a0a1-c32c515750b3	AbpFeatureManagement	zh-Hans	{"Features":"功能","NoFeatureFoundMessage":"没有任何可用的功能。","ManageHostFeatures":"管理主机功能","ManageHostFeaturesText":"点击以下按钮即可管理主机端功能。","Permission:FeatureManagement":"功能管理","Permission:FeatureManagement.ManageHostFeatures":"管理主机功能","Volo.Abp.FeatureManagement:InvalidFeatureValue":"{0}功能值无效！","Menu:FeatureManagement":"功能管理","ResetToDefault":"重置为默认值","ResetedToDefault":"已重置为默认值","AreYouSure":"你确定吗？","AreYouSureToResetToDefault":"您确定要重置为默认设置吗？"}	2026-04-29 11:54:17.990198	\N
3a20e9c3-6b77-22b6-7477-37fa4031b540	AbpLdap	zh-Hans	{"DisplayName:Abp.Ldap.Ldaps":"基于SSL的LDAP","Description:Abp.Ldap.Ldaps":"基于SSL的LDAP","DisplayName:Abp.Ldap.ServerHost":"服务器主机","Description:Abp.Ldap.ServerHost":"服务器主机","DisplayName:Abp.Ldap.ServerPort":"服务器端口","Description:Abp.Ldap.ServerPort":"服务器端口","DisplayName:Abp.Ldap.BaseDc":"基域组件","Description:Abp.Ldap.BaseDc":"基域组件","DisplayName:Abp.Ldap.Domain":"域","Description:Abp.Ldap.Domain":"域","DisplayName:Abp.Ldap.UserName":"用户名","Description:Abp.Ldap.UserName":"用户名","DisplayName:Abp.Ldap.Password":"密码","Description:Abp.Ldap.Password":"密码"}	2026-04-29 11:54:22.858943	\N
3a20e9c3-582b-b11f-ebfe-0a7699b5c413	AbpUi	ru	{"Languages":"Языки","AreYouSure":"Вы уверены?","Cancel":"Отмена","Clear":"Прозрачный","Yes":"Да","No":"Нет","Ok":"ОК","Close":"Закрыть","Save":"Сохранить","SavingWithThreeDot":"Сохранение...","Actions":"Действия","Delete":"Удалить","CreatedSuccessfully":"Успешно создано","SavedSuccessfully":"Успешно сохранено","DeletedSuccessfully":"Успешно удалено","Edit":"Редактировать","Refresh":"Обновить","Language":"Язык","LoadMore":"Загрузить еще","ProcessingWithThreeDot":"Обработка...","LoadingWithThreeDot":"Загрузка...","Welcome":"Добро пожаловать","Login":"Войти","Register":"Зарегистрироваться","Logout":"Выйти","Submit":"Подтвердить","Back":"Назад","PagerSearch":"Поиск","PagerNext":"Следующее","PagerPrevious":"Предыдущее","PagerFirst":"Первая","PagerLast":"Последняя","PagerInfo":"Записи с _START_ до _END_ из _TOTAL_ записей","PagerInfo{0}{1}{2}":"Показано записей от {0} до {1} из {2}","PagerInfoEmpty":"Записи с 0 до 0 из 0 записей.","PagerInfoFiltered":"(отфильтровано из _MAX_ записей)","NoDataAvailableInDatatable":"Данные в таблице отсутствуют","ErrorLoadingDatatable":"Произошла ошибка во время запроса. Проверьте сообщение для получения деталей.","Total":"общий","Selected":"выбранный","PagerShowMenuEntries":"Показать _MENU_ записей","DatatableActionDropdownDefaultText":"Действия","ChangePassword":"Изменить пароль","PersonalInfo":"Мой профиль","AreYouSureYouWantToCancelEditingWarningMessage":"У вас есть несохраненные изменения.","GoHomePage":"Вернуться на главную страницу","GoBack":"Вернуться назад","Search":"поиск","ItemWillBeDeletedMessageWithFormat":"{0} будет удален!","ItemWillBeDeletedMessage":"Этот предмет будет удален!","ManageYourAccount":"Настройте свой аккаунт","OthersGroup":"Другой","Today":"Сегодня","Apply":"Применять","InternetConnectionInfo":"Операцию выполнить не удалось. Ваше интернет-соединение в данный момент недоступно.","CopiedToTheClipboard":"Скопировано в буфер обмена","AddNew":"Добавить новое","ProfilePicture":"Изображение профиля","Theme":"Тема","NotAssigned":"Не назначен","EntityActionsDisabledTooltip":"У вас нет прав на выполнение каких-либо действий.","ResourcePermissions":"Разрешения"}	2026-04-29 11:54:17.990204	\N
3a20e9c3-5832-4b28-ffb7-bffb2583f819	AbpUi	vi	{"Languages":"Ngôn ngữ","AreYouSure":"Bạn có chắc không ?","Cancel":"Hủy bỏ","Clear":"Sạch","Yes":"Đồng ý","No":"Không","Ok":"Vâng","Close":"Đóng","Save":"Lưu","SavingWithThreeDot":"Đang lưu...","Actions":"Hành động","Delete":"Xóa","CreatedSuccessfully":"Đã tạo thành công","SavedSuccessfully":"Đã lưu thành công","DeletedSuccessfully":"Đã xóa thành công","Edit":"Sửa","Refresh":"Làm mới","Language":"Ngôn ngữ","LoadMore":"Tải thêm","ProcessingWithThreeDot":"Đang xử lý...","LoadingWithThreeDot":"Đang tải...","Welcome":"Chào mừng bạn","Login":"Đăng nhập","Register":"Đăng ký","Logout":"Đăng xuất","Submit":"Gửi","Back":"Quay lại","PagerSearch":"Tìm kiếm","PagerNext":"Trang kế","PagerPrevious":"Trang trước","PagerFirst":"Trang đầu","PagerLast":"Trang cuối","PagerInfo":"Hiển thị từ _START_ đến _END_ trong _TOTAL_ mục","PagerInfo{0}{1}{2}":"Hiển thị từ {0} đến {1} trong {2} mục","PagerInfoEmpty":"Hiển thị từ 0 đến 0 trong 0 mục","PagerInfoFiltered":"(được lọc từ tổng số _MAX_ mục)","NoDataAvailableInDatatable":"Không có dữ liệu trong bảng","ErrorLoadingDatatable":"Đã xảy ra lỗi trong quá trình yêu cầu. Kiểm tra tin nhắn để biết chi tiết.","Total":"toàn bộ","Selected":"đã chọn","PagerShowMenuEntries":"Hiển thị _MENU_ mục","DatatableActionDropdownDefaultText":"Hành động","ChangePassword":"Đổi mật khẩu","PersonalInfo":"Hồ sơ của tôi","AreYouSureYouWantToCancelEditingWarningMessage":"Bạn có những thay đổi chưa được lưu","GoHomePage":"Đi đến trang chủ","GoBack":"Quay lại","Search":"Tìm kiếm","ItemWillBeDeletedMessageWithFormat":"{0} sẽ bị xóa!","ItemWillBeDeletedMessage":"Vật phẩm này sẽ bị xoá!","ManageYourAccount":"Quản lý tài khoản của bạn","OthersGroup":"Khác","Today":"Hôm nay","Apply":"Áp dụng","InternetConnectionInfo":"Các hoạt động không thể được thực hiện. Kết nối internet của bạn hiện không khả dụng.","CopiedToTheClipboard":"Đã sao chép vào bảng nhớ tạm","AddNew":"Thêm mới","ProfilePicture":"Ảnh đại diện","Theme":"chủ đề","NotAssigned":"Không được chỉ định","EntityActionsDisabledTooltip":"Bạn không có quyền thực hiện bất kỳ hành động nào.","ResourcePermissions":"Quyền"}	2026-04-29 11:54:17.990213	\N
3a20e9c3-5834-7d6d-63e5-170a21334a11	AbpUi	zh-Hans	{"Languages":"语言","AreYouSure":"你确定吗?","Cancel":"取消","Clear":"清除","Yes":"是","No":"否","Ok":"好","Close":"关闭","Save":"保存","SavingWithThreeDot":"保存中...","Actions":"操作","Delete":"删除","CreatedSuccessfully":"创建成功","SavedSuccessfully":"保存成功","DeletedSuccessfully":"删除成功","Edit":"编辑","Refresh":"刷新","Language":"语言","LoadMore":"加载更多","ProcessingWithThreeDot":"处理中...","LoadingWithThreeDot":"加载中...","Welcome":"欢迎","Login":"登录","Register":"注册","Logout":"注销","Submit":"提交","Back":"返回","PagerSearch":"搜索","PagerNext":"下一页","PagerPrevious":"上一页","PagerFirst":"首页","PagerLast":"尾页","PagerInfo":"显示 _TOTAL_ 个条目中的 _START_ 到 _END_ 个.","PagerInfo{0}{1}{2}":"显示 {2} 个条目中的 {0} 到 {1} 个.","PagerInfoEmpty":"显示0个条目中的0到0","PagerInfoFiltered":"(从 _MAX_ 总条目中过滤掉)","NoDataAvailableInDatatable":"表中没有数据","ErrorLoadingDatatable":" 请求过程中发生错误。检查消息以获取详细信息","Total":"总计","Selected":"已选","PagerShowMenuEntries":"显示 _MENU_ 条数据","DatatableActionDropdownDefaultText":"操作","ChangePassword":"修改密码","PersonalInfo":"个人信息","AreYouSureYouWantToCancelEditingWarningMessage":"你有未保存的更改.","GoHomePage":"返回主页","GoBack":"返回","Search":"搜索","ItemWillBeDeletedMessageWithFormat":"{0} 将被删除!","ItemWillBeDeletedMessage":"此项将被删除!","ManageYourAccount":"管理你的账户","OthersGroup":"其他","Today":"今天","Apply":"应用","InternetConnectionInfo":"无法执行该操作。您的互联网连接目前不可用。","CopiedToTheClipboard":"已复制到剪贴板","AddNew":"添新","ProfilePicture":"个人资料图片","Theme":"主题","NotAssigned":"未分配","EntityActionsDisabledTooltip":"您没有权限执行任何操作。","ResourcePermissions":"权限"}	2026-04-29 11:54:17.99022	\N
3a20e9c3-5841-3ae8-7503-f94b84a43cce	AbpDddApplicationContracts	ru	{"MaxResultCountExceededExceptionMessage":"{0} не может быть больше {1}! Увеличьте {2}.{3} на стороне сервера, чтобы получить больше результатов."}	2026-04-29 11:54:17.990227	\N
3a20e9c3-584b-7a6b-1fea-500770ee4b81	AbpDddApplicationContracts	vi	{"MaxResultCountExceededExceptionMessage":"{0} không được nhiều hơn {1}! Tăng {2}. {3} ở phía máy chủ để cho phép nhiều kết quả hơn."}	2026-04-29 11:54:17.990234	\N
3a20e9c3-584d-915b-19d5-f6fb57104d3e	AbpDddApplicationContracts	zh-Hans	{"MaxResultCountExceededExceptionMessage":"{0}不能多于{1}！在服务器端增加{2}.{3}，以获得更多结果。"}	2026-04-29 11:54:17.990241	\N
3a20e9c3-5852-197f-38e5-fca2f6600c91	LanguageService	ru	{"Welcome":"Добро пожаловать","LongWelcomeMessage":"Добро пожаловать в приложение. Это стартап-проект, основанный на структуре ABP. Для получения дополнительной информации посетите сайт","Menu:Home":"Дома","Login":"Авторизоваться","Menu:Dashboard":"Панель приборов"}	2026-04-29 11:54:17.990269	\N
3a20e9c3-5858-58ce-2bc0-43f1eaedc027	LanguageService	en	{"Menu:Home":"Home","Login":"Login","Menu:Dashboard":"Dashboard","LongWelcomeMessage":"Welcome to the application. This is a startup project based on the ABP framework. For more information visit","Welcome":"Welcome"}	2026-04-29 11:54:17.990277	\N
3a20e9c3-585b-03ce-657d-9095420aad1e	LanguageService	zh-Hans	{"Welcome":"欢迎","LongWelcomeMessage":"欢迎使用本应用程序。这是一个基于 ABP 框架的启动项目。欲了解更多信息，请访问","Menu:Home":"家","Login":"登录","Menu:Dashboard":"仪表盘"}	2026-04-29 11:54:17.990284	\N
3a20e9c3-585d-6fe3-3eb9-13a223aeb942	LanguageService	ko	{"Menu:Home":"Home","Login":"Login","Menu:Dashboard":"Dashboard","LongWelcomeMessage":"Welcome to the application. This is a startup project based on the ABP framework. For more information visit","Welcome":"Welcome"}	2026-04-29 11:54:17.990291	\N
3a20e9c3-5860-73fa-272a-85805108ee56	LanguageService	vi	{"Menu:Home":"Home","Login":"Login","Menu:Dashboard":"Dashboard","LongWelcomeMessage":"Welcome to the application. This is a startup project based on the ABP framework. For more information visit","Welcome":"Welcome"}	2026-04-29 11:54:17.990298	\N
3a20e9c3-5867-ab02-a23a-f0afb2cfbb5a	AbpGlobalFeature	ru	{"Volo.GlobalFeature:010001":"Службе \\u0022{ServiceName}\\u0022 необходимо включить функцию \\u0022{GlobalFeatureName}\\u0022."}	2026-04-29 11:54:17.990318	\N
3a20e9c3-586a-65d4-766a-168bde495b68	AbpGlobalFeature	vi	{"Volo.GlobalFeature:010001":"Dịch vụ \\u0027{ServiceName}\\u0027 cần bật tính năng \\u0027{GlobalFeatureName}\\u0027."}	2026-04-29 11:54:17.990325	\N
3a20e9c3-5871-e0dd-ab34-1afad162a1ff	AbpGlobalFeature	zh-Hans	{"Volo.GlobalFeature:010001":"\\u0027{ServiceName}\\u0027 服务需要启用 \\u0027{GlobalFeatureName}\\u0027。"}	2026-04-29 11:54:17.990332	\N
3a20e9c3-6463-1413-0110-2e1bc8891273	AbpEmailing	en	{"DisplayName:Abp.Mailing.DefaultFromAddress":"Default from address","DisplayName:Abp.Mailing.DefaultFromDisplayName":"Default from display name","DisplayName:Abp.Mailing.Smtp.Host":"Host","DisplayName:Abp.Mailing.Smtp.Port":"Port","DisplayName:Abp.Mailing.Smtp.UserName":"User name","DisplayName:Abp.Mailing.Smtp.Password":"Password","DisplayName:Abp.Mailing.Smtp.Domain":"Domain","DisplayName:Abp.Mailing.Smtp.EnableSsl":"Enable SSL","DisplayName:Abp.Mailing.Smtp.UseDefaultCredentials":"Use default credentials","Description:Abp.Mailing.DefaultFromAddress":"The default from address","Description:Abp.Mailing.DefaultFromDisplayName":"The default from display name","Description:Abp.Mailing.Smtp.Host":"The name or IP address of the host used for SMTP transactions.","Description:Abp.Mailing.Smtp.Port":"The port used for SMTP transactions.","Description:Abp.Mailing.Smtp.UserName":"User name associated with the credentials.","Description:Abp.Mailing.Smtp.Password":"The password for the user name associated with the credentials.","Description:Abp.Mailing.Smtp.Domain":"The domain or computer name that verifies the credentials.","Description:Abp.Mailing.Smtp.EnableSsl":"Whether the SmtpClient uses Secure Sockets Layer (SSL) to encrypt the connection.","Description:Abp.Mailing.Smtp.UseDefaultCredentials":"Whether the DefaultCredentials are sent with requests.","TextTemplate:StandardEmailTemplates.Layout":"Default email layout template","TextTemplate:StandardEmailTemplates.Message":"Simple message template for emails"}	2026-04-29 11:54:21.085526	\N
3a20e9c3-646d-1e2a-7708-7d20b2bcce3e	AbpEmailing	ru	{"DisplayName:Abp.Mailing.DefaultFromAddress":"Адрес отправления по умолчанию","DisplayName:Abp.Mailing.DefaultFromDisplayName":"Имя отправителя по умолчанию","DisplayName:Abp.Mailing.Smtp.Host":"Сервер","DisplayName:Abp.Mailing.Smtp.Port":"Порт","DisplayName:Abp.Mailing.Smtp.UserName":"Имя пользователя","DisplayName:Abp.Mailing.Smtp.Password":"Пароль","DisplayName:Abp.Mailing.Smtp.Domain":"Домен","DisplayName:Abp.Mailing.Smtp.EnableSsl":"Включить SSL","DisplayName:Abp.Mailing.Smtp.UseDefaultCredentials":"Использовать учетные данные по умолчанию","Description:Abp.Mailing.DefaultFromAddress":"Отправление по умолчанию от адреса.","Description:Abp.Mailing.DefaultFromDisplayName":"Имя отправителя по умолчанию.","Description:Abp.Mailing.Smtp.Host":"Имя или IP- адрес сервера, используемого для отправки по протоколу SMTP.","Description:Abp.Mailing.Smtp.Port":"Порт, используемый для отправки по протоколу SMTP.","Description:Abp.Mailing.Smtp.UserName":"Имя пользователя для SMTP-сервера.","Description:Abp.Mailing.Smtp.Password":"Пароль для SMTP-сервера.","Description:Abp.Mailing.Smtp.Domain":"Домен или имя компьютера, от которого производится SMTP-запрос.","Description:Abp.Mailing.Smtp.EnableSsl":"Использовать SSL для подключения к SMTP-серверу.","Description:Abp.Mailing.Smtp.UseDefaultCredentials":"Использовать учетные данные для SMTP по умолчанию.","TextTemplate:StandardEmailTemplates.Layout":"Шаблон макета электронной почты по умолчанию","TextTemplate:StandardEmailTemplates.Message":"Простой шаблон сообщения для писем"}	2026-04-29 11:54:21.085864	\N
3a20e9c3-6478-aa46-b46f-31d71255daa4	AbpEmailing	vi	{"DisplayName:Abp.Mailing.DefaultFromAddress":"Mặc định từ địa chỉ","DisplayName:Abp.Mailing.DefaultFromDisplayName":"Mặc định từ tên hiển thị","DisplayName:Abp.Mailing.Smtp.Host":"Tổ chức","DisplayName:Abp.Mailing.Smtp.Port":"Hải cảng","DisplayName:Abp.Mailing.Smtp.UserName":"Tên tài khoản","DisplayName:Abp.Mailing.Smtp.Password":"Mật khẩu","DisplayName:Abp.Mailing.Smtp.Domain":"Lãnh địa","DisplayName:Abp.Mailing.Smtp.EnableSsl":"Bật SSL","DisplayName:Abp.Mailing.Smtp.UseDefaultCredentials":"Sử dụng thông tin đăng nhập mặc định","Description:Abp.Mailing.DefaultFromAddress":"Địa chỉ từ mặc định","Description:Abp.Mailing.DefaultFromDisplayName":"Mặc định từ tên hiển thị","Description:Abp.Mailing.Smtp.Host":"Tên hoặc địa chỉ IP của máy chủ được sử dụng cho các giao dịch SMTP.","Description:Abp.Mailing.Smtp.Port":"Cổng được sử dụng cho các giao dịch SMTP.","Description:Abp.Mailing.Smtp.UserName":"Tên người dùng được liên kết với thông tin xác thực.","Description:Abp.Mailing.Smtp.Password":"Mật khẩu cho tên người dùng được liên kết với thông tin đăng nhập.","Description:Abp.Mailing.Smtp.Domain":"Miền hoặc tên máy tính xác minh thông tin đăng nhập.","Description:Abp.Mailing.Smtp.EnableSsl":"SmtpClient có sử dụng Lớp cổng bảo mật (SSL) để mã hóa kết nối hay không.","Description:Abp.Mailing.Smtp.UseDefaultCredentials":"Thông tin đăng nhập mặc định có được gửi cùng với các yêu cầu hay không.","TextTemplate:StandardEmailTemplates.Layout":"Mẫu bố cục email mặc định","TextTemplate:StandardEmailTemplates.Message":"Mẫu tin nhắn đơn giản cho email"}	2026-04-29 11:54:21.086175	\N
3a20e9c3-6483-edd0-52b8-419b929dda39	AbpEmailing	zh-Hans	{"DisplayName:Abp.Mailing.DefaultFromAddress":"默认发件人地址","DisplayName:Abp.Mailing.DefaultFromDisplayName":"默认发件人名字","DisplayName:Abp.Mailing.Smtp.Host":"主机","DisplayName:Abp.Mailing.Smtp.Port":"端口","DisplayName:Abp.Mailing.Smtp.UserName":"用户名","DisplayName:Abp.Mailing.Smtp.Password":"密码","DisplayName:Abp.Mailing.Smtp.Domain":"域","DisplayName:Abp.Mailing.Smtp.EnableSsl":"启用SSL","DisplayName:Abp.Mailing.Smtp.UseDefaultCredentials":"使用默认凭据","Description:Abp.Mailing.DefaultFromAddress":"默认的发件人地址.","Description:Abp.Mailing.DefaultFromDisplayName":"默认的发件人名字.","Description:Abp.Mailing.Smtp.Host":"SMTP 事务的主机名或主机 IP 地址.","Description:Abp.Mailing.Smtp.Port":"SMTP 事务的端口.","Description:Abp.Mailing.Smtp.UserName":"凭据关联的用户名.","Description:Abp.Mailing.Smtp.Password":"凭据关联的用户名的密码.","Description:Abp.Mailing.Smtp.Domain":"验证凭据的域名或计算机名.","Description:Abp.Mailing.Smtp.EnableSsl":"指定 SmtpClient 是否使用安全套接字层 (SSL) 加密连接.","Description:Abp.Mailing.Smtp.UseDefaultCredentials":"控制默认凭据是否随请求一起发送.","TextTemplate:StandardEmailTemplates.Layout":"默认电子邮件模板","TextTemplate:StandardEmailTemplates.Message":"电子邮件的简单消息模板"}	2026-04-29 11:54:21.086347	\N
3a20e9c3-6a4e-b237-e6e0-6c88a4e5e247	TextTemplateManagement	en	{"Menu:TextTemplates":"Text templates","Menu:TextTemplates:TemplateDefinitions":"Template definitions","Permission:TextTemplateManagement":"Text template management","Permission:TextTemplates":"Text templates","Permission:EditContents":"Edit contents","TemplateContents":"Template contents","Name":"Name","Layout":"Layout","IsLayout":"Is Layout","LocalizationResource":"Localization resource","DefaultCultureName":"Default culture name","Contents":"Contents","SeeContents":"See contents","EditContents":"Edit contents","BaseCultureName":"Reference culture name","TargetCultureName":"Target culture name","BaseContent":"Reference content","TargetContent":"Target content","SaveContent":"Save content","RestoreToDefault":"Restore to default","RestoreToDefaultMessage":"Are you sure? This action will convert the content to the default value.","IsInlineLocalized":"Inline localized","Success":"Success","TemplateContentUpdated":"Template content updated successfully.","TemplateContentRestoredToDefault":"Template content restored to the default value.","TemplateContent":"Template content","InlineContentDescription":"This template uses inline localization. You can use the \\u003Cb\\u003EL\\u003C/b\\u003E function to localize a text, like \\u003Cb\\u003E{{L \\u0022Hello\\u0022}}\\u003C/b\\u003E. If you still want to define a completely new template for a culture, use the \\u0022Customize per culture\\u0022 button. \\u003Cbr /\\u003E To get more information about syntax and other details, please check the \\u003Ca href=\\u0022https://docs.abp.io/en/abp/latest/Text-Templating\\u0022 target=\\u0022_blank\\u0022\\u003EText Templates documentation\\u003C/a\\u003E.","CustomizePerCulture":"Customize per culture","DisplayName":"Display name","Search":"Search","ReturnToTemplates":"Return to templates","Feature:TextManagementGroup":"Text template management","Feature:TextManagementEnable":"Enable text template management","Feature:TextManagementEnableDescription":"Enable text management system in the application."}	2026-04-29 11:54:22.857336	\N
3a20e9c3-a2db-d1f3-797e-4e509b8c0a0f	AbpOperationRateLimiting	en	{"Volo.Abp.OperationRateLimiting:010001":"Operation rate limit exceeded. You can try again after {RetryAfter}.","RetryAfter:Years":"{0} year(s)","RetryAfter:YearsAndMonths":"{0} year(s) and {1} month(s)","RetryAfter:Months":"{0} month(s)","RetryAfter:MonthsAndDays":"{0} month(s) and {1} day(s)","RetryAfter:Days":"{0} day(s)","RetryAfter:DaysAndHours":"{0} day(s) and {1} hour(s)","RetryAfter:Hours":"{0} hour(s)","RetryAfter:HoursAndMinutes":"{0} hour(s) and {1} minute(s)","RetryAfter:Minutes":"{0} minute(s)","RetryAfter:MinutesAndSeconds":"{0} minute(s) and {1} second(s)","RetryAfter:Seconds":"{0} second(s)","Volo.Abp.OperationRateLimiting:010002":"Operation rate limit exceeded. This request is permanently denied."}	2026-04-29 11:54:37.050242	\N
3a20e9c3-6a53-9a15-1461-f19e4625f0d8	TextTemplateManagement	ru	{"Menu:TextTemplates":"Текстовые шаблоны","Menu:TextTemplates:TemplateDefinitions":"Определения шаблонов","Permission:TextTemplateManagement":"Управление текстовыми шаблонами","Permission:TextTemplates":"Текстовые шаблоны","Permission:EditContents":"Редактировать содержимое","TemplateContents":"Содержимое шаблона","Name":"Имя","Layout":"Макет","IsLayout":"Макет","LocalizationResource":"Ресурс локализации","DefaultCultureName":"Имя культуры по умолчанию","Contents":"Содержание","SeeContents":"См. Содержание","EditContents":"Редактировать содержимое","BaseCultureName":"Имя эталонной культуры","TargetCultureName":"Название целевой культуры","BaseContent":"Справочное содержимое","TargetContent":"Целевой контент","SaveContent":"Сохранить содержимое","RestoreToDefault":"Восстановить по умолчанию","RestoreToDefaultMessage":"Вы уверены? Это действие преобразует содержимое в значение по умолчанию.","IsInlineLocalized":"Встроенный Локализованный","Success":"Успех","TemplateContentUpdated":"Содержимое шаблона успешно обновлено.","TemplateContentRestoredToDefault":"Содержимое шаблона восстановлено до значения по умолчанию.","TemplateContent":"Содержимое шаблона","InlineContentDescription":"В этом шаблоне используется встроенная локализация. Вы можете использовать функцию \\u003Cb\\u003EL\\u003C/b\\u003E для локализации текста, например \\u003Cb\\u003E{{L \\u0022Hello\\u0022}}\\u003C/b\\u003E. Если вы все еще хотите определить совершенно новый шаблон для культуры, используйте кнопку «Настроить для каждой культуры». \\u003Cbr /\\u003E Чтобы получить дополнительную информацию о синтаксисе и других деталях, посетите \\u003Ca href=\\u0022https://docs.abp.io/en/abp/latest/Text-Templating\\u0022 target=\\u0022_blank\\u0022\\u003Eтекстовые шаблоны. документация\\u003C/a\\u003E.","CustomizePerCulture":"Настроить для каждой культуры","DisplayName":"Показать имя","Search":"Поиск","ReturnToTemplates":"Вернуться к шаблонам","Feature:TextManagementGroup":"Управление текстовыми шаблонами","Feature:TextManagementEnable":"Включить управление текстовыми шаблонами","Feature:TextManagementEnableDescription":"Включить систему управления текстом в приложении."}	2026-04-29 11:54:22.857951	\N
3a20e9c3-6a5f-c8a7-7fc3-01799a0fe264	TextTemplateManagement	zh-Hans	{"Menu:TextTemplates":"文本模板","Menu:TextTemplates:TemplateDefinitions":"模板定义","Permission:TextTemplateManagement":"文本模板管理","Permission:TextTemplates":"文本模板","Permission:EditContents":"编辑内容","TemplateContents":"模板内容","Name":"名称","Layout":"布局页","IsLayout":"是否布局","LocalizationResource":"本地化资源","DefaultCultureName":"默认文化名称","Contents":"内容","SeeContents":"查看内容","EditContents":"编辑内容","BaseCultureName":"参考文化名称","TargetCultureName":"目标文化名称","BaseContent":"参考内容","TargetContent":"目标内容","SaveContent":"保存内容","RestoreToDefault":"恢复到默认值","RestoreToDefaultMessage":"您确定吗？此操作将把内容转换为默认值。","IsInlineLocalized":"内联本地化","Success":"成功","TemplateContentUpdated":"模板内容更新成功。","TemplateContentRestoredToDefault":"模板内容恢复为默认值。","TemplateContent":"模板内容","InlineContentDescription":"本模板使用内联本地化。您可以使用 \\u003Cb\\u003EL\\u003C/b\\u003E 函数对文本进行本地化，如 \\u003Cb\\u003E{{L \\u0022Hello\\u0022}}\\u003C/b\\u003E 。如果您还想为某个文化定义一个全新的模板，请使用 \\u0022按文化定制 \\u0022按钮。\\u003Cbr /\\u003E 要获取更多语法信息和其他详细信息，请查看 \\u003Ca href=\\u0022https://docs.abp.io/en/abp/latest/Text-Templating\\u0022 target=\\u0022_blank\\u0022\\u003EText Templates 文档\\u003C/a\\u003E。","CustomizePerCulture":"按文化定制","DisplayName":"显示名称","Search":"搜索","ReturnToTemplates":"返回模板页面","Feature:TextManagementGroup":"文本模板管理","Feature:TextManagementEnable":"启用文本模板管理","Feature:TextManagementEnableDescription":"在应用程序中启用文本模板管理系统"}	2026-04-29 11:54:22.85826	\N
3a20e9c3-6ad2-2da5-a5c4-085af655d5a1	AbpOpenIddict	en	{"TheOpenIDConnectRequestCannotBeRetrieved":"The OpenID Connect request cannot be retrieved.","TheUserDetailsCannotBbeRetrieved":"The user details cannot be retrieved.","TheApplicationDetailsCannotBeFound":"The application details cannot be found.","DetailsConcerningTheCallingClientApplicationCannotBeFound":"Details concerning the calling client application cannot be found.","TheSpecifiedGrantTypeIsNotImplemented":"The specified grant type {0} is not implemented.","Authorization":"Authorization","DoYouWantToGrantAccessToYourData":"Do you want to grant {0} access to your data?","ScopesRequested":"Scopes requested","Accept":"Accept","Deny":"Deny","ApplicationResourcePermissionProviderKeyLookupService":"Client","Permissions":"Permissions","Permission:OpenIddictPro":"OpenId","Permission:Edit":"Edit","Permission:Create":"Create","Permission:Delete":"Delete","Permission:Application":"Application","Permission:ManagePermissions":"Manage permissions","Permission:ViewChangeHistory":"View change history","ChangeHistory":"Change history","Permission:Scope":"Scope","InvalidRedirectUri":"Invalid RedirectUri: {0}","InvalidPostLogoutRedirectUri":"Invalid PostLogoutRedirectUri: {0}","InvalidFrontChannelLogoutUri":"Invalid FrontChannelLogoutUri: {0}","NoClientSecretCanBeSetForPublicApplications":"No client secret can be set for public applications.","TheClientSecretIsRequiredForConfidentialApplications":"The client secret is required for confidential applications.","NoJsonWebKeySetCanBeSetForPublicApplications":"No JSON web key set can be set for public applications.","InvalidJsonWebKeySetFormat":"The JSON web key set format is invalid.","ClientSecretOrJsonWebKeySetIsRequired":"Either the client secret or a JSON web key set is required for confidential applications.","TheClientIdentifierIsAlreadyTakenByAnotherApplication":"The client identifier is already taken by another application.","TheScopeNameCannotContainSpaces":"The scope name cannot contain spaces","TheNameIsAlreadyTakenByAnotherScope":"The name is already taken by another scope.","Menu:OpenIddict":"OpenId","Scopes":"Scopes","ScopeDeletionWarningMessage":"Are you sure you want to delete the scope \\u0027{0}\\u0027?","Name":"Name","DisplayName":"Display name","Description":"Description","Resources":"Resources","NewScope":"New scope","Applications":"Applications","ApplicationDeletionWarningMessage":"Are you sure you want to delete the application \\u0027{0}\\u0027?","AreYouSure":"Are you sure?","NoAvailableScopes":"There is no available scopes","NewApplication":"New application","ApplicationType":"Application type","ClientId":"Client Id","ClientType":"Type","ClientSecret":"Client secret","ClientSecretHelpText":"Enter a new value to replace the existing secret","JsonWebKeySet":"JSON web key set","JsonWebKeySetHelpText":"Paste the JSON Web Key Set (JWKS) containing RSA or ECDSA public keys for private_key_jwt client authentication. Leave empty to use client secret authentication.","ClientUri":"Client uri","LogoUri":"Logo uri","AllowAuthorizationCodeFlow":"Allow authorization code flow","AllowImplicitFlow":"Allow implicit flow","AllowPasswordFlow":"Allow password flow","AllowClientCredentialsFlow":"Allow client credentials flow","AllowHybridFlow":"Allow hybrid flow","AllowRefreshTokenFlow":"Allow refresh token flow","AllowTokenExchangeFlow":"Allow token exchange flow","AllowDeviceAuthorizationEndpoint":"Allow device authorization endpoint","AllowEndSessionEndpoint":"Allow end session endpoint","ForcePkce":"Force enable PKCE","AllowPushedAuthorizationEndpoint":"Allow pushed authorization endpoint","ForcePushedAuthorization":"Force enable pushed authorization requests","ExtensionGrantTypes":"Extension grant types","RedirectUris":"Redirect uris","PostLogoutRedirectUris":"Post logout redirect uris","FrontChannelLogoutUri":"Front channel logout uri","ConsentType":"Consent type","NotAvailableForThisType":"Not available for this type","TokenLifetime":"Token Lifetime","TokenLifetimeHit":"The unit of token life is seconds, leaving blank to use the default life time.","AccessTokenLifetime":"AccessToken lifetime","AuthorizationCodeLifetime":"AuthorizationCode lifetime","DeviceCodeLifetime":"DeviceCode lifetime","IdentityTokenLifetime":"IdentityToken lifetime","RefreshTokenLifetime":"RefreshToken lifetime","RequestTokenLifetime":"RequestToken lifetime","UserCodeLifetime":"UserCode lifetime","IssuedTokenLifetime":"IssuedToken lifetime","ClientAuthenticationMethod":"Client authentication method","ClientSecretMethod":"Client secret","PrivateKeyJwtMethod":"JWKS (private_key_jwt)","FormatJson":"Format JSON"}	2026-04-29 11:54:22.85876	\N
3a20e9c3-6ad7-6fca-9a77-21df6d303f4d	AbpOpenIddict	ru	{"TheOpenIDConnectRequestCannotBeRetrieved":"Запрос OpenID Connect не может быть получен.","TheUserDetailsCannotBbeRetrieved":"Данные пользователя не могут быть получены.","TheApplicationDetailsCannotBeFound":"Детали приложения не найдены.","DetailsConcerningTheCallingClientApplicationCannotBeFound":"Подробности о вызывающем клиентском приложении найти невозможно.","TheSpecifiedGrantTypeIsNotImplemented":"Указанный тип предоставления {0} не реализован.","Authorization":"Авторизация","DoYouWantToGrantAccessToYourData":"Вы хотите предоставить пользователю {0} доступ к вашим данным?","ScopesRequested":"Запрошенные объемы","Accept":"Принимать","Deny":"Отрицать","ApplicationResourcePermissionProviderKeyLookupService":"Клиент","Permissions":"Разрешения","Permission:OpenIddictPro":"OpenId","Permission:Edit":"Редактировать","Permission:Create":"Создавать","Permission:Delete":"Удалить","Permission:Application":"Приложение","Permission:ManagePermissions":"Управление разрешениями","Permission:ViewChangeHistory":"Посмотреть историю изменений","ChangeHistory":"История изменений","Permission:Scope":"Объем","InvalidRedirectUri":"Неверный RedirectUri: {0}.","InvalidPostLogoutRedirectUri":"Неверный PostLogoutRedirectUri: {0}.","InvalidFrontChannelLogoutUri":"Неверный FrontChannelLogoutUri: {0}.","NoClientSecretCanBeSetForPublicApplications":"Никакой секрет клиента не может быть установлен для общедоступных приложений.","TheClientSecretIsRequiredForConfidentialApplications":"Секрет клиента необходим для конфиденциальных приложений.","NoJsonWebKeySetCanBeSetForPublicApplications":"Набор веб-ключей JSON не может быть установлен для общедоступных приложений.","InvalidJsonWebKeySetFormat":"Формат набора веб-ключей JSON недействителен.","ClientSecretOrJsonWebKeySetIsRequired":"Для конфиденциальных приложений требуется секрет клиента или набор веб-ключей JSON.","TheClientIdentifierIsAlreadyTakenByAnotherApplication":"Идентификатор клиента уже занят другим приложением.","TheScopeNameCannotContainSpaces":"Имя области не может содержать пробелы","TheNameIsAlreadyTakenByAnotherScope":"Имя уже занято другой областью.","Menu:OpenIddict":"OpenId","Scopes":"Области применения","ScopeDeletionWarningMessage":"Вы уверены, что хотите удалить область «{0}»?","Name":"Имя","DisplayName":"Отображаемое имя","Description":"Описание","Resources":"Ресурсы","NewScope":"Новая область применения","Applications":"Приложения","ApplicationDeletionWarningMessage":"Вы уверены, что хотите удалить приложение «{0}»?","AreYouSure":"Вы уверены?","NoAvailableScopes":"Нет доступных областей","NewApplication":"Новое приложение","ApplicationType":"Тип приложения","ClientId":"ID клиента","ClientType":"Тип клиента","ClientSecret":"Секрет клиента","ClientSecretHelpText":"Введите новое значение, чтобы заменить существующий секрет.","JsonWebKeySet":"Набор веб-ключей JSON","JsonWebKeySetHelpText":"Вставьте набор веб-ключей JSON (JWKS), содержащий открытые ключи RSA или ECDSA для аутентификации клиента private_key_jwt. Оставьте пустым для использования аутентификации по секрету клиента.","ClientUri":"Клиент Ури","LogoUri":"Логотип Ури","AllowAuthorizationCodeFlow":"Разрешить передачу кода авторизации","AllowImplicitFlow":"Разрешить неявный поток","AllowPasswordFlow":"Разрешить поток паролей","AllowClientCredentialsFlow":"Разрешить поток учетных данных клиента","AllowHybridFlow":"Разрешить гибридный поток","AllowRefreshTokenFlow":"Разрешить обновление потока токенов","AllowTokenExchangeFlow":"Разрешить поток обмена токенами","AllowDeviceAuthorizationEndpoint":"Разрешить конечную точку авторизации устройства","AllowEndSessionEndpoint":"Разрешить конечную точку завершения сеанса","ForcePkce":"Принудительно включить PKCE","AllowPushedAuthorizationEndpoint":"Разрешить конечную точку push-авторизации","ForcePushedAuthorization":"Принудительно включить push-запросы авторизации","ExtensionGrantTypes":"Типы грантов на продление","RedirectUris":"Перенаправление Юриса","PostLogoutRedirectUris":"Перенаправление после выхода из системы Uris","FrontChannelLogoutUri":"Uri выхода из переднего канала","ConsentType":"Тип согласия","NotAvailableForThisType":"Недоступно для этого типа","TokenLifetime":"Срок действия токена","TokenLifetimeHit":"Единица времени жизни токена — секунды. Оставьте поле пустым, чтобы использовать время жизни по умолчанию.","AccessTokenLifetime":"Срок действия токена доступа","AuthorizationCodeLifetime":"Срок действия кода авторизации","DeviceCodeLifetime":"Срок действия кода устройства","IdentityTokenLifetime":"Срок действия токена идентификации","RefreshTokenLifetime":"Срок действия RefreshToken","RequestTokenLifetime":"Срок действия RequestToken","UserCodeLifetime":"Срок действия пользовательского кода","IssuedTokenLifetime":"Срок действия выданного токена","ClientAuthenticationMethod":"Метод аутентификации клиента","ClientSecretMethod":"Секрет клиента","PrivateKeyJwtMethod":"JWKS (private_key_jwt)","FormatJson":"Форматировать JSON"}	2026-04-29 11:54:22.858791	\N
3a20e9c3-6adb-a863-02f1-073e59612e20	AbpOpenIddict	vi	{"TheOpenIDConnectRequestCannotBeRetrieved":"Không thể truy xuất yêu cầu OpenID Connect.","TheUserDetailsCannotBbeRetrieved":"Không thể truy xuất chi tiết người dùng.","TheApplicationDetailsCannotBeFound":"Không thể tìm thấy chi tiết ứng dụng.","DetailsConcerningTheCallingClientApplicationCannotBeFound":"Không thể tìm thấy thông tin chi tiết liên quan đến ứng dụng khách đang gọi.","TheSpecifiedGrantTypeIsNotImplemented":"Loại trợ cấp được chỉ định {0} không được triển khai.","Authorization":"Ủy quyền","DoYouWantToGrantAccessToYourData":"Bạn có muốn cấp cho {0} quyền truy cập vào dữ liệu của mình không?","ScopesRequested":"Phạm vi được yêu cầu","Accept":"Chấp nhận","Deny":"Từ chối","ApplicationResourcePermissionProviderKeyLookupService":"Máy khách"}	2026-04-29 11:54:22.858805	\N
3a20e9c3-6adf-b052-9496-97dc9f263c95	AbpOpenIddict	zh-Hans	{"TheOpenIDConnectRequestCannotBeRetrieved":"无法检索 OpenID Connect 请求。","TheUserDetailsCannotBbeRetrieved":"无法检索用户详细信息。","TheApplicationDetailsCannotBeFound":"找不到应用详情。","DetailsConcerningTheCallingClientApplicationCannotBeFound":"无法找到有关调用客户端应用程序的详细信息。","TheSpecifiedGrantTypeIsNotImplemented":"指定的授权类型 {0} 未实现。","Authorization":"授权","DoYouWantToGrantAccessToYourData":"是否要授予 {0} 访问你的数据的权限？","ScopesRequested":"要求的Scope","Accept":"接受","Deny":"拒绝","ApplicationResourcePermissionProviderKeyLookupService":"客户端","Permissions":"权限","Permission:OpenIddictPro":"OpenId","Permission:Edit":"编辑","Permission:Create":"创建","Permission:Delete":"删除","Permission:Application":"应用","Permission:ManagePermissions":"管理权限","Permission:ViewChangeHistory":"查看更改历史","ChangeHistory":"更改历史","Permission:Scope":"范围","InvalidRedirectUri":"无效的 RedirectUri: {0}","InvalidPostLogoutRedirectUri":"无效的的 PostLogoutRedirectUri: {0}","InvalidFrontChannelLogoutUri":"无效的 FrontChannelLogoutUri: {0}","NoClientSecretCanBeSetForPublicApplications":"不能为类型为Public的应用设置ClientSecret.","TheClientSecretIsRequiredForConfidentialApplications":"类型为Confidential的应用需要提供ClientSecret.","NoJsonWebKeySetCanBeSetForPublicApplications":"不能为类型为Public的应用设置JSON Web密钥集。","InvalidJsonWebKeySetFormat":"JSON Web密钥集格式无效。","ClientSecretOrJsonWebKeySetIsRequired":"类型为Confidential的应用需要提供客户端机密或JSON Web密钥集。","TheClientIdentifierIsAlreadyTakenByAnotherApplication":"Client Id已被另一个应用使用.","TheScopeNameCannotContainSpaces":"范围名称不能包含空格","TheNameIsAlreadyTakenByAnotherScope":"该名称已被另一个范围占用。","Menu:OpenIddict":"OpenId","Scopes":"范围","ScopeDeletionWarningMessage":"您确定要删除范围\\u0022{0}\\u0022吗？","Name":"名称","DisplayName":"显示名称","Description":"说明","Resources":"资源","NewScope":"新范围","Applications":"应用","ApplicationDeletionWarningMessage":"您确定要删除应用程序\\u0022{0}\\u0022吗？","AreYouSure":"你确定吗？","NoAvailableScopes":"没有可用的范围","NewApplication":"新应用","ApplicationType":"应用类型","ClientId":"Client Id","ClientType":"客户端类型","ClientSecret":"客户端机密","ClientSecretHelpText":"输入新值替换现有客户端机密","JsonWebKeySet":"JSON Web 密钥集","JsonWebKeySetHelpText":"粘贴包含RSA或ECDSA公钥的JSON Web密钥集(JWKS), 用于private_key_jwt客户端认证。留空则使用客户端机密认证。","ClientUri":"客户端 Uri","LogoUri":"Logo Uri","AllowAuthorizationCodeFlow":"允许授权码流程","AllowImplicitFlow":"允许隐式流程","AllowPasswordFlow":"允许密码流程","AllowClientCredentialsFlow":"允许客户端凭据流程","AllowHybridFlow":"允许混合流程","AllowRefreshTokenFlow":"允许刷新令牌流程","AllowTokenExchangeFlow":"允许令牌交换流程","AllowDeviceAuthorizationEndpoint":"允许设备授权端点","AllowEndSessionEndpoint":"允许结束会话端点","ForcePkce":"强制启用 PKCE","AllowPushedAuthorizationEndpoint":"允许推送授权端点","ForcePushedAuthorization":"强制启用推送授权请求","ExtensionGrantTypes":"扩展授权类型","RedirectUris":"重定向 Uris","PostLogoutRedirectUris":"注销后重定向 Uris","FrontChannelLogoutUri":"前端通道注销 Uri","ConsentType":"同意类型","NotAvailableForThisType":"不可用于此类型","TokenLifetime":"Token 有效期","TokenLifetimeHit":"Token有效期的单位是秒, 留空则使用默认的值.","AccessTokenLifetime":"AccessToken 有效期","AuthorizationCodeLifetime":"AuthorizationCode 有效期","DeviceCodeLifetime":"DeviceCode 有效期","IdentityTokenLifetime":"IdentityToken 有效期","RefreshTokenLifetime":"RefreshToken 有效期","RequestTokenLifetime":"RequestToken 有效期","UserCodeLifetime":"UserCode 有效期","IssuedTokenLifetime":"IssuedToken 有效期","ClientAuthenticationMethod":"客户端认证方式","ClientSecretMethod":"客户端机密","PrivateKeyJwtMethod":"JWKS (private_key_jwt)","FormatJson":"格式化 JSON"}	2026-04-29 11:54:22.858819	\N
3a20e9c3-6b3d-a983-8206-78285a30c494	AdministrationService	en	{"AdministrationService":"Administration Service","Permission:Dashboard":"Dashboard"}	2026-04-29 11:54:22.85883	\N
3a20e9c3-6b40-4d58-7671-fd6d2e00041b	AdministrationService	ko	{"AdministrationService":"Administration Service","Permission:Dashboard":"Dashboard"}	2026-04-29 11:54:22.858843	\N
3a20e9c3-6b47-02eb-cf5e-55152636bdd6	AdministrationService	ru	{"AdministrationService":"Служба Администрирования","Permission:Dashboard":"Панель приборов"}	2026-04-29 11:54:22.858852	\N
3a20e9c3-6b4a-e24c-a04c-7bbbecff2b04	AdministrationService	vi	{"AdministrationService":"Administration Service","Permission:Dashboard":"Dashboard"}	2026-04-29 11:54:22.858861	\N
3a20e9c3-6b4c-b2a9-36bc-3ad62299d6d0	AdministrationService	zh-Hans	{"AdministrationService":"管理服务","Permission:Dashboard":"仪表盘"}	2026-04-29 11:54:22.858869	\N
3a20e9c3-6b53-622b-feaa-9ec21d471094	AbpIdentity	en	{"Menu:IdentityManagement":"Identity management","Users":"Users","NewUser":"New user","UserName":"User name","Surname":"Surname","EmailAddress":"Email address","PhoneNumber":"Phone number","UserInformations":"User Information","DisplayName:IsDefault":"Default","DisplayName:IsStatic":"Static","DisplayName:IsPublic":"Public","Roles":"Roles","Password":"Password","PersonalInfo":"My profile","PersonalSettings":"Personal settings","UserDeletionConfirmationMessage":"Are you sure you want to delete the user \\u0027{0}\\u0027?","RoleDeletionConfirmationMessage":"Are you sure you want to delete the role \\u0027{0}\\u0027?","DisplayName:RoleName":"Role name","DisplayName:UserName":"User name","DisplayName:Name":"Name","DisplayName:Surname":"Surname","DisplayName:Password":"Password","DisplayName:Email":"Email","DisplayName:PhoneNumber":"Phone number","DisplayName:TwoFactorEnabled":"Enable two-factor authentication","DisplayName:IsActive":"Active","DisplayName:LockoutEnabled":"Account lockout","Description:LockoutEnabled":"Lock account after failed login attempts","NewRole":"New role","RoleName":"Role name","CreationTime":"Creation time","Permissions":"Permissions","DisplayName:CurrentPassword":"Current password","DisplayName:NewPassword":"New password","DisplayName:NewPasswordConfirm":"Confirm new password","PasswordChangedMessage":"Your password has been changed successfully.","PersonalSettingsSavedMessage":"Your personal settings has been saved successfully.","Volo.Abp.Identity:DefaultError":"An unknown failure has occurred.","Volo.Abp.Identity:ConcurrencyFailure":"Optimistic concurrency check has been failed. The entity you\\u0027re working on has modified by another user. Please discard your changes and try again.","Volo.Abp.Identity:DuplicateEmail":"Email \\u0027{0}\\u0027 is already taken.","Volo.Abp.Identity:DuplicateRoleName":"Role name \\u0027{0}\\u0027 is already taken.","Volo.Abp.Identity:DuplicateUserName":"Username \\u0027{0}\\u0027 is already taken.","Volo.Abp.Identity:InvalidEmail":"Email \\u0027{0}\\u0027 is invalid.","Volo.Abp.Identity:InvalidPasswordHasherCompatibilityMode":"The provided PasswordHasherCompatibilityMode is invalid.","Volo.Abp.Identity:InvalidPasswordHasherIterationCount":"The iteration count must be a positive integer.","Volo.Abp.Identity:InvalidRoleName":"Role name \\u0027{0}\\u0027 is invalid.","Volo.Abp.Identity:InvalidToken":"Invalid token.","Volo.Abp.Identity:InvalidUserName":"Username \\u0027{0}\\u0027 is invalid.","Volo.Abp.Identity:LoginAlreadyAssociated":"A user with this login already exists.","Volo.Abp.Identity:PasswordMismatch":"Incorrect password.","Volo.Abp.Identity:PasswordRequiresDigit":"Passwords must have at least one digit (\\u00270\\u0027-\\u00279\\u0027).","Volo.Abp.Identity:PasswordRequiresLower":"Passwords must have at least one lowercase (\\u0027a\\u0027-\\u0027z\\u0027).","Volo.Abp.Identity:PasswordRequiresNonAlphanumeric":"Passwords must have at least one non alphanumeric character.","Volo.Abp.Identity:PasswordRequiresUpper":"Passwords must have at least one uppercase (\\u0027A\\u0027-\\u0027Z\\u0027).","Volo.Abp.Identity:PasswordTooShort":"Passwords must be at least {0} characters.","Volo.Abp.Identity:PasswordRequiresUniqueChars":"Passwords must use at least {0} different characters.","Volo.Abp.Identity:PasswordInHistory":"Passwords must not match your last {0} passwords.","Volo.Abp.Identity:RoleNotFound":"Role {0} does not exist.","Volo.Abp.Identity:UserAlreadyHasPassword":"User already has a password set.","Volo.Abp.Identity:UserAlreadyInRole":"User already in role \\u0027{0}\\u0027.","Volo.Abp.Identity:UserLockedOut":"User is locked out.","Volo.Abp.Identity:UserLockoutNotEnabled":"Lockout is not enabled for this user.","Volo.Abp.Identity:UserNameNotFound":"User {0} does not exist.","Volo.Abp.Identity:UserNotInRole":"User is not in role \\u0027{0}\\u0027.","Volo.Abp.Identity:PasswordConfirmationFailed":"Password does not match the confirm password.","Volo.Abp.Identity:NullSecurityStamp":"User security stamp cannot be null.","Volo.Abp.Identity:RecoveryCodeRedemptionFailed":"Recovery code redemption failed.","Volo.Abp.Identity:010001":"You can not delete your own account!","Volo.Abp.Identity:010002":"Can not set more than {MaxUserMembershipCount} organization unit for a user!","Volo.Abp.Identity:010003":"Can not change password of an externally logged in user!","Volo.Abp.Identity:010004":"There is already an organization unit with name {0}. Two units with same name can not be created in same level.","Volo.Abp.Identity:010005":"Static roles can not be renamed.","Volo.Abp.Identity:010006":"Static roles can not be deleted.","Volo.Abp.Identity:010007":"You can\\u0027t change your two factor setting.","Volo.Abp.Identity:010008":"It\\u0027s not allowed to change two factor setting.","Volo.Abp.Identity:010009":"You can not delegate yourself.","Volo.Abp.Identity:010021":"Name exist: \\u0027{0}\\u0027.","Volo.Abp.Identity:010022":"Can not update a static ClaimType.","Volo.Abp.Identity:010023":"Can not delete a static ClaimType.","Identity.OrganizationUnit.MaxUserMembershipCount":"Maximum allowed organization unit membership count for a user","ThisUserIsNotActiveMessage":"This user is not active.","Permission:IdentityManagement":"Identity management","Permission:RoleManagement":"Role management","Permission:Create":"Create","Permission:Edit":"Edit","Permission:Delete":"Delete","Permission:ChangePermissions":"Change permissions","Permission:ManageRoles":"Managing roles","Permission:UserManagement":"User management","Permission:UserLookup":"User lookup","DisplayName:Abp.Identity.Password.RequiredLength":"Required length","DisplayName:Abp.Identity.Password.RequiredUniqueChars":"Required unique characters number","DisplayName:Abp.Identity.Password.RequireNonAlphanumeric":"Required non-alphanumeric character","DisplayName:Abp.Identity.Password.RequireLowercase":"Required lower case character","DisplayName:Abp.Identity.Password.RequireUppercase":"Required upper case character","DisplayName:Abp.Identity.Password.RequireDigit":"Required digit","DisplayName:Abp.Identity.Password.ForceUsersToPeriodicallyChangePassword":"Force users to periodically change password","DisplayName:Abp.Identity.Password.PasswordChangePeriodDays":"Password change period(days)","DisplayName:Abp.Identity.Password.EnablePreventPasswordReuse":"Enable prevent password reuse","DisplayName:Abp.Identity.Password.PreventPasswordReuseCount":"Prevent password reuse count","DisplayName:Abp.Identity.Lockout.AllowedForNewUsers":"Enabled for new users","DisplayName:Abp.Identity.Lockout.LockoutDuration":"Lockout duration(seconds)","DisplayName:Abp.Identity.Lockout.MaxFailedAccessAttempts":"Max failed access attempts","DisplayName:Abp.Identity.SignIn.RequireConfirmedEmail":"Enforce email verification to sign in","DisplayName:Abp.Identity.SignIn.EnablePhoneNumberConfirmation":"Allow users to confirm their phone number","DisplayName:Abp.Identity.SignIn.RequireConfirmedPhoneNumber":"Enforce phone number verification to sign in","DisplayName:Abp.Identity.User.IsUserNameUpdateEnabled":"Allow users to change their usernames","DisplayName:Abp.Identity.User.IsEmailUpdateEnabled":"Allow users to change their email addresses","Description:Abp.Identity.Password.RequiredLength":"The minimum length a password must be.","Description:Abp.Identity.Password.RequiredUniqueChars":"The minimum number of unique characters which a password must contain.","Description:Abp.Identity.Password.RequireNonAlphanumeric":"If passwords must contain a non-alphanumeric character.","Description:Abp.Identity.Password.RequireLowercase":"If passwords must contain a lower case ASCII character.","Description:Abp.Identity.Password.RequireUppercase":"If passwords must contain a upper case ASCII character.","Description:Abp.Identity.Password.RequireDigit":"If passwords must contain a digit.","Description:Abp.Identity.Password.ForceUsersToPeriodicallyChangePassword":"Whether users are forced to periodically change their password.","Description:Abp.Identity.Password.PasswordChangePeriodDays":"The number of days a user\\u0027s password is valid for.","Description:Abp.Identity.Password.EnablePreventPasswordReuse":"Whether to prevent users from reusing their previous passwords.","Description:Abp.Identity.Password.PreventPasswordReuseCount":"The number of previous passwords that cannot be reused.","Description:Abp.Identity.Lockout.AllowedForNewUsers":"Whether a new user can be locked out.","Description:Abp.Identity.Lockout.LockoutDuration":"The duration a user is locked out for when a lockout occurs.","Description:Abp.Identity.Lockout.MaxFailedAccessAttempts":"The number of failed access attempts allowed before a user is locked out, assuming lock out is enabled.","Description:Abp.Identity.SignIn.RequireConfirmedEmail":"Users can create accounts but cannot sign in until they verify their email address.","Description:Abp.Identity.SignIn.EnablePhoneNumberConfirmation":"Users can verify their phone numbers. SMS integration required.","Description:Abp.Identity.SignIn.RequireConfirmedPhoneNumber":"Users can create accounts but cannot sign in until they verify their phone numbers.","Description:Abp.Identity.User.IsUserNameUpdateEnabled":"Whether the username can be updated by the user.","Description:Abp.Identity.User.IsEmailUpdateEnabled":"Whether the email can be updated by the user.","DisplayName:Abp.Identity.SignIn.RequireEmailVerificationToRegister":"Enforce email verification to register","Description:Abp.Identity.SignIn.RequireEmailVerificationToRegister":"User accounts will not be created unless they verify their email addresses.","Details":"Details","CreatedBy":"Created by","ModifiedBy":"Modified by","ModificationTime":"Modification time","PasswordUpdateTime":"Password update time","LockoutEndTime":"Lockout end time","FailedAccessCount":"Failed access count","UserResourcePermissionProviderKeyLookupService":"User","RoleResourcePermissionProviderKeyLookupService":"Role","Feature:IdentityGroup":"Identity","Feature:TwoFactor":"Two factor behaviour","Feature:TwoFactorDescription":"Disabled: No one can set two-factor, Required: All users must set two-factor, Optional: Users can set two-factor if they wish.","Feature:TwoFactor.Optional":"Optional","Feature:TwoFactor.Disabled":"Disabled","Feature:TwoFactor.Forced":"Forced","Feature:EnableLdapLogin":"LDAP login","Feature:EnableLdapLoginDescription":"LDAP login","Feature:EnableOAuthLogin":"OAuth login","Feature:EnableOAuthLoginDescription":"OAuth login","DisplayName:Abp.Identity.TwoFactorBehaviour":"Two factor behaviour","Description:Abp.Identity.TwoFactorBehaviour":"Two factor behaviour","DisplayName:Abp.Identity.UsersCanChange":"Allow users to change their Two Factor.","Description:Abp.Identity.UsersCanChange":"Allow users to change their Two Factor.","DisplayName:Abp.Identity.EnableLdapLogin":"Ldap Login","Description:Abp.Identity.EnableLdapLogin":"Enable Ldap login","DisplayName:Abp.Identity.EnableOAuthLogin":"OAuth login","Description:Abp.Identity.EnableOAuthLogin":"Enable OAuth login","DisplayName:Abp.Identity.Authority":"Authority","Description:Abp.Identity.Authority":"Authority","DisplayName:Abp.Identity.ClientId":"Client Id","Description:Abp.Identity.ClientId":"Client Id","DisplayName:Abp.Identity.ClientSecret":"Client secret","Description:Abp.Identity.ClientSecret":"Client secret","DisplayName:Abp.Identity.Scope":"Scope","Description:Abp.Identity.Scope":"Scope","DisplayName:Abp.Identity.RequireHttpsMetadata":"Require Https Metadata","Description:Abp.Identity.RequireHttpsMetadata":"Require Https Metadata","DisplayName:Abp.Identity.ValidateEndpoints":"Validate Endpoints","Description:Abp.Identity.ValidateEndpoints":"Validate Endpoints","DisplayName:Abp.Identity.ValidateIssuerName":"Validate Issuer Name","Description:Abp.Identity.ValidateIssuerName":"Validate Issuer Name","Feature:MaximumUserCount":"Maximum user count","Feature:MaximumUserCountDescription":"Limits the number of users in the public registration and admin user creation screens. For unlimited users enter 0.","Volo.Abp.Identity:010015":"Reached maximum allowed user count! This tenant is allowed to have a maximum of {MaxUserCount} users.","Volo.Abp.Identity:010016":"The invitation is invalid","Volo.Abp.Identity:010017":"The user with the given email address {Email} already exists.","Volo.Abp.Identity:010018":"The user with the given user name {UserName} already exists.","LdapPasswordPlaceholder":"Enter to update password","LdapLoginSettings":"Ldap Login Settings","OAuthLoginSettings":"OAuth Login Settings","Menu:Identity:Ldap":"Ldap Account","IdentitySettingsLdap":"Ldap","IdentitySettingsOAuth":"OAuth","IdentitySettingsGeneral":"General","IdentitySettingsPasswordPolicy":"Password policy","IdentitySettingsLockout":"Lockout","IdentitySettingsIdentityVerification":"Identity verification","IdentitySettingsUserProfile":"User profile","IdentitySettingsSessions":"Sessions","DisplayName:Abp.Identity.PreventConcurrentLogin":"Prevent concurrent login","Description:Abp.Identity.PreventConcurrentLogin":"Allow users to log in simultaneously from any platform or restrict them from having multiple sessions across different clients.","Enum:IdentityProPreventConcurrentLoginBehaviour.Disabled":"Disabled","Enum:IdentityProPreventConcurrentLoginBehaviour.LogoutFromSameTypeDevices":"Logout from same type devices","Enum:IdentityProPreventConcurrentLoginBehaviour.LogoutFromAllDevices":"Logout from all devices","Description:IdentityProPreventConcurrentLoginBehaviour.Disabled":"Users can stay logged in across multiple platforms.","Description:IdentityProPreventConcurrentLoginBehaviour.LogoutFromSameTypeDevices":"Users can keep their sessions active only on different platforms. Only the most recent session is allowed on the same platform. For example, users cannot have 2 different web browser sessions.","Description:IdentityProPreventConcurrentLoginBehaviour.LogoutFromAllDevices":"The system denies concurrent logins; therefore, users can only have 1 active session, which is the most recent one.","ClaimValueCanNotBeBlank":"Claim value can not be blank!","ClaimValue":"Value","Date":"Date","ThereIsNoUsersCurrentlyInThisRole":"There is no users currently in this role.","UserCount":"User Count","ThereIsNoUsersCurrentlyInThisOrganizationUnit":"There is no users currently in this organization unit.","ClaimValueIsInvalid":"Claim value \\u0027{0}\\u0027 is invalid.","YouCanNotEnableTwoFactorForThisUser":"You can not enable two factor for this user because the user does not configured any two factor providers.","Permission:ClaimManagement":"Claim management","Permission:ViewChangeHistory":"View change history","Permission:Impersonation":"Impersonation","Permission:SettingManagement":"Setting management","Permission:OrganizationUnitManagement":"Organization unit management","Permission:ManageOU":"Managing organization tree","Permission:ManageUsers":"Managing users","Permission:SecurityLogs":"Security logs","Permission:Import":"Import","Menu:SecurityLogs":"Security logs","Lock":"Lock","Unlock":"Unlock","UserLockoutNotEnabled{0}":"User \\u0027{0}\\u0027 lockout is not enabled!","TwoFactor":"Two factor","HideShow":"Hide / Show","Name":"Name","UserInformation":"User information","ClaimTypes":"Claim types","Roles{0}":"Roles ({0})","Members":"Members","OrganizationUnits":"Organization units","OrganizationUnits{0}":"Organization units ({0})","OrganizationUnit:Parent{0}":"Parent: {0}","OrganizationUnit:Root":"Root unit","Type":"Type","Claims":"Claims","NewClaimType":"New claim type","ValueType":"Value type","Description":"Description","Required":"Required","Regex":"Regex","RegexDescription":"Regex description","RegexDescriptionFormText":"Use localization key","IsStatic":"Is Static","ClaimTypeDeletionConfirmationMessage":"Are you sure you want to delete the claim type \\u0027{0}\\u0027?","ChooseAnActionForRole":"There are {0} users assigned to the role you are trying to delete. Please choose an action for these users:","UnassignTheRoleFromTheUsers":"Unassign the role from the users","AssignUsersToOtherRole":"Assign users to another role","SelectAnRoleToAssign":"Select a role to assign","MoveAllUsers":"Move all users","MoveAllUsersWithRoleTo":"Move all users with \\u003Cb\\u003E{0}\\u003C/b\\u003E role to:","UnassignRole":"Unassign role","OrganizationUnitDeletionConfirmationMessage":"Are you sure you want to delete the organization unit \\u0027{0}\\u0027?","ChooseAnActionForOrganizationUnit":"There are {0} users assigned to the organization unit you are trying to delete. Please choose an action for these users:","UnassignTheOrganizationUnitFromTheUsers":"Unassign the organization unit from the users","AssignUsersToOtherOrganizationUnit":"Assign users to another organization unit","SelectAnOrganizationUnitToAssign":"Select an organization unit to assign","MoveAllUsersWithOrganizationUnitTo":"Move all users with \\u003Cb\\u003E{0}\\u003C/b\\u003E organization unit to:","UnassignOrganizationUnit":"Unassign organization unit","OrganizationUnitMoveSameParentMessage":"Can not move to the same parent!","SelectMembers":"Select members","SelectRoles":"Select roles","DisplayName:AccessFailedCount":"Access failed count","DisplayName:UserId":"User id","DisplayName:EmailConfirmed":"Email confirmed","DisplayName:EmailAddress":"Email address","DisplayName:PhoneNumberConfirmed":"Phone number confirmed","DisplayName:ShouldChangePasswordOnNextLogin":"Force password change","Description:ShouldChangePasswordOnNextLogin":"User must change the password at next login","UserMustChangePasswordAtNextLogin":"User must change the password at next login","DisplayName:SendConfirmationEmail":"Send confirmation email","SelectAnOrganizationUnitToSeeMembers":"Select an organization unit to see members","SelectAnOrganizationUnitToSeeRoles":"Select an organization unit to see roles","RemoveUserFromOuWarningMessage":"Are you sure to you want to remove the user {0} from organization unit {1}?","RemoveRoleFromOuWarningMessage":"Are you sure to you want to remove the role {0} from organization unit {1}?","OrganizationTree":"Organization tree","AddRootUnit":"Add root unit","AddSubUnit":"Add sub-unit","AddRole":"Add role","AddMember":"Add member","LastModificationTime":"Last modification time","GenerateRandomPasswordTooltip":"Generate Random Password","PasswordStrengthSettings":"Password strength settings","PasswordRenewingSettings":"Password renewing settings","PasswordHistorySettings":"Password history settings","SetPassword":"Set password","PasswordsAreNotSame":"The given passwords are not equal.","UserNotFound":"User is not exist","LockoutSettings":"Lockout settings","IdentityVerificationSettings":"Identity verification","PasswordSettings":"Password settings","UserSettings":"User settings","DisplayName:LockoutEnd":"Lockout end date","ThisUserIsLockedOutMessage":"This user is locked out. To unlock, please click Actions and then Unlock.","UserUnlocked":"User unlocked","NotActive":"Not active","AddClaim":"Add claim","PleaseSelectClaimType":"Please select a claim type.","SelectedClaimTypeNotFound":"Selected claim type not found.","ClaimAlreadyExist":"The claim already exists","ChangeHistory":"Change history","LoginWithThisUser":"Log in with this user","NewOrganizationUnit":"New organization unit","EditOrganizationUnit":"Edit organization unit","DisplayName":"Display name","OrganizationUnitMoveConfirmMessage":"Are you sure you want to move \\u0027{0}\\u0027 under \\u0027{1}\\u0027?","NoOrganizationUnits":"No organization units!","OrganizationUnitDuplicateDisplayNameWarning":"The organization display name already exists. Please write a different one.","SecurityLogs":"Security logs","SecurityLogs:StartTime":"Start time","SecurityLogs:EndTime":"End time","SecurityLogs:Application":"Application","SecurityLogs:ApplicationDescription":"Application name","SecurityLogs:Identity":"Identity","SecurityLogs:IdentityDescription":"Source of the logs. eg: Identity, IdentityExternal, IdentityServer, OpenIddict","SecurityLogs:Action":"Action","SecurityLogs:ActionDescription":"Security action. eg:LoginSucceeded, LoginFailed, Logout","SecurityLogs:UserName":"Username","SecurityLogs:Client":"Client","SecurityLogs:CorrelationId":"Correlation Id","SecurityLogs:Date":"Date","SecurityLogs:IpAddress":"IP address","SecurityLogs:Browser":"Browser","InvalidLinkToken":"Invalid link token","SelectedOrganizationUnit":"Selected organization unit","Move":"Move","MoveOrganizationUnit":"Move: {0}","Filters":"Filters","Role":"Role","OrganizationUnit":"Organization unit","UserNameOrEmailAddress":"User name or email address","Provider":"Provider","NoExternalLoginProviderAvailable":"No external login provider available","ExternalUser":"External user","Volo.Abp.Identity:010010":"Invalid external login provider","Volo.Abp.Identity:010011":"External login provider authenticate failed","Volo.Abp.Identity:010012":"Local user already exists","Import":"Import","True":"True","False":"False","Tenant":"Tenant","CreationStartDate":"Creation start date","CreationEndDate":"Creation end date","CreationDate":"Creation date","ModificationStartDate":"Modification start date","ModificationEndDate":"Modification end date","ModificationDate":"Modification date","EmailConfirmed":"Email confirmed","IsExternal":"External","External":"External","ViewDetails":"View details","Permission:ViewDetails":"View details","Dash":"-","Export":"Export","ToExcel":"To Excel","ToCSV":"To CSV","UploadFile":"Upload file","ChooseFile":"Choose file","Upload":"Upload","ClickHereToDownloadSampleImportFile":"Click here to download sample import file","PleaseSelectFile":"Please Select File","DownloadTemplateFile":"Download template file","ImportSuccessMessage":"Import completed successfully.","ImportFailedMessage":"{1} users failed and {0} successful with the import. Do you want to download non-imported users?","Permission:Export":"Export","Volo.Abp.Identity:010013":"No user found in the file.","FromExcel":"From Excel","FromCSV":"From CSV","Active":"Active","PleaseSelectAFile":"Please select a file.","PleaseSelectAValidExcelFile":"Please select a valid Excel file","PleaseSelectAValidCSVFile":"Please select a valid CSV file","Volo.Abp.Identity:010014":"Invalid import file format.","AuthorityDelegation":"Authority delegation","DelegateNewUser":"Delegate new user","DelegatedUsers":"Delegated users","MyDelegatedUsers":"My delegated users","DelegationDateRange":"Delegation date range","StartTime":"Start time","EndTime":"End time","Status":"Status","StatusActive":"Active","StatusExpired":"Expired","StatusFuture":"Future","{0}IsStaticRoleCanNotBeRenamed":"{0} role is a static role can not be renamed","General":"General","Permission:Sessions":"Sessions","Sessions":"Sessions","Session:SessionId":"Session id","Session:Current":"Current session","Session:Device":"Device","Session:DeviceInfo":"Device info","Session:UserInfo":"User info","Session:ClientId":"Client id","Session:IpAddresses":"IP addresses","Session:SignedIn":"Signed in date","Session:LastAccessed":"Last access date","Session:Detail":"Detail","Session:Logout":"Log out","SessionLogoutConfirmationMessage":"Are you sure you want to log out the session?","SessionPageDescription":"This page shows all the devices you\\u0027ve logged in on. You can force log out from any device you wish.","SignInSettings":"Sign in settings","AtLeastOneInviteUserEmailAddressRequired":"At least one invite user email address is required","InvalidInviteUserEmailAddress":"Invalid invite user email address: {0}","InvalidAssignedRoleName":"Invalid assigned role name: {0}","UsersAlreadyExistInYourTenant":"The following users already exist in your tenant: {0}","InviteUser":"Invite user","PendingInvitation":"Pending invitation","OneEmailPerLines":"One email per lines","InviteEmailAddress":"Invite email address","PreassignRolesForInvitees":"Preassign roles for invitees","CancelAllPendingIvitations":"Cancel all pending invitations","InvitationSentSuccessfullyToUsers":"Invitation sent successfully to {0} user(s)","InvitationResentSuccessfullyToUsers":"Invitation resent successfully to {0} user(s)","NoPendingInvitations":"No pending invitations","AreYouSureToCancelAllPendingInvitations":"Are you sure to cancel all pending invitations?","AllPendingInvitationsHaveBeenCancelled":"All pending invitations have been cancelled","ResendInvitation":"Resend invitation","InvitationResent":"Invitation resent","CancelInvitation":"Cancel invitation","InvitationCancelled":"Invitation cancelled","InviteeEmail":"Invitee email","InvitationDate":"Invitation date","TextTemplate:Abp.Identity.InviteNewUser":"Invite new user to join tenant","TextTemplate:Abp.Identity.InviteExistUser":"Invite exist user to join tenant","EmailInviteNewUser":"Tenant Invitation","EmailInviteNewUserInfo":"You have received an invitation to join a tenant. Please use the link below to register and join the tenant. You can ignore this email if you do not wish to join the tenant.","RegisterAndAcceptInvitation":"Register and accept invitation","EmailInviteExistUser":"Tenant Invitation","EmailInviteExistUserInfo":"You have received an invitation to join a tenant. Please use the link below to accept this invitation. You can ignore this email if you do not wish to join the tenant.","AcceptInvitation":"Accept invitation","RejectInvitation":"Reject invitation","MailSendingFailed":"Mail sending failed, please check your email configuration and try again."}	2026-04-29 11:54:22.858882	\N
3a20e9c3-6b58-891d-2ddc-f00864c01b93	AbpIdentity	ru	{"Menu:IdentityManagement":"Управление идентификацией","Users":"Пользователи","NewUser":"Новый пользователь","UserName":"Имя пользователя","Surname":"Фамилия","EmailAddress":"Адрес электронной почты","PhoneNumber":"Телефонный номер","UserInformations":"Информация о Пользователе","DisplayName:IsDefault":"По умолчанию","DisplayName:IsStatic":"Статический","DisplayName:IsPublic":"Общественный","Roles":"Роли","Password":"Пароль","PersonalInfo":"Мой профиль","PersonalSettings":"Персональные настройки","UserDeletionConfirmationMessage":"Вы уверены, что хотите удалить пользователя \\u0022{0}\\u0022?","RoleDeletionConfirmationMessage":"Вы уверены, что хотите удалить роль \\u0022{0}\\u0022?","DisplayName:RoleName":"Имя роли","DisplayName:UserName":"Имя пользователя","DisplayName:Name":"Имя","DisplayName:Surname":"Фамилия","DisplayName:Password":"Пароль","DisplayName:Email":"Эл. адрес","DisplayName:PhoneNumber":"Телефонный номер","DisplayName:TwoFactorEnabled":"Включить двухфакторную аутентификацию","DisplayName:IsActive":"Активный","DisplayName:LockoutEnabled":"Блокировка аккаунта","Description:LockoutEnabled":"Включить блокировку аккаунта","NewRole":"Новая роль","RoleName":"Имя роли","CreationTime":"Время создания","Permissions":"Разрешения","DisplayName:CurrentPassword":"Текущий пароль","DisplayName:NewPassword":"Новый пароль","DisplayName:NewPasswordConfirm":"Подтвердите новый пароль","PasswordChangedMessage":"Ваш пароль был успешно изменен.","PersonalSettingsSavedMessage":"Ваши персональные настройки были успешно сохранены.","Volo.Abp.Identity:DefaultError":"Возникла непредвиденная ошибка.","Volo.Abp.Identity:ConcurrencyFailure":"Проверка оптимистического параллелизма не удалась. Объект, над которым вы работаете, был изменен другим пользователем. Отмените изменения и повторите попытку.","Volo.Abp.Identity:DuplicateEmail":"Электронная почта \\u0027{0}\\u0027 уже зарегистрирована.","Volo.Abp.Identity:DuplicateRoleName":"Имя роли \\u0027{0}\\u0027 уже занято.","Volo.Abp.Identity:DuplicateUserName":"Имя пользователя \\u0027{0}\\u0027 уже занято.","Volo.Abp.Identity:InvalidEmail":"Адрес электронной почты \\u0027{0}\\u0027 недействителен.","Volo.Abp.Identity:InvalidPasswordHasherCompatibilityMode":"Предоставленный PasswordHasherCompatibilityMode недействителен.","Volo.Abp.Identity:InvalidPasswordHasherIterationCount":"Число итераций должно быть положительным целым числом.","Volo.Abp.Identity:InvalidRoleName":"Имя роли \\u0027{0}\\u0027 недопустимо.","Volo.Abp.Identity:InvalidToken":"Недопустимый маркер.","Volo.Abp.Identity:InvalidUserName":"Имя пользователя \\u0027{0}\\u0027 недопустимо.","Volo.Abp.Identity:LoginAlreadyAssociated":"Пользователь с таким логином уже существует.","Volo.Abp.Identity:PasswordMismatch":"Неверный пароль.","Volo.Abp.Identity:PasswordRequiresDigit":"Пароль должен содержать по крайней мере одну цифру.","Volo.Abp.Identity:PasswordRequiresLower":"Пароль должен содержать хотя бы одну строчную букву.","Volo.Abp.Identity:PasswordRequiresNonAlphanumeric":"Пароль должен иметь по крайней мере один не буквенно-цифровой символ.","Volo.Abp.Identity:PasswordRequiresUpper":"Пароль должен иметь хотя бы одну букву верхнего регистра.","Volo.Abp.Identity:PasswordTooShort":"Пароль должен содержать не менее {0} символов.","Volo.Abp.Identity:PasswordRequiresUniqueChars":"Пароль должен содержать по крайней мере {0} уникальных символов.","Volo.Abp.Identity:PasswordInHistory":"Пароль не должен совпадать с вашими последними {0} паролями.","Volo.Abp.Identity:RoleNotFound":"Роль {0} не существует.","Volo.Abp.Identity:UserAlreadyHasPassword":"У пользователя уже установлен пароль.","Volo.Abp.Identity:UserAlreadyInRole":"Пользователь уже имеет роль \\u0027{0}\\u0027.","Volo.Abp.Identity:UserLockedOut":"Пользователь временно заблокирован.","Volo.Abp.Identity:UserLockoutNotEnabled":"Блокировка не активирована для этого пользователя.","Volo.Abp.Identity:UserNameNotFound":"Пользователь {0} не существует.","Volo.Abp.Identity:UserNotInRole":"Пользователь не имеет роль \\u0027{0}\\u0027.","Volo.Abp.Identity:PasswordConfirmationFailed":"Пароли не совпадают.","Volo.Abp.Identity:NullSecurityStamp":"Пользовательский штамп безопасности не может быть нулевым.","Volo.Abp.Identity:RecoveryCodeRedemptionFailed":"Не удалось восстановить код.","Volo.Abp.Identity:010001":"Вы не можете удалить свой собственный аккаунт!","Volo.Abp.Identity:010002":"Невозможно установить для пользователя более {MaxUserMembershipCount} организационной единицы!","Volo.Abp.Identity:010003":"Невозможно изменить пароль пользователя, вошедшего в систему извне!","Volo.Abp.Identity:010004":"Уже существует организационное подразделение с названием {0}. Два юнита с одинаковым именем не могут быть созданы на одном уровне.","Volo.Abp.Identity:010005":"Статические роли не могут быть переименованы.","Volo.Abp.Identity:010006":"Статические роли не могут быть удалены.","Volo.Abp.Identity:010007":"Вы не можете изменить настройку двух факторов.","Volo.Abp.Identity:010008":"Изменение двухфакторной настройки не допускается.","Volo.Abp.Identity:010009":"Вы не можете делегировать свои собственные права.","Volo.Abp.Identity:010021":"Имя \\u0027{0}\\u0027 уже существует.","Volo.Abp.Identity:010022":"Невозможно изменить тип статической декларации.","Volo.Abp.Identity:010023":"Невозможно удалить тип статической декларации.","Identity.OrganizationUnit.MaxUserMembershipCount":"Максимально допустимое количество членов организационного подразделения для пользователя","ThisUserIsNotActiveMessage":"Этот пользователь не активен.","Permission:IdentityManagement":"Управление идентификацией","Permission:RoleManagement":"Управление ролями","Permission:Create":"Создавать","Permission:Edit":"Редактировать","Permission:Delete":"Удалить","Permission:ChangePermissions":"Изменить разрешения","Permission:ManageRoles":"Управление ролями","Permission:UserManagement":"Управление пользователями","Permission:UserLookup":"Поиск пользователя","DisplayName:Abp.Identity.Password.RequiredLength":"Требуемая длина","DisplayName:Abp.Identity.Password.RequiredUniqueChars":"Обязательное количество уникальных символов","DisplayName:Abp.Identity.Password.RequireNonAlphanumeric":"Обязательный не буквенно-цифровой символ","DisplayName:Abp.Identity.Password.RequireLowercase":"Обязательный символ нижнего регистра","DisplayName:Abp.Identity.Password.RequireUppercase":"Обязательный символ верхнего регистра","DisplayName:Abp.Identity.Password.RequireDigit":"Требуемая цифра","DisplayName:Abp.Identity.Password.ForceUsersToPeriodicallyChangePassword":"Требовать периодическое изменение пароля","DisplayName:Abp.Identity.Password.PasswordChangePeriodDays":"Периодичность изменения пароля (дни)","DisplayName:Abp.Identity.Password.EnablePreventPasswordReuse":"Включить предотвращение повторного использования пароля","DisplayName:Abp.Identity.Password.PreventPasswordReuseCount":"Количество предотвращений повторного использования пароля","DisplayName:Abp.Identity.Lockout.AllowedForNewUsers":"Включено для новых пользователей","DisplayName:Abp.Identity.Lockout.LockoutDuration":"Длительность блокировки (секунды)","DisplayName:Abp.Identity.Lockout.MaxFailedAccessAttempts":"Максимальное количество неудачных попыток доступа","DisplayName:Abp.Identity.SignIn.RequireConfirmedEmail":"Требовать подтверждение электронной почты для входа","DisplayName:Abp.Identity.SignIn.EnablePhoneNumberConfirmation":"Разрешить пользователям подтверждать свой номер телефона","DisplayName:Abp.Identity.SignIn.RequireConfirmedPhoneNumber":"Требовать подтверждение номера телефона для входа","DisplayName:Abp.Identity.User.IsUserNameUpdateEnabled":"Пользователь может изменять имя","DisplayName:Abp.Identity.User.IsEmailUpdateEnabled":"Электронная почта может быть изменена","Description:Abp.Identity.Password.RequiredLength":"Минимальная длина пароля.","Description:Abp.Identity.Password.RequiredUniqueChars":"Минимальное количество уникальных символов, которые должен содержать пароль.","Description:Abp.Identity.Password.RequireNonAlphanumeric":"Если пароли должны содержать не буквенно- цифровой символ.","Description:Abp.Identity.Password.RequireLowercase":"Если пароли должны содержать строчные символы ASCII.","Description:Abp.Identity.Password.RequireUppercase":"Если пароли должны содержать символ ASCII в верхнем регистре.","Description:Abp.Identity.Password.RequireDigit":"Если пароли должны содержать цифру.","Description:Abp.Identity.Password.ForceUsersToPeriodicallyChangePassword":"Если пользователи должны периодически изменять пароль.","Description:Abp.Identity.Password.PasswordChangePeriodDays":"Периодичность изменения пароля (дни).","Description:Abp.Identity.Password.EnablePreventPasswordReuse":"Следует ли запретить пользователям повторно использовать свои предыдущие пароли.","Description:Abp.Identity.Password.PreventPasswordReuseCount":"Количество предыдущих паролей, которые не могут быть использованы повторно.","Description:Abp.Identity.Lockout.AllowedForNewUsers":"Может ли новый пользователь быть заблокирован.","Description:Abp.Identity.Lockout.LockoutDuration":"Длительность блокировки пользователя.","Description:Abp.Identity.Lockout.MaxFailedAccessAttempts":"Число неудачных попыток доступа, после которых пользователь будет заблокирован.","Description:Abp.Identity.SignIn.RequireConfirmedEmail":"Пользователи могут создавать учетные записи, но не могут войти в систему, пока не подтвердят свой адрес электронной почты.","Description:Abp.Identity.SignIn.EnablePhoneNumberConfirmation":"Пользователи могут подтверждать свои номера телефонов. Требуется интеграция SMS.","Description:Abp.Identity.SignIn.RequireConfirmedPhoneNumber":"Пользователи могут создавать учетные записи, но не могут войти в систему, пока не подтвердят свой номер телефона.","Description:Abp.Identity.User.IsUserNameUpdateEnabled":"Может ли пользователь обновить имя пользователя.","Description:Abp.Identity.User.IsEmailUpdateEnabled":"Может ли пользователь обновить адрес электронной почты.","Details":"Подробности","CreatedBy":"Сделано","ModifiedBy":"Модифицирован","ModificationTime":"Время модификации","PasswordUpdateTime":"Время обновления пароля","LockoutEndTime":"Время окончания блокировки","FailedAccessCount":"Количество неудачных попыток доступа","DisplayName:Abp.Identity.SignIn.RequireEmailVerificationToRegister":"Требовать подтверждение электронной почты для регистрации","Description:Abp.Identity.SignIn.RequireEmailVerificationToRegister":"Учетные записи пользователей не будут созданы, пока они не подтвердят свои адреса электронной почты.","UserResourcePermissionProviderKeyLookupService":"Пользователь","RoleResourcePermissionProviderKeyLookupService":"Роль","Feature:IdentityGroup":"Личность","Feature:TwoFactor":"Двухфакторное поведение","Feature:TwoFactorDescription":"Отключено: никто не может установить двухфакторный, Требуется: все пользователи должны установить двухфакторный, Необязательный: пользователи могут установить двухфакторный, если они хотят","Feature:TwoFactor.Optional":"Необязательный","Feature:TwoFactor.Disabled":"Неполноценный","Feature:TwoFactor.Forced":"Принужденный","Feature:EnableLdapLogin":"Включить вход через Ldap","Feature:EnableLdapLoginDescription":"Включить вход через Ldap","Feature:EnableOAuthLogin":"Включить вход через OAuth","Feature:EnableOAuthLoginDescription":"Включить вход через OAuth","DisplayName:Abp.Identity.TwoFactorBehaviour":"Двухфакторное поведение","Description:Abp.Identity.TwoFactorBehaviour":"Двухфакторное поведение","DisplayName:Abp.Identity.UsersCanChange":"Разрешить пользователям изменять их два фактора.","Description:Abp.Identity.UsersCanChange":"Разрешить пользователям изменять их два фактора.","DisplayName:Abp.Identity.EnableLdapLogin":"Вход в LDAP","Description:Abp.Identity.EnableLdapLogin":"Включить вход через Ldap","DisplayName:Abp.Identity.EnableOAuthLogin":"Вход в OAuth","Description:Abp.Identity.EnableOAuthLogin":"Включить вход через OAuth","DisplayName:Abp.Identity.Authority":"Власть","Description:Abp.Identity.Authority":"Власть","DisplayName:Abp.Identity.ClientId":"ID клиента","Description:Abp.Identity.ClientId":"ID клиента","DisplayName:Abp.Identity.ClientSecret":"Секрет клиента","Description:Abp.Identity.ClientSecret":"Секрет клиента","DisplayName:Abp.Identity.Scope":"Объем","Description:Abp.Identity.Scope":"Объем","DisplayName:Abp.Identity.RequireHttpsMetadata":"Требовать метаданные https","Description:Abp.Identity.RequireHttpsMetadata":"Требовать метаданные https","DisplayName:Abp.Identity.ValidateEndpoints":"Проверить конечные точки","Description:Abp.Identity.ValidateEndpoints":"Проверить конечные точки","DisplayName:Abp.Identity.ValidateIssuerName":"Проверить имя издателя","Description:Abp.Identity.ValidateIssuerName":"Проверить имя издателя","Feature:MaximumUserCount":"Максимальное количество пользователей","Feature:MaximumUserCountDescription":"Ограничивает количество пользователей на публичном экране регистрации и создания администратора. Для неограниченного количества пользователей введите 0.","Volo.Abp.Identity:010015":"Достигнуто максимально допустимое количество пользователей! У этого арендатора может быть не более {MaxUserCount} пользователей.","Volo.Abp.Identity:010016":"Приглашение недействительно","Volo.Abp.Identity:010017":"Пользователь с адресом электронной почты {Email} уже существует.","Volo.Abp.Identity:010018":"Пользователь с именем пользователя {UserName} уже существует.","LdapPasswordPlaceholder":"Введите, чтобы обновить пароль","LdapLoginSettings":"Настройки входа в LDAP","OAuthLoginSettings":"Настройки входа в OAuth","Menu:Identity:Ldap":"Лдап-аккаунт","IdentitySettingsLdap":"Лдап","IdentitySettingsOAuth":"OAuth","IdentitySettingsGeneral":"Общий","IdentitySettingsPasswordPolicy":"Политика паролей","IdentitySettingsLockout":"Блокировка","IdentitySettingsIdentityVerification":"Проверка личности","IdentitySettingsUserProfile":"Профиль пользователя","ClaimValueCanNotBeBlank":"Значение утверждения не может быть пустым!","ClaimValue":"Ценить","Date":"Дата","ThereIsNoUsersCurrentlyInThisRole":"В этой роли в настоящее время нет пользователей","UserCount":"Количество пользователей","ThereIsNoUsersCurrentlyInThisOrganizationUnit":"В этой организационной единице в настоящее время нет пользователей","YouCanNotEnableTwoFactorForThisUser":"Вы не можете включить двухфакторный для этого пользователя, потому что пользователь не настроил ни одного поставщика двухфакторного.","Permission:ClaimManagement":"Управление претензиями","Permission:ViewChangeHistory":"Посмотреть историю изменений","Permission:Impersonation":"Олицетворение","Permission:SettingManagement":"Управление настройками","Permission:OrganizationUnitManagement":"Управление подразделением организации","Permission:ManageOU":"Управление деревом организации","Permission:ManageUsers":"Управление пользователями","Permission:SecurityLogs":"Журналы безопасности","Permission:Import":"импорт","Permission:Sessions":"Сессии","Menu:SecurityLogs":"Журналы безопасности","Lock":"Замок","Unlock":"Разблокировать","UserLockoutNotEnabled{0}":"Блокировка пользователя \\u0027{0}\\u0027 не включена!","TwoFactor":"Два фактора","HideShow":"Скрыть/Показать","Name":"Имя","UserInformation":"Информация о пользователе","ClaimTypes":"Типы претензий","Roles{0}":"Роли ({0})","Members":"Члены","OrganizationUnits":"Организационные подразделения","OrganizationUnits{0}":"Орг. единицы ({0})","OrganizationUnit:Parent{0}":"Родитель: {0}","OrganizationUnit:Root":"Корневой блок","Type":"Тип","Claims":"Претензии","NewClaimType":"Новый тип претензии","ValueType":"Тип значения","Description":"Описание","Required":"Необходимый","Regex":"регулярное выражение","RegexDescription":"Описание регулярного выражения","IsStatic":"Является статическим","ClaimTypeDeletionConfirmationMessage":"Вы уверены, что хотите удалить тип заявки \\u0022{0}\\u0022?","ChooseAnActionForRole":"Роли, которую вы пытаетесь удалить, назначено {0} пользователей. Пожалуйста, выберите действие для этих пользователей:","UnassignTheRoleFromTheUsers":"Отменить назначение роли у пользователей","AssignUsersToOtherRole":"Назначьте пользователям другую роль","SelectAnRoleToAssign":"Выберите роль, которую хотите назначить","MoveAllUsers":"Переместить всех пользователей","MoveAllUsersWithRoleTo":"Переместите всех пользователей с ролью \\u003Cb\\u003E{0}\\u003C/b\\u003E в:","UnassignRole":"Отменить назначение роли","OrganizationUnitDeletionConfirmationMessage":"Вы уверены, что хотите удалить организационное подразделение \\u0022{0}\\u0022?","ChooseAnActionForOrganizationUnit":"К организационному подразделению, которое вы пытаетесь удалить, назначено {0} пользователей. Пожалуйста, выберите действие для этих пользователей:","UnassignTheOrganizationUnitFromTheUsers":"Отменить назначение организационного подразделения у пользователей","AssignUsersToOtherOrganizationUnit":"Назначение пользователей другому организационному подразделению","SelectAnOrganizationUnitToAssign":"Выберите организационное подразделение для назначения","MoveAllUsersWithOrganizationUnitTo":"Переместите всех пользователей из организационного подразделения \\u003Cb\\u003E{0}\\u003C/b\\u003E в:","UnassignOrganizationUnit":"Отменить назначение организационного подразделения","OrganizationUnitMoveSameParentMessage":"Вы не можете переместить организационное подразделение внутри себя!","SelectMembers":"Выберите участников","SelectRoles":"Выберите роли","DisplayName:AccessFailedCount":"Счетчик неудачных попыток доступа","DisplayName:UserId":"Идентификатор пользователя","DisplayName:EmailConfirmed":"Электронная почта подтверждена","DisplayName:EmailAddress":"Адрес электронной почты","DisplayName:ShouldChangePasswordOnNextLogin":"Принудительная смена пароля","Description:ShouldChangePasswordOnNextLogin":"Пользователь должен изменить пароль при следующем входе в систему","DisplayName:SendConfirmationEmail":"Отправить письмо с подтверждением","SelectAnOrganizationUnitToSeeMembers":"Выберите организационное подразделение, чтобы увидеть участников","SelectAnOrganizationUnitToSeeRoles":"Выберите организационное подразделение, чтобы просмотреть роли","RemoveUserFromOuWarningMessage":"Вы уверены, что хотите удалить пользователя {0} из организационного подразделения {1}?","RemoveRoleFromOuWarningMessage":"Вы уверены, что хотите удалить роль {0} из организационного подразделения {1}?","OrganizationTree":"Организационное дерево","AddRootUnit":"Добавить корневой блок","AddSubUnit":"Добавить подразделение","AddRole":"Добавить роль","AddMember":"Добавить члена","LastModificationTime":"Время последней модификации","GenerateRandomPasswordTooltip":"Сгенерировать случайный пароль","PasswordStrengthSettings":"Настройки сложности пароля","PasswordRenewingSettings":"Настройки обновления пароля","PasswordHistorySettings":"Настройки истории паролей","SetPassword":"Установка пароля","PasswordsAreNotSame":"Данные пароли не равны.","UserNotFound":"Пользователь не существует","LockoutSettings":"Настройки блокировки","IdentityVerificationSettings":"Проверка личности","PasswordSettings":"Настройки пароля","UserSettings":"Пользовательские настройки","DisplayName:LockoutEnd":"Дата окончания блокировки","ThisUserIsLockedOutMessage":"Этот пользователь заблокирован. Чтобы разблокировать, нажмите «Действия», а затем «Разблокировать».","UserUnlocked":"Пользователь разблокирован","NotActive":"Не активен","AddClaim":"Добавить претензию","PleaseSelectClaimType":"Пожалуйста, выберите тип претензии.","SelectedClaimTypeNotFound":"Выбранный тип претензии не найден.","ClaimAlreadyExist":"Претензия уже имеет значение.","ChangeHistory":"История изменений","LoginWithThisUser":"Войти под этим пользователем","NewOrganizationUnit":"Новое организационное подразделение","EditOrganizationUnit":"Редактировать подразделение организации","DisplayName":"Показать имя","OrganizationUnitMoveConfirmMessage":"Вы уверены, что хотите переместить \\u0022{0}\\u0022 под \\u0022{1}\\u0022?","NoOrganizationUnits":"Нет организационных единиц!","OrganizationUnitDuplicateDisplayNameWarning":"Отображаемое имя организации уже существует. Пожалуйста, напишите другой.","SecurityLogs":"Журналы безопасности","SecurityLogs:StartTime":"Время начала","SecurityLogs:EndTime":"Время окончания","SecurityLogs:Application":"Заявление","SecurityLogs:ApplicationDescription":"Имя приложения","SecurityLogs:Identity":"Личность","SecurityLogs:IdentityDescription":"Источник журналов. например: Identity, IdentityExternal, IdentityServer, OpenIddict","SecurityLogs:Action":"Действие","SecurityLogs:ActionDescription":"Охранное действие. например: Успешный вход, Ошибка входа, Выход из системы","SecurityLogs:UserName":"Имя пользователя","SecurityLogs:Client":"Клиент","SecurityLogs:CorrelationId":"Идентификатор корреляции","SecurityLogs:Date":"Дата","SecurityLogs:IpAddress":"Айпи адрес","SecurityLogs:Browser":"Браузер","InvalidLinkToken":"Недействительный токен ссылки","SelectedOrganizationUnit":"Выбранное организационное подразделение","Move":"Двигаться","MoveOrganizationUnit":"Переместить: {0}","Filters":"Фильтры","Role":"Роль","OrganizationUnit":"Организационная единица","UserNameOrEmailAddress":"Имя пользователя или адрес электронной почты","Provider":"Провайдер","NoExternalLoginProviderAvailable":"Нет доступных внешних провайдеров входа","ExternalUser":"Внешний пользователь","Volo.Abp.Identity:010010":"Недопустимый внешний провайдер входа в систему","Volo.Abp.Identity:010011":"Ошибка аутентификации внешнего провайдера входа","Volo.Abp.Identity:010012":"Локальный пользователь уже существует","Import":"импорт","True":"Истинный","False":"ЛОЖЬ","Tenant":"Жилец","CreationStartDate":"Дата начала создания","CreationEndDate":"Дата окончания создания","CreationDate":"Дата создания","ModificationStartDate":"Дата начала модификации","ModificationEndDate":"Дата окончания модификации","ModificationDate":"Дата модификации","EmailConfirmed":"Электронная почта подтверждена","IsExternal":"Является внешним","External":"Внешний","ViewDetails":"Посмотреть детали","Permission:ViewDetails":"Посмотреть детали","Dash":"-","Export":"Экспорт","ToExcel":"В Excel","ToCSV":"В CSV","UploadFile":"Загрузить файл","ChooseFile":"Выберите файл","Upload":"Загрузить","ClickHereToDownloadSampleImportFile":"Нажмите здесь, чтобы загрузить образец файла импорта","PleaseSelectFile":"Пожалуйста, выберите файл","DownloadTemplateFile":"Скачать шаблонный файл","ImportSuccessMessage":"Импорт успешно завершен.","ImportFailedMessage":"Не все пользователи были импортированы, успешно: {0} не удалось: {1}. Вы хотите скачать файл недействительных пользователей?","Permission:Export":"Экспорт","Volo.Abp.Identity:010013":"В файле не найдено пользователей.","FromExcel":"Из Excel","FromCSV":"Из CSV","Active":"Активный","PleaseSelectAFile":"Пожалуйста, выберите файл","PleaseSelectAValidExcelFile":"Пожалуйста, выберите действительный файл Excel","PleaseSelectAValidCSVFile":"Пожалуйста, выберите действительный файл CSV","Volo.Abp.Identity:010014":"Неверный формат файла импорта","AuthorityDelegation":"Делегирование","DelegateNewUser":"Делегировать нового пользователя","DelegatedUsers":"Делегированные пользователи","MyDelegatedUsers":"Мои делегированные пользователи","DelegationDateRange":"Диапазон дат делегирования","StartTime":"Время начала","EndTime":"Время окончания","Status":"Статус","StatusActive":"Активный","StatusExpired":"Истекший","StatusFuture":"Будущий","{0}IsStaticRoleCanNotBeRenamed":"Статическая роль {0} не может быть переименована","General":"Общий","Sessions":"Сессии","Session:SessionId":"ID сессии","Session:Current":"Текущая сессия","Session:Device":"Устройство","Session:DeviceInfo":"Информация об устройстве","Session:UserInfo":"Информация о пользователе","Session:ClientId":"ID клиента","Session:IpAddresses":"IP адреса","Session:SignedIn":"Дата входа","Session:LastAccessed":"Дата последнего доступа","Session:Detail":"Деталь","Session:Logout":"Выйти","SessionLogoutConfirmationMessage":"Вы уверены, что хотите выйти из сессии?","SessionPageDescription":"Эта страница показывает все устройства, на которых вы вошли в систему. Вы можете принудительно выйти из любого устройства, которое хотите.","SignInSettings":"Настройки входа","RegexDescriptionFormText":"Использовать ключ локализации","DisplayName:PhoneNumberConfirmed":"Номер телефона подтвержден","UserMustChangePasswordAtNextLogin":"Пользователь должен изменить пароль при следующем входе в систему","AtLeastOneInviteUserEmailAddressRequired":"Требуется как минимум один адрес электронной почты приглашенного пользователя","InvalidInviteUserEmailAddress":"Недопустимый адрес электронной почты приглашенного пользователя: {0}","InvalidAssignedRoleName":"Недопустимое имя назначенной роли: {0}","UsersAlreadyExistInYourTenant":"Следующие пользователи уже существуют в вашем тенанте: {0}","InviteUser":"Пригласить пользователя","PendingInvitation":"Ожидающее приглашение","OneEmailPerLines":"Один e‑mail на строку","InviteEmailAddress":"Адрес электронной почты для приглашения","PreassignRolesForInvitees":"Предварительно назначить роли приглашённым","CancelAllPendingIvitations":"Отменить все ожидающие приглашения","InvitationSentSuccessfullyToUsers":"Приглашение успешно отправлено {0} пользователю(ам)","InvitationResentSuccessfullyToUsers":"Приглашение успешно повторно отправлено {0} пользователю(ам)","NoPendingInvitations":"Нет ожидающих приглашений","AreYouSureToCancelAllPendingInvitations":"Вы уверены, что хотите отменить все ожидающие приглашения?","AllPendingInvitationsHaveBeenCancelled":"Все ожидающие приглашения были отменены","ResendInvitation":"Повторно отправить приглашение","InvitationResent":"Приглашение повторно отправлено","CancelInvitation":"Отменить приглашение","InvitationCancelled":"Приглашение отменено","InviteeEmail":"E‑mail приглашённого","InvitationDate":"Дата приглашения","TextTemplate:Abp.Identity.InviteNewUser":"Пригласить нового пользователя присоединиться к тенанту","TextTemplate:Abp.Identity.InviteExistUser":"Пригласить существующего пользователя присоединиться к тенанту","EmailInviteNewUser":"Приглашение тенанта","EmailInviteNewUserInfo":"Вы получили приглашение присоединиться к тенанту. Используйте ссылку ниже, чтобы зарегистрироваться и присоединиться. Если вы не хотите присоединяться, просто проигнорируйте это письмо.","RegisterAndAcceptInvitation":"Зарегистрироваться и принять приглашение","EmailInviteExistUser":"Приглашение тенанта","EmailInviteExistUserInfo":"Вы получили приглашение присоединиться к тенанту. Используйте ссылку ниже, чтобы принять приглашение. Если вы не хотите присоединяться, просто проигнорируйте это письмо.","AcceptInvitation":"Принять приглашение","RejectInvitation":"Отклонить приглашение","MailSendingFailed":"Не удалось отправить письмо, пожалуйста, проверьте настройку почты и попробуйте снова."}	2026-04-29 11:54:22.858891	\N
3a20e9c3-6b5a-928a-e805-7e256468746a	AbpIdentity	vi	{"Menu:IdentityManagement":"Quản lý danh tính","Users":"Người dùng","NewUser":"Tạo người dùng mới","UserName":"Tên đăng nhập","Surname":"Họ","EmailAddress":"Địa chỉ email","PhoneNumber":"Số điện thoại","UserInformations":"Thông tin người dùng","DisplayName:IsDefault":"Mặc định","DisplayName:IsStatic":"Cố định","DisplayName:IsPublic":"Công khai","Roles":"Vai trò","Password":"Mật khẩu","PersonalInfo":"Thông tin của tôi","PersonalSettings":"Thiết lập cá nhân","UserDeletionConfirmationMessage":"Người \\u0027{0}\\u0027 sẽ bị xóa. Bạn có xác nhận điều này không?","RoleDeletionConfirmationMessage":"vai trò \\u0027{0}\\u0027 sẽ bị xóa. Bạn có xác nhận điều này không?","DisplayName:RoleName":"Tên vai trò","DisplayName:UserName":"Tên người dùng","DisplayName:Name":"Tên","DisplayName:Surname":"Họ","DisplayName:Password":"mật khẩu","DisplayName:Email":"Địa chỉ email","DisplayName:PhoneNumber":"Số điện thoại","DisplayName:TwoFactorEnabled":"Xác thực hai yếu tố","DisplayName:IsActive":"Tích cực","DisplayName:LockoutEnabled":"Khóa tài khoản","Description:LockoutEnabled":"Khóa tài khoản sau các lần đăng nhập thất bại","NewRole":"Vai trò mới","RoleName":"Tên vai trò","CreationTime":"Thời gian tạo","Permissions":"Quyền","DisplayName:CurrentPassword":"Mật khẩu hiện tại","DisplayName:NewPassword":"Mật khẩu mới","DisplayName:NewPasswordConfirm":"Xác nhận mật khẩu","PasswordChangedMessage":"Mật khẩu của bạn đã được thay đổi thành công.","PersonalSettingsSavedMessage":"Thiết lập của bạn đã được lưu lại thành công.","Volo.Abp.Identity:DefaultError":"Một thất bại chưa biết đã xảy ra.","Volo.Abp.Identity:ConcurrencyFailure":"Kiểm tra đồng thời lạc quan đã không thành công. Thực thể bạn đang làm việc đã bị người dùng khác sửa đổi. Vui lòng hủy các thay đổi của bạn và thử lại.","Volo.Abp.Identity:DuplicateEmail":"Email \\u0027{0}\\u0027 đã được sử dụng.","Volo.Abp.Identity:DuplicateRoleName":"Tên vài trò \\u0027{0}\\u0027 đã được sử dụng.","Volo.Abp.Identity:DuplicateUserName":"User name \\u0027{0}\\u0027 đã được sử dụng.","Volo.Abp.Identity:InvalidEmail":"Email \\u0027{0}\\u0027 Không hợp lệ.","Volo.Abp.Identity:InvalidPasswordHasherCompatibilityMode":"PasswordHasherCompatibilityMode được cung cấp không hợp lệ.","Volo.Abp.Identity:InvalidPasswordHasherIterationCount":"Số lần lặp phải là số nguyên dương.","Volo.Abp.Identity:InvalidRoleName":"Tên người dùng \\u0027{0}\\u0027 Không hợp lệ.","Volo.Abp.Identity:InvalidToken":"Token không hợp lệ.","Volo.Abp.Identity:InvalidUserName":"Tên người dùng \\u0027{0}\\u0027 không hợp lệ.","Volo.Abp.Identity:LoginAlreadyAssociated":"Một người dùng có thông tin đăng nhập này đã tồn tại.","Volo.Abp.Identity:PasswordMismatch":"Mật khẩu không đúng.","Volo.Abp.Identity:PasswordRequiresDigit":"Mật khẩu phải có ít nhất một chữ số (\\u00270\\u0027-\\u00279\\u0027).","Volo.Abp.Identity:PasswordRequiresLower":"Mật khẩu phải có ít nhất một chữ thường (\\u0027a\\u0027-\\u0027z\\u0027).","Volo.Abp.Identity:PasswordRequiresNonAlphanumeric":"Mật khẩu phải có ít nhất một ký tự không phải là chữ và số.","Volo.Abp.Identity:PasswordRequiresUpper":"Mật khẩu phải có ít nhất một chữ hoa (\\u0027A\\u0027-\\u0027Z\\u0027).","Volo.Abp.Identity:PasswordTooShort":"Mật khẩu phải ít nhất {0} kí tự.","Volo.Abp.Identity:PasswordRequiresUniqueChars":"Mật khẩu không được chứa {0} ký tự trùng lặp.","Volo.Abp.Identity:PasswordInHistory":"Mật khẩu không được trùng với {0} mật khẩu gần đây của bạn.","Volo.Abp.Identity:RoleNotFound":"Vai trò {0} không tồn tại.","Volo.Abp.Identity:UserAlreadyHasPassword":"Người dùng đã có một mật khẩu.","Volo.Abp.Identity:UserAlreadyInRole":"Người dùng đã có vai trò \\u0027{0}\\u0027.","Volo.Abp.Identity:UserLockedOut":"Người dùng bị khóa.","Volo.Abp.Identity:UserLockoutNotEnabled":"Khóa không được kích hoạt cho người dùng này.","Volo.Abp.Identity:UserNameNotFound":"Người dùng {0} không tồn tại.","Volo.Abp.Identity:UserNotInRole":"Người dùng không có vai trò \\u0027{0}\\u0027.","Volo.Abp.Identity:PasswordConfirmationFailed":"Mật khẩu không khớp với mật khẩu xác nhận.","Volo.Abp.Identity:NullSecurityStamp":"Dấu bảo mật của người dùng không thể là null.","Volo.Abp.Identity:RecoveryCodeRedemptionFailed":"Khôi phục mã không hợp lệ cho người dùng này.","Volo.Abp.Identity:010001":"Bạn không thể xóa tài khoản của riêng bạn!","Volo.Abp.Identity:010002":"Không thể đặt nhiều hơn {MaxUserMembershipCount} đơn vị tổ chức cho một người dùng!","Volo.Abp.Identity:010003":"Không thể thay đổi mật khẩu của người dùng đã đăng nhập bên ngoài!","Volo.Abp.Identity:010004":"Đã có một đơn vị tổ chức có tên {0}. Hai đơn vị có cùng tên không thể được tạo trong cùng một cấp.","Volo.Abp.Identity:010005":"Vai trò này là cố định không được phép đổi tên.","Volo.Abp.Identity:010006":"Vai trò này là cố định không được phép xóa.","Volo.Abp.Identity:010007":"Bạn không thể thay đổi cài đặt hai yếu tố của mình.","Volo.Abp.Identity:010008":"Không được phép thay đổi cài đặt hai yếu tố.","Volo.Abp.Identity:010009":"Bạn không thể ủy quyền cho chính mình.","Volo.Abp.Identity:010021":"Tên \\u0027{0}\\u0027 đã tồn tại.","Volo.Abp.Identity:010022":"Không thể thay đổi kiểu của phát biểu tĩnh.","Volo.Abp.Identity:010023":"Không thể xóa phát biểu tĩnh.","Identity.OrganizationUnit.MaxUserMembershipCount":"Số lượng thành viên đơn vị tổ chức tối đa được phép cho một người dùng","ThisUserIsNotActiveMessage":"Người dùng này không hoạt động.","Permission:IdentityManagement":"Quản lý danh tính","Permission:RoleManagement":"Quản lý vai trò","Permission:Create":"Tạo","Permission:Edit":"Sửa","Permission:Delete":"Xóa","Permission:ChangePermissions":"Thay đổi quyền","Permission:ManageRoles":"Quản lý vai trò","Permission:UserManagement":"Quản lý người dùng","Permission:UserLookup":"Tra cứu người dùng","DisplayName:Abp.Identity.Password.RequiredLength":"Chiều dài yêu cầu","DisplayName:Abp.Identity.Password.RequiredUniqueChars":"Số ký tự duy nhất bắt buộc","DisplayName:Abp.Identity.Password.RequireNonAlphanumeric":"Ký tự không phải chữ và số bắt buộc","DisplayName:Abp.Identity.Password.RequireLowercase":"Ký tự viết thường bắt buộc","DisplayName:Abp.Identity.Password.RequireUppercase":"Ký tự viết hoa bắt buộc","DisplayName:Abp.Identity.Password.RequireDigit":"Chữ số bắt buộc","DisplayName:Abp.Identity.Password.ForceUsersToPeriodicallyChangePassword":"Buộc người dùng thay đổi mật khẩu định kỳ","DisplayName:Abp.Identity.Password.PasswordChangePeriodDays":"Thời gian thay đổi mật khẩu (ngày)","DisplayName:Abp.Identity.Password.EnablePreventPasswordReuse":"Bật chức năng ngăn chặn tái sử dụng mật khẩu","DisplayName:Abp.Identity.Password.PreventPasswordReuseCount":"Số lượng ngăn chặn tái sử dụng mật khẩu","DisplayName:Abp.Identity.Lockout.AllowedForNewUsers":"Đã bật cho người dùng mới","DisplayName:Abp.Identity.Lockout.LockoutDuration":"Thời gian khóa (giây)","DisplayName:Abp.Identity.Lockout.MaxFailedAccessAttempts":"Số lần truy cập không thành công tối đa","DisplayName:Abp.Identity.SignIn.RequireConfirmedEmail":"Yêu cầu xác minh email để đăng nhập","DisplayName:Abp.Identity.SignIn.EnablePhoneNumberConfirmation":"Cho phép người dùng xác nhận số điện thoại của họ","DisplayName:Abp.Identity.SignIn.RequireConfirmedPhoneNumber":"Yêu cầu xác minh số điện thoại để đăng nhập","DisplayName:Abp.Identity.User.IsUserNameUpdateEnabled":"Cho phép người dùng thay đổi tên người dùng của họ","DisplayName:Abp.Identity.User.IsEmailUpdateEnabled":"Cho phép người dùng thay đổi địa chỉ email của họ","Description:Abp.Identity.Password.RequiredLength":"Độ dài tối thiểu của mật khẩu phải là.","Description:Abp.Identity.Password.RequiredUniqueChars":"Số lượng ký tự duy nhất tối thiểu mà mật khẩu phải chứa.","Description:Abp.Identity.Password.RequireNonAlphanumeric":"Nếu mật khẩu phải chứa một ký tự không phải chữ và số.","Description:Abp.Identity.Password.RequireLowercase":"Nếu mật khẩu phải chứa ký tự ASCII viết thường.","Description:Abp.Identity.Password.RequireUppercase":"Nếu mật khẩu phải chứa ký tự ASCII viết hoa.","Description:Abp.Identity.Password.RequireDigit":"Nếu mật khẩu phải chứa một chữ số.","Description:Abp.Identity.Password.ForceUsersToPeriodicallyChangePassword":"Nếu người dùng phải thay đổi mật khẩu của họ định kỳ.","Description:Abp.Identity.Password.PasswordChangePeriodDays":"Số ngày mà người dùng phải thay đổi mật khẩu của họ.","Description:Abp.Identity.Password.EnablePreventPasswordReuse":"Có ngăn người dùng tái sử dụng mật khẩu trước đó của họ hay không.","Description:Abp.Identity.Password.PreventPasswordReuseCount":"Số lượng mật khẩu trước đó không thể tái sử dụng.","Description:Abp.Identity.Lockout.AllowedForNewUsers":"Người dùng mới có thể bị khóa hay không.","Description:Abp.Identity.Lockout.LockoutDuration":"Khoảng thời gian người dùng bị khóa khi xảy ra quá trình khóa.","Description:Abp.Identity.Lockout.MaxFailedAccessAttempts":"Số lần truy cập không thành công được phép trước khi người dùng bị khóa, giả sử tính năng khóa được bật.","Description:Abp.Identity.SignIn.RequireConfirmedEmail":"Người dùng có thể tạo tài khoản nhưng không thể đăng nhập cho đến khi xác minh địa chỉ email của họ.","Description:Abp.Identity.SignIn.EnablePhoneNumberConfirmation":"Người dùng có thể xác minh số điện thoại của họ. Yêu cầu tích hợp SMS.","Description:Abp.Identity.SignIn.RequireConfirmedPhoneNumber":"Người dùng có thể tạo tài khoản nhưng không thể đăng nhập cho đến khi xác minh số điện thoại của họ.","Description:Abp.Identity.User.IsUserNameUpdateEnabled":"Người dùng có thể cập nhật tên người dùng hay không.","Description:Abp.Identity.User.IsEmailUpdateEnabled":"Liệu email có thể được cập nhật bởi người dùng hay không.","Details":"Chi tiết","CreatedBy":"Được tạo bởi","ModifiedBy":"Được sửa đổi bởi","ModificationTime":"Thời gian sửa đổi","PasswordUpdateTime":"Thời gian cập nhật mật khẩu","LockoutEndTime":"Thời gian kết thúc khóa","FailedAccessCount":"Số lượt truy cập không thành công","DisplayName:Abp.Identity.SignIn.RequireEmailVerificationToRegister":"Yêu cầu xác minh email để đăng ký","Description:Abp.Identity.SignIn.RequireEmailVerificationToRegister":"Tài khoản người dùng sẽ không được tạo ra trừ khi họ xác minh địa chỉ email của mình.","UserResourcePermissionProviderKeyLookupService":"Người dùng","RoleResourcePermissionProviderKeyLookupService":"Vai trò"}	2026-04-29 11:54:22.8589	\N
3a20e9c3-6b5e-dc57-4d8f-155ee2ce33f2	AbpIdentity	zh-Hans	{"Menu:IdentityManagement":"身份管理","Users":"用户","NewUser":"新用户","UserName":"用户名","Surname":"姓氏","EmailAddress":"电子邮件地址","PhoneNumber":"电话号码","UserInformations":"用户信息","DisplayName:IsDefault":"默认值","DisplayName:IsStatic":"静态","DisplayName:IsPublic":"公开","Roles":"角色","Password":"密码","PersonalInfo":" 个人信息","PersonalSettings":"个人设置","UserDeletionConfirmationMessage":"您确定要删除用户\\u0022{0}\\u0022吗？","RoleDeletionConfirmationMessage":"您确定要删除角色\\u0022{0}\\u0022吗？","DisplayName:RoleName":"角色名称","DisplayName:UserName":"用户名","DisplayName:Name":"名称","DisplayName:Surname":"姓氏","DisplayName:Password":"密码","DisplayName:Email":"电子邮件","DisplayName:PhoneNumber":"电话号码","DisplayName:TwoFactorEnabled":"启用双因素认证","DisplayName:IsActive":"启用","DisplayName:LockoutEnabled":"账户锁定","Description:LockoutEnabled":"启用此选项将在用户登录尝试失败一定次数后锁定帐户","NewRole":"新角色","RoleName":"角色名称","CreationTime":"创建时间","Permissions":"权限","DisplayName:CurrentPassword":"当前密码","DisplayName:NewPassword":"新密码","DisplayName:NewPasswordConfirm":"确认新密码","PasswordChangedMessage":"您的密码已成功更改。","PersonalSettingsSavedMessage":"你的个人设置保存成功。","Volo.Abp.Identity:DefaultError":"发生了一个未知错误。","Volo.Abp.Identity:ConcurrencyFailure":"乐观并发检查失败. 你正在处理的对象已被其他用户修改. 请放弃你的更改, 然后重试。","Volo.Abp.Identity:DuplicateEmail":"邮箱 \\u0027{0}\\u0027 已存在。","Volo.Abp.Identity:DuplicateRoleName":"角色名 \\u0027{0}\\u0027 已存在。","Volo.Abp.Identity:DuplicateUserName":"用户名 \\u0027{0}\\u0027 已存在。","Volo.Abp.Identity:InvalidEmail":"邮箱 \\u0027{0}\\u0027 无效。","Volo.Abp.Identity:InvalidPasswordHasherCompatibilityMode":"提供的 PasswordHasherCompatibilityMode 无效。","Volo.Abp.Identity:InvalidPasswordHasherIterationCount":"迭代计数必须是正整数。","Volo.Abp.Identity:InvalidRoleName":"角色名 \\u0027{0}\\u0027 无效。","Volo.Abp.Identity:InvalidToken":"token无效。","Volo.Abp.Identity:InvalidUserName":"用户名 \\u0027{0}\\u0027 无效。","Volo.Abp.Identity:LoginAlreadyAssociated":"此登录名的用户已存在。","Volo.Abp.Identity:PasswordMismatch":"密码错误。","Volo.Abp.Identity:PasswordRequiresDigit":"密码至少包含一位数字 (\\u00270\\u0027-\\u00279\\u0027)。","Volo.Abp.Identity:PasswordRequiresLower":"密码至少包含一位小写字母 (\\u0027a\\u0027-\\u0027z\\u0027)。","Volo.Abp.Identity:PasswordRequiresNonAlphanumeric":"密码至少包含一位非字母数字字符。","Volo.Abp.Identity:PasswordRequiresUpper":"密码至少包含一位大写字母 (\\u0027A\\u0027-\\u0027Z\\u0027)。","Volo.Abp.Identity:PasswordTooShort":"密码至少为{0}个字符。","Volo.Abp.Identity:PasswordRequiresUniqueChars":"密码至少包含{0}个唯一字符。","Volo.Abp.Identity:PasswordInHistory":"密码不能与最近{0}次使用的密码相同。","Volo.Abp.Identity:RoleNotFound":"角色 {0} 不存在。","Volo.Abp.Identity:UserAlreadyHasPassword":"用户已设置密码。","Volo.Abp.Identity:UserAlreadyInRole":"用户已具有角色 \\u0027{0}\\u0027。","Volo.Abp.Identity:UserLockedOut":"用户被锁定。","Volo.Abp.Identity:UserLockoutNotEnabled":"该用户未启用锁定。","Volo.Abp.Identity:UserNameNotFound":"用户 {0} 不存在。","Volo.Abp.Identity:UserNotInRole":"用户不具有 \\u0027{0}\\u0027 角色。","Volo.Abp.Identity:PasswordConfirmationFailed":"密码或确认密码不一致。","Volo.Abp.Identity:NullSecurityStamp":"用户安全标识不能为空。","Volo.Abp.Identity:RecoveryCodeRedemptionFailed":"恢复代码兑换失败。","Volo.Abp.Identity:010001":"您不能删除自己的账户！","Volo.Abp.Identity:010002":"不能为用户设置超过{MaxUserMembershipCount}个组织单位!","Volo.Abp.Identity:010003":"无法更改外部登录用户的密码!","Volo.Abp.Identity:010004":"已存在名为 {0} 的组织单位. 无法在同一级别创建相同名称的组织单位。","Volo.Abp.Identity:010005":"无法重命名静态角色。","Volo.Abp.Identity:010006":"无法删除静态角色。","Volo.Abp.Identity:010007":"你不能修改你的双因素身份验证设置","Volo.Abp.Identity:010008":"不允许修改双因素身份验证设置。","Volo.Abp.Identity:010009":"你不能委托给自己。","Volo.Abp.Identity:010021":"名称：\\u0027{0}\\u0027 已存在","Volo.Abp.Identity:010022":"不能更改静态声明的类型。","Volo.Abp.Identity:010023":"不能删除静态声明的类型。","Identity.OrganizationUnit.MaxUserMembershipCount":"组织单位最大允许的成员资格计数","ThisUserIsNotActiveMessage":"该用户不可用。","Permission:IdentityManagement":"身份管理","Permission:RoleManagement":"角色管理","Permission:Create":"创建","Permission:Edit":"编辑","Permission:Delete":"删除","Permission:ChangePermissions":"更改权限","Permission:ManageRoles":"管理角色","Permission:UserManagement":"用户管理","Permission:UserLookup":"用户查询","DisplayName:Abp.Identity.Password.RequiredLength":"要求长度","DisplayName:Abp.Identity.Password.RequiredUniqueChars":"要求唯一字符数量","DisplayName:Abp.Identity.Password.RequireNonAlphanumeric":"要求非字母数字","DisplayName:Abp.Identity.Password.RequireLowercase":"要求小写字母","DisplayName:Abp.Identity.Password.RequireUppercase":"要求大写字母","DisplayName:Abp.Identity.Password.RequireDigit":"要求数字","DisplayName:Abp.Identity.Password.ForceUsersToPeriodicallyChangePassword":"强制用户定期更改密码","DisplayName:Abp.Identity.Password.PasswordChangePeriodDays":"密码更改周期(天)","DisplayName:Abp.Identity.Password.EnablePreventPasswordReuse":"启用防止密码重用","DisplayName:Abp.Identity.Password.PreventPasswordReuseCount":"防止密码重用数量","DisplayName:Abp.Identity.Lockout.AllowedForNewUsers":"允许新用户","DisplayName:Abp.Identity.Lockout.LockoutDuration":"锁定时间(秒)","DisplayName:Abp.Identity.Lockout.MaxFailedAccessAttempts":"最大失败访问尝试次数","DisplayName:Abp.Identity.SignIn.RequireConfirmedEmail":"登录需要验证邮箱","DisplayName:Abp.Identity.SignIn.EnablePhoneNumberConfirmation":"允许用户验证手机号码","DisplayName:Abp.Identity.SignIn.RequireConfirmedPhoneNumber":"登录需要验证手机号码","DisplayName:Abp.Identity.User.IsUserNameUpdateEnabled":"启用用户名更新","DisplayName:Abp.Identity.User.IsEmailUpdateEnabled":"启用电子邮箱更新","Description:Abp.Identity.Password.RequiredLength":"密码的最小长度。","Description:Abp.Identity.Password.RequiredUniqueChars":"密码必须包含唯一字符的数量。","Description:Abp.Identity.Password.RequireNonAlphanumeric":"密码是否必须包含非字母数字。","Description:Abp.Identity.Password.RequireLowercase":"密码是否必须包含小写字母。","Description:Abp.Identity.Password.RequireUppercase":"密码是否必须包含大写字母。","Description:Abp.Identity.Password.RequireDigit":"密码是否必须包含数字。","Description:Abp.Identity.Password.ForceUsersToPeriodicallyChangePassword":"是否强制用户定期更改密码。","Description:Abp.Identity.Password.PasswordChangePeriodDays":"用户必须更改密码的周期(天)。","Description:Abp.Identity.Password.EnablePreventPasswordReuse":"是否防止用户重用以前的密码。","Description:Abp.Identity.Password.PreventPasswordReuseCount":"不能重用的以前密码的数量。","Description:Abp.Identity.Lockout.AllowedForNewUsers":"允许新用户被锁定。","Description:Abp.Identity.Lockout.LockoutDuration":"当锁定发生时用户被的锁定的时间(秒)。","Description:Abp.Identity.Lockout.MaxFailedAccessAttempts":"如果启用锁定, 当用户被锁定前失败的访问尝试次数。","Description:Abp.Identity.SignIn.RequireConfirmedEmail":"用户可以创建账户但在验证邮箱地址之前无法登录。","Description:Abp.Identity.SignIn.EnablePhoneNumberConfirmation":"用户可以验证他们的手机号码。需要短信集成。","Description:Abp.Identity.SignIn.RequireConfirmedPhoneNumber":"用户可以创建账户但在验证手机号码之前无法登录。","Description:Abp.Identity.User.IsUserNameUpdateEnabled":"是否允许用户更新用户名。","Description:Abp.Identity.User.IsEmailUpdateEnabled":"用户是否可以更新电子邮件.","DisplayName:Abp.Identity.SignIn.RequireEmailVerificationToRegister":"强制要求验证电子邮件才能注册","Description:Abp.Identity.SignIn.RequireEmailVerificationToRegister":"用户帐户将不会被创建，除非他们验证他们的电子邮件地址。","Details":"详细信息","CreatedBy":"创建者","ModifiedBy":"修改者","ModificationTime":"修改时间","PasswordUpdateTime":"密码更新时间","LockoutEndTime":"锁定结束时间","FailedAccessCount":"访问失败次数","UserResourcePermissionProviderKeyLookupService":"用户","RoleResourcePermissionProviderKeyLookupService":"角色","Feature:IdentityGroup":"身份标识","Feature:TwoFactor":"双因素验证","Feature:TwoFactorDescription":"禁用: 没有人可以设置双因素, 强制启用: 所有用户必须设置双因素, 可选: 用户可以选择设置双因素","Feature:TwoFactor.Optional":"可选","Feature:TwoFactor.Disabled":"禁用","Feature:TwoFactor.Forced":"强制启用","Feature:EnableLdapLogin":"LDAP 登录","Feature:EnableLdapLoginDescription":"LDAP 登录","Feature:EnableOAuthLogin":"OAuth 登录","Feature:EnableOAuthLoginDescription":"OAuth 登录","DisplayName:Abp.Identity.TwoFactorBehaviour":"双因素身份验证行为","Description:Abp.Identity.TwoFactorBehaviour":"双因素身份验证行为","DisplayName:Abp.Identity.UsersCanChange":"允许用户更改其因素身份验证.","Description:Abp.Identity.UsersCanChange":"允许用户更改其因素身份验证.","DisplayName:Abp.Identity.EnableLdapLogin":"登录","Description:Abp.Identity.EnableLdapLogin":"启用Ldap登录","DisplayName:Abp.Identity.EnableOAuthLogin":"OAuth登录","Description:Abp.Identity.EnableOAuthLogin":"启用OAuth登录","DisplayName:Abp.Identity.Authority":"授权服务器地址","Description:Abp.Identity.Authority":"授权服务器地址","DisplayName:Abp.Identity.ClientId":"客户端 Id","Description:Abp.Identity.ClientId":"客户端 Id","DisplayName:Abp.Identity.ClientSecret":"客户端 Secret","Description:Abp.Identity.ClientSecret":"客户端 Secret","DisplayName:Abp.Identity.Scope":"范围","Description:Abp.Identity.Scope":"范围","DisplayName:Abp.Identity.RequireHttpsMetadata":"要求 Https 元数据","Description:Abp.Identity.RequireHttpsMetadata":"要求 Https 元数据","DisplayName:Abp.Identity.ValidateEndpoints":"验证端点","Description:Abp.Identity.ValidateEndpoints":"验证端点","DisplayName:Abp.Identity.ValidateIssuerName":"验证发行人名称","Description:Abp.Identity.ValidateIssuerName":"验证发行人名称","Feature:MaximumUserCount":"最大用户数","Feature:MaximumUserCountDescription":"限制公共注册和管理员用户创建屏幕中的用户数量。请输入0无限制数量，。","Volo.Abp.Identity:010015":"已达到允许的最大用户数！该租户最多允许有 {MaxUserCount} 个用户。","Volo.Abp.Identity:010016":"邀请无效","Volo.Abp.Identity:010017":"具有电子邮件地址 {Email} 的用户已存在。","Volo.Abp.Identity:010018":"具有用户名 {UserName} 的用户已存在。","LdapPasswordPlaceholder":"输入更新密码","LdapLoginSettings":"Ldap 登录设置","OAuthLoginSettings":"OAuth 登录设置","Menu:Identity:Ldap":"Ldap 账户","IdentitySettingsLdap":"Ldap","IdentitySettingsOAuth":"OAuth","IdentitySettingsGeneral":"通用","IdentitySettingsPasswordPolicy":"密码策略","IdentitySettingsLockout":"锁定","IdentitySettingsIdentityVerification":"身份验证","IdentitySettingsUserProfile":"用户资料","IdentitySettingsSessions":"会话","DisplayName:Abp.Identity.PreventConcurrentLogin":"防止并发登录","Description:Abp.Identity.PreventConcurrentLogin":"防止并发登录的行为","Enum:IdentityProPreventConcurrentLoginBehaviour.Disabled":"禁用","Enum:IdentityProPreventConcurrentLoginBehaviour.LogoutFromSameTypeDevices":"从相同类型设备注销","Enum:IdentityProPreventConcurrentLoginBehaviour.LogoutFromAllDevices":"从所有设备注销","Description:IdentityProPreventConcurrentLoginBehaviour.Disabled":"用户可以在多个平台上保持登录状态。","Description:IdentityProPreventConcurrentLoginBehaviour.LogoutFromSameTypeDevices":"用户只能在不同的平台上保持他们的会话活跃。同一个平台上只允许最近的会话。 例如，用户不能同时有两个不同的网络浏览器会话。","Description:IdentityProPreventConcurrentLoginBehaviour.LogoutFromAllDevices":"系统拒绝并发登录；因此，用户只能有一个活动会话，即最近的会话。","ClaimValueCanNotBeBlank":"声明值不能为空！","ClaimValue":"声明值","Date":"日期","ThereIsNoUsersCurrentlyInThisRole":"目前没有用户在此角色中。","UserCount":"用户数量","ThereIsNoUsersCurrentlyInThisOrganizationUnit":"该组织机构目前没有用户。","ClaimValueIsInvalid":"声明值\\u0027{0}\\u0027无效。","YouCanNotEnableTwoFactorForThisUser":"您无法为此用户启用双因素身份验证，因为用户没有配置任何双因素提供者。","Permission:ClaimManagement":"声明管理","Permission:ViewChangeHistory":"查看更改历史","Permission:Impersonation":"冒充","Permission:SettingManagement":"设置管理","Permission:OrganizationUnitManagement":"组织机构管理","Permission:ManageOU":"管理组织机构树","Permission:ManageUsers":"管理用户","Permission:SecurityLogs":"安全日志","Permission:Import":"导入","Menu:SecurityLogs":"安全日志","Lock":"锁定","Unlock":"解锁","UserLockoutNotEnabled{0}":"用户\\u0027{0}\\u0027锁定未启用！","TwoFactor":"双因素","HideShow":"隐藏/显示","Name":"名称","UserInformation":"用户信息","ClaimTypes":"声明类型","Roles{0}":"角色 ({0})","Members":"成员","OrganizationUnits":"组织机构","OrganizationUnits{0}":"组织机构 ({0})","OrganizationUnit:Parent{0}":"父机构: {0}","OrganizationUnit:Root":"根机构","Type":"类型","Claims":"声明","NewClaimType":"新的声明类型","ValueType":"值类型","Description":"说明","Required":"需要","Regex":"Regex","RegexDescription":"Regex 描述","RegexDescriptionFormText":"使用本地化键","IsStatic":"是否静态","ClaimTypeDeletionConfirmationMessage":"您确定要删除\\u0022{0}\\u0022声明类型吗？","ChooseAnActionForRole":"有 {0} 个用户被分配到您要删除的角色。请为这些用户选择一个操作：","UnassignTheRoleFromTheUsers":"取消用户的角色分配","AssignUsersToOtherRole":"将用户分配到其他角色","SelectAnRoleToAssign":"选择要分配的角色","MoveAllUsers":"移动所有用户","MoveAllUsersWithRoleTo":"将所有具有 \\u003Cb\\u003E{0}\\u003C/b\\u003E 角色的用户移至：","UnassignRole":"取消角色分配","OrganizationUnitDeletionConfirmationMessage":"您确定要删除组织机构\\u0022{0}\\u0022吗？","ChooseAnActionForOrganizationUnit":"有 {0} 个用户分配给您要删除的组织机构。请为这些用户选择一项操作：","UnassignTheOrganizationUnitFromTheUsers":"从用户中取消指定组织机构","AssignUsersToOtherOrganizationUnit":"将用户分配到另一个组织机构","SelectAnOrganizationUnitToAssign":"选择要分配的组织机构","MoveAllUsersWithOrganizationUnitTo":"将组织机构为 \\u003Cb\\u003E{0}\\u003C/b\\u003E 的所有用户移至：","UnassignOrganizationUnit":"取消分配组织机构","OrganizationUnitMoveSameParentMessage":"不能将组织机构移动到其自身下面！","SelectMembers":"选择成员","SelectRoles":"选择角色","DisplayName:AccessFailedCount":"访问失败计数","DisplayName:UserId":"用户 ID","DisplayName:EmailConfirmed":"电子邮件已确认","DisplayName:EmailAddress":"电子邮件地址","DisplayName:ShouldChangePasswordOnNextLogin":"强制更改密码","Description:ShouldChangePasswordOnNextLogin":"用户必须在下次登录时更改密码","DisplayName:SendConfirmationEmail":"发送确认电子邮件","SelectAnOrganizationUnitToSeeMembers":"选择一个组织机构来查看成员","SelectAnOrganizationUnitToSeeRoles":"选择一个组织机构来查看角色","RemoveUserFromOuWarningMessage":"您确定要从组织机构 {1} 中删除用户 {0}？","RemoveRoleFromOuWarningMessage":"您确定要从组织机构 {1} 中删除角色 {0}？","OrganizationTree":"组织机构树","AddRootUnit":"添加根机构","AddSubUnit":"添加子机构","AddRole":"添加角色","AddMember":"添加成员","LastModificationTime":"最后修改时间","GenerateRandomPasswordTooltip":"生成随机密码","PasswordStrengthSettings":"密码强度设置","PasswordRenewingSettings":"密码更新设置","PasswordHistorySettings":"密码历史设置","SetPassword":"设置密码","PasswordsAreNotSame":"给定的密码不相同。","UserNotFound":"用户不存在","LockoutSettings":"锁定设置","IdentityVerificationSettings":"身份验证","PasswordSettings":"密码设置","UserSettings":"用户设置","DisplayName:LockoutEnd":"锁定结束日期","ThisUserIsLockedOutMessage":"该用户已被锁定。要解锁，请单击 \\u0022操作\\u0022，然后单击 \\u0022解锁\\u0022。","UserUnlocked":"用户已解锁","NotActive":"用户不可用","AddClaim":"添加声明","PleaseSelectClaimType":"请选择声明类型。","SelectedClaimTypeNotFound":"未找到选定的声明类型。","ClaimAlreadyExist":"声明已经有了价值。","ChangeHistory":"更改历史","LoginWithThisUser":"使用此用户登录","NewOrganizationUnit":"新组织机构","EditOrganizationUnit":"编辑组织单元","DisplayName":"显示名称","OrganizationUnitMoveConfirmMessage":"您确定要将\\u0022{0}\\u0022移到\\u0022{1}\\u0022下面吗？","NoOrganizationUnits":"无组织机构！","OrganizationUnitDuplicateDisplayNameWarning":"组织显示名称已存在。请填写不同的名称。","SecurityLogs":"安全日志","SecurityLogs:StartTime":"开始时间","SecurityLogs:EndTime":"结束时间","SecurityLogs:Application":"应用程序","SecurityLogs:ApplicationDescription":"应用程序名称","SecurityLogs:Identity":"Identity","SecurityLogs:IdentityDescription":"日志来源，例如：Identity, IdentityExternal, IdentityServer, OpenIddict","SecurityLogs:Action":"操作","SecurityLogs:ActionDescription":"例如：LoginSucceeded（登录成功）、LoginFailed（登录失败）、Logout（注销）。","SecurityLogs:UserName":"用户名","SecurityLogs:Client":"Client","SecurityLogs:CorrelationId":"CorrelationId","SecurityLogs:Date":"日期","SecurityLogs:IpAddress":"IP 地址","SecurityLogs:Browser":"浏览器","InvalidLinkToken":"无效的关联Token","SelectedOrganizationUnit":"选定的组织机构","Move":"移动","MoveOrganizationUnit":"移动： {0}","Filters":"过滤器","Role":"角色","OrganizationUnit":"组织机构","UserNameOrEmailAddress":"用户名或电子邮件地址","Provider":"提供者","NoExternalLoginProviderAvailable":"无外部登录提供者","ExternalUser":"外部用户","Volo.Abp.Identity:010010":"外部登录提供程序无效","Volo.Abp.Identity:010011":"外部登录提供程序验证失败","Volo.Abp.Identity:010012":"本地用户已经存在","Import":"导入","True":"真","False":"假","Tenant":"租户","CreationStartDate":"创建开始日期","CreationEndDate":"创建结束日期","CreationDate":"创建日期","ModificationStartDate":"修改开始日期","ModificationEndDate":"修改结束日期","ModificationDate":"修改日期","EmailConfirmed":"电子邮件已确认","IsExternal":"是外部的","External":"外部的","ViewDetails":"查看详情","Permission:ViewDetails":"查看详情","Dash":"-","Export":"导出","ToExcel":"导出至 Excel","ToCSV":"导出至 CSV","UploadFile":"上传文件","ChooseFile":"选择文件","Upload":"上传","ClickHereToDownloadSampleImportFile":"单击此处下载导入文件示例","PleaseSelectFile":"请选择文件","DownloadTemplateFile":"下载模板文件","ImportSuccessMessage":"导入成功。","ImportFailedMessage":"{1}个用户导入失败，{0}个用户导入成功。您想下载未导入的用户吗？","Permission:Export":"导出","Volo.Abp.Identity:010013":"文件中未发现用户。","FromExcel":"从 Excel","FromCSV":"来自 CSV","Active":"活跃","PleaseSelectAFile":"请选择文件。","PleaseSelectAValidExcelFile":"请选择有效的Excel文件","PleaseSelectAValidCSVFile":"请选择有效的CSV文件","Volo.Abp.Identity:010014":"导入文件格式无效。","AuthorityDelegation":"授权委托","DelegateNewUser":"委托新用户","DelegatedUsers":"委托用户","MyDelegatedUsers":"我的委托用户","DelegationDateRange":"委托日期范围","StartTime":"开始时间","EndTime":"结束时间","Status":"状态","StatusActive":"有效","StatusExpired":"已过期","StatusFuture":"未来","{0}IsStaticRoleCanNotBeRenamed":"{0} 角色是静态角色，不能重命名","General":"常规","Permission:Sessions":"会话","Sessions":"会话","Session:SessionId":"会话 ID","Session:Current":"当前会话","Session:Device":"设备","Session:DeviceInfo":"设备信息","Session:UserInfo":"用户信息","Session:ClientId":"客户端 ID","Session:IpAddresses":"IP 地址","Session:SignedIn":"登录时间","Session:LastAccessed":"最后访问时间","Session:Detail":"详情","Session:Logout":"注销","SessionLogoutConfirmationMessage":"您确定要注销此会话吗？","SessionPageDescription":"此页面显示您登录的所有设备。您可以强制注销任何设备。","SignInSettings":"登录设置","DisplayName:PhoneNumberConfirmed":"电话号码已确认","UserMustChangePasswordAtNextLogin":"用户必须在下次登录时更改密码","AtLeastOneInviteUserEmailAddressRequired":"至少需要一个受邀用户的电子邮件地址","InvalidInviteUserEmailAddress":"无效的受邀用户电子邮件地址：{0}","InvalidAssignedRoleName":"无效的分配角色名：{0}","UsersAlreadyExistInYourTenant":"以下用户已存在于您的租户中：{0}","InviteUser":"邀请用户","PendingInvitation":"待处理邀请","OneEmailPerLines":"每行一个邮箱","InviteEmailAddress":"邀请电子邮箱地址","PreassignRolesForInvitees":"为受邀者预先分配角色","CancelAllPendingIvitations":"取消所有待处理邀请","InvitationSentSuccessfullyToUsers":"已成功向 {0} 位用户发送邀请","InvitationResentSuccessfullyToUsers":"已成功重新向 {0} 位用户发送邀请","NoPendingInvitations":"无待处理邀请","AreYouSureToCancelAllPendingInvitations":"确定要取消所有待处理邀请吗？","AllPendingInvitationsHaveBeenCancelled":"所有待处理邀请已被取消","ResendInvitation":"重新发送邀请","InvitationResent":"邀请已重新发送","CancelInvitation":"取消邀请","InvitationCancelled":"邀请已取消","InviteeEmail":"受邀者邮箱","InvitationDate":"邀请日期","TextTemplate:Abp.Identity.InviteNewUser":"邀请新用户加入租户","TextTemplate:Abp.Identity.InviteExistUser":"邀请已有用户加入租户","EmailInviteNewUser":"租户邀请","EmailInviteNewUserInfo":"您已收到加入某个租户的邀请。请使用下面的链接进行注册并加入该租户。如果您不希望加入，可忽略此邮件。","RegisterAndAcceptInvitation":"注册并接受邀请","EmailInviteExistUser":"租户邀请","EmailInviteExistUserInfo":"您已收到加入某个租户的邀请。请使用下面的链接接受此邀请。如果您不希望加入，可忽略此邮件。","AcceptInvitation":"接受邀请","RejectInvitation":"拒绝邀请","MailSendingFailed":"邮件发送失败，请检查您的邮件配置后重试。"}	2026-04-29 11:54:22.858911	\N
3a20e9c3-6b72-0211-7810-0f4787de50de	AbpLdap	en	{"DisplayName:Abp.Ldap.Ldaps":"LDAP over SSL","Description:Abp.Ldap.Ldaps":"LDAP over SSL","DisplayName:Abp.Ldap.ServerHost":"Server host","Description:Abp.Ldap.ServerHost":"Server host","DisplayName:Abp.Ldap.ServerPort":"Server port","Description:Abp.Ldap.ServerPort":"Server port","DisplayName:Abp.Ldap.BaseDc":"Base domain component","Description:Abp.Ldap.BaseDc":"Base domain component","DisplayName:Abp.Ldap.Domain":"Domain","Description:Abp.Ldap.Domain":"Domain","DisplayName:Abp.Ldap.UserName":"Username","Description:Abp.Ldap.UserName":"Username","DisplayName:Abp.Ldap.Password":"Password","Description:Abp.Ldap.Password":"Password"}	2026-04-29 11:54:22.858921	\N
3a20e9c3-6b74-8a9e-2951-3e1003746c15	AbpLdap	ru	{"DisplayName:Abp.Ldap.Ldaps":"LDAP через SSL","Description:Abp.Ldap.Ldaps":"LDAP через SSL","DisplayName:Abp.Ldap.ServerHost":"Хост сервера","Description:Abp.Ldap.ServerHost":"Хост сервера","DisplayName:Abp.Ldap.ServerPort":"Порт сервера","Description:Abp.Ldap.ServerPort":"Порт сервера","DisplayName:Abp.Ldap.BaseDc":"Компонент базового домена","Description:Abp.Ldap.BaseDc":"Компонент базового домена","DisplayName:Abp.Ldap.Domain":"Домен","Description:Abp.Ldap.Domain":"Домен","DisplayName:Abp.Ldap.UserName":"Имя пользователя","Description:Abp.Ldap.UserName":"Имя пользователя","DisplayName:Abp.Ldap.Password":"Пароль","Description:Abp.Ldap.Password":"Пароль"}	2026-04-29 11:54:22.858928	\N
3a20e9c3-734e-5820-cf40-c93d1118c13c	AIManagement	en	{"MyAccount":"My account","Menu:AIManagement":"AI Management","Menu:Workspaces":"Workspaces","NewChatClientConfiguration":"New Chat Client Configuration","Permission:AIManagement":"AI Management","Permission:Workspaces":"Workspaces","Permission:Workspaces:Create":"Create","Permission:Workspaces:Delete":"Delete","Permission:Workspaces:Update":"Update","Basics":"Basics","BasicInformation":"Basic Information","Provider":"Provider","Name":"Name","Description":"Description","ApiBaseUrl":"API Base URL","ModelName":"Model Name","ApiKey":"API Key","Temperature":"Temperature","SystemPrompt":"System Prompt","IsActive":"Active","IsSystem":"System","ApplicationName":"Application Name","OverrideSystemConfiguration":"Override System Configuration","ChatPlayground":"Chat Playground","GoToChatPlayground":"Go to Playground","AreYouSureYouWantToDeleteThisWorkspace":"Are you sure you want to delete this workspace?","EditChatClientConfiguration":"Edit Workspace","EditWorkspace":"Edit Workspace","AIManagement:WorkspaceProviderNotFound":"Provider \\u0027{Provider}\\u0027 not found! Available providers: {AvailableProviders}","AIManagement:WorkspaceNameAlreadyExists":"Workspace name \\u0027{Name}\\u0027 already exists.","AIManagement:CannotDeleteSystemWorkspace":"Cannot delete a system workspace.","AIManagement:WorkspaceInactive":"Workspace \\u0027{Name}\\u0027 is not active. Provider: {Provider}","AIManagement:WorkspaceApiKeyMissing":"Workspace \\u0027{Name}\\u0027 has no API key. Provider: {Provider}","AIManagement:WorkspaceModelNameMissing":"Workspace \\u0027{Name}\\u0027 has no model name. Provider: {Provider}","AIManagement:OpenAIChatClientCreationFailed":"Cannot create chat client for workspace \\u0027{Name}\\u0027. Provider: {Provider}","AIManagement:WorkspaceNameCannotContainSpaces":"Workspace name cannot contain spaces. Name: \\u0027{Name}\\u0027","ProviderTooltip":"You can use any other providers that support the OpenAI API with OpenAI Provider.","NewWorkspace":"New Workspace","Permissions":"Permissions","RequiredPermissionName":"Required Permission Name","RequiredPermissionNameTooltip":"The permission name that is required to use this workspace by clients. If not specified, the workspace can be used by any user.","Settings":"Settings","AIChat":"AI Chat","AIChat:TypeYourMessage":"Type your message...","AIChat:Send":"Send","AIChat:StreamResponse":"Stream response","AIChat:NoChats":"No chats. Click New to start.","ChatPlayground:NewChat":"New Chat","ChatPlayground:Chats":"Chats","Duplicate":"Duplicate","SuccessfullyDuplicated":"Successfully duplicated","AIManagement:WorkspaceProviderIsRequired":"Provider is required.","AIManagement:WorkspaceModelNameIsRequired":"Model name is required.","AIChat:NewLineHint":"Use \\u0060Shift\\u0060 \\u002B \\u0060Enter\\u0060 for a new line.","BackToWorkspaces":"Back to Workspaces","WorkspaceIdIsMissing":"Workspace id is missing.","AreYouSureYouWantToDeleteThisChat":"Are you sure you want to delete this chat?","ChatDeleted":"Chat is deleted successfully.","Permission:Workspaces.Consume":"Consume","Permission:Workspaces:Playground":"Playground","Permission:Workspaces:ManagePermissions":"Manage Permissions","ManagePermissions":"Manage Permissions","Copied":"Copied","CopyApiKey":"Copy API Key","Menu:WorkspaceDataSources":"Data Sources","DataSources":"Data Sources","ManageDataSources":"Manage Data Sources","NewDataSource":"New Data Source","UploadFile":"Upload File","FileName":"File Name","FileSize":"File Size","ContentType":"Content Type","IsProcessed":"Is Processed","ProcessedTime":"Processed Time","Status":"Status","Processing":"Processing","Failed":"Failed","Permission:WorkspaceDataSources":"Workspace Data Sources","Permission:WorkspaceDataSources:Create":"Upload","Permission:WorkspaceDataSources:Delete":"Delete","Permission:WorkspaceDataSources:Update":"Update","Permission:WorkspaceDataSources:Download":"Download","Permission:WorkspaceDataSources:ReIndex":"ReIndex","AreYouSureYouWantToDeleteThisDataSource":"Are you sure you want to delete this data source?","Download":"Download","ReIndex":"ReIndex","ReIndexAll":"ReIndex All","ReIndexingStarted":"Re-indexing started for {Count} document(s).","ReIndexingAllStarted":"Re-indexing started for all documents.","AreYouSureYouWantToReIndexThisDocument":"Are you sure you want to re-index this document?","AreYouSureYouWantToReIndexAllDocuments":"Are you sure you want to re-index all documents in this workspace?\\nThis action will take a while to complete.","AllowedFileTypes":"Allowed file types: {0}","MaxFileSize":"Maximum file size: {0}","AIManagement:InvalidFileType":"Invalid file type. Allowed types: {AllowedTypes}","AIManagement:FileSizeExceeded":"File size exceeds the maximum limit of {MaxSize}","UploadedSuccessfully":"File uploaded successfully","Yes":"Yes","No":"No","RAGConfiguration":"RAG Configuration","RAGConfigurationTooltip":"Configure Retrieval-Augmented Generation settings for this workspace. RAG allows the AI to use uploaded documents as context when responding to queries.","EmbedderConfiguration":"Embedder Configuration","EmbedderProvider":"Embedder Provider","SelectEmbedderProvider":"-- Select Embedder Provider --","EmbedderModelName":"Embedder Model Name","EmbedderApiKey":"Embedder API Key","EmbedderApiBaseUrl":"Embedder API Base URL","VectorStoreConfiguration":"Vector Store Configuration","VectorStoreProvider":"Vector Store Provider","SelectVectorStoreProvider":"-- Select Vector Store Provider --","VectorStoreSettings":"Vector Store Settings (JSON)","VectorStoreSettingsHint":"Enter provider-specific settings as JSON. For Pgvector, include ConnectionString. For Qdrant, enter the URL (e.g., http://localhost:6334).","EnableRAG":"Enable RAG","ProcessingStatus":"Processing Status","Processed":"Processed","Pending":"Pending","AIManagement:UnsupportedContentType":"Unsupported content type for document processing: {ContentType}","AIManagement:EmbedderProviderNotConfigured":"Embedder provider is not configured for workspace \\u0027{WorkspaceName}\\u0027 (ID: {WorkspaceId})","AIManagement:EmbedderProviderNotFound":"Embedder provider \\u0027{Provider}\\u0027 not found for workspace (ID: {WorkspaceId})","AIManagement:VectorStoreProviderNotConfigured":"Vector store provider is not configured for workspace \\u0027{WorkspaceName}\\u0027 (ID: {WorkspaceId})","AIManagement:VectorStoreProviderNotFound":"Vector store provider \\u0027{Provider}\\u0027 not found for workspace (ID: {WorkspaceId})","AIProvider":"AI Provider","Embedder":"Embedder","EmbedderTooltip":"Configure the embedder provider for generating document embeddings. This is required for RAG functionality.","VectorStore":"Vector Store","VectorStoreTooltip":"Configure the vector store for saving and searching document embeddings. Select a provider and enter connection settings.","AIManagement:MongoDbVectorStoreConnectionFailed":"MongoDB Vector Store connection failed. Error: {Message}","AIManagement:MongoDbVectorStoreInitializationFailed":"MongoDB Vector Store initialization failed. Error: {Message}","AIManagement:MongoDbVectorStoreOperationFailed":"MongoDB Vector Store operation failed. Error: {Message}","AIManagement:MongoDbVectorStoreSearchFailed":"MongoDB Vector Store search failed. Error: {Message}","AIManagement:PgvectorConnectionFailed":"PostgreSQL Vector Store connection failed. Error: {Message}","AIManagement:PgvectorInitializationFailed":"PostgreSQL Vector Store initialization failed. Error: {Message}","AIManagement:PgvectorStoreOperationFailed":"PostgreSQL Vector Store operation failed. Error: {Message}","AIManagement:PgvectorSearchFailed":"PostgreSQL Vector Store search failed. Error: {Message}","AIManagement:QdrantInitializationFailed":"Qdrant Vector Store initialization failed. Error: {Message}","AIManagement:QdrantOperationFailed":"Qdrant Vector Store operation failed. Error: {Message}","AIManagement:QdrantSearchFailed":"Qdrant Vector Store search failed. Error: {Message}","VectorStoreProvider:Qdrant":"Qdrant","VectorStoreProvider:MongoDB":"MongoDB","VectorStoreProvider:Pgvector":"Pgvector","PleaseCorrectTheErrorsFromAllTabsAndTryAgain":"Please correct the errors from all tabs and try again.","Refresh":"Refresh","Menu:McpServers":"MCP Servers","Permission:McpServers":"MCP Servers","Permission:McpServers:Create":"Create","Permission:McpServers:Update":"Update","Permission:McpServers:Delete":"Delete","McpServers":"MCP Servers","McpServerConnections":"MCP Server Connections","NewMcpServer":"New MCP Server","EditMcpServer":"Edit MCP Server","McpServer":"MCP Server","DisplayName":"Display Name","TransportType":"Transport Type","TransportType:Stdio":"Standard I/O (Stdio)","TransportType:Sse":"HTTP (Server-Sent Events)","TransportType:StreamableHttp":"HTTP (Streamable)","AuthType":"Authentication Type","AuthType:None":"None","AuthType:ApiKey":"API Key","AuthType:Bearer":"Bearer Token","AuthType:Custom":"Custom Header","Endpoint":"Endpoint URL","Command":"Command","Arguments":"Arguments","WorkingDirectory":"Working Directory","EnvironmentVariables":"Environment Variables","Headers":"HTTP Headers","CustomAuthHeaderName":"Custom Auth Header Name","CustomAuthHeaderValue":"Custom Auth Header Value","TransportConfiguration":"Transport Configuration","Authentication":"Authentication","ConnectedWorkspaces":"Connected Workspaces","ConnectToWorkspace":"Connect to Workspace","AddMcpServer":"Add MCP Server","DisconnectFromWorkspace":"Disconnect from Workspace","TestConnection":"Test Connection","ConnectionSuccessful":"Connection successful","ConnectionFailed":"Connection failed","AreYouSureYouWantToDeleteThisMcpServer":"Are you sure you want to delete this MCP server?","McpServerDeleted":"MCP server is deleted successfully.","McpServerCreated":"MCP server is created successfully.","McpServerUpdated":"MCP server is updated successfully.","SelectMcpServers":"Select MCP Servers","NoMcpServersConnected":"No MCP servers connected to this workspace.","AvailableMcpServers":"Available MCP Servers","ConnectedMcpServers":"Connected MCP Servers","McpServerConnectionTab":"MCP Servers","StdioTransportHelp":"Configure the command to execute the MCP server process. Arguments and environment variables are optional.","HttpTransportHelp":"Configure the HTTP endpoint URL for the MCP server. Authentication and custom headers are optional.","ArgumentsHelp":"Command-line arguments as a JSON array, e.g., [\\u0022--port\\u0022, \\u00228080\\u0022]","EnvironmentVariablesHelp":"Environment variables as a JSON object, e.g., {\\u0022KEY\\u0022: \\u0022value\\u0022}","HeadersHelp":"Custom HTTP headers as a JSON object, e.g., {\\u0022X-Custom-Header\\u0022: \\u0022value\\u0022}","AIManagement:McpServerNameAlreadyExists":"MCP server with name \\u0027{Name}\\u0027 already exists.","AIManagement:McpServerNotFound":"MCP server \\u0027{McpServerId}\\u0027 not found.","AIManagement:McpServerConnectionFailed":"Failed to connect to MCP server \\u0027{Name}\\u0027. Error: {Error}","AIManagement:McpServerConnectionTimeout":"Connection to MCP server timed out. Please check the server configuration and ensure the server is reachable.","AIManagement:McpServerAlreadyConnectedToWorkspace":"MCP server \\u0027{McpServerName}\\u0027 is already connected to workspace \\u0027{WorkspaceName}\\u0027.","AIManagement:McpServerNotConnectedToWorkspace":"MCP server \\u0027{McpServerName}\\u0027 is not connected to workspace \\u0027{WorkspaceName}\\u0027.","AIManagement:McpServerEndpointRequired":"Endpoint URL is required for HTTP transport.","AIManagement:McpServerCommandRequired":"Command is required for Stdio transport.","AIManagement:McpServerInvalidTransportConfiguration":"Invalid transport configuration for MCP server \\u0027{Name}\\u0027. Transport type: {TransportType}","AIManagement:McpServerToolExecutionFailed":"Failed to execute tool \\u0027{ToolName}\\u0027 on MCP server \\u0027{McpServerName}\\u0027. Error: {Error}","AIManagement:McpServerIsInactive":"MCP server \\u0027{Name}\\u0027 is not active.","SelectMcpServer":"Select MCP Server","SelectMcpServerDescription":"Click on an MCP server to select it, then click Connect. You can also double-click to connect immediately.","NoMcpServersAvailable":"No MCP servers available to connect.","McpServerConnected":"MCP server connected successfully.","McpServerDisconnected":"MCP server disconnected successfully.","FailedToConnectMcpServer":"Failed to connect MCP server.","AreYouSureYouWantToDisconnectThisMcpServer":"Are you sure you want to disconnect this MCP server from the workspace?","Connect":"Connect","Connecting":"Connecting...","Cancel":"Cancel","StdioConfiguration":"Stdio Configuration","HttpConfiguration":"HTTP Configuration","ArgumentsTooltip":"Command-line arguments as a JSON array, e.g., [\\u0022--port\\u0022, \\u00228080\\u0022]","EnvironmentVariablesTooltip":"Environment variables as a JSON object, e.g., {\\u0022KEY\\u0022: \\u0022value\\u0022}","HeadersTooltip":"Custom HTTP headers as a JSON object, e.g., {\\u0022X-Custom-Header\\u0022: \\u0022value\\u0022}","TestingConnection":"Testing connection","AvailableTools":"Available Tools","NoToolsAvailable":"No tools available on this MCP server.","ViewParameters":"View Parameters","HideParameters":"Hide Parameters","Loading":"Loading","Close":"Close","Enable":"Enable","Disable":"Disable","Enabled":"Enabled","Disabled":"Disabled","IsEnabled":"Enabled","Actions":"Actions","CreationTime":"Creation Time","Remove":"Remove","CopyFailed":"Failed to copy to clipboard","AIChat:UsageDetails":"Usage details","AIChat:TokenUsage":"Tokens: {0} input / {1} output / {2} total","AIChat:ToolCalls":"Tool calls","AIChat:Stop":"Stop","AIChat:Sources":"Sources"}	2026-04-29 11:54:25.163654	\N
3a20e9c3-7357-21aa-a5e1-041cbc1f1f91	AIManagement	ru	{"MyAccount":"Мой аккаунт","Menu:Workspaces":"Рабочие пространства","NewChatClientConfiguration":"Новая конфигурация чат‑клиента","Permission:AIManagement":"Управление ИИ","Permission:Workspaces":"Рабочие пространства","Permission:Workspaces:Create":"Создать","Permission:Workspaces:Delete":"Удалить","Permission:Workspaces:Update":"Обновить","BasicInformation":"Основная информация","Provider":"Поставщик","Name":"Название","Description":"Описание","ApiBaseUrl":"Базовый URL API","ModelName":"Название модели","ApiKey":"Ключ API","Temperature":"Температура","SystemPrompt":"Системная подсказка","IsActive":"Активно","IsSystem":"Системный","ChatPlayground":"Песочница чата","GoToChatPlayground":"Перейти в Playground","AreYouSureYouWantToDeleteThisWorkspace":"Вы уверены, что хотите удалить это рабочее пространство?","EditChatClientConfiguration":"Редактировать рабочее пространство","EditWorkspace":"Редактировать рабочее пространство","Menu:AIManagement":"Управление ИИ","ApplicationName":"Имя приложения","OverrideSystemConfiguration":"Переопределить системную конфигурацию","AIManagement:WorkspaceProviderNotFound":"Поставщик «{Provider}» не найден! Доступные поставщики: {AvailableProviders}","AIManagement:WorkspaceNameAlreadyExists":"Имя рабочего пространства «{Name}» уже существует.","AIManagement:CannotDeleteSystemWorkspace":"Нельзя удалить системное рабочее пространство.","AIManagement:WorkspaceInactive":"Рабочее пространство «{Name}» не активно. Поставщик: {Provider}","AIManagement:WorkspaceApiKeyMissing":"В рабочем пространстве «{Name}» нет ключа API. Поставщик: {Provider}","AIManagement:WorkspaceModelNameMissing":"В рабочем пространстве «{Name}» нет названия модели. Поставщик: {Provider}","AIManagement:OpenAIChatClientCreationFailed":"Не удалось создать чат‑клиент для рабочего пространства «{Name}». Поставщик: {Provider}","AIManagement:WorkspaceNameCannotContainSpaces":"Имя рабочего пространства не может содержать пробелы. Имя: «{Name}»","ProviderTooltip":"С провайдером OpenAI можно использовать любых других провайдеров, поддерживающих OpenAI API.","NewWorkspace":"Новое рабочее пространство","Permissions":"Разрешения","RequiredPermissionName":"Название требуемого разрешения","RequiredPermissionNameTooltip":"Название разрешения, требуемого для использования этого рабочего пространства клиентами. Если не указано, рабочее пространство доступно любому пользователю.","Settings":"Настройки","AIChat":"AI-чат","AIChat:TypeYourMessage":"Введите сообщение...","AIChat:Send":"Отправить","AIChat:StreamResponse":"Потоковый ответ","AIChat:NoChats":"Нет чатов. Нажмите «Новый», чтобы начать.","ChatPlayground:NewChat":"Новый чат","ChatPlayground:Chats":"Чаты","Duplicate":"Дублировать","SuccessfullyDuplicated":"Успешно продублировано","AIManagement:WorkspaceProviderIsRequired":"Поставщик обязателен.","AIManagement:WorkspaceModelNameIsRequired":"Название модели обязательно.","AIChat:NewLineHint":"Используйте \\u0060Shift\\u0060 \\u002B \\u0060Enter\\u0060 для новой строки.","BackToWorkspaces":"Назад к рабочим областям","WorkspaceIdIsMissing":"Идентификатор рабочего пространства отсутствует.","AreYouSureYouWantToDeleteThisChat":"Вы уверены, что хотите удалить этот чат?","ChatDeleted":"Чат успешно удален.","Permission:Workspaces.Consume":"Использовать","Permission:Workspaces:Playground":"Песочница","Permission:Workspaces:ManagePermissions":"Управление разрешениями","ManagePermissions":"Управление разрешениями","Copied":"Скопировано","CopyApiKey":"Скопировать ключ API","Menu:WorkspaceDataSources":"Источники данных","DataSources":"Источники данных","ManageDataSources":"Управление источниками данных","NewDataSource":"Новый источник данных","UploadFile":"Загрузить файл","FileName":"Имя файла","FileSize":"Размер файла","ContentType":"Тип содержимого","IsProcessed":"Обработано","ProcessedTime":"Время обработки","Status":"Статус","Processing":"Обработка","Failed":"Сбой","Permission:WorkspaceDataSources":"Источники данных рабочего пространства","Permission:WorkspaceDataSources:Create":"Загрузить","Permission:WorkspaceDataSources:Delete":"Удалить","Permission:WorkspaceDataSources:Update":"Обновить","Permission:WorkspaceDataSources:Download":"Скачать","Permission:WorkspaceDataSources:ReIndex":"Переиндексировать","AreYouSureYouWantToDeleteThisDataSource":"Вы уверены, что хотите удалить этот источник данных?","Download":"Скачать","ReIndex":"Переиндексировать","ReIndexAll":"Переиндексировать всё","ReIndexingStarted":"Переиндексация запущена для {Count} документа(ов).","ReIndexingAllStarted":"Переиндексация запущена для всех документов.","AreYouSureYouWantToReIndexThisDocument":"Вы уверены, что хотите переиндексировать этот документ?","AreYouSureYouWantToReIndexAllDocuments":"Вы уверены, что хотите переиндексировать все документы в этом рабочем пространстве?","AllowedFileTypes":"Разрешённые типы файлов: {0}","MaxFileSize":"Максимальный размер файла: {0}","AIManagement:InvalidFileType":"Недопустимый тип файла. Разрешённые типы: {AllowedTypes}","AIManagement:FileSizeExceeded":"Размер файла превышает максимальный лимит {MaxSize}","UploadedSuccessfully":"Файл успешно загружен","Yes":"Да","No":"Нет","RAGConfiguration":"Настройка RAG","RAGConfigurationTooltip":"Настройте параметры генерации с дополнением через поиск для этого рабочего пространства. RAG позволяет ИИ использовать загруженные документы как контекст при ответе на запросы.","EmbedderConfiguration":"Настройка встраивания","EmbedderProvider":"Поставщик встраивания","SelectEmbedderProvider":"-- Выберите поставщика встраивания --","EmbedderModelName":"Имя модели встраивания","EmbedderApiKey":"Ключ API встраивания","EmbedderApiBaseUrl":"Базовый URL API встраивания","VectorStoreConfiguration":"Настройка векторного хранилища","VectorStoreProvider":"Поставщик векторного хранилища","SelectVectorStoreProvider":"-- Выберите поставщика векторного хранилища --","VectorStoreSettings":"Настройки векторного хранилища (JSON)","VectorStoreSettingsHint":"Введите специфичные для поставщика настройки в формате JSON. Для Pgvector включите ConnectionString. Для Qdrant введите URL (например: http://localhost:6334).","EnableRAG":"Включить RAG","ProcessingStatus":"Статус обработки","Processed":"Обработано","Pending":"Ожидает","AIManagement:UnsupportedContentType":"Неподдерживаемый тип содержимого для обработки документа: {ContentType}","AIManagement:EmbedderProviderNotConfigured":"Поставщик встраивания не настроен для рабочего пространства \\u0027{WorkspaceName}\\u0027 (ID: {WorkspaceId})","AIManagement:EmbedderProviderNotFound":"Поставщик встраивания \\u0027{Provider}\\u0027 не найден для рабочего пространства (ID: {WorkspaceId})","AIManagement:VectorStoreProviderNotConfigured":"Поставщик векторного хранилища не настроен для рабочего пространства \\u0027{WorkspaceName}\\u0027 (ID: {WorkspaceId})","AIManagement:VectorStoreProviderNotFound":"Поставщик векторного хранилища \\u0027{Provider}\\u0027 не найден для рабочего пространства (ID: {WorkspaceId})","AIProvider":"Поставщик ИИ","Embedder":"Встраивание","EmbedderTooltip":"Настройте поставщика встраивания для генерации встраиваний документов. Это необходимо для функциональности RAG.","VectorStore":"Векторное хранилище","VectorStoreTooltip":"Настройте векторное хранилище для сохранения и поиска встраиваний документов. Выберите поставщика и введите параметры подключения.","AIManagement:MongoDbVectorStoreConnectionFailed":"Сбой подключения к векторному хранилищу MongoDB. Ошибка: {Message}","AIManagement:MongoDbVectorStoreInitializationFailed":"Сбой инициализации векторного хранилища MongoDB. Ошибка: {Message}","AIManagement:MongoDbVectorStoreOperationFailed":"Сбой операции векторного хранилища MongoDB. Ошибка: {Message}","AIManagement:MongoDbVectorStoreSearchFailed":"Сбой поиска в векторном хранилище MongoDB. Ошибка: {Message}","AIManagement:PgvectorConnectionFailed":"Сбой подключения к векторному хранилищу PostgreSQL. Ошибка: {Message}","AIManagement:PgvectorInitializationFailed":"Сбой инициализации векторного хранилища PostgreSQL. Ошибка: {Message}","AIManagement:PgvectorStoreOperationFailed":"Сбой операции векторного хранилища PostgreSQL. Ошибка: {Message}","AIManagement:PgvectorSearchFailed":"Сбой поиска в векторном хранилище PostgreSQL. Ошибка: {Message}","AIManagement:QdrantInitializationFailed":"Сбой инициализации векторного хранилища Qdrant. Ошибка: {Message}","AIManagement:QdrantOperationFailed":"Сбой операции векторного хранилища Qdrant. Ошибка: {Message}","AIManagement:QdrantSearchFailed":"Сбой поиска в векторном хранилище Qdrant. Ошибка: {Message}","VectorStoreProvider:Qdrant":"Qdrant","VectorStoreProvider:MongoDB":"MongoDB","VectorStoreProvider:Pgvector":"Pgvector","PleaseCorrectTheErrorsFromAllTabsAndTryAgain":"Пожалуйста, исправьте ошибки на всех вкладках и попробуйте снова.","Menu:McpServers":"MCP Servers","Permission:McpServers":"MCP Servers","Permission:McpServers:Create":"Create","Permission:McpServers:Update":"Update","Permission:McpServers:Delete":"Delete","McpServers":"MCP Servers","NewMcpServer":"New MCP Server","EditMcpServer":"Edit MCP Server","McpServer":"MCP Server","DisplayName":"Display Name","TransportType":"Transport Type","TransportType:Stdio":"Standard I/O (Stdio)","TransportType:Sse":"HTTP (Server-Sent Events)","TransportType:StreamableHttp":"HTTP (Streamable)","AuthType":"Authentication Type","AuthType:None":"None","AuthType:ApiKey":"API Key","AuthType:Bearer":"Bearer Token","AuthType:Custom":"Custom Header","Endpoint":"Endpoint URL","Command":"Command","Arguments":"Arguments","WorkingDirectory":"Working Directory","EnvironmentVariables":"Environment Variables","Headers":"HTTP Headers","CustomAuthHeaderName":"Custom Auth Header Name","CustomAuthHeaderValue":"Custom Auth Header Value","TransportConfiguration":"Transport Configuration","Authentication":"Authentication","ConnectedWorkspaces":"Connected Workspaces","ConnectToWorkspace":"Connect to Workspace","AddMcpServer":"Add MCP Server","DisconnectFromWorkspace":"Disconnect from Workspace","TestConnection":"Test Connection","ConnectionSuccessful":"Connection successful","ConnectionFailed":"Connection failed","AreYouSureYouWantToDeleteThisMcpServer":"Are you sure you want to delete this MCP server?","McpServerDeleted":"MCP server is deleted successfully.","McpServerCreated":"MCP server is created successfully.","McpServerUpdated":"MCP server is updated successfully.","SelectMcpServers":"Select MCP Servers","NoMcpServersConnected":"No MCP servers connected to this workspace.","AvailableMcpServers":"Available MCP Servers","ConnectedMcpServers":"Connected MCP Servers","McpServerConnectionTab":"MCP Servers","StdioTransportHelp":"Configure the command to execute the MCP server process. Arguments and environment variables are optional.","HttpTransportHelp":"Configure the HTTP endpoint URL for the MCP server. Authentication and custom headers are optional.","ArgumentsHelp":"Command-line arguments as a JSON array, e.g., [\\u0022--port\\u0022, \\u00228080\\u0022]","EnvironmentVariablesHelp":"Environment variables as a JSON object, e.g., {\\u0022KEY\\u0022: \\u0022value\\u0022}","HeadersHelp":"Custom HTTP headers as a JSON object, e.g., {\\u0022X-Custom-Header\\u0022: \\u0022value\\u0022}","AIManagement:McpServerNameAlreadyExists":"MCP server with name \\u0027{Name}\\u0027 already exists.","AIManagement:McpServerNotFound":"MCP server \\u0027{McpServerId}\\u0027 not found.","AIManagement:McpServerConnectionFailed":"Failed to connect to MCP server \\u0027{Name}\\u0027. Error: {Error}","AIManagement:McpServerAlreadyConnectedToWorkspace":"MCP server \\u0027{McpServerName}\\u0027 is already connected to workspace \\u0027{WorkspaceName}\\u0027.","AIManagement:McpServerNotConnectedToWorkspace":"MCP server \\u0027{McpServerName}\\u0027 is not connected to workspace \\u0027{WorkspaceName}\\u0027.","AIManagement:McpServerEndpointRequired":"Endpoint URL is required for HTTP transport.","AIManagement:McpServerCommandRequired":"Command is required for Stdio transport.","AIManagement:McpServerInvalidTransportConfiguration":"Invalid transport configuration for MCP server \\u0027{Name}\\u0027. Transport type: {TransportType}","AIManagement:McpServerToolExecutionFailed":"Failed to execute tool \\u0027{ToolName}\\u0027 on MCP server \\u0027{McpServerName}\\u0027. Error: {Error}","AIManagement:McpServerIsInactive":"MCP server \\u0027{Name}\\u0027 is not active.","SelectMcpServer":"Select MCP Server","SelectMcpServerDescription":"Click on an MCP server to select it, then click Connect. You can also double-click to connect immediately.","NoMcpServersAvailable":"No MCP servers available to connect.","McpServerConnected":"MCP server connected successfully.","McpServerDisconnected":"MCP server disconnected successfully.","FailedToConnectMcpServer":"Failed to connect MCP server.","AreYouSureYouWantToDisconnectThisMcpServer":"Are you sure you want to disconnect this MCP server from the workspace?","Connect":"Connect","Connecting":"Connecting...","Cancel":"Cancel","StdioConfiguration":"Stdio Configuration","HttpConfiguration":"HTTP Configuration","ArgumentsTooltip":"Command-line arguments as a JSON array, e.g., [\\u0022--port\\u0022, \\u00228080\\u0022]","EnvironmentVariablesTooltip":"Environment variables as a JSON object, e.g., {\\u0022KEY\\u0022: \\u0022value\\u0022}","HeadersTooltip":"Custom HTTP headers as a JSON object, e.g., {\\u0022X-Custom-Header\\u0022: \\u0022value\\u0022}","TestingConnection":"Testing connection","AvailableTools":"Available Tools","NoToolsAvailable":"No tools available on this MCP server.","ViewParameters":"View Parameters","HideParameters":"Hide Parameters","Loading":"Loading","Close":"Close","Enable":"Enable","Disable":"Disable","Enabled":"Включено","Disabled":"Отключено","IsEnabled":"Enabled","Actions":"Actions","CreationTime":"Creation Time","Remove":"Удалить","AIChat:UsageDetails":"Сведения об использовании","AIChat:TokenUsage":"Токены: {0} ввод / {1} вывод / {2} всего","AIChat:ToolCalls":"Вызовы инструментов","AIChat:Stop":"Остановить"}	2026-04-29 11:54:25.16402	\N
3a20e9c3-735c-a824-0cda-e11b52cdfa4e	AIManagement	vi	{"MyAccount":"Tài khoản của tôi","Menu:Workspaces":"Không gian làm việc","NewChatClientConfiguration":"Cấu hình khách hàng trò chuyện mới","Permission:AIManagement":"Quản lý AI","Permission:Workspaces":"Không gian làm việc","Permission:Workspaces:Create":"Tạo","Permission:Workspaces:Delete":"Xóa","Permission:Workspaces:Update":"Cập nhật","BasicInformation":"Thông tin cơ bản","Provider":"Nhà cung cấp","Name":"Tên","Description":"Mô tả","ApiBaseUrl":"URL cơ sở API","ModelName":"Tên mô hình","ApiKey":"Khóa API","Temperature":"Nhiệt độ","SystemPrompt":"Lời nhắc hệ thống","IsActive":"Kích hoạt","IsSystem":"Hệ thống","ChatPlayground":"Sân chơi trò chuyện","GoToChatPlayground":"Đi tới Playground","AreYouSureYouWantToDeleteThisWorkspace":"Bạn có chắc chắn muốn xóa không gian làm việc này không?","EditChatClientConfiguration":"Chỉnh sửa không gian làm việc","EditWorkspace":"Chỉnh sửa không gian làm việc","Menu:AIManagement":"Quản lý AI","ApplicationName":"Tên ứng dụng","OverrideSystemConfiguration":"Ghi đè cấu hình hệ thống","AIManagement:WorkspaceProviderNotFound":"Không tìm thấy nhà cung cấp \\u0027{Provider}\\u0027! Nhà cung cấp khả dụng: {AvailableProviders}","AIManagement:WorkspaceNameAlreadyExists":"Tên không gian làm việc \\u0027{Name}\\u0027 đã tồn tại.","AIManagement:CannotDeleteSystemWorkspace":"Không thể xóa không gian làm việc hệ thống.","AIManagement:WorkspaceInactive":"Không gian làm việc \\u0027{Name}\\u0027 không hoạt động. Nhà cung cấp: {Provider}","AIManagement:WorkspaceApiKeyMissing":"Không gian làm việc \\u0027{Name}\\u0027 không có khóa API. Nhà cung cấp: {Provider}","AIManagement:WorkspaceModelNameMissing":"Không gian làm việc \\u0027{Name}\\u0027 không có tên mô hình. Nhà cung cấp: {Provider}","AIManagement:OpenAIChatClientCreationFailed":"Không thể tạo khách hàng trò chuyện cho không gian làm việc \\u0027{Name}\\u0027. Nhà cung cấp: {Provider}","AIManagement:WorkspaceNameCannotContainSpaces":"Tên không gian làm việc không được chứa khoảng trắng. Tên: \\u0027{Name}\\u0027","ProviderTooltip":"Với nhà cung cấp OpenAI, bạn có thể dùng bất kỳ nhà cung cấp nào khác hỗ trợ OpenAI API.","NewWorkspace":"Không gian làm việc mới","Permissions":"Quyền","RequiredPermissionName":"Tên quyền bắt buộc","RequiredPermissionNameTooltip":"Tên quyền mà khách hàng cần để dùng không gian làm việc này. Nếu không chỉ định, mọi người dùng đều có thể sử dụng.","Settings":"Cài đặt","AIChat":"Trò chuyện AI","AIChat:TypeYourMessage":"Nhập tin nhắn của bạn...","AIChat:Send":"Gửi","AIChat:StreamResponse":"Phản hồi dạng luồng","AIChat:NoChats":"Chưa có cuộc trò chuyện. Nhấp Mới để bắt đầu.","ChatPlayground:NewChat":"Cuộc trò chuyện mới","ChatPlayground:Chats":"Cuộc trò chuyện","Duplicate":"Nhân bản","SuccessfullyDuplicated":"Nhân bản thành công","AIManagement:WorkspaceProviderIsRequired":"Nhà cung cấp là bắt buộc.","AIManagement:WorkspaceModelNameIsRequired":"Tên mô hình là bắt buộc.","AIChat:NewLineHint":"Sử dụng \\u0060Shift\\u0060 \\u002B \\u0060Enter\\u0060 để xuống dòng.","BackToWorkspaces":"Quay lại không gian làm việc","WorkspaceIdIsMissing":"Thiếu ID không gian làm việc.","AreYouSureYouWantToDeleteThisChat":"Bạn có chắc chắn muốn xóa cuộc trò chuyện này không?","ChatDeleted":"Cuộc trò chuyện đã được xóa thành công.","Permission:Workspaces.Consume":"Sử dụng","Permission:Workspaces:Playground":"Playground","Permission:Workspaces:ManagePermissions":"Quản lý quyền","ManagePermissions":"Quản lý quyền","Copied":"Đã sao chép","CopyApiKey":"Sao chép khóa API","Menu:WorkspaceDataSources":"Nguồn dữ liệu","DataSources":"Nguồn dữ liệu","ManageDataSources":"Quản lý nguồn dữ liệu","NewDataSource":"Nguồn dữ liệu mới","UploadFile":"Tải lên tệp","FileName":"Tên tệp","FileSize":"Kích thước tệp","ContentType":"Loại nội dung","IsProcessed":"Đã xử lý","ProcessedTime":"Thời gian xử lý","Status":"Trạng thái","Processing":"Đang xử lý","Failed":"Thất bại","Permission:WorkspaceDataSources":"Nguồn dữ liệu không gian làm việc","Permission:WorkspaceDataSources:Create":"Tải lên","Permission:WorkspaceDataSources:Delete":"Xóa","Permission:WorkspaceDataSources:Update":"Cập nhật","Permission:WorkspaceDataSources:Download":"Tải xuống","Permission:WorkspaceDataSources:ReIndex":"Lập chỉ mục lại","AreYouSureYouWantToDeleteThisDataSource":"Bạn có chắc chắn muốn xóa nguồn dữ liệu này không?","Download":"Tải xuống","ReIndex":"Lập chỉ mục lại","ReIndexAll":"Lập chỉ mục lại tất cả","ReIndexingStarted":"Đã bắt đầu lập chỉ mục lại cho {Count} tài liệu.","ReIndexingAllStarted":"Đã bắt đầu lập chỉ mục lại cho tất cả tài liệu.","AreYouSureYouWantToReIndexThisDocument":"Bạn có chắc chắn muốn lập chỉ mục lại tài liệu này không?","AreYouSureYouWantToReIndexAllDocuments":"Bạn có chắc chắn muốn lập chỉ mục lại tất cả tài liệu trong không gian làm việc này không?","AllowedFileTypes":"Các loại tệp được phép: {0}","MaxFileSize":"Kích thước tệp tối đa: {0}","AIManagement:InvalidFileType":"Loại tệp không hợp lệ. Các loại được phép: {AllowedTypes}","AIManagement:FileSizeExceeded":"Kích thước tệp vượt quá giới hạn tối đa {MaxSize}","UploadedSuccessfully":"Đã tải lên tệp thành công","Yes":"Có","No":"Không","RAGConfiguration":"Cấu hình RAG","RAGConfigurationTooltip":"Cấu hình cài đặt Tăng cường Truy xuất cho không gian làm việc này. RAG cho phép AI sử dụng các tài liệu đã tải lên làm ngữ cảnh khi phản hồi truy vấn.","EmbedderConfiguration":"Cấu hình bộ nhúng","EmbedderProvider":"Nhà cung cấp bộ nhúng","SelectEmbedderProvider":"-- Chọn nhà cung cấp bộ nhúng --","EmbedderModelName":"Tên mô hình bộ nhúng","EmbedderApiKey":"Khóa API bộ nhúng","EmbedderApiBaseUrl":"URL cơ sở API bộ nhúng","VectorStoreConfiguration":"Cấu hình kho vectơ","VectorStoreProvider":"Nhà cung cấp kho vectơ","SelectVectorStoreProvider":"-- Chọn nhà cung cấp kho vectơ --","VectorStoreSettings":"Cài đặt kho vectơ (JSON)","VectorStoreSettingsHint":"Nhập cài đặt cụ thể của nhà cung cấp ở định dạng JSON. Đối với Pgvector, bao gồm ConnectionString. Đối với Qdrant, nhập URL (ví dụ: http://localhost:6334).","EnableRAG":"Bật RAG","ProcessingStatus":"Trạng thái xử lý","Processed":"Đã xử lý","Pending":"Đang chờ","AIManagement:UnsupportedContentType":"Loại nội dung không được hỗ trợ để xử lý tài liệu: {ContentType}","AIManagement:EmbedderProviderNotConfigured":"Nhà cung cấp bộ nhúng không được cấu hình cho không gian làm việc \\u0027{WorkspaceName}\\u0027 (ID: {WorkspaceId})","AIManagement:EmbedderProviderNotFound":"Không tìm thấy nhà cung cấp bộ nhúng \\u0027{Provider}\\u0027 cho không gian làm việc (ID: {WorkspaceId})","AIManagement:VectorStoreProviderNotConfigured":"Nhà cung cấp kho vectơ không được cấu hình cho không gian làm việc \\u0027{WorkspaceName}\\u0027 (ID: {WorkspaceId})","AIManagement:VectorStoreProviderNotFound":"Không tìm thấy nhà cung cấp kho vectơ \\u0027{Provider}\\u0027 cho không gian làm việc (ID: {WorkspaceId})","AIProvider":"Nhà cung cấp AI","Embedder":"Bộ nhúng","EmbedderTooltip":"Cấu hình nhà cung cấp bộ nhúng để tạo nhúng tài liệu. Điều này được yêu cầu cho chức năng RAG.","VectorStore":"Kho vectơ","VectorStoreTooltip":"Cấu hình kho vectơ để lưu và tìm kiếm nhúng tài liệu. Chọn nhà cung cấp và nhập cài đặt kết nối.","AIManagement:MongoDbVectorStoreConnectionFailed":"Kết nối kho vectơ MongoDB thất bại. Lỗi: {Message}","AIManagement:MongoDbVectorStoreInitializationFailed":"Khởi tạo kho vectơ MongoDB thất bại. Lỗi: {Message}","AIManagement:MongoDbVectorStoreOperationFailed":"Hoạt động kho vectơ MongoDB thất bại. Lỗi: {Message}","AIManagement:MongoDbVectorStoreSearchFailed":"Tìm kiếm kho vectơ MongoDB thất bại. Lỗi: {Message}","AIManagement:PgvectorConnectionFailed":"Kết nối kho vectơ PostgreSQL thất bại. Lỗi: {Message}","AIManagement:PgvectorInitializationFailed":"Khởi tạo kho vectơ PostgreSQL thất bại. Lỗi: {Message}","AIManagement:PgvectorStoreOperationFailed":"Hoạt động kho vectơ PostgreSQL thất bại. Lỗi: {Message}","AIManagement:PgvectorSearchFailed":"Tìm kiếm kho vectơ PostgreSQL thất bại. Lỗi: {Message}","AIManagement:QdrantInitializationFailed":"Khởi tạo kho vectơ Qdrant thất bại. Lỗi: {Message}","AIManagement:QdrantOperationFailed":"Hoạt động kho vectơ Qdrant thất bại. Lỗi: {Message}","AIManagement:QdrantSearchFailed":"Tìm kiếm kho vectơ Qdrant thất bại. Lỗi: {Message}","VectorStoreProvider:Qdrant":"Qdrant","VectorStoreProvider:MongoDB":"MongoDB","VectorStoreProvider:Pgvector":"Pgvector","PleaseCorrectTheErrorsFromAllTabsAndTryAgain":"Vui lòng sửa các lỗi từ tất cả các thẻ và thử lại.","Menu:McpServers":"MCP Servers","Permission:McpServers":"MCP Servers","Permission:McpServers:Create":"Create","Permission:McpServers:Update":"Update","Permission:McpServers:Delete":"Delete","McpServers":"MCP Servers","NewMcpServer":"New MCP Server","EditMcpServer":"Edit MCP Server","McpServer":"MCP Server","DisplayName":"Display Name","TransportType":"Transport Type","TransportType:Stdio":"Standard I/O (Stdio)","TransportType:Sse":"HTTP (Server-Sent Events)","TransportType:StreamableHttp":"HTTP (Streamable)","AuthType":"Authentication Type","AuthType:None":"None","AuthType:ApiKey":"API Key","AuthType:Bearer":"Bearer Token","AuthType:Custom":"Custom Header","Endpoint":"Endpoint URL","Command":"Command","Arguments":"Arguments","WorkingDirectory":"Working Directory","EnvironmentVariables":"Environment Variables","Headers":"HTTP Headers","CustomAuthHeaderName":"Custom Auth Header Name","CustomAuthHeaderValue":"Custom Auth Header Value","TransportConfiguration":"Transport Configuration","Authentication":"Authentication","ConnectedWorkspaces":"Connected Workspaces","ConnectToWorkspace":"Connect to Workspace","AddMcpServer":"Add MCP Server","DisconnectFromWorkspace":"Disconnect from Workspace","TestConnection":"Test Connection","ConnectionSuccessful":"Connection successful","ConnectionFailed":"Connection failed","AreYouSureYouWantToDeleteThisMcpServer":"Are you sure you want to delete this MCP server?","McpServerDeleted":"MCP server is deleted successfully.","McpServerCreated":"MCP server is created successfully.","McpServerUpdated":"MCP server is updated successfully.","SelectMcpServers":"Select MCP Servers","NoMcpServersConnected":"No MCP servers connected to this workspace.","AvailableMcpServers":"Available MCP Servers","ConnectedMcpServers":"Connected MCP Servers","McpServerConnectionTab":"MCP Servers","StdioTransportHelp":"Configure the command to execute the MCP server process. Arguments and environment variables are optional.","HttpTransportHelp":"Configure the HTTP endpoint URL for the MCP server. Authentication and custom headers are optional.","ArgumentsHelp":"Command-line arguments as a JSON array, e.g., [\\u0022--port\\u0022, \\u00228080\\u0022]","EnvironmentVariablesHelp":"Environment variables as a JSON object, e.g., {\\u0022KEY\\u0022: \\u0022value\\u0022}","HeadersHelp":"Custom HTTP headers as a JSON object, e.g., {\\u0022X-Custom-Header\\u0022: \\u0022value\\u0022}","AIManagement:McpServerNameAlreadyExists":"MCP server with name \\u0027{Name}\\u0027 already exists.","AIManagement:McpServerNotFound":"MCP server \\u0027{McpServerId}\\u0027 not found.","AIManagement:McpServerConnectionFailed":"Failed to connect to MCP server \\u0027{Name}\\u0027. Error: {Error}","AIManagement:McpServerAlreadyConnectedToWorkspace":"MCP server \\u0027{McpServerName}\\u0027 is already connected to workspace \\u0027{WorkspaceName}\\u0027.","AIManagement:McpServerNotConnectedToWorkspace":"MCP server \\u0027{McpServerName}\\u0027 is not connected to workspace \\u0027{WorkspaceName}\\u0027.","AIManagement:McpServerEndpointRequired":"Endpoint URL is required for HTTP transport.","AIManagement:McpServerCommandRequired":"Command is required for Stdio transport.","AIManagement:McpServerInvalidTransportConfiguration":"Invalid transport configuration for MCP server \\u0027{Name}\\u0027. Transport type: {TransportType}","AIManagement:McpServerToolExecutionFailed":"Failed to execute tool \\u0027{ToolName}\\u0027 on MCP server \\u0027{McpServerName}\\u0027. Error: {Error}","AIManagement:McpServerIsInactive":"MCP server \\u0027{Name}\\u0027 is not active.","SelectMcpServer":"Select MCP Server","SelectMcpServerDescription":"Click on an MCP server to select it, then click Connect. You can also double-click to connect immediately.","NoMcpServersAvailable":"No MCP servers available to connect.","McpServerConnected":"MCP server connected successfully.","McpServerDisconnected":"MCP server disconnected successfully.","FailedToConnectMcpServer":"Failed to connect MCP server.","AreYouSureYouWantToDisconnectThisMcpServer":"Are you sure you want to disconnect this MCP server from the workspace?","Connect":"Connect","Connecting":"Connecting...","Cancel":"Cancel","StdioConfiguration":"Stdio Configuration","HttpConfiguration":"HTTP Configuration","ArgumentsTooltip":"Command-line arguments as a JSON array, e.g., [\\u0022--port\\u0022, \\u00228080\\u0022]","EnvironmentVariablesTooltip":"Environment variables as a JSON object, e.g., {\\u0022KEY\\u0022: \\u0022value\\u0022}","HeadersTooltip":"Custom HTTP headers as a JSON object, e.g., {\\u0022X-Custom-Header\\u0022: \\u0022value\\u0022}","TestingConnection":"Testing connection","AvailableTools":"Available Tools","NoToolsAvailable":"No tools available on this MCP server.","ViewParameters":"View Parameters","HideParameters":"Hide Parameters","Loading":"Loading","Close":"Close","Enable":"Enable","Disable":"Disable","Enabled":"Đã bật","Disabled":"Đã tắt","IsEnabled":"Enabled","Actions":"Actions","CreationTime":"Creation Time","Remove":"Xóa","AIChat:UsageDetails":"Chi tiết sử dụng","AIChat:TokenUsage":"Token: {0} đầu vào / {1} đầu ra / {2} tổng cộng","AIChat:ToolCalls":"Lời gọi công cụ","AIChat:Stop":"Dừng"}	2026-04-29 11:54:25.164465	\N
3a20e9c3-7362-7a28-6213-9cd74f21ca47	AIManagement	zh-Hans	{"MyAccount":"我的账户","Menu:Workspaces":"工作区","NewChatClientConfiguration":"新的聊天客户端配置","Permission:AIManagement":"人工智能管理","Permission:Workspaces":"工作区","Permission:Workspaces:Create":"创建","Permission:Workspaces:Delete":"删除","Permission:Workspaces:Update":"更新","BasicInformation":"基本信息","Provider":"提供商","Name":"名称","Description":"描述","ApiBaseUrl":"API 基础地址","ModelName":"模型名称","ApiKey":"API 密钥","Temperature":"温度","SystemPrompt":"系统提示","IsActive":"启用","IsSystem":"系统","ChatPlayground":"聊天演示场","GoToChatPlayground":"前往 Playground","AreYouSureYouWantToDeleteThisWorkspace":"确定要删除此工作区吗？","EditChatClientConfiguration":"编辑工作区","EditWorkspace":"编辑工作区","Menu:AIManagement":"AI 管理","ApplicationName":"应用名称","OverrideSystemConfiguration":"覆盖系统配置","AIManagement:WorkspaceProviderNotFound":"未找到提供商“{Provider}”！可用提供商：{AvailableProviders}","AIManagement:WorkspaceNameAlreadyExists":"工作区名称“{Name}”已存在。","AIManagement:CannotDeleteSystemWorkspace":"无法删除系统工作区。","AIManagement:WorkspaceInactive":"工作区“{Name}”未启用。提供商：{Provider}","AIManagement:WorkspaceApiKeyMissing":"工作区“{Name}”没有 API 密钥。提供商：{Provider}","AIManagement:WorkspaceModelNameMissing":"工作区“{Name}”没有模型名称。提供商：{Provider}","AIManagement:OpenAIChatClientCreationFailed":"无法为工作区“{Name}”创建聊天客户端。提供商：{Provider}","AIManagement:WorkspaceNameCannotContainSpaces":"工作区名称不能包含空格。名称：“{Name}”","ProviderTooltip":"使用 OpenAI 提供商时，你可以使用任何支持 OpenAI API 的其他提供商。","NewWorkspace":"新建工作区","Permissions":"权限","RequiredPermissionName":"所需权限名称","RequiredPermissionNameTooltip":"客户端使用此工作区所需的权限名。如未指定，任何用户都可使用该工作区。","Settings":"设置","AIChat":"AI 聊天","AIChat:TypeYourMessage":"输入你的消息...","AIChat:Send":"发送","AIChat:StreamResponse":"流式响应","AIChat:NoChats":"暂无聊天。点击新建开始。","ChatPlayground:NewChat":"新建聊天","ChatPlayground:Chats":"聊天","Duplicate":"复制","SuccessfullyDuplicated":"成功复制","AIManagement:WorkspaceProviderIsRequired":"提供商为必填项。","AIManagement:WorkspaceModelNameIsRequired":"模型名称为必填项。","AIChat:NewLineHint":"使用 \\u0060Shift\\u0060 \\u002B \\u0060Enter\\u0060 换行。","BackToWorkspaces":"返回工作区","WorkspaceIdIsMissing":"工作区 ID 缺失。","AreYouSureYouWantToDeleteThisChat":"确定要删除此聊天吗？","ChatDeleted":"聊天已成功删除。","Permission:Workspaces.Consume":"使用","Permission:Workspaces:Playground":"演示场","Permission:Workspaces:ManagePermissions":"管理权限","ManagePermissions":"管理权限","Copied":"已复制","CopyApiKey":"复制 API 密钥","Menu:WorkspaceDataSources":"数据源","DataSources":"数据源","ManageDataSources":"管理数据源","NewDataSource":"新建数据源","UploadFile":"上传文件","FileName":"文件名","FileSize":"文件大小","ContentType":"内容类型","IsProcessed":"已处理","ProcessedTime":"处理时间","Status":"状态","Processing":"处理中","Failed":"失败","Permission:WorkspaceDataSources":"工作区数据源","Permission:WorkspaceDataSources:Create":"上传","Permission:WorkspaceDataSources:Delete":"删除","Permission:WorkspaceDataSources:Update":"更新","Permission:WorkspaceDataSources:Download":"下载","Permission:WorkspaceDataSources:ReIndex":"重新索引","AreYouSureYouWantToDeleteThisDataSource":"确定要删除此数据源吗？","Download":"下载","ReIndex":"重新索引","ReIndexAll":"全部重新索引","ReIndexingStarted":"已开始为 {Count} 个文档重新索引。","ReIndexingAllStarted":"已开始为所有文档重新索引。","AreYouSureYouWantToReIndexThisDocument":"确定要重新索引此文档吗？","AreYouSureYouWantToReIndexAllDocuments":"确定要重新索引此工作区中的所有文档吗？","AllowedFileTypes":"允许的文件类型：{0}","MaxFileSize":"最大文件大小：{0}","AIManagement:InvalidFileType":"无效的文件类型。允许的类型：{AllowedTypes}","AIManagement:FileSizeExceeded":"文件大小超出最大限制 {MaxSize}","UploadedSuccessfully":"文件上传成功","Yes":"是","No":"否","RAGConfiguration":"RAG 配置","RAGConfigurationTooltip":"为此工作区配置检索增强生成设置。RAG 允许 AI 在响应查询时使用上传的文档作为上下文。","EmbedderConfiguration":"嵌入器配置","EmbedderProvider":"嵌入器提供商","SelectEmbedderProvider":"-- 选择嵌入器提供商 --","EmbedderModelName":"嵌入器模型名称","EmbedderApiKey":"嵌入器 API 密钥","EmbedderApiBaseUrl":"嵌入器 API 基础地址","VectorStoreConfiguration":"向量存储配置","VectorStoreProvider":"向量存储提供商","SelectVectorStoreProvider":"-- 选择向量存储提供商 --","VectorStoreSettings":"向量存储设置 (JSON)","VectorStoreSettingsHint":"以 JSON 格式输入提供商特定的设置。对于 Pgvector，请包含 ConnectionString。对于 Qdrant，请输入 URL（例如：http://localhost:6334）。","EnableRAG":"启用 RAG","ProcessingStatus":"处理状态","Processed":"已处理","Pending":"待处理","AIManagement:UnsupportedContentType":"文档处理不支持的内容类型：{ContentType}","AIManagement:EmbedderProviderNotConfigured":"工作区 \\u0027{WorkspaceName}\\u0027 (ID: {WorkspaceId}) 未配置嵌入器提供商","AIManagement:EmbedderProviderNotFound":"未找到工作区 (ID: {WorkspaceId}) 的嵌入器提供商 \\u0027{Provider}\\u0027","AIManagement:VectorStoreProviderNotConfigured":"工作区 \\u0027{WorkspaceName}\\u0027 (ID: {WorkspaceId}) 未配置向量存储提供商","AIManagement:VectorStoreProviderNotFound":"未找到工作区 (ID: {WorkspaceId}) 的向量存储提供商 \\u0027{Provider}\\u0027","AIProvider":"AI 提供商","Embedder":"嵌入器","EmbedderTooltip":"配置嵌入器提供商以生成文档嵌入。这是 RAG 功能所必需的。","VectorStore":"向量存储","VectorStoreTooltip":"配置向量存储以保存和搜索文档嵌入。选择提供商并输入连接设置。","AIManagement:MongoDbVectorStoreConnectionFailed":"MongoDB 向量存储连接失败。错误：{Message}","AIManagement:MongoDbVectorStoreInitializationFailed":"MongoDB 向量存储初始化失败。错误：{Message}","AIManagement:MongoDbVectorStoreOperationFailed":"MongoDB 向量存储操作失败。错误：{Message}","AIManagement:MongoDbVectorStoreSearchFailed":"MongoDB 向量存储搜索失败。错误：{Message}","AIManagement:PgvectorConnectionFailed":"PostgreSQL 向量存储连接失败。错误：{Message}","AIManagement:PgvectorInitializationFailed":"PostgreSQL 向量存储初始化失败。错误：{Message}","AIManagement:PgvectorStoreOperationFailed":"PostgreSQL 向量存储操作失败。错误：{Message}","AIManagement:PgvectorSearchFailed":"PostgreSQL 向量存储搜索失败。错误：{Message}","AIManagement:QdrantInitializationFailed":"Qdrant 向量存储初始化失败。错误：{Message}","AIManagement:QdrantOperationFailed":"Qdrant 向量存储操作失败。错误：{Message}","AIManagement:QdrantSearchFailed":"Qdrant 向量存储搜索失败。错误：{Message}","VectorStoreProvider:Qdrant":"Qdrant","VectorStoreProvider:MongoDB":"MongoDB","VectorStoreProvider:Pgvector":"Pgvector","PleaseCorrectTheErrorsFromAllTabsAndTryAgain":"请修正所有标签页中的错误并重试。","Menu:McpServers":"MCP 服务器","Permission:McpServers":"MCP 服务器","Permission:McpServers:Create":"创建","Permission:McpServers:Update":"更新","Permission:McpServers:Delete":"删除","McpServers":"MCP 服务器","NewMcpServer":"新建 MCP 服务器","EditMcpServer":"编辑 MCP 服务器","McpServer":"MCP 服务器","DisplayName":"显示名称","TransportType":"传输类型","TransportType:Stdio":"标准输入/输出 (Stdio)","TransportType:Sse":"HTTP (Server-Sent Events)","TransportType:StreamableHttp":"HTTP (Streamable)","AuthType":"认证类型","AuthType:None":"无","AuthType:ApiKey":"API 密钥","AuthType:Bearer":"Bearer Token","AuthType:Custom":"自定义头","Endpoint":"端点 URL","Command":"命令","Arguments":"参数","WorkingDirectory":"工作目录","EnvironmentVariables":"环境变量","Headers":"HTTP 头","CustomAuthHeaderName":"自定义认证头名称","CustomAuthHeaderValue":"自定义认证头值","TransportConfiguration":"传输配置","Authentication":"认证","ConnectedWorkspaces":"已连接的工作区","ConnectToWorkspace":"连接到工作区","AddMcpServer":"添加 MCP 服务器","DisconnectFromWorkspace":"从工作区断开","TestConnection":"测试连接","ConnectionSuccessful":"连接成功","ConnectionFailed":"连接失败","AreYouSureYouWantToDeleteThisMcpServer":"确定要删除此 MCP 服务器吗？","McpServerDeleted":"MCP 服务器已成功删除。","McpServerCreated":"MCP 服务器已成功创建。","McpServerUpdated":"MCP 服务器已成功更新。","SelectMcpServers":"选择 MCP 服务器","NoMcpServersConnected":"此工作区没有连接的 MCP 服务器。","AvailableMcpServers":"可用的 MCP 服务器","ConnectedMcpServers":"已连接的 MCP 服务器","McpServerConnectionTab":"MCP 服务器","StdioTransportHelp":"配置执行 MCP 服务器进程的命令。参数和环境变量是可选的。","HttpTransportHelp":"配置 MCP 服务器的 HTTP 端点 URL。认证和自定义头是可选的。","ArgumentsHelp":"命令行参数（JSON 数组），例如：[\\u0022--port\\u0022, \\u00228080\\u0022]","EnvironmentVariablesHelp":"环境变量（JSON 对象），例如：{\\u0022KEY\\u0022: \\u0022value\\u0022}","HeadersHelp":"自定义 HTTP 头（JSON 对象），例如：{\\u0022X-Custom-Header\\u0022: \\u0022value\\u0022}","AIManagement:McpServerNameAlreadyExists":"名称为\\u0027{Name}\\u0027的 MCP 服务器已存在。","AIManagement:McpServerNotFound":"未找到 MCP 服务器\\u0027{McpServerId}\\u0027。","AIManagement:McpServerConnectionFailed":"连接到 MCP 服务器\\u0027{Name}\\u0027失败。错误：{Error}","AIManagement:McpServerAlreadyConnectedToWorkspace":"MCP 服务器\\u0027{McpServerName}\\u0027已连接到工作区\\u0027{WorkspaceName}\\u0027。","AIManagement:McpServerNotConnectedToWorkspace":"MCP 服务器\\u0027{McpServerName}\\u0027未连接到工作区\\u0027{WorkspaceName}\\u0027。","AIManagement:McpServerEndpointRequired":"HTTP 传输需要端点 URL。","AIManagement:McpServerCommandRequired":"Stdio 传输需要命令。","AIManagement:McpServerInvalidTransportConfiguration":"MCP 服务器\\u0027{Name}\\u0027的传输配置无效。传输类型：{TransportType}","AIManagement:McpServerToolExecutionFailed":"在 MCP 服务器\\u0027{McpServerName}\\u0027上执行工具\\u0027{ToolName}\\u0027失败。错误：{Error}","AIManagement:McpServerIsInactive":"MCP 服务器\\u0027{Name}\\u0027未启用。","SelectMcpServer":"选择 MCP 服务器","SelectMcpServerDescription":"点击 MCP 服务器进行选择，然后点击连接。您也可以双击立即连接。","NoMcpServersAvailable":"没有可连接的 MCP 服务器。","McpServerConnected":"MCP 服务器连接成功。","McpServerDisconnected":"MCP 服务器断开成功。","FailedToConnectMcpServer":"连接 MCP 服务器失败。","AreYouSureYouWantToDisconnectThisMcpServer":"确定要从工作区断开此 MCP 服务器吗？","Connect":"连接","Connecting":"连接中...","Cancel":"取消","StdioConfiguration":"Stdio 配置","HttpConfiguration":"HTTP 配置","ArgumentsTooltip":"命令行参数（JSON 数组），例如：[\\u0022--port\\u0022, \\u00228080\\u0022]","EnvironmentVariablesTooltip":"环境变量（JSON 对象），例如：{\\u0022KEY\\u0022: \\u0022value\\u0022}","HeadersTooltip":"自定义 HTTP 头（JSON 对象），例如：{\\u0022X-Custom-Header\\u0022: \\u0022value\\u0022}","TestingConnection":"正在测试连接","AvailableTools":"可用工具","NoToolsAvailable":"此 MCP 服务器没有可用工具。","ViewParameters":"查看参数","HideParameters":"隐藏参数","Loading":"加载中","Close":"关闭","Enable":"启用","Disable":"禁用","Enabled":"已启用","Disabled":"已禁用","IsEnabled":"已启用","Actions":"操作","CreationTime":"创建时间","Remove":"移除","AIChat:UsageDetails":"使用详情","AIChat:TokenUsage":"令牌: {0} 输入 / {1} 输出 / {2} 总计","AIChat:ToolCalls":"工具调用","AIChat:Stop":"停止"}	2026-04-29 11:54:25.164711	\N
3a20e9c3-a2d3-2d8b-6a66-c50e7af396b8	AbpAccount	en	{"Volo.Account:InvalidEmailAddress":"Can not find the given email address:{0}","Volo.Account:SelfRegistrationDisabled":"Self registration is disabled!","Volo.Account:PhoneNumberEmpty":"Phone number is empty!","Volo.Account:PhoneNumberConfirmationDisabled":"Phone number confirmation is disabled!","Volo.Account:InvalidUserToken":"Invalid user token!","Volo.Account:UnsupportedTwoFactorProvider":"Unsupported Two factor provider!","Volo.Account:ImpersonateTenantOnlyAvailableForHost":"Impersonate tenant only available for host!","Volo.Account:RequirePermissionToImpersonateTenant":"Require {PermissionName} permission to impersonate tenant!","Volo.Account:ThereIsNoUserWithUserName":"There is no user with username: {UserName}!","Volo.Account:YouCanNotImpersonateYourself":"You can\\u0027t impersonate yourself!","Volo.Account:NestedImpersonationIsNotAllowed":"Nested impersonation is not allowed!","Volo.Account:RequirePermissionToImpersonateUser":"Require {PermissionName} permission to impersonate user!","Volo.Account:ThereIsNoUserWithId":"There is no user with id:{UserId}!","Volo.Account:InvalidAccessToken":"Invalid access_token!","Volo.Account:InvalidTenantIdOrUserId":"Invalid TenantId or UserId!","Volo.Account:InvalidUserDelegationId":"Invalid UserDelegationId!","Volo.Account:InvalidEmailConfirmationCode":"The confirmation code is invalid. Please check again.","Volo.Account:EmailConfirmationCodeExpired":"The confirmation code is expired. Please resend a new code and try again.","Volo.Account:EmailConfirmationCodeLimitReached":"You have recently sent a confirmation code e-mail. You can resend the confirmation code on {0}.","Volo.Account:PhoneConfirmationCodeLimitReached":"You have recently sent a phone confirmation code. You can resend the confirmation code on {0}.","Volo.Account:DelegatedImpersonationIsDisabled":"Delegated impersonation is disabled!","Volo.Account:UserDelegationIsNotAvailableForImpersonatedUsers":"User delegation is not available for impersonated users!","Volo.Account:ImpersonateError":"Impersonate error!","Volo.Account:StartTimeMustBeLessThanEndTime":"Start time must be less than end time!","DelegatedImpersonation":"Delegated impersonation","BackToImpersonator":"Back to my account","SwitchToUser":"Switch to user","ExpiresAt":"Expires at","AuthorityDelegation":"Authority delegation","DelegateNewUser":"Delegate new user","DelegatedUsers":"Delegated users","MyDelegatedUsers":"My delegated users","DelegationDateRange":"Delegation date range","AreYouSure":"Are you sure?","StartTime":"Start time","EndTime":"End time","Status":"Status","StatusActive":"Active","StatusExpired":"Expired","StatusFuture":"Future","DeleteUserDelegationConfirmationMessage":"Are you sure you want to delete this delegation \\u0027{0}\\u0027?","AuthorityDelegation:PleaseSelectUser":"Please select a user","AuthorityDelegation:PleaseSelectDelegationDateRange":"Please select a delegation date range","PasswordReset":"Password reset","InvitationLinkIsInvalid":"The invitation link is invalid.","RegisterToJoinTenant":"Register to join the tenant \\u0027{0}\\u0027?","YouHaveSuccessfullyJoinedTheTenant":"You have successfully joined the tenant \\u0027{0}\\u0027.","TenantName":"Tenant name","SwitchTenant":"Switch tenant","HostDisplayName":"Host","SwitchToThisTenant":"Switch to this tenant","AreYouSureYouWantToLeaveTenant":"Are you sure you want to leave tenant \\u0027{0}\\u0027?","YouAreAlreadyInTheTenant":"You are already in the tenant \\u0027{0}\\u0027!","UserNotFoundByEmail":"User not found by email: {0}","SelectTenant":"Select tenant","SelectTenantMessage":"Select a tenant to continue signing in","PendingTenant":"Tenant required","PendingTenantContactAdmin":"Ask your administrator to send an invite to your email address: {0}.","CreateNewTenantInfo":"Create a new tenant and be the admin of it.","CreateNewTenant":"Create new tenant","TenantNameAlreadyExistsWarningMessage":"The tenant name \\u0027{0}\\u0027 already exists. Please choose another name.","TheTenantCreateRequestHasBeenSubmitted":"The tenant create request has been submitted. Please try to login with {0} again later.","PasswordResetInfoInEmail":"We received an account recovery request! If you initiated this request, click the following link to reset your password.","ResetMyPassword":"Reset my password","NotAMemberYet":"Not a member yet?","OrSignInWith":"Or sign in with","SignInWith":"Sign in with {0}","RegisterWith":"Register with {0}","SignInWithOneOfTheFollowingProviders":"Sign in with one of the following providers","EmailConfirmation":"Email confirmation","EmailConfirmationInfoInEmail":"Please confirm your email address by clicking the following link.","ConfirmMyEmail":"Confirm my email address","UserName":"User name","EmailAddress":"Email address","UserNameOrEmailAddress":"User name or email address","Password":"Password","RememberMe":"Remember me","SelectedProvider":"Selected provider","UseAnotherServiceToLogin":"Use another service to log in","UserLockedOut":"Locked Out!","UserLockedOutMessage":"Your account has been locked by the admin or due to invalid login attempts. Please try again later. Contact your system administrator if you think this is a mistake.","InvalidUserNameOrPassword":"Invalid username or password!","LoginIsNotAllowed":"You are not allowed to login! Your account is inactive or needs to confirm your email/phone number.","SelfRegistrationDisabledMessage":"Self user registration is disabled for this application. Contact to the application administrator to register a new user.","VerifySecurityCodeNotLoggedInErrorMessage":"You should be login first, in order to verify yourself! Probably, your login has been timeout. Please go to the login page and re-try it.","InvalidSecurityCode":"Invalid Two-factor code!","EmailSecurityCodeBody":"Your Two-factor code is: {0}","EmailSecurityCodeSubject":"Two-factor Code","EmailConfirmationCodeSubject":"E-mail confirmation","EmailConfirmationCodeBody":"Please use the code to finish your registration process. Your registration code: {0}","VerifySecurityCode_Information":"Please enter the verification code sent to you.","SendSecurityCode_Information":"You should verify yourself to login. Please select a verification type. A code will be sent based on the selected verification type.","ForgotPassword":"Forgot password?","SendPasswordResetLink_Information":"A password reset link will be sent to your email to reset your password. If you don\\u0027t get an email within a few minutes, please re-try.","PasswordResetMailSentMessage":"An account recovery email has been sent. If you don\\u0027t see it in 15 minutes, check your junk folder and mark it as \\u0027Not Junk\\u0027.","PreventEmailEnumerationPasswordResetMailSentMessage":"If the provided e-mail address is registered in the system, we will send a password reset link. If you don\\u0027t get an email within a few minutes, please check your spam box or try again later.","ResetPassword":"Reset Password","ConfirmPassword":"Confirm (repeat) the password","ResetPassword_Information":"Please enter your new password.","YourPasswordIsSuccessfullyReset":"Your password is successfully reset.","YourEmailAddressIsSuccessfullyConfirmed":"Your email address has been successfully confirmed.","MailSendingFailed":"Mail sending failed, please check your email configuration and try again.","GoToTheHomePage":"Go to the home page","TwoFactorVerification":"Two factor verification","BackToLogin":"Back to login","AlreadyRegistered":"Already registered?","Permission:Account":"Account","Permission:SettingManagement":"Setting management","TwoFactorAuthentication":"Two Factor Authentication","DisplayName:IsSelfRegistrationEnabled":"Enable self registration","Description:IsSelfRegistrationEnabled":"Users can create accounts themselves.","DisplayName:IsRememberBrowserEnabled":"Remember browser","DisplayName:PreventEmailEnumeration":"Prevent email enumeration","Description:PreventEmailEnumeration":"The \\u0027Forgot my password\\u0027 screen will not return whether the given email is registered in the system or not.","Menu:Account":"Account","Menu:Account.ExternalProvider":"Account external provider","AccountSettingsGeneral":"General","AccountSettingsTwoFactor":"Two factor","TwoFactorHasBeenDisabled":"Two factor has been disabled.","DisplayName:CurrentPassword":"Current password","DisplayName:NewPassword":"New password","DisplayName:NewPasswordConfirm":"Confirm new password","PasswordChangedMessage":"Your password has been changed successfully.","DisplayName:UserName":"User name","DisplayName:Email":"Email","DisplayName:Name":"Name","DisplayName:Surname":"Surname","DisplayName:Password":"Password","DisplayName:EmailAddress":"Email address","DisplayName:PhoneNumber":"Phone number","DisplayName:Timezone":"Time zone","DefaultTimeZone":"Default time zone","TimezoneHelpText":"The default timezone will try to use the browser\\u0027s or the server\\u0027s timezone.","PersonalSettings":"Personal settings","PersonalSettingsSaved":"Personal settings saved","PersonalSettingsChangedConfirmationModalTitle":"Personal info changed","PersonalSettingsChangedConfirmationModalDescription":"If you want to apply these changes, you have to login. Do you want to log out?","PasswordChanged":"Password changed","TwoFactorChanged":"Two factor settings saved","DisplayName:TwoFactorEnabled":"Enable two-factor authentication","TwoFactorEnabledInfo":"Two-factor authentication will not work when you remove all two-factor providers.","YouHaveToEnableAtLeastOneTwoFactorProvider":"You have to enable at least one two factor provider to enable two factor!","NewPasswordConfirmFailed":"Please confirm the new password.","NewPasswordSameAsOld":"New password must be different from the old one.","Manage":"Manage","MyAccount":"My account","UserInformation":"User information","DisplayName:EnableLocalLogin":"Allow to register and log in with local username and password","Description:EnableLocalLogin":"Indicates if Server will allow to register and log in with local username and password.","SocialAccountSecurity":"Social account security","DisplayName:VerifyPasswordDuringExternalLogin":"Require Local Password on Social Account Linking","Description:VerifyPasswordDuringExternalLogin":"Users who register via both local registration and external/social login using the same email address will be required to enter their local password on the first external/social login.","Feature:AccountGroup":"Account","DoYouWantToVerifyPhoneNumberMessage":"You\\u0027ve changed your phone number information. Would you like to verify it now?","InvalidPhoneConfirmationToken":"Verify code is invalid!","ConfirmYourPhoneNumber":"Verify your phone number","Verify":"Verify","Verified":"Verified","NotVerified":"Not verified","FirstlySubmitToVerify":"Firstly save your new information, then you can verify.","EmailConfirmationSentMessage":"A verification link has been sent to your email ({0}).","ConfirmationTokenSentMessage":"A verification code has been sent to your phone number.","PhoneConfirmationToken":"Please enter the code below to verify your phone number:","PhoneConfirmationSms":"Hi {0}! Here is your verification code: {1}","ConfirmUser":"Confirm your email/phone number","PhoneNumberEmptyHelpText":"You don\\u0027t have a phone number, Verify will set it.","TextTemplate:Abp.Account.Layout":"Account email layout","TextTemplate:Abp.Account.PasswordResetLink":"Password reset email","TextTemplate:Abp.Account.EmailConfirmationLink":"Email confirmation email","TextTemplate:Abp.Account.EmailSecurityCode":"Email security code","LoggedOutTitle":"Signed Out","LoggedOutText":"You have been signed out and you will be redirected soon.","ReturnToText":"Click here to return to the application","MySecurityLogs":"Security logs","MySecurityLogs:StartTime":"Start time","MySecurityLogs:EndTime":"End time","MySecurityLogs:Application":"Application","MySecurityLogs:Identity":"Identity","MySecurityLogs:Action":"Action","MySecurityLogs:Client":"Client","MySecurityLogs:Time":"Time","MySecurityLogs:CorrelationId":"CorrelationId","MySecurityLogs:IpAddress":"Ip address","MySecurityLogs:Browser":"Browser","LoginToTheApplication":"Login to the application","RememberBrowser":"Remember this browser","Code":"Code","ProfilePicture":"Profile picture","MoveCursorOnExamples":"Move your cursor on images to see rounded style.","ProfilePictureWillBeChanged":"Your profile picture will be changed.","CurrentProfilePicture":"Current profile picture","ChangeProfilePicture":"Change profile picture","SelectNewImage":"Select new image","DisplayName:UseGravatar":"Use Gravatar","Description:UseGravatar":"Use Gravatar service for your profile picture.","SaveChanges":"Save changes","UploadFile":"Upload file","UseDefault":"Use default avatar","UseGravatarConfirm":"You will be using Gravatar as your profile picture.","NoProfilePictureConfirm":"You will use default avatar as your profile picture.","PPUploadConfirm":"You will use your selected image as your profile picture.","PleaseSelectImage":"Please select an image","UploadFailedMessage":"Upload failed!","AccountPro:0001":"You must upload an image!","AccountPro:0002":"The invitation token is invalid or has already been used.","ProfilePicture:InvalidFileExtension":"The uploaded file has an unsupported extension. Please upload a valid image file.","ProfilePicture:InvalidFileContent":"The uploaded file content does not match the expected image format.","ProfilePicture:FileSizeExceeded":"The uploaded file exceeds the maximum allowed size.","ProfileTab:Picture":"Profile picture","ProfileTab:Password":"Change password","ProfileTab:PersonalInfo":"Personal info","ProfileTab:TwoFactor":"Two factor authentication","ProfileTab:AuthenticatorApp":"Authenticator app","ProfileTab:Passkeys":"Passkeys","Passkey":"Passkey","PasskeyDescription":"Passkeys let users sign in using device-based authentication (Face ID, Touch ID, Windows Hello) instead of passwords. This improves security and removes the need to remember credentials. Enable passkeys to allow users to create and manage secure login keys across their devices.","EnablePasskey":"Enable passkey","MaximumPasskeysPerUser":"Maximum passkeys per user","PasskeyLogin":"Passkey login","AddPasskey":"Add passkey","ChangePasskeyName":"Change passkey name","PasskeyName":"Passkey name","UnnamedPasskey":"Unnamed passkey","PasskeyCreatedAt":"Created at","PasskeyDeletionConfirmationMessage":"Are you sure you want to delete this passkey?","PasskeyFeatureDisabled":"Passkey feature is disabled.","PasskeyWarningMessage":"With passkeys, you can sign in to this application using your device’s fingerprint, face recognition, screen lock or a hardware security key. You should keep your devices and screen locks secure to ensure only authorized access to your account.","AddOrUpdatePasskeyFailed":"The passkey could not be added to your account.","RemovePasskeyFailed":"The passkey could not be removed from your account","PasskeyAttestationFailed":"Passkey attestation failed.","InvalidCredential":"The credential is not a valid Base64 string.","MaxPasskeysReached":"You have reached the maximum number of passkeys.","MaximumPasskeysPerUserMustBeGreaterThanZero":"Maximum passkeys per user must be greater than zero","DisplayName:Passkey.Enabled":"Enable passkey login","Description:Passkey.Enabled":"Allow users to log in using passkeys.","DisplayName:Passkey.MaximumPasskeysPerUser":"Maximum passkeys per user","Description:Passkey.MaximumPasskeysPerUser":"The maximum number of passkeys a user can register.","BrowserDoesNotSupportPasskeys":"Your browser does not support passkeys. Please use a compatible browser such as Chrome, Edge, or Firefox.","InvalidPasskey":"The passkey is invalid or could not be recognized.","AddPasskeyForAccount":"Add a passkey for your account","SkipAddPasskeyAndContinue":"Skip adding a passkey and continue","DisplayName:UseCaptchaOnLogin":"Use captcha on user login","Description:UseCaptchaOnLogin":"Use captcha on user login","DisplayName:UseCaptchaOnRegistration":"Use captcha on user registration","Description:UseCaptchaOnRegistration":"Use captcha on user registration","DisplayName:UseCaptchaOnForgotPassword":"Use captcha on password reset","Description:UseCaptchaOnForgotPassword":"Use captcha on password reset","Captcha":"Captcha","GoogleCaptcha":"Google reCAPTCHA","GoogleCaptchaDescription":"reCAPTCHA protects your website from fraud and abuse without creating friction","DisplayName:VerificationUrl":"Verification URL","Description:VerificationUrl":"The base URL of the Google service. Example URLs: https://www.google.com/, https://www.recaptcha.net/","DisplayName:SiteKey":"Site key","Description:SiteKey":"It is used to invoke reCAPTCHA service on your site. Leave empty to use the default value in the website configuration code.","DisplayName:SiteSecret":"Site secret","Description:SiteSecret":"It authorizes communication between your website and the reCAPTCHA server to verify the user\\u0027s response. Leave empty to use the default value in website configuration code.","DisplayName:Version":"Version","Description:Version":"reCAPTCHA v3 verifies requests with a score, reCAPTCHA v2 verifies requests with a challenge.","DisplayName:Score":"Score","Description:Score":"It is the numeric threshold value from 0.0 to 1.0. The lower threshold values will allow spammers to pass the verification. By default, you can use a threshold of 0.5.","InvalidSiteKeyOrSiteSecret":"Invalid site key or site secret","CaptchaCanNotBeEmpty":"Please check the reCAPTCHA box","IncorrectCaptchaAnswer":"Incorrect captcha answer","ScoreBelowThreshold":"Verification failed, score below threshold","SetNullWillUseGlobalSettings":"Leave as empty to use the defaults","ReturnToApplication":"Return to application","AccountExternalProviderSettings":"External provider","ExternalProviderEnabled":"Enabled","ExternalProviderEnabledForTenant":"Allow tenants to override settings","ExternalProviderEnabledForTenantExplanation":"When you enable this feature, you can set external login settings specific to the tenant users. If no external login client information is provided while enabling the feature, the host settings will be used.","ExternalProviderEnabledForHost":"Enabled for host users","ExternalProviderEnabledForHostExplanation":"When you enable this feature, the external login client information below will apply to all host users.","ExternalProviderOverrideHostValue":"Override host client information","LinkedAccounts":"Linked accounts","LoginAsThisAccount":"Login as this account","DeleteLinkAccountConfirmationMessage":"Are you sure to delete the link account \\u0027{0}\\u0027?","NewLinkAccount":"New link account","NewLinkAccountWarning":"You\\u0027ll be logged out from the current account and logging in with another account. Once you do it, two accounts will be linked.","TenantAndUserName":"User name","DirectlyLinked":"Directly linked","BackToMyAccount":"Back to: {0}","LinkLogged":"Accounts are linked","StayWithCurrentAccount":"Stay with the current account","ReturnToPreviousAccount":"Return to {0} account","TheTargetAccountIsNotLinkedToYou":"The target account is not linked to you!","LinkAccountWarning":"Please note that you are linking to other accounts, \\u003Ca href=\\u0022{0}\\u0022\\u003EClick here\\u003C/a\\u003E to cancel the link login!","SavedSuccessfully":"Saved successfully","AccessDenied":"Access denied!","AccessDeniedMessage":"You do not have access to this resource.","RequestingYourPermission":"is requesting your permission","UncheckThePermissionsYouDoNotWishToGrant":"Uncheck the permissions you do not wish to grant.","ConsentPersonalInformation":"Personal information","ConsentApplicationAccess":"Application access","ScopeRequired":"required","RememberConsent":"Remember my decision","UserDecisionYes":"Yes, allow","UserDecisionNo":"No, do not allow","DeviceAuthorization":"Device authorization","UserCode":"User code","UserCodeInvalid":"UserCode invalid!","DeviceAuthorizationSuccessfully":"Success!","DeviceAuthorizationSuccessfullyInfo":"You have successfully authorized the device","LocalLoginIsNotEnabled":"Local login is not enabled!","ExternalLoginCallbackError":"External login callback error: {0}","ExternalLoginInfoIsNotAvailable":"External login info is not available","ExternalLoginUserIsNotFound":"External login user is not found","SignIn":"Sign in","SignOut":"Sign out","Date":"Date","ResetAuthenticator":"Reset authenticator","ResetAuthenticator_Information":"If you would like to change your authenticator app, you can reset it here.","ResetAuthenticatorWarningMessage":"Reset your authenticator key your authenticator app will not work until you reconfigure it.","UseTwoFactorAuthenticatorApp_Information":"Open your two factor authentication app and do one of the following actions:","UseTwoFactorAuthenticatorApp_Code":"Your two-factor authentication application will generate a code, enter this code in the box below and confirm.","RecoveryCode":"Recovery code","RecoveryCodes":"Recovery codes","RecoveryCodes_Information":"Put these codes in a safe place. If you lose your device and don\\u0027t have the recovery codes you will lose access to your account.","CopyToClipboard":"Copy to clipboard","PrintCodes":"Print codes","LoginWithRecoveryCode":"Login with recovery code","LoginWithRecoveryCode_Information":" You have requested to log in with a recovery code. \\u0027Remember me\\u0027 will not be enabled for this login until you provide an authenticator app code or disable 2FA and log in again.","LoginWithRecoveryCode_URL":"Don\\u0027t have access to your authenticator device? You can log in with a recovery code","UseQrCode":"Use the QR Code","ManuallyEnterCode":"Or manually enter the code","TwoFactorAuthenticationProviders":"Two-Factor authentication providers","AuthenticatorApp":"Authenticator app","VerifyTheAuthenticator":"Verify the authenticator","Step":"Step {0}","NextStep":"Next step","Done":"Done","ShowPassword":"Show password","HidePassword":"Hide password","CapsLockOn":"Caps lock on","TakePhoto":"Take photo","ChoosePhoto":"Choose photo","Strength":"Strength","Weak":"Weak!","Fair":"Fair.","Normal":"Normal.","Good":"Good.","Strong":"Strong!","Authentication:YouAreLoggedOut":"You are logged out.","OrRegisterWith":"Or register with","RegisterWithOneOfTheFollowingProviders":"Register with one of the following providers","RegisterText":"By clicking Register button, you agree to our \\u003Ca href=\\u0022/Account/TermsConditions\\u0022 target=\\u0022_blank\\u0022 class=\\u0022text-decoration-none\\u0022 rel=\\u0022noopener\\u0022\\u003ETerms \\u0026 Conditions\\u003C/a\\u003E and \\u003Ca href=\\u0022/Account/Privacy\\u0022 target=\\u0022_blank\\u0022 class=\\u0022text-decoration-none\\u0022 rel=\\u0022noopener\\u0022\\u003EPrivacy Policy\\u003C/a\\u003E.","ProtocolCallbackTitle":"Redirecting...","ProtocolCallbackHeader":"You\\u0027re almost done!","ProtocolCallbackDescription":"We\\u0027re redirecting you to the app. If you don\\u0027t see a dialog, please click the button below.","ProceedToApp":"Proceed to App","UserLockedOut:WhatDoYouNeedToDo":"What do you need to do?","YourAccountWillBeUnlockedOn":"Your account will be unlocked on","TryAgainAfterThisTimeOrContactAdministrator":"You can try again after this time or contact the system administrator.","ExternalLogins":"External logins","ExternalLogin:LoginProvider":"External provider","ExternalLogin:ProviderDisplayName":"Provider display name","NewExternalLogin":"New external login","ClickToLoginWithYourExternalAccount":"Click to login with your external account","YouHaveNoExternalLoginProviderToSignIn":"You have no external login provider to sign in","ExternalLoginDeleteConfirmationMessage":"Are you sure you want to delete this external login \\u0027{0}\\u0027?","YourSessionIsAboutToExpire":"Your session is about to expire","YourSessionIsAboutToExpireInfo":"For security reasons, your connection times out after you\\u0027ve been inactive for a while. Do you want to stay signed in?","IdleSignOutNow":"Sign out now","IdleStaySignedIn":"Stay signed in","IdleSessionTimeout":"Idle session timeout","IdleSessionTimeoutInfo":"Idle session timeout signs users automatically out of the web apps after a period of inactivity. If you turn this on, users may start seeing more sign-in prompts.","LearnMoreAboutIdleSessionTimeout":"Learn more about idle session timeout","IdleSignOutInactiveUserAutomatically":"Sign out inactive user automatically","IdleTimeoutMinutes":"When do you want users signed out?","IdleTimeoutMinutesMustBeGreaterThanZero":"Idle timeout minutes must be greater than zero","OneHour":"1 hour","ThreeHours":"3 hours","SixHours":"6 hours","TwelveHours":"12 hours","TwentyFourHours":"24 hours","CustomIdleTimeoutMinutes":"Custom (in minutes)","IdleCountdownSeconds":"Countdown seconds","IdleCountdownSecondsInfo":"The countdown duration (in seconds) to show the session countdown before signing out.","SessionRevokeDescription":"This page shows all the devices you\\u0027ve logged in on. You can force log out from any device you wish.","RequireMigrateSeedTitle":"Could not find the admin user","RequireMigrateSeedMessage":"Please ensure that the database seed is executed. See \\u003Ca target=\\u0022_blank\\u0022 href=\\u0022https://abp.io/kb/0003\\u0022\\u003Edocumentation\\u003C/a\\u003E for possible solutions.","ReSendCode":"Resend code","EmailSentTitle":"Confirmation code is sent to your e-mail account.","EmailSentInfo":"Please check your inbox to see the confirmation code and complete the registration.","EnterAValueToUpdateSecret":"Enter a value to update the secret","SelectAccount":"Select account","SignedInAs":"Signed in as","Continue":"Continue","SwitchToAnotherAccount":"Switch to another account","CreateANewAccount":"Create a new account","ChangeEmail":"Confirm your new email address","ChangeEmailInfoInEmailHi":"Hi,","ChangeEmailInfoInEmail":"We received a request to change the email address associated with your account. To confirm this change, please click the link below:","ConfirmNewEmailAddress":"Confirm New Email Address","ChangeEmailInfoInEmailTip":"If you didn’t request this, you can safely ignore this message — your current email will remain unchanged.","YourEmailAddressWillNotBeChangedUntilYouVerifyIt":"The email address change to {0} is pending verification. Please check your inbox and follow the link to confirm the update.","YourEmailAddressIsSuccessfullyChanged":"Your email address has been successfully updated to {0}."}	2026-04-29 11:54:37.049963	\N
3a20e9c3-a2d5-5e43-d7d8-3dffd8e5c1c5	AbpAccount	ru	{"Volo.Account:InvalidEmailAddress":"Не удается найти указанный адрес электронной почты:{0}","Volo.Account:SelfRegistrationDisabled":"Самостоятельная регистрация отключена!","Volo.Account:PhoneNumberEmpty":"Номер телефона пуст!","Volo.Account:PhoneNumberConfirmationDisabled":"Подтверждение номера телефона отключено!","Volo.Account:InvalidUserToken":"Недействительный токен пользователя!","Volo.Account:UnsupportedTwoFactorProvider":"Неподдерживаемый двухфакторный провайдер!","Volo.Account:ImpersonateTenantOnlyAvailableForHost":"Олицетворение арендатора доступно только для хоста!","Volo.Account:RequirePermissionToImpersonateTenant":"Требуется разрешение {PermissionName} для олицетворения арендатора!","Volo.Account:ThereIsNoUserWithUserName":"Нет пользователя с именем пользователя: {UserName}!","Volo.Account:YouCanNotImpersonateYourself":"Вы не можете выдавать себя за себя!","Volo.Account:NestedImpersonationIsNotAllowed":"Вложенное олицетворение не допускается!","Volo.Account:RequirePermissionToImpersonateUser":"Требуется разрешение {PermissionName} для олицетворения пользователя!","Volo.Account:ThereIsNoUserWithId":"Нет пользователя с идентификатором: {UserId}!","Volo.Account:InvalidAccessToken":"Недопустимый маркер доступа!","Volo.Account:InvalidTenantIdOrUserId":"Недопустимый TenantId или UserId!","Volo.Account:InvalidUserDelegationId":"Недопустимый идентификатор делегирования пользователя!","Volo.Account:InvalidEmailConfirmationCode":"Введенный вами код подтверждения недействителен. Пожалуйста, проверьте еще раз.","Volo.Account:EmailConfirmationCodeExpired":"Введенный вами код подтверждения истек. Пожалуйста, отправьте код повторно и попробуйте снова.","Volo.Account:EmailConfirmationCodeLimitReached":"Вы недавно отправили код подтверждения по электронной почте. Вы можете отправить следующий код подтверждения снова на {0}.","Volo.Account:PhoneConfirmationCodeLimitReached":"Вы недавно отправили код подтверждения по телефону. Вы можете отправить код подтверждения снова {0}.","Volo.Account:DelegatedImpersonationIsDisabled":"Делегированное олицетворение отключено!","Volo.Account:UserDelegationIsNotAvailableForImpersonatedUsers":"Делегирование пользователей недоступно для олицетворенных пользователей!","Volo.Account:ImpersonateError":"Ошибка олицетворения!","Volo.Account:StartTimeMustBeLessThanEndTime":"Время начала должно быть меньше времени окончания!","DelegatedImpersonation":"Олицетворение","BackToImpersonator":"Вернуться к моей учетной записи","SwitchToUser":"Переключиться на пользователя","ExpiresAt":"Истекает в","AuthorityDelegation":"Делегирование","DelegateNewUser":"Делегировать нового пользователя","DelegatedUsers":"Делегированные пользователи","MyDelegatedUsers":"Мои делегированные пользователи","DelegationDateRange":"Диапазон дат делегирования","AreYouSure":"Вы уверены?","StartTime":"Время начала","EndTime":"Время окончания","Status":"Статус","StatusActive":"Активный","StatusExpired":"Истекший","StatusFuture":"Будущий","DeleteUserDelegationConfirmationMessage":"Вы уверены, что хотите удалить делегирование для пользователя \\u0027{0}\\u0027?","AuthorityDelegation:PleaseSelectUser":"Пожалуйста, выберите пользователя","AuthorityDelegation:PleaseSelectDelegationDateRange":"Пожалуйста, выберите диапазон дат делегирования","PasswordReset":"Сброс пароля","InvitationLinkIsInvalid":"Ссылка приглашения недействительна.","RegisterToJoinTenant":"Зарегистрироваться, чтобы присоединиться к арендатору \\u0027{0}\\u0027?","YouHaveSuccessfullyJoinedTheTenant":"Вы успешно присоединились к арендатору \\u0027{0}\\u0027.","TenantName":"Имя арендатора","SwitchTenant":"Сменить арендатора","HostDisplayName":"Хост","SwitchToThisTenant":"Переключиться на этого арендатора","AreYouSureYouWantToLeaveTenant":"Вы уверены, что хотите покинуть арендатора \\u0027{0}\\u0027?","YouAreAlreadyInTheTenant":"Вы уже в арендаторе \\u0027{0}\\u0027!","UserNotFoundByEmail":"Пользователь не найден по электронной почте: {0}","SelectTenant":"Выбрать арендатора","SelectTenantMessage":"Выберите арендатора, чтобы продолжить вход в систему.","PendingTenant":"Требуется арендатор","PendingTenantContactAdmin":"Попросите администратора отправить приглашение на ваш адрес электронной почты: {0}.","CreateNewTenantInfo":"Создайте нового арендатора и станьте его администратором.","CreateNewTenant":"Создать нового арендатора","TenantNameAlreadyExistsWarningMessage":"Имя арендатора «{0}» уже существует. Пожалуйста, выберите другое имя.","TheTenantCreateRequestHasBeenSubmitted":"Запрос на создание арендатора отправлен. Попробуйте войти позже, используя {0}.","PasswordResetInfoInEmail":"Мы получили запрос на восстановление аккаунта! Если вы инициировали этот запрос, щелкните следующую ссылку, чтобы сбросить пароль.","ResetMyPassword":"Сбросить пароль","NotAMemberYet":"Еще не зарегистрированы?","OrSignInWith":"Или войдите с помощью","SignInWith":"Войдите через {0}","RegisterWith":"Зарегистрируйтесь через {0}","SignInWithOneOfTheFollowingProviders":"Войдите с помощью одного из следующих поставщиков","EmailConfirmation":"Подтверждение электронной почты","EmailConfirmationInfoInEmail":"Пожалуйста, подтвердите свой адрес электронной почты, нажав на следующую ссылку.","ConfirmMyEmail":"Подтвердите мой адрес электронной почты","UserName":"Имя пользователя","EmailAddress":"Адрес электронной почты","UserNameOrEmailAddress":"Имя пользователя или адрес электронной почты","Password":"Пароль","RememberMe":"Запомни меня","SelectedProvider":"Выбранный провайдер","UseAnotherServiceToLogin":"Используйте другой сервис для входа","UserLockedOut":"Заблокировано!","UserLockedOutMessage":"Ваша учетная запись заблокирована администратором или из-за неверных попыток входа в систему. Пожалуйста, попробуйте позже. Если вы считаете, что это ошибка, обратитесь к системному администратору.","InvalidUserNameOrPassword":"Неправильное имя пользователя или пароль!","LoginIsNotAllowed":"Вы не можете войти в систему! Ваша учетная запись неактивна или требует подтверждения вашего адреса электронной почты/номера телефона.","SelfRegistrationDisabledMessage":"Для этого приложения отключена самостоятельная регистрация пользователей. Свяжитесь с администратором приложения для регистрации нового пользователя.","VerifySecurityCodeNotLoggedInErrorMessage":"Вы должны сначала войти в систему, чтобы подтвердить себя! Вероятно, ваш логин истек по тайм-ауту. Пожалуйста, перейдите на страницу входа и повторите попытку.","InvalidSecurityCode":"Неверный двухфакторный код!","EmailSecurityCodeBody":"Ваш двухфакторный код: {0}","EmailSecurityCodeSubject":"Двухфакторный код","EmailConfirmationCodeSubject":"Подтверждение электронной почты","EmailConfirmationCodeBody":"Пожалуйста, используйте код, чтобы завершить процесс регистрации. Ваш регистрационный код: {0}","VerifySecurityCode_Information":"Пожалуйста, введите код подтверждения, отправленный вам.","SendSecurityCode_Information":"Вы должны подтвердить себя, чтобы войти. Пожалуйста, выберите тип проверки. Код будет отправлен в зависимости от выбранного типа проверки.","ForgotPassword":"забыл пароль?","SendPasswordResetLink_Information":"На ваш адрес электронной почты будет отправлена ссылка для сброса пароля для сброса пароля. Если вы не получили электронное письмо в течение нескольких минут, повторите попытку.","PasswordResetMailSentMessage":"Письмо для восстановления учетной записи отправлено на ваш адрес электронной почты. Если вы не видите это письмо в папке «Входящие» в течение 15 минут, найдите его в папке нежелательной почты. Если вы найдете его там, пожалуйста, отметьте его как -Not Junk-.","ResetPassword":"Сброс пароля","ConfirmPassword":"Подтвердите (повторите) пароль","ResetPassword_Information":"Пожалуйста, введите новый пароль.","YourPasswordIsSuccessfullyReset":"Ваш пароль успешно сброшен.","YourEmailAddressIsSuccessfullyConfirmed":"Ваш адрес электронной почты успешно подтвержден.","MailSendingFailed":"Не удалось отправить письмо. Пожалуйста, проверьте настройки электронной почты и повторите попытку.","GoToTheHomePage":"Перейти на главную страницу","TwoFactorVerification":"Двухфакторная проверка","BackToLogin":"Вернуться на страницу входа","AlreadyRegistered":"Уже зарегистрирован?","Permission:Account":"Счет","Permission:SettingManagement":"Управление настройками","TwoFactorAuthentication":"Двухфакторная аутентификация","DisplayName:IsSelfRegistrationEnabled":"Включить самостоятельную регистрацию","Description:IsSelfRegistrationEnabled":"Пользователи могут создавать учетные записи самостоятельно.","DisplayName:IsRememberBrowserEnabled":"Запомнить браузер","Menu:Account":"Счет","Menu:Account.ExternalProvider":"Внешний поставщик учетной записи","AccountSettingsGeneral":"Общий","AccountSettingsTwoFactor":"Два фактора","TwoFactorHasBeenDisabled":"Два фактора были отключены.","DisplayName:CurrentPassword":"Текущий пароль","DisplayName:NewPassword":"Новый пароль","DisplayName:NewPasswordConfirm":"Подтвердите новый пароль","PasswordChangedMessage":"Ваш пароль был успешно изменен.","DisplayName:UserName":"Имя пользователя","DisplayName:Email":"Эл. адрес","DisplayName:Name":"Имя","DisplayName:Surname":"Фамилия","DisplayName:Password":"Пароль","DisplayName:EmailAddress":"Адрес электронной почты","DisplayName:PhoneNumber":"Телефонный номер","DisplayName:Timezone":"Часовой пояс","DefaultTimeZone":"Часовой пояс по умолчанию","TimezoneHelpText":"Часовой пояс по умолчанию попытается использовать часовой пояс браузера или сервера.","PersonalSettings":"Персональные настройки","PersonalSettingsSaved":"Персональные настройки сохранены","PersonalSettingsChangedConfirmationModalTitle":"Личная информация изменена","PersonalSettingsChangedConfirmationModalDescription":"Если вы хотите применить эти изменения, вам необходимо войти в систему. Вы хотите выйти из системы?","PasswordChanged":"пароль изменен","TwoFactorChanged":"Сохранены двухфакторные настройки","DisplayName:TwoFactorEnabled":"Включить двухфакторную аутентификацию","TwoFactorEnabledInfo":"Двухфакторная аутентификация не будет работать, если вы удалите все двухфакторные поставщики.","YouHaveToEnableAtLeastOneTwoFactorProvider":"Чтобы включить двухфакторный режим, вам необходимо включить хотя бы одного двухфакторного поставщика!","NewPasswordConfirmFailed":"Пожалуйста, подтвердите новый пароль.","NewPasswordSameAsOld":"Новый пароль не должен совпадать со старым.","Manage":"Управлять","MyAccount":"Мой счет","UserInformation":"информация о пользователе","DisplayName:EnableLocalLogin":"Разрешить регистрацию и вход с локальным именем пользователя и паролем.","Description:EnableLocalLogin":"Указывает, разрешит ли сервер регистрацию и вход с локальным именем пользователя и паролем.","SocialAccountSecurity":"Безопасность социального аккаунта","DisplayName:VerifyPasswordDuringExternalLogin":"Требовать локальный пароль при привязке социального аккаунта","Description:VerifyPasswordDuringExternalLogin":"Пользователи, которые регистрируются как через локальную регистрацию, так и через внешний/социальный вход, используя один и тот же адрес электронной почты, должны будут ввести свой локальный пароль при первом внешнем/социальном входе.","Feature:AccountGroup":"Счет","DoYouWantToVerifyPhoneNumberMessage":"Вы изменили информацию о своем номере телефона. Хотите проверить это сейчас?","InvalidPhoneConfirmationToken":"Код подтверждения недействителен!","ConfirmYourPhoneNumber":"Подтвердите свой номер телефона","Verify":"Проверять","Verified":"проверено","NotVerified":"не подтверждено","FirstlySubmitToVerify":"Сначала сохраните новую информацию, затем вы сможете ее подтвердить.","EmailConfirmationSentMessage":"Ссылка для подтверждения отправлена на ваш адрес электронной почты ({0}).","ConfirmationTokenSentMessage":"Код подтверждения был отправлен на ваш номер телефона.","PhoneConfirmationToken":"Пожалуйста, введите код ниже, чтобы подтвердить свой номер телефона:","PhoneConfirmationSms":"Привет {0}! Вот ваш проверочный код: {1}","ConfirmUser":"Подтвердите свой адрес электронной почты/номер телефона","PhoneNumberEmptyHelpText":"У вас нет номера телефона, Verify установит его.","TextTemplate:Abp.Account.Layout":"Макет электронной почты аккаунта","TextTemplate:Abp.Account.PasswordResetLink":"Электронная почта для сброса пароля","TextTemplate:Abp.Account.EmailConfirmationLink":"Письмо с подтверждением по электронной почте","TextTemplate:Abp.Account.EmailSecurityCode":"Код безопасности электронной почты","LoggedOutTitle":"Вышел","LoggedOutText":"Вы вышли из системы и скоро будете перенаправлены.","ReturnToText":"Нажмите здесь, чтобы вернуться в приложение","MySecurityLogs":"Журналы безопасности","MySecurityLogs:StartTime":"Время начала","MySecurityLogs:EndTime":"Время окончания","MySecurityLogs:Application":"Заявление","MySecurityLogs:Identity":"Личность","MySecurityLogs:Action":"Действие","MySecurityLogs:Client":"Клиент","MySecurityLogs:Time":"Время","MySecurityLogs:CorrelationId":"CorrelationId","MySecurityLogs:IpAddress":"Айпи адрес","MySecurityLogs:Browser":"Браузер","LoginToTheApplication":"Войти в приложение","RememberBrowser":"Запомнить этот браузер","Code":"Код","ProfilePicture":"Изображение профиля","MoveCursorOnExamples":"Наведите курсор на изображения, чтобы увидеть округлый стиль.","ProfilePictureWillBeChanged":"Изображение вашего профиля будет изменено.","CurrentProfilePicture":"Текущее изображение профиля","ChangeProfilePicture":"Изменить изображение профиля","SelectNewImage":"Выбрать новое изображение","DisplayName:UseGravatar":"Использовать Граватар","Description:UseGravatar":"Используйте сервис Gravatar для изображения вашего профиля.","SaveChanges":"Сохранить изменения","UploadFile":"Загрузить файл","UseDefault":"Использовать аватар по умолчанию","UseGravatarConfirm":"Вы будете использовать Gravatar в качестве изображения профиля.","NoProfilePictureConfirm":"В качестве изображения профиля вы будете использовать аватар по умолчанию.","PPUploadConfirm":"Вы будете использовать выбранное изображение в качестве изображения профиля.","PleaseSelectImage":"Пожалуйста, выберите изображение","UploadFailedMessage":"Загрузка не удалась!","AccountPro:0001":"Вы должны загрузить изображение!","AccountPro:0002":"Токен приглашения недействителен или уже был использован.","ProfilePicture:InvalidFileExtension":"Загруженный файл имеет неподдерживаемое расширение. Пожалуйста, загрузите допустимый файл изображения.","ProfilePicture:InvalidFileContent":"Содержимое загруженного файла не соответствует ожидаемому формату изображения.","ProfilePicture:FileSizeExceeded":"Загруженный файл превышает максимально допустимый размер.","ProfileTab:Picture":"Изображение профиля","ProfileTab:Password":"Изменить пароль","ProfileTab:PersonalInfo":"Личная информация","ProfileTab:TwoFactor":"Двухфакторная аутентификация","ProfileTab:AuthenticatorApp":"Приложение для аутентификации","ProfileTab:Passkeys":"Ключи доступа","Passkey":"Ключ доступа","PasskeyDescription":"Ключи доступа позволяют пользователям входить с использованием аутентификации на устройстве (Face ID, Touch ID, Windows Hello) вместо паролей. Это повышает безопасность и устраняет необходимость запоминать учетные данные. Включите ключи доступа, чтобы позволить пользователям создавать и управлять безопасными ключами входа на своих устройствах.","EnablePasskey":"Включить ключ доступа","MaximumPasskeysPerUser":"Максимум ключей доступа на пользователя","PasskeyLogin":"Вход по ключу доступа","AddPasskey":"Добавить ключ доступа","ChangePasskeyName":"Изменить имя ключа доступа","PasskeyName":"Имя ключа доступа","UnnamedPasskey":"Ключ доступа без имени","PasskeyCreatedAt":"Создан","PasskeyDeletionConfirmationMessage":"Вы уверены, что хотите удалить этот ключ доступа?","PasskeyFeatureDisabled":"Функция ключей доступа отключена.","PasskeyWarningMessage":"С ключами доступа вы можете входить в это приложение, используя отпечаток пальца, распознавание лица, блокировку экрана устройства или аппаратный ключ безопасности. Держите свои устройства и блокировки экрана в безопасности, чтобы обеспечить доступ к вашему аккаунту только авторизованным пользователям.","AddOrUpdatePasskeyFailed":"Не удалось добавить ключ доступа к вашей учетной записи.","RemovePasskeyFailed":"Не удалось удалить ключ доступа из вашей учетной записи","PasskeyAttestationFailed":"Сбой аттестации ключа доступа.","InvalidCredential":"Учетные данные не являются допустимой строкой Base64.","MaxPasskeysReached":"Вы достигли максимального количества ключей доступа.","MaximumPasskeysPerUserMustBeGreaterThanZero":"Максимум ключей доступа на пользователя должен быть больше нуля","DisplayName:Passkey.Enabled":"Включить вход по ключам доступа","Description:Passkey.Enabled":"Разрешить пользователям входить с использованием ключей доступа.","DisplayName:Passkey.MaximumPasskeysPerUser":"Максимум ключей доступа на пользователя","Description:Passkey.MaximumPasskeysPerUser":"Максимальное количество ключей доступа, которое пользователь может зарегистрировать.","BrowserDoesNotSupportPasskeys":"Ваш браузер не поддерживает ключи доступа. Пожалуйста, используйте совместимый браузер, например Chrome, Edge или Firefox.","InvalidPasskey":"Ключ доступа недействителен или не распознан.","AddPasskeyForAccount":"Добавить ключ доступа для вашей учетной записи","SkipAddPasskeyAndContinue":"Пропустить добавление ключа доступа и продолжить","DisplayName:UseCaptchaOnLogin":"Использовать капчу при входе пользователя","Description:UseCaptchaOnLogin":"Использовать капчу при входе пользователя","DisplayName:UseCaptchaOnRegistration":"Использовать капчу при регистрации пользователя","Description:UseCaptchaOnRegistration":"Использовать капчу при регистрации пользователя","DisplayName:UseCaptchaOnForgotPassword":"Использовать капчу при сбросе пароля","Description:UseCaptchaOnForgotPassword":"Использовать капчу при сбросе пароля","Captcha":"Капча","GoogleCaptcha":"Google reCAPTCHA","GoogleCaptchaDescription":"reCAPTCHA защищает ваш сайт от мошенничества и злоупотреблений без создания трения","DisplayName:VerificationUrl":"URL-адрес проверки","Description:VerificationUrl":"Базовый URL службы Google. Примеры URL-адресов: https://www.google.com/, https://www.recaptcha.net/","DisplayName:SiteKey":"Ключ сайта","Description:SiteKey":"Он используется для вызова службы reCAPTCHA на вашем сайте. Оставьте пустым, чтобы использовать значение по умолчанию в коде конфигурации веб-сайта.","DisplayName:SiteSecret":"Секрет сайта","Description:SiteSecret":"Он авторизует общение между вашим веб-сайтом и сервером reCAPTCHA для проверки ответа пользователя. Оставьте пустым, чтобы использовать значение по умолчанию в коде конфигурации веб-сайта.","DisplayName:Version":"Версия","Description:Version":"reCAPTCHA v3 проверяет запросы с оценкой, reCAPTCHA v2 проверяет запросы с вызовом.","DisplayName:Score":"Счет","Description:Score":"Это числовое пороговое значение от 0,0 до 1,0. Низкие пороговые значения позволят спамерам пройти проверку. По умолчанию вы можете использовать порог 0,5.","InvalidSiteKeyOrSiteSecret":"Недействительный ключ сайта или секрет сайта","CaptchaCanNotBeEmpty":"Пожалуйста, установите флажок reCAPTCHA","IncorrectCaptchaAnswer":"Неверный ответ на капчу","ScoreBelowThreshold":"Проверка не удалась, оценка ниже порогового значения","SetNullWillUseGlobalSettings":"Оставьте пустым, чтобы использовать значения по умолчанию.","ReturnToApplication":"Вернуться к приложению","AccountExternalProviderSettings":"Внешний провайдер","ExternalProviderEnabled":"Включено","ExternalProviderEnabledForTenant":"Разрешить арендаторам переопределять настройки","ExternalProviderEnabledForTenantExplanation":"При включении этой функции вы можете настроить параметры внешнего входа специально для пользователей арендатора. Если при включении функции не предоставлена информация о клиенте внешнего входа, будут использоваться настройки хоста","ExternalProviderEnabledForHost":"Включено для пользователей хоста","ExternalProviderEnabledForHostExplanation":"Когда вы включите эту функцию, информация о внешнем клиенте для входа, указанная ниже, будет применяться ко всем пользователям хоста","ExternalProviderOverrideHostValue":"Переопределить информацию о клиенте хоста","LinkedAccounts":"Связанные аккаунты","LoginAsThisAccount":"Войти как этот аккаунт","DeleteLinkAccountConfirmationMessage":"Вы уверены, что хотите удалить связанный аккаунт \\u0022{0}\\u0022?","NewLinkAccount":"Новый связанный аккаунт","NewLinkAccountWarning":"Вы выйдете из текущей учетной записи и войдете в другую учетную запись. Как только вы это сделаете, две учетные записи будут связаны.","TenantAndUserName":"Имя пользователя","DirectlyLinked":"Прямая ссылка","BackToMyAccount":"Назад к: {0}","LinkLogged":"Аккаунты связаны","StayWithCurrentAccount":"Оставайтесь с текущей учетной записью","ReturnToPreviousAccount":"Вернуться к аккаунту {0}","TheTargetAccountIsNotLinkedToYou":"Целевая учетная запись не связана с вами!","LinkAccountWarning":"Обратите внимание, что вы устанавливаете связь с другими аккаунтами. \\u003Ca href=\\u0022{0}\\u0022\\u003EНажмите здесь\\u003C/a\\u003E, чтобы отменить вход по ссылке!","SavedSuccessfully":"Успешно сохранено","AccessDenied":"Доступ запрещен!","AccessDeniedMessage":"У вас нет доступа к этому ресурсу.","RequestingYourPermission":"запрашивает ваше разрешение","UncheckThePermissionsYouDoNotWishToGrant":"Снимите флажки с разрешений, которые вы не хотите предоставлять.","ConsentPersonalInformation":"Личная информация","ConsentApplicationAccess":"Доступ к приложению","ScopeRequired":"требуется","RememberConsent":"Помни Мое Решение","UserDecisionYes":"Да, разрешить","UserDecisionNo":"Нет, не разрешать","DeviceAuthorization":"Авторизация устройства","UserCode":"Код пользователя","UserCodeInvalid":"Код пользователя недействителен!","DeviceAuthorizationSuccessfully":"Успех!","DeviceAuthorizationSuccessfullyInfo":"Вы успешно авторизовали устройство","LocalLoginIsNotEnabled":"Локальный вход не включен!","ExternalLoginCallbackError":"Ошибка обратного вызова внешнего входа: {0}","ExternalLoginInfoIsNotAvailable":"Информация о внешнем входе недоступна","ExternalLoginUserIsNotFound":"Пользователь внешнего входа не найден","SignIn":"Войти","SignOut":"выход","Date":"Дата","ResetAuthenticator":"Сбросить аутентификатор","ResetAuthenticator_Information":"Если вы хотите изменить приложение для аутентификации, вы можете сбросить его здесь.","ResetAuthenticatorWarningMessage":"Сбросьте ключ аутентификатора. Ваше приложение аутентификатора не будет работать, пока вы не переконфигурируете его.","UseTwoFactorAuthenticatorApp_Information":"Откройте приложение двухфакторной аутентификации и выполните одно из следующих действий:","UseTwoFactorAuthenticatorApp_Code":"Ваше приложение двухфакторной аутентификации сгенерирует код. Введите этот код в поле ниже и подтвердите.","RecoveryCode":"Код восстановления","RecoveryCodes":"Коды восстановления","RecoveryCodes_Information":"Поместите эти коды в надежное место. Если вы потеряете свое устройство и у вас не будет кодов восстановления, вы потеряете доступ к своей учетной записи.","CopyToClipboard":"Скопировать в буфер обмена","PrintCodes":"Распечатать коды","LoginWithRecoveryCode":"Войти с кодом восстановления","LoginWithRecoveryCode_Information":"Вы запросили вход с кодом восстановления. Функция «Запомнить меня» не будет активирована для этого входа в систему, пока вы не предоставите код приложения для аутентификации или не отключите 2FA и не войдете в систему снова.","LoginWithRecoveryCode_URL":"У вас нет доступа к устройству аутентификации? Вы можете войти с кодом восстановления","UseQrCode":"Используйте QR-код","ManuallyEnterCode":"Или вручную введите код","TwoFactorAuthenticationProviders":"Поставщики двухфакторной аутентификации","AuthenticatorApp":"Приложение для аутентификации","VerifyTheAuthenticator":"Проверьте аутентификатор","Step":"Шаг {0}","NextStep":"Следующий шаг","Done":"Сделанный","ShowPassword":"Показать пароль","HidePassword":"скрыть пароль","CapsLockOn":"Включен верхний регистр","TakePhoto":"Сделать фотографию","ChoosePhoto":"Выбрать фото","Strength":"Сила","Weak":"Слабый!","Fair":"Справедливо.","Normal":"Нормально.","Good":"Хорошо.","Strong":"Сильный!","Authentication:YouAreLoggedOut":"Вы вышли из системы.","OrRegisterWith":"Или зарегистрируйтесь с","RegisterWithOneOfTheFollowingProviders":"Зарегистрируйтесь с помощью одного из следующих поставщиков","ProtocolCallbackTitle":"Перенаправление...","ProtocolCallbackHeader":"Вы почти закончили!","ProtocolCallbackDescription":"Мы перенаправляем вас в приложение. Если вы не видите диалоговое окно, нажмите кнопку ниже.","ProceedToApp":"Перейти к приложению","UserLockedOut:WhatDoYouNeedToDo:":"Что вам нужно сделать?","YourAccountWillBeUnlockedOn":"Ваша учетная запись будет разблокирована","TryAgainAfterThisTimeOrContactAdministrator":"Попробуйте снова после этого времени или свяжитесь с администратором.","ExternalLogins":"Внешние входы","ExternalLogin:LoginProvider":"Поставщик входа","ExternalLogin:ProviderDisplayName":"Отображаемое имя поставщика","NewExternalLogin":"Новый внешний вход","ClickToLoginWithYourExternalAccount":"Нажмите, чтобы войти с вашей внешней учетной записью","YouHaveNoExternalLoginProviderToSignIn":"У вас нет внешнего поставщика входа для входа.","ExternalLoginDeleteConfirmationMessage":"Вы уверены, что хотите удалить внешний вход \\u0027{0}\\u0027?","YourSessionIsAboutToExpire":"Ваша сессия скоро истечет","YourSessionIsAboutToExpireInfo":"По соображениям безопасности ваше соединение разрывается после того, как вы были неактивны в течение некоторого времени. Вы хотите остаться в системе?","IdleSignOutNow":"Выход из системы","IdleStaySignedIn":"Оставаться в системе","IdleSessionTimeout":"Тайм-аут неактивной сессии","IdleSessionTimeoutInfo":"Тайм-аут неактивной сессии автоматически выходит из веб-приложений после периода бездействия. Если вы включите это, пользователи могут начать видеть больше запросов на вход.","LearnMoreAboutIdleSessionTimeout":"Узнайте больше о тайм-ауте неактивной сессии","IdleSignOutInactiveUserAutomatically":"Автоматически выходить из системы неактивного пользователя","IdleTimeoutMinutes":"Когда вы хотите, чтобы пользователи выходили из системы?","IdleTimeoutMinutesMustBeGreaterThanZero":"Тайм-аут неактивной сессии должен быть больше нуля","OneHour":"1 час","ThreeHours":"3 часа","SixHours":"6 часов","TwelveHours":"12 часов","TwentyFourHours":"24 часа","CustomIdleTimeoutMinutes":"Пользовательский (в минутах)","IdleCountdownSeconds":"Секунды обратного отсчета","IdleCountdownSecondsInfo":"Продолжительность обратного отсчета (в секундах) для отображения обратного отсчета сеанса перед выходом.","RequireMigrateSeedTitle":"Не удалось найти пользователя-администратора","RequireMigrateSeedMessage":"Убедитесь, что инициализация базы данных выполнена. См. \\u003Ca target=\\u0022_blank\\u0022 href=\\u0022https://abp.io/kb/0003\\u0022\\u003Eдокументацию\\u003C/a\\u003E для возможных решений.","ReSendCode":"Отправить код повторно","EmailSentTitle":"Код подтверждения отправлен на ваш электронный адрес.","EmailSentInfo":"Пожалуйста, проверьте свою почту, чтобы увидеть код подтверждения и завершить регистрацию.","SessionRevokeDescription":"Эта страница показывает все устройства, на которых вы вошли в систему. Вы можете принудительно выйти с любого устройства.","EnterAValueToUpdateSecret":"Введите значение, чтобы обновить секрет","SelectAccount":"Выбрать аккаунт","SignedInAs":"Вы вошли как","Continue":"Продолжить","SwitchToAnotherAccount":"Переключиться на другой аккаунт","CreateANewAccount":"Создать новый аккаунт","ChangeEmail":"Подтвердите новый адрес электронной почты","ChangeEmailInfoInEmailHi":"Здравствуйте,","ChangeEmailInfoInEmail":"Мы получили запрос на изменение адреса электронной почты, связанного с вашей учетной записью. Чтобы подтвердить это изменение, пожалуйста, перейдите по ссылке ниже:","ConfirmNewEmailAddress":"Подтвердить новый адрес электронной почты","ChangeEmailInfoInEmailTip":"Если вы не запрашивали это, вы можете безопасно проигнорировать это сообщение — ваш текущий адрес электронной почты останется без изменений.","YourEmailAddressWillNotBeChangedUntilYouVerifyIt":"Изменение адреса электронной почты на {0} ожидает подтверждения. Пожалуйста, проверьте свой почтовый ящик и перейдите по ссылке, чтобы подтвердить обновление.","YourEmailAddressIsSuccessfullyChanged":"Ваш адрес электронной почты успешно обновлен на {0}."}	2026-04-29 11:54:37.050072	\N
3a20e9c3-a2d9-6e36-55b3-13288b8e8a1f	AbpAccount	zh-Hans	{"Volo.Account:InvalidEmailAddress":"找不到给定的电子邮件地址：{0}","Volo.Account:SelfRegistrationDisabled":"已禁用自助注册！","Volo.Account:PhoneNumberEmpty":"电话号码为空！","Volo.Account:PhoneNumberConfirmationDisabled":"电话号码确认已禁用！","Volo.Account:InvalidUserToken":"用户令牌无效！","Volo.Account:UnsupportedTwoFactorProvider":"不支持双因素提供者！","Volo.Account:ImpersonateTenantOnlyAvailableForHost":"模拟租户只适用于主机！","Volo.Account:RequirePermissionToImpersonateTenant":"需要 {PermissionName} 权限才能模拟租户！","Volo.Account:ThereIsNoUserWithUserName":"没有用户名为 {UserName} 的用户！","Volo.Account:YouCanNotImpersonateYourself":"你不能模拟自己！","Volo.Account:NestedImpersonationIsNotAllowed":"不允许嵌套模拟！","Volo.Account:RequirePermissionToImpersonateUser":"需要 {PermissionName} 权限才能模拟用户！","Volo.Account:ThereIsNoUserWithId":"没有 ID:{UserId} 的用户！","Volo.Account:InvalidAccessToken":"访问令牌无效！","Volo.Account:InvalidTenantIdOrUserId":"无效的TenantId或UserId!","Volo.Account:InvalidUserDelegationId":"无效的UserDelegationId!","Volo.Account:InvalidEmailConfirmationCode":"您输入的确认码无效。请再检查一次。","Volo.Account:EmailConfirmationCodeExpired":"您输入的确认码已过期。请重新发送验证码并重试。","Volo.Account:EmailConfirmationCodeLimitReached":"您最近已发送电子邮件验证码。您可以在 {0} 重新发送下一个验证码。","Volo.Account:PhoneConfirmationCodeLimitReached":"您最近已发送手机验证码。您可以在 {0} 重新发送验证码。","Volo.Account:DelegatedImpersonationIsDisabled":"委托模拟已禁用！","Volo.Account:UserDelegationIsNotAvailableForImpersonatedUsers":"用户授权不适用于模拟用户！","Volo.Account:ImpersonateError":"模拟错误！","Volo.Account:StartTimeMustBeLessThanEndTime":"开始时间必须小于结束时间！","DelegatedImpersonation":"委托模拟","BackToImpersonator":"返回我的账户","SwitchToUser":"切换到用户","ExpiresAt":"到期日","AuthorityDelegation":"授权委托","DelegateNewUser":"委托新用户","DelegatedUsers":"委托用户","MyDelegatedUsers":"我的委托用户","DelegationDateRange":"委托日期范围","AreYouSure":"你确定吗？","StartTime":"开始时间","EndTime":"结束时间","Status":"状态","StatusActive":"有效","StatusExpired":"已过期","StatusFuture":"未来","DeleteUserDelegationConfirmationMessage":"您确定要删除委托\\u0022{0}\\u0022吗？","AuthorityDelegation:PleaseSelectUser":"请选择用户","AuthorityDelegation:PleaseSelectDelegationDateRange":"请选择委托日期范围","PasswordReset":"密码重置","InvitationLinkIsInvalid":"邀请链接无效。","RegisterToJoinTenant":"注册以加入租户“{0}”？","YouHaveSuccessfullyJoinedTheTenant":"您已成功加入租户“{0}”。","TenantName":"租户名称","SwitchTenant":"切换租户","HostDisplayName":"主机","SwitchToThisTenant":"切换到此租户","AreYouSureYouWantToLeaveTenant":"您确定要离开租户“{0}”吗？","YouAreAlreadyInTheTenant":"您已经在租户“{0}”中！","UserNotFoundByEmail":"按邮箱未找到用户：{0}","SelectTenant":"选择租户","SelectTenantMessage":"选择一个租户以继续登录。","PendingTenant":"需要租户","PendingTenantContactAdmin":"请让管理员将邀请发送到你的邮箱：{0}。","CreateNewTenantInfo":"创建一个新租户并成为其管理员。","CreateNewTenant":"创建新租户","TenantNameAlreadyExistsWarningMessage":"租户名称“{0}”已存在。请选用其他名称。","TheTenantCreateRequestHasBeenSubmitted":"租户创建请求已提交。请稍后再次使用 {0} 登录。","PasswordResetInfoInEmail":"我们收到了账户恢复请求！如果您发起了该请求，请单击以下关联重置密码。","ResetMyPassword":"重置密码","NotAMemberYet":"还不是会员？","OrSignInWith":"或通过以下方式登录","SignInWith":"使用{0}登录","RegisterWith":"使用{0}注册","SignInWithOneOfTheFollowingProviders":"使用以下提供者之一登录","EmailConfirmation":"电子邮件确认","EmailConfirmationInfoInEmail":"请点击以下关联确认您的电子邮件地址。","ConfirmMyEmail":"确认我的电子邮件地址","UserName":"用户名","EmailAddress":"电子邮件地址","UserNameOrEmailAddress":"用户名或电子邮件地址","Password":"密码","RememberMe":"记住我","SelectedProvider":"选定的提供者","UseAnotherServiceToLogin":"使用其他服务登录","UserLockedOut":"锁定","UserLockedOutMessage":"您的账户已被管理员锁定或由于登录尝试无效。请稍后再试。如果您认为这是一个错误，请联系您的系统管理员。","InvalidUserNameOrPassword":"用户名或密码无效！","LoginIsNotAllowed":"您无法登录！您的帐户未激活或需要确认您的电子邮件/电话号码。","SelfRegistrationDisabledMessage":"此应用程序已禁用自助用户注册。请联系应用程序管理员注册新用户。","VerifySecurityCodeNotLoggedInErrorMessage":"您应该先登录，以便验证自己！您的登录可能已超时。请进入登录页面重新尝试。","InvalidSecurityCode":"双因素代码无效！","EmailSecurityCodeBody":"你的双因素代码是: {0}","EmailSecurityCodeSubject":"双因素代码","EmailConfirmationCodeSubject":"电子邮件确认","EmailConfirmationCodeBody":"请使用代码完成注册过程。您的注册代码：{0}","VerifySecurityCode_Information":"请输入发送给你的验证码.","SendSecurityCode_Information":"您必须通过验证才能登录。请选择验证类型。系统将根据所选验证类型发送验证码。","ForgotPassword":"忘记密码？","SendPasswordResetLink_Information":"我们将向您的电子邮件发送密码重置关联，以重置您的密码。如果您在几分钟内没有收到电子邮件，请重新尝试。","PasswordResetMailSentMessage":"帐户恢复电子邮件已发送到您的电子邮箱。如果您在 15 分钟内没有在收件箱中看到这封邮件，请在垃圾邮件文件夹中查找。如果在那里找到，请将其标记为 \\u0022非垃圾邮件\\u0022。","PreventEmailEnumerationPasswordResetMailSentMessage":"如果提供的电子邮件地址在系统中注册过，我们会发送一个重置密码链接。如果您几分钟内没有收到邮件，请检查您的垃圾箱或稍后再试。","ResetPassword":"重置密码","ConfirmPassword":"确认（重复）密码","ResetPassword_Information":"请输入您的新密码。","YourPasswordIsSuccessfullyReset":"您的密码重置成功。","YourEmailAddressIsSuccessfullyConfirmed":"您的电子邮件地址已成功确认。","MailSendingFailed":"邮件发送失败，请检查您的电子邮件配置并重试。","GoToTheHomePage":"转到主页","TwoFactorVerification":"双因素验证","BackToLogin":"返回登录","AlreadyRegistered":"已经注册？","Permission:Account":"账户","Permission:SettingManagement":"设置管理","TwoFactorAuthentication":"双因素验证","DisplayName:IsSelfRegistrationEnabled":"启用自助注册","Description:IsSelfRegistrationEnabled":"用户可以自己创建账户。","DisplayName:IsRememberBrowserEnabled":"记住浏览器","DisplayName:PreventEmailEnumeration":"防止电子邮件枚举","Description:PreventEmailEnumeration":"“忘记密码”页面不会返回给定的电子邮件是否在系统中注册。","Menu:Account":"账户","Menu:Account.ExternalProvider":"账户外部提供者","AccountSettingsGeneral":"通用","AccountSettingsTwoFactor":"双因素","TwoFactorHasBeenDisabled":"双因素已被禁用。","DisplayName:CurrentPassword":"当前密码","DisplayName:NewPassword":"新密码","DisplayName:NewPasswordConfirm":"确认新密码","PasswordChangedMessage":"您的密码已成功更改。","DisplayName:UserName":"用户名","DisplayName:Email":"电子邮件","DisplayName:Name":"名称","DisplayName:Surname":"姓氏","DisplayName:Password":"密码","DisplayName:EmailAddress":"电子邮件地址","DisplayName:PhoneNumber":"电话号码","DisplayName:Timezone":"时区","DefaultTimeZone":"默认时区","TimezoneHelpText":"默认时区将尝试使用浏览器或服务器的时区。","PersonalSettings":"个人设置","PersonalSettingsSaved":"已保存个人设置","PersonalSettingsChangedConfirmationModalTitle":"个人信息已更改","PersonalSettingsChangedConfirmationModalDescription":"如果要应用这些更改，您必须先登录。要退出吗？","PasswordChanged":"密码已更改","TwoFactorChanged":"已保存双因素设置","DisplayName:TwoFactorEnabled":"启用双因素验证","TwoFactorEnabledInfo":"当您删除所有双因素提供者时，双因素验证将无法工作。","YouHaveToEnableAtLeastOneTwoFactorProvider":"您必须启用至少一个双因素提供者才能启用双因素功能！","NewPasswordConfirmFailed":"请确认新密码。","NewPasswordSameAsOld":"新密码必须与旧密码不同。","Manage":"管理","MyAccount":"我的账户","UserInformation":"用户信息","DisplayName:EnableLocalLogin":"允许使用本地用户名和密码注册和登录。","Description:EnableLocalLogin":"服务器是否允许使用本地用户名和密码注册和登录。","SocialAccountSecurity":"社交账户安全","DisplayName:VerifyPasswordDuringExternalLogin":"在社交账户关联时需要本地密码","Description:VerifyPasswordDuringExternalLogin":"通过本地注册和外部/社交登录使用相同电子邮件地址注册的用户将在第一次外部/社交登录时需要输入本地密码。","Feature:AccountGroup":"账户","DoYouWantToVerifyPhoneNumberMessage":"您更改了电话号码信息。现在要验证吗？","InvalidPhoneConfirmationToken":"验证码无效！","ConfirmYourPhoneNumber":"验证您的电话号码","Verify":"验证","Verified":"已验证","NotVerified":"未核实","FirstlySubmitToVerify":"首先保存新信息，然后进行验证。","EmailConfirmationSentMessage":"验证关联已发送到您的电子邮件 ({0})。","ConfirmationTokenSentMessage":"验证码已发送至您的电话号码。","PhoneConfirmationToken":"请输入下面的代码以验证您的电话号码：","PhoneConfirmationSms":"你好 {0}！这是您的验证码：{1}","ConfirmUser":"确认您的电子邮件/电话号码","PhoneNumberEmptyHelpText":"您没有电话号码，验证后会为您设置。","TextTemplate:Abp.Account.Layout":"账户电子邮件布局","TextTemplate:Abp.Account.PasswordResetLink":"密码重置电子邮件","TextTemplate:Abp.Account.EmailConfirmationLink":"确认电子邮件","TextTemplate:Abp.Account.EmailSecurityCode":"电子邮件安全代码","LoggedOutTitle":"签出","LoggedOutText":"您已退出登录，您将很快被重新定向。","ReturnToText":"单击此处返回申请表","MySecurityLogs":"安全日志","MySecurityLogs:StartTime":"开始时间","MySecurityLogs:EndTime":"结束时间","MySecurityLogs:Application":"应用","MySecurityLogs:Identity":"身份","MySecurityLogs:Action":"操作","MySecurityLogs:Client":"Client","MySecurityLogs:Time":"创建时间","MySecurityLogs:CorrelationId":"CorrelationId","MySecurityLogs:IpAddress":"IP 地址","MySecurityLogs:Browser":"浏览器","LoginToTheApplication":"登录应用程序","RememberBrowser":"记住这个浏览器","Code":"代码","ProfilePicture":"简介图片","MoveCursorOnExamples":"在图片上移动光标，即可看到圆形样式。","ProfilePictureWillBeChanged":"您的个人照片将被更改。","CurrentProfilePicture":"当前简介图片","ChangeProfilePicture":"更改简介图片","SelectNewImage":"选择新图像","DisplayName:UseGravatar":"使用 Gravatar","Description:UseGravatar":"使用 Gravatar 服务制作个人照片。","SaveChanges":"保存更改","UploadFile":"上传文件","UseDefault":"使用默认头像","UseGravatarConfirm":"您将使用 Gravatar 作为个人资料图片。","NoProfilePictureConfirm":"您将使用默认头像作为个人照片。","PPUploadConfirm":"您将使用所选图片作为个人照片。","PleaseSelectImage":"请选择图片","UploadFailedMessage":"上传失败！","AccountPro:0001":"您必须上传图片！","AccountPro:0002":"邀请令牌无效或已被使用。","ProfilePicture:InvalidFileExtension":"上传的文件扩展名不受支持。请上传有效的图片文件。","ProfilePicture:InvalidFileContent":"上传的文件内容与预期的图片格式不匹配。","ProfilePicture:FileSizeExceeded":"上传的文件超过了允许的最大大小。","ProfileTab:Picture":"简介图片","ProfileTab:Password":"更改密码","ProfileTab:PersonalInfo":"个人信息","ProfileTab:TwoFactor":"双因素验证","ProfileTab:AuthenticatorApp":"身份验证程序","ProfileTab:Passkeys":"通行密钥","Passkey":"通行密钥","PasskeyDescription":"通行密钥允许用户使用基于设备的身份验证（Face ID、Touch ID、Windows Hello）而不是密码进行登录。这可以提高安全性并免去记住凭据的负担。启用通行密钥后，用户可以在其设备间创建和管理安全的登录密钥。","EnablePasskey":"启用通行密钥","MaximumPasskeysPerUser":"每个用户的通行密钥上限","PasskeyLogin":"通行密钥登录","AddPasskey":"添加通行密钥","ChangePasskeyName":"更改通行密钥名称","PasskeyName":"通行密钥名称","UnnamedPasskey":"未命名的通行密钥","PasskeyCreatedAt":"创建于","PasskeyDeletionConfirmationMessage":"确定要删除此通行密钥吗？","PasskeyFeatureDisabled":"已禁用通行密钥功能。","PasskeyWarningMessage":"使用通行密钥后，你可以通过设备的指纹、面部识别、屏幕锁或硬件安全密钥登录此应用。请保护好你的设备和屏幕锁，确保只有授权的人可以访问你的账户。","AddOrUpdatePasskeyFailed":"无法将通行密钥添加到你的账户。","RemovePasskeyFailed":"无法从你的账户移除通行密钥","PasskeyAttestationFailed":"通行密钥证明失败。","InvalidCredential":"凭据不是有效的 Base64 字符串。","MaxPasskeysReached":"你已达到通行密钥数量上限。","MaximumPasskeysPerUserMustBeGreaterThanZero":"每个用户的通行密钥上限必须大于零","DisplayName:Passkey.Enabled":"启用通行密钥登录","Description:Passkey.Enabled":"允许用户使用通行密钥登录。","DisplayName:Passkey.MaximumPasskeysPerUser":"每个用户的通行密钥上限","Description:Passkey.MaximumPasskeysPerUser":"用户可注册的通行密钥最大数量。","BrowserDoesNotSupportPasskeys":"你的浏览器不支持通行密钥。请使用兼容浏览器（如 Chrome、Edge 或 Firefox）。","InvalidPasskey":"通行密钥无效或无法识别。","AddPasskeyForAccount":"为你的账户添加通行密钥","SkipAddPasskeyAndContinue":"跳过添加通行密钥并继续","DisplayName:UseCaptchaOnLogin":"在用户登录时使用验证码","Description:UseCaptchaOnLogin":"在用户登录时使用验证码","DisplayName:UseCaptchaOnRegistration":"在用户注册时使用验证码","Description:UseCaptchaOnRegistration":"在用户注册时使用验证码","DisplayName:UseCaptchaOnForgotPassword":"在密码重置时使用验证码","Description:UseCaptchaOnForgotPassword":"在密码重置时使用验证码","Captcha":"验证码","GoogleCaptcha":"Google reCAPTCHA","GoogleCaptchaDescription":"reCAPTCHA 保护您的网站免受欺诈和滥用","DisplayName:VerificationUrl":"验证 URL","Description:VerificationUrl":"谷歌服务的URL。示例 URL：https://www.google.com/，https://www.recaptcha.net/","DisplayName:SiteKey":"Site key","Description:SiteKey":"它用于在您的网站上调用 reCAPTCHA 服务。留空以使用网站配置代码中的默认值。","DisplayName:SiteSecret":"Site secret","Description:SiteSecret":"它授权您的网站与 reCAPTCHA 服务器之间的通信，以验证用户的响应。留空以使用网站配置代码中的默认值。","DisplayName:Version":"版本","Description:Version":"reCAPTCHA v3 使用分数验证请求，reCAPTCHA v2 使用质询验证请求。","DisplayName:Score":"分数","Description:Score":"它是从 0.0 到 1.0 的数字阈值。较低的阈值值将允许垃圾邮件发送者通过验证。默认情况下，您可以使用 0.5 的阈值。","InvalidSiteKeyOrSiteSecret":"网站密钥或网站秘密无效","CaptchaCanNotBeEmpty":"请勾选 reCAPTCHA 框","IncorrectCaptchaAnswer":"不正确的验证码答案","ScoreBelowThreshold":"验证失败，得分低于阈值","SetNullWillUseGlobalSettings":"留空以使用全局设置","ReturnToApplication":"返回到应用程序","AccountExternalProviderSettings":"外部登录设置","ExternalProviderEnabled":"已启用","ExternalProviderEnabledForTenant":"允许租户覆盖设置","ExternalProviderEnabledForTenantExplanation":"启用此功能后，您可以为租户用户设置特定的外部登录设置。如果在启用该功能时未提供外部登录客户端信息，将使用主机设置","ExternalProviderEnabledForHost":"为主机用户启用","ExternalProviderEnabledForHostExplanation":"启用此功能后，以下外部登录客户端信息将应用于所有主机用户","ExternalProviderOverrideHostValue":"覆盖主机客户端信息","LinkedAccounts":"关联账户","LoginAsThisAccount":"以该账户登录","DeleteLinkAccountConfirmationMessage":"您确定要删除关联帐户\\u0022{0}\\u0022吗？","NewLinkAccount":"新关联帐户","NewLinkAccountWarning":"您将从当前账户退出，然后用另一个账户登录。完成登录后，两个账户将被关联起来。","TenantAndUserName":"用户名","DirectlyLinked":"直接关联","BackToMyAccount":"返回：{0}","LinkLogged":"帐户已经关联","StayWithCurrentAccount":"继续使用当前账户","ReturnToPreviousAccount":"返回 {0} 账户","TheTargetAccountIsNotLinkedToYou":"目标账户与您没有关联！","LinkAccountWarning":"请注意，您正在关联到其他账户，\\u003Ca href=\\u0022{0}\\u0022\\u003E单击此处\\u003C/a\\u003E取消关联登录！","SavedSuccessfully":"保存成功","AccessDenied":"拒绝访问！","AccessDeniedMessage":"您无法访问此资源。","RequestingYourPermission":"请求您的许可","UncheckThePermissionsYouDoNotWishToGrant":"取消选中不希望授予的权限。","ConsentPersonalInformation":"个人信息","ConsentApplicationAccess":"应用程序访问","ScopeRequired":"所需","RememberConsent":"记住我的决定","UserDecisionYes":"是，允许","UserDecisionNo":"否，不允许","DeviceAuthorization":"设备授权","UserCode":"用户代码","UserCodeInvalid":"用户代码无效！","DeviceAuthorizationSuccessfully":"成功！","DeviceAuthorizationSuccessfullyInfo":"您已成功授权设备","LocalLoginIsNotEnabled":"未启用本地登录！","ExternalLoginCallbackError":"外部登录回调错误：{0}","ExternalLoginInfoIsNotAvailable":"外部登录信息不可用","ExternalLoginUserIsNotFound":"未找到外部登录用户","SignIn":"登录","SignOut":"签出","Date":"日期","ResetAuthenticator":"重置验证器","ResetAuthenticator_Information":"如果你想更换身份验证程序，可以在这里重新设置。","ResetAuthenticatorWarningMessage":"重置身份验证器密钥 在重新配置之前，身份验证器应用程序将无法运行。","UseTwoFactorAuthenticatorApp_Information":"打开双因素身份验证应用程序，执行以下操作之一：","UseTwoFactorAuthenticatorApp_Code":"您的双因素身份验证应用程序将生成一个代码，请在下框中输入该代码并确认。","RecoveryCode":"恢复代码","RecoveryCodes":"恢复代码","RecoveryCodes_Information":"将这些代码放在安全的地方。如果您丢失了设备而又没有恢复密码，您将无法访问您的帐户。","CopyToClipboard":"复制到剪贴板","PrintCodes":"打印代码","LoginWithRecoveryCode":"使用恢复密码登录","LoginWithRecoveryCode_Information":"您请求使用恢复密码登录。在您提供身份验证应用程序代码或禁用 2FA 并重新登录之前，\\u0022记住我 \\u0022将不会在此次登录中启用。","LoginWithRecoveryCode_URL":"无法使用身份验证设备？您可以使用恢复密码登录","UseQrCode":"使用二维码","ManuallyEnterCode":"或手动输入密码","TwoFactorAuthenticationProviders":"双因素身份验证提供者","AuthenticatorApp":"身份验证应用程序","VerifyTheAuthenticator":"验证身份验证器","Step":"步骤 {0}","NextStep":"下一步","Done":"已完成","ShowPassword":"显示密码","HidePassword":"隐藏密码","CapsLockOn":"大写锁定打开","TakePhoto":"拍摄照片","ChoosePhoto":"选择照片","Strength":"强度","Weak":"弱！","Fair":"一般","Normal":"正常。","Good":"很好。","Strong":"强！","Authentication:YouAreLoggedOut":"您已退出登录。","OrRegisterWith":"或注册","RegisterWithOneOfTheFollowingProviders":"使用以下提供者之一注册","Sessions":"会话","ProtocolCallbackTitle":"重定向...","ProtocolCallbackHeader":"快完成了！","ProtocolCallbackDescription":"我们将重定向到应用程序。如果你没有看到对话框，请单击下面的按钮。","ProceedToApp":"继续到应用程序","UserLockedOut:WhatDoYouNeedToDo:":"你需要做什么？","YourAccountWillBeUnlockedOn":"你的账户被锁定至","TryAgainAfterThisTimeOrContactAdministrator":"请在此时间之后再试一次或联系管理员。","ExternalLogins":"外部登录","ExternalLogin:LoginProvider":"登录提供者","ExternalLogin:ProviderDisplayName":"提供者显示名称","NewExternalLogin":"新的外部登录","ClickToLoginWithYourExternalAccount":"点击使用您的外部账户登录","YouHaveNoExternalLoginProviderToSignIn":"您没有外部登录提供者可供登录。","ExternalLoginDeleteConfirmationMessage":"您确定要删除外部登录 \\u0027{0}\\u0027 吗？","YourSessionIsAboutToExpire":"您的会话即将过期","YourSessionIsAboutToExpireInfo":"出于安全原因，您在一段时间内处于非活动状态后，连接将超时。您要保持登录状态吗？","IdleSignOutNow":"现在退出","IdleStaySignedIn":"保持登录","IdleSessionTimeout":"空闲会话超时","IdleSessionTimeoutInfo":"空闲会话超时会在一段时间内自动将用户从 Web 应用程序中退出。如果您打开此功能，用户可能会开始看到更多的登录提示。","LearnMoreAboutIdleSessionTimeout":"了解有关空闲会话超时的更多信息","IdleSignOutInactiveUserAutomatically":"自动退出不活动用户","IdleTimeoutMinutes":"您希望用户何时退出登录？","IdleTimeoutMinutesMustBeGreaterThanZero":"空闲超时分钟必须大于零","OneHour":"1 小时","ThreeHours":"3 小时","SixHours":"6 小时","TwelveHours":"12 小时","TwentyFourHours":"24 小时","CustomIdleTimeoutMinutes":"自定义（分钟）","IdleCountdownSeconds":"倒计时秒数","IdleCountdownSecondsInfo":"在注销之前显示会话倒计时的倒计时时长（以秒为单位）。","RequireMigrateSeedTitle":"找不到管理员用户","RequireMigrateSeedMessage":"请确保已执行数据库种子。有关可能的解决方案，请参阅\\u003Ca target=\\u0022_blank\\u0022 href=\\u0022https://abp.io/kb/0003\\u0022\\u003E文档\\u003C/a\\u003E。","ReSendCode":"重新发送验证码","EmailSentTitle":"确认码已发送到您的电子邮箱。","EmailSentInfo":"请检查您的收件箱以查看确认码并完成注册。","SessionRevokeDescription":"此页面显示您已登录的所有设备。您可以强制退出任意设备。","EnterAValueToUpdateSecret":"输入一个值以更新密钥。","SelectAccount":"选择账户","SignedInAs":"登录为","Continue":"继续","SwitchToAnotherAccount":"切换到其他账户","CreateANewAccount":"创建新账户","ChangeEmail":"确认您的新电子邮件地址","ChangeEmailInfoInEmailHi":"您好，","ChangeEmailInfoInEmail":"我们收到更改与您的帐户关联的电子邮件地址的请求。要确认此更改，请单击以下链接：","ConfirmNewEmailAddress":"确认新电子邮件地址","ChangeEmailInfoInEmailTip":"如果您未请求此操作，则可以安全地忽略此消息 —— 您当前的电子邮件将保持不变。","YourEmailAddressWillNotBeChangedUntilYouVerifyIt":"将电子邮件地址更改为 {0} 的请求正在等待验证。请检查您的收件箱并按照链接确认更新。","YourEmailAddressIsSuccessfullyChanged":"您的电子邮件地址已成功更新为 {0}。"}	2026-04-29 11:54:37.050176	\N
3a20e9c3-a2dd-5861-6153-2a693c1c8b5c	AbpOperationRateLimiting	ru	{"Volo.Abp.OperationRateLimiting:010001":"Превышен лимит частоты операций. Вы можете повторить попытку через {RetryAfter}.","RetryAfter:Years":"{0} год/лет","RetryAfter:YearsAndMonths":"{0} год/лет и {1} месяц/месяцев","RetryAfter:Months":"{0} месяц/месяцев","RetryAfter:MonthsAndDays":"{0} месяц/месяцев и {1} день/дней","RetryAfter:Days":"{0} день/дней","RetryAfter:DaysAndHours":"{0} день/дней и {1} час/часов","RetryAfter:Hours":"{0} час/часов","RetryAfter:HoursAndMinutes":"{0} час/часов и {1} минута/минут","RetryAfter:Minutes":"{0} минута/минут","RetryAfter:MinutesAndSeconds":"{0} минута/минут и {1} секунда/секунд","RetryAfter:Seconds":"{0} секунда/секунд","Volo.Abp.OperationRateLimiting:010002":"Превышен лимит частоты операций. Этот запрос постоянно отклонён."}	2026-04-29 11:54:37.050259	\N
3a20e9c3-a2df-bed4-10b7-c95ac5b42b6f	AbpOperationRateLimiting	vi	{"Volo.Abp.OperationRateLimiting:010001":"Đã vượt quá giới hạn tốc độ thao tác. Bạn có thể thử lại sau {RetryAfter}.","RetryAfter:Years":"{0} năm","RetryAfter:YearsAndMonths":"{0} năm và {1} tháng","RetryAfter:Months":"{0} tháng","RetryAfter:MonthsAndDays":"{0} tháng và {1} ngày","RetryAfter:Days":"{0} ngày","RetryAfter:DaysAndHours":"{0} ngày và {1} giờ","RetryAfter:Hours":"{0} giờ","RetryAfter:HoursAndMinutes":"{0} giờ và {1} phút","RetryAfter:Minutes":"{0} phút","RetryAfter:MinutesAndSeconds":"{0} phút và {1} giây","RetryAfter:Seconds":"{0} giây","Volo.Abp.OperationRateLimiting:010002":"Vượt quá giới hạn tần suất thao tác. Yêu cầu này bị từ chối vĩnh viễn."}	2026-04-29 11:54:37.050272	\N
3a20e9c3-a2e1-099a-f139-58dfbc00b9e2	AbpOperationRateLimiting	zh-Hans	{"Volo.Abp.OperationRateLimiting:010001":"操作频率超出限制。请在 {RetryAfter} 后重试。","RetryAfter:Years":"{0} 年","RetryAfter:YearsAndMonths":"{0} 年 {1} 个月","RetryAfter:Months":"{0} 个月","RetryAfter:MonthsAndDays":"{0} 个月 {1} 天","RetryAfter:Days":"{0} 天","RetryAfter:DaysAndHours":"{0} 天 {1} 小时","RetryAfter:Hours":"{0} 小时","RetryAfter:HoursAndMinutes":"{0} 小时 {1} 分钟","RetryAfter:Minutes":"{0} 分钟","RetryAfter:MinutesAndSeconds":"{0} 分钟 {1} 秒","RetryAfter:Seconds":"{0} 秒","Volo.Abp.OperationRateLimiting:010002":"操作频率超出限制。此请求已被永久拒绝。"}	2026-04-29 11:54:37.050283	\N
3a20e9c3-a2e2-9f36-0b0f-d72f08e4beef	LeptonX	en	{"Login":"Login","Appearance":"Appearance","ContainerWidth":"Container Width","ContainerWidth:Boxed":"Boxed Layout","ContainerWidth:Fixed":"Fixed","ContainerWidth:Fluid":"Fluid","ContainerWidth:FullWidth":"Full Width","GeneralSettings":"General Settings","Language":"Language","Settings":"Settings","Theme:dark":"Dark","Theme:dim":"Semi-Dark","Theme:light":"Light","Theme:system":"System","Welcome":"Welcome","FilterMenu":"Filter menu","Authentication:YouAreLoggedOut":"You are logged out.","GivenTenantIsNotExist":"Given tenant doesn\\u0027t exist: {0}","GivenTenantIsNotAvailable":"Given tenant isn\\u0027t available: {0}","Tenant":"Tenant","Switch":"switch","Name":"Name","SwitchTenantHint":"Leave the name field blank to switch to the host side.","SwitchTenant":"Switch tenant","NotSelected":"Not selected","Menu":"Menu"}	2026-04-29 11:54:37.050294	\N
3a20e9c3-a2e4-f856-1c65-c0c6628536ce	LeptonX	ru	{"Login":"Войти","Appearance":"вид","ContainerWidth":"Ширина контейнера","ContainerWidth:Boxed":"Макет в штучной упаковке","ContainerWidth:Fixed":"Фиксированная","ContainerWidth:Fluid":"Жидкость","ContainerWidth:FullWidth":"Полная ширина","GeneralSettings":"общие настройки","Language":"Язык","Settings":"Настройки","Theme:dark":"Темный","Theme:dim":"полутемный","Theme:light":"Легкий","Theme:system":"Система","Welcome":"Добро пожаловать","FilterMenu":"Меню фильтр","Menu":"Меню"}	2026-04-29 11:54:37.050304	\N
3a20e9c3-a2e5-3334-c0ca-6f4feffa4802	LeptonX	vi	{"Login":"Đăng nhập","Appearance":"Vẻ bề ngoài","ContainerWidth":"Chiều rộng vùng chứa","ContainerWidth:Boxed":"Bố cục hộp","ContainerWidth:Fixed":"Bố cục cố định","ContainerWidth:Fluid":"Chất lỏng","ContainerWidth:FullWidth":"Toàn bộ chiều rộng","GeneralSettings":"Cài đặt chung","Language":"Ngôn ngữ","Settings":"Cài đặt","Theme:dark":"Tối","Theme:dim":"Nửa tối","Theme:light":"Ánh sáng","Theme:system":"Hệ thống","Welcome":"Chào mừng","FilterMenu":"Menu lọc","Menu":"Menu"}	2026-04-29 11:54:37.050316	\N
3a20e9c3-a2e7-9b8c-338a-514960db58d2	LeptonX	zh-Hans	{"Login":"登录","Appearance":"外貌","ContainerWidth":"容器宽度","ContainerWidth:Boxed":"盒装布局","ContainerWidth:Fixed":"固定布局","ContainerWidth:Fluid":"流体布局","ContainerWidth:FullWidth":"全宽","GeneralSettings":"通用设置","Language":"语","Settings":"设置","Theme:dark":"黑暗的","Theme:dim":"半暗","Theme:light":"光","Theme:system":"与系统相同","Welcome":"欢迎","FilterMenu":"过滤菜单","Authentication:YouAreLoggedOut":"你已经注销","GivenTenantIsNotExist":"给定的租户不存在: {0}","GivenTenantIsNotAvailable":"给定的租户不可用: {0}","Tenant":"租户","Switch":"切换","Name":"名称","SwitchTenantHint":"将名称字段留空以切换到宿主端.","SwitchTenant":"切换租户","NotSelected":"未选中","Menu":"菜单"}	2026-04-29 11:54:37.050327	\N
3a20e9c3-a2e8-51f1-5170-5b350cb8e0fc	AbpUiMultiTenancy	en	{"GivenTenantIsNotExist":"Given tenant doesn\\u0027t exist: {0}","GivenTenantIsNotAvailable":"Given tenant isn\\u0027t available: {0}","Tenant":"Tenant","Switch":"switch","Name":"Name","SwitchTenantHint":"Leave the name field blank to switch to the host side.","SwitchTenant":"Switch tenant","NotSelected":"Not selected"}	2026-04-29 11:54:37.050336	\N
3a20e9c3-a2ea-3cc8-78f8-f284ff7b4020	AbpUiMultiTenancy	ru	{"GivenTenantIsNotExist":"Данный арендатор не существует: {0}","GivenTenantIsNotAvailable":"Данный арендатор недоступен: {0}","Tenant":"Арендатор","Switch":"переключиться","Name":"Имя","SwitchTenantHint":"Оставьте поле Имя пустым, чтобы переключиться на администратора.","SwitchTenant":"Сменить арендатора","NotSelected":"Не выбрано"}	2026-04-29 11:54:37.050346	\N
3a20e9c3-a2eb-deed-352d-df7451be59c0	AbpUiMultiTenancy	vi	{"GivenTenantIsNotExist":"Người thuê đã cho không tồn tại: {0}","GivenTenantIsNotAvailable":"Người thuê không có sẵn: {0}","Tenant":"Người thuê","Switch":"Chuyển đổi","Name":"Tên","SwitchTenantHint":"Để trống trường tên để chuyển sang phía máy chủ.","SwitchTenant":"Chuyển đổi người thuê nhà","NotSelected":"Không được chọn"}	2026-04-29 11:54:37.050355	\N
3a20e9c3-a2ed-3c69-2f0a-9d7149bdecdb	AbpUiMultiTenancy	zh-Hans	{"GivenTenantIsNotExist":"给定租户不存在：{0}","GivenTenantIsNotAvailable":"给定租户不可用：{0}","Tenant":"租户","Switch":"切换","Name":"名称","SwitchTenantHint":"将名称字段留空以切换到主机端。","SwitchTenant":"切换租户","NotSelected":"未选择"}	2026-04-29 11:54:37.050367	\N
3a20eee2-7863-3a70-4ae8-7959dc688faa	AbpGdpr	ru	{"Volo.Abp.Gdpr:010001":"Вы ранее запросили загрузку личных данных. По истечении заданного периода времени запроса вы можете создать новый.","Volo.Abp.Gdpr:010002":"Ваши персональные данные все еще готовятся. Вы можете скачать его по адресу {GdprDataReadyTime}.","PersonalData":"Личные данные","Menu:PersonalData":"Личные данные","PersonalDataDescription":"Ваша учетная запись содержит персональные данные, которые вы нам предоставили. Эта страница позволяет вам загрузить или удалить эти данные.","RequestPersonalData":"Запросить персональные данные","DeletePersonalData":"Удалить персональные данные","CreationTime":"Время создания","Action":"Действие","Preparing":"Подготовка","Download":"Скачать","ReadyTime":"Время готовности","DeletePersonalDataWarning":"Удаление этих данных приведет к удалению вашей учетной записи, и вы больше не сможете войти в приложение! Вы уверены, что хотите продолжить?","PersonalDataDeleteRequestReceived":"Ваш запрос на удаление персональных данных обрабатывается... По окончании процесса удаления данных ваша учетная запись будет удалена и вы больше не сможете ею пользоваться.","PersonalDataPrepareRequestReceived":"Ваш запрос персональных данных обрабатывается. Вы можете скачать его с этой страницы, как только он будет готов!","NoDataAvailable":"Данных нет.","Accept":"Принимать","CookiePolicy":"Политика использования файлов cookie","PrivacyPolicy":"политика конфиденциальности","ThisWebsiteUsesCookie":"Этот веб-сайт использует файлы cookie, чтобы обеспечить вам максимально эффективное использование веб-сайта.","CookieConsentAgreePolicies":"Если вы продолжите просмотр, вы соглашаетесь с нашими {0} и {1}.","CookieConsentAgreePolicy":"Если вы продолжите просмотр, вы соглашаетесь с нашими {0}.","CanNotGetDownloadToken":"Вы не можете получить токен загрузки по этому запросу!"}	2026-04-30 11:46:24.173417	\N
3a20eee2-7869-4459-cb07-e910df128f9a	AbpGdpr	zh-Hans	{"Volo.Abp.Gdpr:010001":"您曾申请下载个人数据。一旦过了规定的申请期限，您可以创建新的申请。","Volo.Abp.Gdpr:010002":"您的个人数据仍在准备中。您可以从 {GdprDataReadyTime} 下载。","PersonalData":"个人数据","Menu:PersonalData":"个人数据","PersonalDataDescription":"您的账户包含您提供给我们的个人数据。本页面允许您下载或删除这些数据。","RequestPersonalData":"请求个人数据","DeletePersonalData":"删除个人数据","CreationTime":"创建时间","Action":"操作","Preparing":"准备中","Download":"下载","ReadyTime":"就绪时间","DeletePersonalDataWarning":"删除此数据将删除您的账户，您将无法再登录应用程序！您确定要继续吗？","PersonalDataDeleteRequestReceived":"您的个人数据删除请求正在处理中...数据删除过程结束后，您的账户将被删除，您将无法再使用该账户。","PersonalDataPrepareRequestReceived":"您的个人数据申请正在处理中。准备就绪后，您可从本页下载！","NoDataAvailable":"没有相关数据。","Accept":"接受","CookiePolicy":"Cookies 政策","PrivacyPolicy":"隐私政策","ThisWebsiteUsesCookie":"本网站使用 Cookies 来确保您在网站上获得最佳体验。","CookieConsentAgreePolicies":"如果您继续浏览，则表示您同意我们的 {0} 和 {1}。","CookieConsentAgreePolicy":"如果您继续浏览，则表示您同意我们的 {0}。","CanNotGetDownloadToken":"您无法为该请求获取下载令牌！"}	2026-04-30 11:46:24.210247	\N
3a20f439-fdd4-7fc8-68dc-0559d39abef4	MasterDataService	vi	{"ArticleInformation":"Thông tin bài viết","BasicInfo":"Thông tin cơ bản","Keyword":"Từ khóa","Slug":"Đường dẫn bài viết","ThumbnailUrl":"Thumbnail","ArticleSettingsTab":"Bài viết","SeoTab":"SEO","Media":"Media","UploadFromDevice":"Tải lên từ thiết bị","ImagePreview":"Xem trước","ArticleFormLayoutHint":"Nội dung chính ở bên trái; media, tùy chọn xuất bản và SEO ở tab bên phải.","LastModificationTime":"Lần cập nhật cuối","ImageUploaded":"Ảnh đã tải lên","SeoInformation":"Thông tin SEO","SeoKeywordsEditorHint":"Mỗi dòng một từ khóa. Khi lưu, các giá trị được nối bằng dấu |.","SeoKeywordName":"Từ khóa","Articles":"Bài viết","Actions":"Hành động","NewArticle":"Bài viết mới","ExportToExcel":"Xuất file Excel","Search":"Tìm kiếm","ArticleCategory":"Danh mục bài viết","ArticleCategorySavedSuccessfully":"Đã lưu danh mục bài viết.","AddNewArticleCategory":"Thêm danh mục bài viết","ArticleCategoryName":"Tên danh mục bài viết","ArticleCategoryDescription":"Mô tả danh mục bài viết","ArticleCategoryIcon":"Icon danh mục bài viết","ArticleCategoryParentId":"Danh mục cha","ArticleCategoryDisplayOrder":"Thứ tự hiển thị","ArticleCategoryIsActive":"Trạng thái","ArticleCategoryThumbnailUrl":"Ảnh danh mục bài viết","ArticleCategoryCoverImageUrl":"Ảnh bìa danh mục bài viết","ArticleCategoryMinDisplayOrder":"Thứ tự hiển thị tối thiểu","DeleteArticle":"Xóa","BackToList":"Quay lại danh sách","SaveAndPublish":"Lưu và xuất bản","Save":"Lưu","Cancel":"Hủy","Delete":"Xóa","Edit":"Sửa","Create":"Tạo","View":"Xem","ArticleCategoryRequired":"Vui lòng chọn danh mục bài viết.","ArticleSavedSuccessfully":"Đã lưu bài viết.","AnErrorOccurred":"Đã xảy ra lỗi. Vui lòng thử lại.","AddNewSeoKeyword":"Thêm giá trị","Title":"Tiêu đề","Summary":"Tóm tắt","Content":"Nội dung","CoverImageUrl":"Ảnh bìa","Type":"Loại","AuthorName":"Tên tác giả","Source":"Nguồn","SourceUrl":"URL nguồn","Status":"Trạng thái","PublishedAt":"Ngày xuất bản","IsFeatured":"Đặc biệt","IsHot":"Nổi bật","IsTrending":"Xu hướng","ViewCount":"Lượt xem","LikeCount":"Lượt thích","ShareCount":"Lượt chia sẻ","CommentCount":"Lượt bình luận","ReadingTime":"Thời gian đọc","SeoTitle":"Tiêu đề SEO","SeoDescription":"Mô tả SEO","SeoKeywords":"Từ khóa SEO","MinPublishedAt":"Ngày xuất bản tối thiểu","MinViewCount":"Lượt xem tối thiểu","MinLikeCount":"Lượt thích tối thiểu","MinShareCount":"Lượt chia sẻ tối thiểu","MinCommentCount":"Lượt bình luận tối thiểu","MinReadingTime":"Thời gian đọc tối thiểu","MaxPublishedAt":"Ngày xuất bản tối đa","MaxViewCount":"Lượt xem tối đa","MaxLikeCount":"Lượt thích tối đa","MaxShareCount":"Lượt chia sẻ tối đa","MaxCommentCount":"Lượt bình luận tối đa","MaxReadingTime":"Thời gian đọc tối đa","EditArticle":"Sửa bài viết","Settings":"Cài đặt","ArticleStatus:Draft":"Bản nháp","ArticleStatus:Published":"Đã xuất bản","ArticleStatus:Archived":"Đã lưu trữ","ArticleStatus:Pending":"Chờ duyệt","Enum:ArticleType.0":"Tin tức","Enum:ArticleType.1":"Blog","Enum:ArticleType.2":"Hướng dẫn","Enum:ArticleType.3":"Đánh giá","Enum:ArticleType.4":"Khuyến mãi","Enum:ArticleType.5":"Sự kiện","Enum:ArticleStatus.0":"Bản nháp","Enum:ArticleStatus.1":"Chờ duyệt","Enum:ArticleStatus.2":"Đã xuất bản","Enum:ArticleStatus.3":"Đã lưu trữ","Menu:MasterDataCatalogGroup":"Danh mục","Menu:ArticleContentGroup":"Bài viết","Menu:Provinces":"Tỉnh/thành","Menu:Wards":"Phường/xã","Permission:Provinces":"Tỉnh/thành","Provinces":"Tỉnh/thành","Province":"Tỉnh/thành","NewProvince":"Thêm tỉnh/thành","Code":"Mã","Permission:Wards":"Phường/xã","Wards":"Phường/xã","NewWard":"Thêm phường/xã","ProvinceCode":"Mã tỉnh/thành","ProvinceName":"Tên tỉnh/thành","WardCode":"Mã phường/xã","WardName":"Tên phường/xã","FilterByProvinceHint":"Gõ để chọn hoặc tìm theo tỉnh/thành","All":"Tất cả","Yes":"Có","No":"Không","ArticleCategorySlug":"Đường dẫn","SuccessfullyDeleted":"Đã xóa thành công.","DeleteConfirmationMessage":"Bạn có chắc chắn muốn xóa bản ghi này?","Description":"Mô tả","Icon":"Biểu tượng","ParentId":"Mã danh mục cha","DisplayOrder":"Thứ tự hiển thị","IsActive":"Đang hoạt động","Permission:ArticleCategories":"Danh mục bài viết","ArticleCategories":"Danh mục bài viết","NewArticleCategory":"Danh mục mới","Menu:ArticleCategories":"Danh mục bài viết","Permission:ArticleTags":"Thẻ bài viết","ArticleTags":"Thẻ bài viết","NewArticleTag":"Thẻ mới","UsageCount":"Số lần sử dụng","MinUsageCount":"Số lần dùng tối thiểu","MaxUsageCount":"Số lần dùng tối đa","Menu:ArticleTags":"Thẻ bài viết","Permission:ArticleViews":"Lượt xem bài viết","ArticleViews":"Lượt xem bài viết","NewArticleView":"Lượt xem mới","IpAddress":"Địa chỉ IP","Device":"Thiết bị","ViewedAt":"Thời điểm xem","Duration":"Thời lượng","UserId":"Mã người dùng","Article":"Bài viết","MinViewedAt":"Thời điểm xem tối thiểu","MaxViewedAt":"Thời điểm xem tối đa","MinDuration":"Thời lượng tối thiểu","MaxDuration":"Thời lượng tối đa","Menu:ArticleViews":"Lượt xem bài viết","Permission:ArticleTagMappings":"Gán thẻ bài viết","ArticleTagMappings":"Gán thẻ cho bài viết","NewArticleTagMapping":"Gán thẻ mới","ArticleTag":"Thẻ","ArticleTagSlug":"Slug (đường dẫn)","IsPrimary":"Thẻ chính","Order":"Thứ tự","MinOrder":"Thứ tự tối thiểu","MaxOrder":"Thứ tự tối đa","Menu:ArticleTagMappings":"Gán thẻ bài viết","Update":"Cập nhật","Pick":"Chọn","ItemAlreadyAdded":"Mục này đã được thêm.","Filters":"Bộ lọc","ClearSelection":"Bỏ chọn","DeleteAllRecords":"Bạn có chắc chắn muốn xóa tất cả bản ghi?","DeleteSelectedRecords":"Bạn có chắc chắn muốn xóa {0} bản ghi đã chọn?","AllItemsAreSelected":"Đã chọn tất cả {0} mục","OneItemOnThisPageIsSelected":"Đã chọn 1 mục trên trang này","NumberOfItemsOnThisPageAreSelected":"Đã chọn {0} mục trên trang này","SelectAllItems":"Chọn tất cả {0} mục","Name":"Tên","Permission:Articles":"Bài viết","Menu:Articles":"Bài viết","UploadFailedMessage":"Tải lên thất bại: Định dạng không hỗ trợ hoặc dung lượng quá lớn. Vui lòng kiểm tra và thử lại.","DownloadSelectedFile":"Tải file đã chọn","RemoveSelectedFile":"Xóa file đã chọn","FileUploading":"Đang tải lên...","MaxFileSizeLimit":"Dung lượng tối đa: {0} MB"}	2026-05-01 12:40:05.7301	2026-05-01 18:10:01.992447
3a20eed5-37c5-7371-47b1-46e42c8150d8	MasterDataService	en	{"Permission:Provinces":"Provinces","Permission:MasterDataService":"Master Data Service","Permission:Create":"Create","Permission:Edit":"Edit","Permission:Delete":"Delete","Provinces":"Provinces","NewProvince":"New Province","Actions":"Actions","SuccessfullyDeleted":"Successfully deleted","DeleteConfirmationMessage":"Are you sure you want to delete this record?","Search":"Search","Pick":"Pick","SeeAdvancedFilters":"Filters","ItemAlreadyAdded":"This item is already added.","ExportToExcel":"Export to Excel","AllItemsAreSelected":"All {0} items are selected","OneItemOnThisPageIsSelected":"1 item on this page is selected","NumberOfItemsOnThisPageAreSelected":"All {0} items on this page are selected","SelectAllItems":"Select all {0} items","ClearSelection":"Clear selection","DeleteAllRecords":"Are you sure you want to delete all records?","DeleteSelectedRecords":"Are you sure you want to delete {0} record(s)?","UploadFailedMessage":"Upload Failed: Unsupported file format or file size too large. Please ensure the file meets the required format and size limits, and try again.","DownloadSelectedFile":"Download selected file","RemoveSelectedFile":"Remove selected file","FileUploading":"File uploading...","MaxFileSizeLimit":"Max file size: {0}mb","Filters":"Filters","Code":"Code","Name":"Name","Menu:Provinces":"Provinces","Menu:MasterDataService":"Master Data Service","Province":"Province","Permission:Wards":"Wards","Wards":"Wards","NewWard":"New Ward","Menu:Wards":"Wards","Permission:ArticleCategories":"Article Categories","ArticleCategories":"Article Categories","NewArticleCategory":"New Article Category","Slug":"Slug","Description":"Description","Icon":"Icon","ParentId":"Parent Id","DisplayOrder":"Display Order","IsActive":"Is Active","ThumbnailUrl":"Thumbnail Url","MinDisplayOrder":"Min Display Order","MaxDisplayOrder":"Max Display Order","Menu:ArticleCategories":"Article Categories","Permission:ArticleTags":"Article Tags","ArticleTags":"Article Tags","NewArticleTag":"New Article Tag","UsageCount":"Usage Count","MinUsageCount":"Min Usage Count","MaxUsageCount":"Max Usage Count","Menu:ArticleTags":"Article Tags","Enum:ArticleType.0":"News","Enum:ArticleType.1":"Blog","Enum:ArticleType.2":"Guide","Enum:ArticleType.3":"Review","Enum:ArticleType.4":"Promotion","Enum:ArticleType.5":"Event","Enum:ArticleStatus.0":"Draft","Enum:ArticleStatus.1":"Pending","Enum:ArticleStatus.2":"Published","Enum:ArticleStatus.3":"Archived","ArticleCategory":"Article category","Permission:Articles":"Articles","Articles":"Articles","NewArticle":"New Article","Title":"Title","Summary":"Summary","Content":"Content","CoverImageUrl":"Cover Image Url","Type":"Type","AuthorName":"Author Name","Source":"Source","SourceUrl":"Source Url","Status":"Status","PublishedAt":"Published At","IsFeatured":"Is Featured","IsHot":"Is Hot","IsTrending":"Is Trending","ViewCount":"View Count","LikeCount":"Like Count","ShareCount":"Share Count","CommentCount":"Comment Count","ReadingTime":"Reading Time","SeoTitle":"Seo Title","SeoDescription":"Seo Description","SeoKeywords":"Seo Keywords","MinPublishedAt":"Min Published At","MinViewCount":"Min View Count","MinLikeCount":"Min Like Count","MinShareCount":"Min Share Count","MinCommentCount":"Min Comment Count","MinReadingTime":"Min Reading Time","MaxPublishedAt":"Max Published At","MaxViewCount":"Max View Count","MaxLikeCount":"Max Like Count","MaxShareCount":"Max Share Count","MaxCommentCount":"Max Comment Count","MaxReadingTime":"Max Reading Time","Menu:Articles":"Articles","ArticleTag":"Article tag","Article":"Article","Permission:ArticleTagMappings":"Article Tag Mappings","ArticleTagMappings":"Article Tag Mappings","NewArticleTagMapping":"New Article Tag Mapping","IsPrimary":"Is Primary","Order":"Order","MinOrder":"Min Order","MaxOrder":"Max Order","Menu:ArticleTagMappings":"Article Tag Mappings","Permission:ArticleViews":"Article Views","ArticleViews":"Article Views","NewArticleView":"New Article View","IpAddress":"Ip Address","Device":"Device","ViewedAt":"Viewed At","Duration":"Duration","UserId":"User Id","MinViewedAt":"Min Viewed At","MinDuration":"Min Duration","MaxViewedAt":"Max Viewed At","MaxDuration":"Max Duration","Menu:ArticleViews":"Article Views","SaveAndPublish":"Save \\u0026 Publish","EditArticle":"Edit Article","BackToList":"Back to list","DeleteArticle":"Delete","BasicInfo":"Basic info","ArticleSettingsTab":"Article","SeoTab":"SEO","Media":"Media","Settings":"Settings","UploadFromDevice":"Upload from device","ImagePreview":"Preview","ArticleFormLayoutHint":"Main content on the left; media, publishing options and SEO in the tabs on the right.","LastModificationTime":"Last updated","ImageUploaded":"Image uploaded","ArticleCategoryRequired":"Please select an article category.","ArticleSavedSuccessfully":"Article saved successfully.","AnErrorOccurred":"An error occurred. Please try again.","AddNewSeoKeyword":"Add new value","SeoKeywordsEditorHint":"Enter one keyword per row. Values are saved separated by |.","SeoKeywordName":"Keyword","Update":"Update","All":"All","Yes":"Yes","No":"No","Menu:MasterDataCatalogGroup":"Catalog","Menu:ArticleContentGroup":"Publishing","ProvinceCode":"Province code","ProvinceName":"Province name","WardCode":"Ward code","WardName":"Ward name","FilterByProvinceHint":"Type to search or select province","ArticleCategoryName":"Category name","ArticleCategorySlug":"URL path","ArticleCategoryDescription":"Description","ArticleCategoryIcon":"Icon","ArticleCategoryDisplayOrder":"Display order","ArticleCategoryIsActive":"Active","ArticleCategoryThumbnailUrl":"Thumbnail","ArticleCategoryParentId":"Parent category","ArticleTagSlug":"URL slug","Enum:FileType.0":"Image","Enum:FileType.1":"Video","Enum:FileType.2":"Document","Enum:FileType.3":"Avatar","Enum:FileType.4":"Thumbnail","Enum:FileType.5":"Gallery","Enum:FileStatus.0":"Uploading","Enum:FileStatus.1":"Ready","Enum:FileStatus.2":"Failed","Enum:FileStatus.3":"Deleted","Permission:MediaFiles":"Media Files","MediaFiles":"Media Files","NewMediaFile":"New Media File","FileName":"File Name","OriginalFileName":"Original File Name","Extension":"Extension","ContentType":"Content Type","Size":"Size","StorageProvider":"Storage Provider","Bucket":"Bucket","Folder":"Folder","Path":"Path","Url":"Url","Checksum":"Checksum","Width":"Width","Height":"Height","FileType":"File Type","MinSize":"Min Size","MinWidth":"Min Width","MinHeight":"Min Height","MaxSize":"Max Size","MaxWidth":"Max Width","MaxHeight":"Max Height","Menu:MediaFiles":"Media Files","Permission:PlaceCategories":"Place Categories","PlaceCategories":"Place Categories","NewPlaceCategory":"New Place Category","Color":"Color","Menu:PlaceCategories":"Place Categories","Permission:PlaceTags":"Place Tags","PlaceTags":"Place Tags","NewPlaceTag":"New Place Tag","Menu:PlaceTags":"Place Tags","Enum:PriceRange.0":"Free","Enum:PriceRange.1":"Cheap","Enum:PriceRange.2":"Medium","Enum:PriceRange.3":"High","Enum:PriceRange.4":"Luxury","Enum:PlaceStatus.0":"Draft","Enum:PlaceStatus.1":"Published","Enum:PlaceStatus.2":"Hidden","Enum:PlaceStatus.3":"Archived","PlaceCategory":"Place category","Ward":"Ward","Permission:Places":"Places","Places":"Places","NewPlace":"New Place","ShortDescription":"Short Description","Address":"Address","Latitude":"Latitude","Longituded":"Longituded","PhoneNumber":"Phone Number","Email":"Email","Website":"Website","OpeningHours":"Opening Hours","PriceRange":"Price Range","GoogleMapUrl":"Google Map Url","FavoriteCount":"Favorite Count","ReviewCount":"Review Count","RatingAveraged":"Rating Averaged","RatingTotal":"Rating Total","IsVerified":"Is Verified","MinLatitude":"Min Latitude","MinLongituded":"Min Longituded","MinFavoriteCount":"Min Favorite Count","MinReviewCount":"Min Review Count","MinRatingAveraged":"Min Rating Averaged","MinRatingTotal":"Min Rating Total","MaxLatitude":"Max Latitude","MaxLongituded":"Max Longituded","MaxFavoriteCount":"Max Favorite Count","MaxReviewCount":"Max Review Count","MaxRatingAveraged":"Max Rating Averaged","MaxRatingTotal":"Max Rating Total","Menu:Places":"Places","PlaceTag":"Place tag","Place":"Place","Permission:PlaceTagMappings":"Place Tag Mappings","PlaceTagMappings":"Place Tag Mappings","NewPlaceTagMapping":"New Place Tag Mapping","SortOrder":"Sort Order","MinSortOrder":"Min Sort Order","MaxSortOrder":"Max Sort Order","Menu:PlaceTagMappings":"Place Tag Mappings","MediaFile":"Media file","Permission:EntityFiles":"Entity Files","EntityFiles":"Entity Files","NewEntityFile":"New Entity File","EntityType":"Entity Type","EntityId":"Entity Id","Collection":"Collection","Menu:EntityFiles":"Entity Files","Enum:PlaceReviewStatus.0":"Pending","Enum:PlaceReviewStatus.1":"Approved","Enum:PlaceReviewStatus.2":"Rejected","Permission:PlaceReviews":"Place Reviews","PlaceReviews":"Place Reviews","NewPlaceReview":"New Place Review","Rating":"Rating","Comment":"Comment","MinRating":"Min Rating","MaxRating":"Max Rating","Menu:PlaceReviews":"Place Reviews","Permission:PlaceFavorites":"Place Favorites","PlaceFavorites":"Place Favorites","NewPlaceFavorite":"New Place Favorite","Menu:PlaceFavorites":"Place Favorites","Permission:PlaceViews":"Place Views","PlaceViews":"Place Views","NewPlaceView":"New Place View","Menu:PlaceViews":"Place Views"}	2026-04-30 11:31:55.345246	2026-05-01 18:55:08.625705
\.


--
-- Data for Name: __LanguageService_Migrations; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."__LanguageService_Migrations" ("MigrationId", "ProductVersion") FROM stdin;
20260429043506_Initial	10.0.2
\.


--
-- Name: AbpEventInbox PK_AbpEventInbox; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpEventInbox"
    ADD CONSTRAINT "PK_AbpEventInbox" PRIMARY KEY ("Id");


--
-- Name: AbpEventOutbox PK_AbpEventOutbox; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpEventOutbox"
    ADD CONSTRAINT "PK_AbpEventOutbox" PRIMARY KEY ("Id");


--
-- Name: AbpLanguageTexts PK_AbpLanguageTexts; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpLanguageTexts"
    ADD CONSTRAINT "PK_AbpLanguageTexts" PRIMARY KEY ("Id");


--
-- Name: AbpLanguages PK_AbpLanguages; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpLanguages"
    ADD CONSTRAINT "PK_AbpLanguages" PRIMARY KEY ("Id");


--
-- Name: AbpLocalizationResources PK_AbpLocalizationResources; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpLocalizationResources"
    ADD CONSTRAINT "PK_AbpLocalizationResources" PRIMARY KEY ("Id");


--
-- Name: AbpLocalizationTexts PK_AbpLocalizationTexts; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpLocalizationTexts"
    ADD CONSTRAINT "PK_AbpLocalizationTexts" PRIMARY KEY ("Id");


--
-- Name: __LanguageService_Migrations PK___LanguageService_Migrations; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."__LanguageService_Migrations"
    ADD CONSTRAINT "PK___LanguageService_Migrations" PRIMARY KEY ("MigrationId");


--
-- Name: IX_AbpEventInbox_MessageId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpEventInbox_MessageId" ON public."AbpEventInbox" USING btree ("MessageId");


--
-- Name: IX_AbpEventInbox_Status_CreationTime; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpEventInbox_Status_CreationTime" ON public."AbpEventInbox" USING btree ("Status", "CreationTime");


--
-- Name: IX_AbpEventOutbox_CreationTime; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpEventOutbox_CreationTime" ON public."AbpEventOutbox" USING btree ("CreationTime");


--
-- Name: IX_AbpLanguageTexts_TenantId_ResourceName_CultureName; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpLanguageTexts_TenantId_ResourceName_CultureName" ON public."AbpLanguageTexts" USING btree ("TenantId", "ResourceName", "CultureName");


--
-- Name: IX_AbpLanguages_CultureName; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpLanguages_CultureName" ON public."AbpLanguages" USING btree ("CultureName");


--
-- Name: IX_AbpLocalizationResources_Name; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "IX_AbpLocalizationResources_Name" ON public."AbpLocalizationResources" USING btree ("Name");


--
-- Name: IX_AbpLocalizationTexts_ResourceName_CultureName; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "IX_AbpLocalizationTexts_ResourceName_CultureName" ON public."AbpLocalizationTexts" USING btree ("ResourceName", "CultureName");


--
-- PostgreSQL database dump complete
--

\unrestrict b5bLrd3HAD8gkgYacR4lfFDNwzKJ0g5UXlGD02koe3Dg1s1kUqzZnMvoubutKvr

--
-- Database "KHHub_MasterData" dump
--

--
-- PostgreSQL database dump
--

\restrict c1gRWjaYy0pocMoClFyIguTg1uzc3FQpff2PHzKSfuiYG7yPJ6N2DRYLwA91nOL

-- Dumped from database version 14.22
-- Dumped by pg_dump version 14.22

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: KHHub_MasterData; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE "KHHub_MasterData" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'en_US.utf8';


ALTER DATABASE "KHHub_MasterData" OWNER TO postgres;

\unrestrict c1gRWjaYy0pocMoClFyIguTg1uzc3FQpff2PHzKSfuiYG7yPJ6N2DRYLwA91nOL
\connect "KHHub_MasterData"
\restrict c1gRWjaYy0pocMoClFyIguTg1uzc3FQpff2PHzKSfuiYG7yPJ6N2DRYLwA91nOL

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: AbpEventInbox; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpEventInbox" (
    "Id" uuid NOT NULL,
    "ExtraProperties" text NOT NULL,
    "MessageId" text NOT NULL,
    "EventName" character varying(256) NOT NULL,
    "EventData" bytea NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL,
    "Status" integer NOT NULL,
    "HandledTime" timestamp without time zone,
    "RetryCount" integer NOT NULL,
    "NextRetryTime" timestamp without time zone
);


ALTER TABLE public."AbpEventInbox" OWNER TO postgres;

--
-- Name: AbpEventOutbox; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AbpEventOutbox" (
    "Id" uuid NOT NULL,
    "ExtraProperties" text NOT NULL,
    "EventName" character varying(256) NOT NULL,
    "EventData" bytea NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL
);


ALTER TABLE public."AbpEventOutbox" OWNER TO postgres;

--
-- Name: ArticleCategories; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."ArticleCategories" (
    "Id" uuid NOT NULL,
    "ExtraProperties" text NOT NULL,
    "ConcurrencyStamp" character varying(40) NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL,
    "CreatorId" uuid,
    "LastModificationTime" timestamp without time zone,
    "LastModifierId" uuid,
    "IsDeleted" boolean DEFAULT false NOT NULL,
    "DeleterId" uuid,
    "DeletionTime" timestamp without time zone,
    "Name" character varying(255) NOT NULL,
    "Slug" character varying(255) NOT NULL,
    "Description" text,
    "Icon" character varying(20),
    "ParentId" uuid NOT NULL,
    "DisplayOrder" integer NOT NULL,
    "IsActive" boolean NOT NULL,
    "ThumbnailUrl" character varying(255)
);


ALTER TABLE public."ArticleCategories" OWNER TO postgres;

--
-- Name: ArticleTagMappings; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."ArticleTagMappings" (
    "Id" uuid NOT NULL,
    "ExtraProperties" text NOT NULL,
    "ConcurrencyStamp" character varying(40) NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL,
    "CreatorId" uuid,
    "LastModificationTime" timestamp without time zone,
    "LastModifierId" uuid,
    "IsDeleted" boolean DEFAULT false NOT NULL,
    "DeleterId" uuid,
    "DeletionTime" timestamp without time zone,
    "IsPrimary" boolean NOT NULL,
    "Order" integer NOT NULL,
    "ArticleTagId" uuid NOT NULL,
    "ArticleId" uuid NOT NULL
);


ALTER TABLE public."ArticleTagMappings" OWNER TO postgres;

--
-- Name: ArticleTags; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."ArticleTags" (
    "Id" uuid NOT NULL,
    "ExtraProperties" text NOT NULL,
    "ConcurrencyStamp" character varying(40) NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL,
    "CreatorId" uuid,
    "LastModificationTime" timestamp without time zone,
    "LastModifierId" uuid,
    "IsDeleted" boolean DEFAULT false NOT NULL,
    "DeleterId" uuid,
    "DeletionTime" timestamp without time zone,
    "Name" character varying(255) NOT NULL,
    "Slug" character varying(255) NOT NULL,
    "Description" text,
    "UsageCount" integer NOT NULL
);


ALTER TABLE public."ArticleTags" OWNER TO postgres;

--
-- Name: ArticleViews; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."ArticleViews" (
    "Id" uuid NOT NULL,
    "ExtraProperties" text NOT NULL,
    "ConcurrencyStamp" character varying(40) NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL,
    "CreatorId" uuid,
    "LastModificationTime" timestamp without time zone,
    "LastModifierId" uuid,
    "IsDeleted" boolean DEFAULT false NOT NULL,
    "DeleterId" uuid,
    "DeletionTime" timestamp without time zone,
    "IpAddress" character varying(20),
    "Device" character varying(100),
    "Source" character varying(255),
    "ViewedAt" timestamp without time zone NOT NULL,
    "Duration" integer NOT NULL,
    "UserId" uuid NOT NULL,
    "ArticleId" uuid NOT NULL
);


ALTER TABLE public."ArticleViews" OWNER TO postgres;

--
-- Name: Articles; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Articles" (
    "Id" uuid NOT NULL,
    "ExtraProperties" text NOT NULL,
    "ConcurrencyStamp" character varying(40) NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL,
    "CreatorId" uuid,
    "LastModificationTime" timestamp without time zone,
    "LastModifierId" uuid,
    "IsDeleted" boolean DEFAULT false NOT NULL,
    "DeleterId" uuid,
    "DeletionTime" timestamp without time zone,
    "Title" character varying(255) NOT NULL,
    "Slug" character varying(255) NOT NULL,
    "Summary" character varying(500) NOT NULL,
    "Content" text NOT NULL,
    "ThumbnailUrl" character varying(255),
    "CoverImageUrl" character varying(255),
    "Type" integer NOT NULL,
    "AuthorName" character varying(100) NOT NULL,
    "Source" character varying(255),
    "SourceUrl" character varying(255),
    "Status" integer NOT NULL,
    "PublishedAt" timestamp without time zone NOT NULL,
    "IsFeatured" boolean NOT NULL,
    "IsHot" boolean NOT NULL,
    "IsTrending" boolean NOT NULL,
    "ViewCount" integer NOT NULL,
    "LikeCount" integer NOT NULL,
    "ShareCount" integer NOT NULL,
    "CommentCount" integer NOT NULL,
    "ReadingTime" integer NOT NULL,
    "SeoTitle" character varying(255) NOT NULL,
    "SeoDescription" text,
    "SeoKeywords" text,
    "ArticleCategoryId" uuid NOT NULL
);


ALTER TABLE public."Articles" OWNER TO postgres;

--
-- Name: EntityFiles; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."EntityFiles" (
    "Id" uuid NOT NULL,
    "ExtraProperties" text NOT NULL,
    "ConcurrencyStamp" character varying(40) NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL,
    "CreatorId" uuid,
    "LastModificationTime" timestamp without time zone,
    "LastModifierId" uuid,
    "IsDeleted" boolean DEFAULT false NOT NULL,
    "DeleterId" uuid,
    "DeletionTime" timestamp without time zone,
    "EntityType" character varying(100) NOT NULL,
    "EntityId" uuid NOT NULL,
    "Collection" character varying(100),
    "SortOrder" integer NOT NULL,
    "IsPrimary" boolean NOT NULL,
    "MediaFileId" uuid NOT NULL
);


ALTER TABLE public."EntityFiles" OWNER TO postgres;

--
-- Name: MediaFiles; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."MediaFiles" (
    "Id" uuid NOT NULL,
    "ExtraProperties" text NOT NULL,
    "ConcurrencyStamp" character varying(40) NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL,
    "CreatorId" uuid,
    "LastModificationTime" timestamp without time zone,
    "LastModifierId" uuid,
    "IsDeleted" boolean DEFAULT false NOT NULL,
    "DeleterId" uuid,
    "DeletionTime" timestamp without time zone,
    "FileName" character varying(255),
    "OriginalFileName" character varying(255),
    "Extension" character varying(20),
    "ContentType" character varying(100),
    "Size" bigint NOT NULL,
    "StorageProvider" character varying(50),
    "Bucket" character varying(100),
    "Folder" character varying(255),
    "Path" character varying(500),
    "Url" character varying(1000),
    "Checksum" character varying(128),
    "Width" integer NOT NULL,
    "Height" integer NOT NULL,
    "Duration" integer NOT NULL,
    "FileType" integer NOT NULL,
    "Status" integer NOT NULL
);


ALTER TABLE public."MediaFiles" OWNER TO postgres;

--
-- Name: PlaceCategories; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."PlaceCategories" (
    "Id" uuid NOT NULL,
    "ExtraProperties" text NOT NULL,
    "ConcurrencyStamp" character varying(40) NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL,
    "CreatorId" uuid,
    "LastModificationTime" timestamp without time zone,
    "LastModifierId" uuid,
    "IsDeleted" boolean DEFAULT false NOT NULL,
    "DeleterId" uuid,
    "DeletionTime" timestamp without time zone,
    "Name" character varying(150) NOT NULL,
    "Slug" character varying(200) NOT NULL,
    "Description" character varying(500),
    "Icon" character varying(50),
    "Color" character varying(20),
    "ParentId" uuid NOT NULL,
    "DisplayOrder" integer NOT NULL,
    "IsActive" boolean NOT NULL
);


ALTER TABLE public."PlaceCategories" OWNER TO postgres;

--
-- Name: PlaceFavorites; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."PlaceFavorites" (
    "Id" uuid NOT NULL,
    "ExtraProperties" text NOT NULL,
    "ConcurrencyStamp" character varying(40) NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL,
    "CreatorId" uuid,
    "LastModificationTime" timestamp without time zone,
    "LastModifierId" uuid,
    "IsDeleted" boolean DEFAULT false NOT NULL,
    "DeleterId" uuid,
    "DeletionTime" timestamp without time zone,
    "UserId" uuid,
    "PlaceId" uuid NOT NULL
);


ALTER TABLE public."PlaceFavorites" OWNER TO postgres;

--
-- Name: PlaceReviews; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."PlaceReviews" (
    "Id" uuid NOT NULL,
    "ExtraProperties" text NOT NULL,
    "ConcurrencyStamp" character varying(40) NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL,
    "CreatorId" uuid,
    "LastModificationTime" timestamp without time zone,
    "LastModifierId" uuid,
    "IsDeleted" boolean DEFAULT false NOT NULL,
    "DeleterId" uuid,
    "DeletionTime" timestamp without time zone,
    "Rating" integer NOT NULL,
    "Title" character varying(255) NOT NULL,
    "Comment" text,
    "LikeCount" integer NOT NULL,
    "Status" integer NOT NULL,
    "UserId" uuid,
    "PlaceId" uuid NOT NULL
);


ALTER TABLE public."PlaceReviews" OWNER TO postgres;

--
-- Name: PlaceTagMappings; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."PlaceTagMappings" (
    "Id" uuid NOT NULL,
    "ExtraProperties" text NOT NULL,
    "ConcurrencyStamp" character varying(40) NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL,
    "CreatorId" uuid,
    "LastModificationTime" timestamp without time zone,
    "LastModifierId" uuid,
    "IsDeleted" boolean DEFAULT false NOT NULL,
    "DeleterId" uuid,
    "DeletionTime" timestamp without time zone,
    "IsPrimary" boolean NOT NULL,
    "SortOrder" integer NOT NULL,
    "PlaceTagId" uuid NOT NULL,
    "PlaceId" uuid NOT NULL
);


ALTER TABLE public."PlaceTagMappings" OWNER TO postgres;

--
-- Name: PlaceTags; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."PlaceTags" (
    "Id" uuid NOT NULL,
    "ExtraProperties" text NOT NULL,
    "ConcurrencyStamp" character varying(40) NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL,
    "CreatorId" uuid,
    "LastModificationTime" timestamp without time zone,
    "LastModifierId" uuid,
    "IsDeleted" boolean DEFAULT false NOT NULL,
    "DeleterId" uuid,
    "DeletionTime" timestamp without time zone,
    "Name" character varying(100) NOT NULL,
    "Slug" character varying(150) NOT NULL,
    "Description" character varying(500),
    "UsageCount" integer NOT NULL
);


ALTER TABLE public."PlaceTags" OWNER TO postgres;

--
-- Name: PlaceViews; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."PlaceViews" (
    "Id" uuid NOT NULL,
    "ExtraProperties" text NOT NULL,
    "ConcurrencyStamp" character varying(40) NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL,
    "CreatorId" uuid,
    "LastModificationTime" timestamp without time zone,
    "LastModifierId" uuid,
    "IsDeleted" boolean DEFAULT false NOT NULL,
    "DeleterId" uuid,
    "DeletionTime" timestamp without time zone,
    "UserId" uuid NOT NULL,
    "IpAddress" character varying(100),
    "Device" character varying(255),
    "ViewedAt" timestamp without time zone NOT NULL,
    "Duration" integer NOT NULL,
    "Source" character varying(100),
    "PlaceId" uuid NOT NULL
);


ALTER TABLE public."PlaceViews" OWNER TO postgres;

--
-- Name: Places; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Places" (
    "Id" uuid NOT NULL,
    "ExtraProperties" text NOT NULL,
    "ConcurrencyStamp" character varying(40) NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL,
    "CreatorId" uuid,
    "LastModificationTime" timestamp without time zone,
    "LastModifierId" uuid,
    "IsDeleted" boolean DEFAULT false NOT NULL,
    "DeleterId" uuid,
    "DeletionTime" timestamp without time zone,
    "Name" character varying(255) NOT NULL,
    "Slug" character varying(300) NOT NULL,
    "ShortDescription" character varying(500),
    "Description" text,
    "ThumbnailUrl" character varying(1000),
    "CoverImageUrl" character varying(1000),
    "Address" character varying(500),
    "Latitude" numeric NOT NULL,
    "Longituded" numeric NOT NULL,
    "PhoneNumber" character varying(50),
    "Email" character varying(255),
    "Website" character varying(500),
    "OpeningHours" character varying(500),
    "PriceRange" integer NOT NULL,
    "GoogleMapUrl" character varying(1000),
    "Status" integer NOT NULL,
    "ViewCount" integer NOT NULL,
    "FavoriteCount" integer NOT NULL,
    "ReviewCount" integer NOT NULL,
    "RatingAveraged" numeric NOT NULL,
    "RatingTotal" integer NOT NULL,
    "IsFeatured" boolean NOT NULL,
    "IsHot" boolean NOT NULL,
    "IsVerified" boolean NOT NULL,
    "SeoTitle" character varying(255) NOT NULL,
    "SeoDescription" character varying(500),
    "SeoKeywords" character varying(500),
    "PlaceCategoryId" uuid NOT NULL,
    "ProvinceId" uuid NOT NULL,
    "WardId" uuid NOT NULL
);


ALTER TABLE public."Places" OWNER TO postgres;

--
-- Name: Provinces; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Provinces" (
    "Id" uuid NOT NULL,
    "ExtraProperties" text NOT NULL,
    "ConcurrencyStamp" character varying(40) NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL,
    "CreatorId" uuid,
    "LastModificationTime" timestamp without time zone,
    "LastModifierId" uuid,
    "IsDeleted" boolean DEFAULT false NOT NULL,
    "DeleterId" uuid,
    "DeletionTime" timestamp without time zone,
    "Code" character varying(12) NOT NULL,
    "Name" character varying(255) NOT NULL
);


ALTER TABLE public."Provinces" OWNER TO postgres;

--
-- Name: Wards; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Wards" (
    "Id" uuid NOT NULL,
    "ExtraProperties" text NOT NULL,
    "ConcurrencyStamp" character varying(40) NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL,
    "CreatorId" uuid,
    "LastModificationTime" timestamp without time zone,
    "LastModifierId" uuid,
    "IsDeleted" boolean DEFAULT false NOT NULL,
    "DeleterId" uuid,
    "DeletionTime" timestamp without time zone,
    "Code" character varying(10) NOT NULL,
    "Name" character varying(255) NOT NULL,
    "ProvinceId" uuid NOT NULL
);


ALTER TABLE public."Wards" OWNER TO postgres;

--
-- Name: __MasterDataService_Migrations; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."__MasterDataService_Migrations" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__MasterDataService_Migrations" OWNER TO postgres;

--
-- Data for Name: AbpEventInbox; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpEventInbox" ("Id", "ExtraProperties", "MessageId", "EventName", "EventData", "CreationTime", "Status", "HandledTime", "RetryCount", "NextRetryTime") FROM stdin;
\.


--
-- Data for Name: AbpEventOutbox; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AbpEventOutbox" ("Id", "ExtraProperties", "EventName", "EventData", "CreationTime") FROM stdin;
\.


--
-- Data for Name: ArticleCategories; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."ArticleCategories" ("Id", "ExtraProperties", "ConcurrencyStamp", "CreationTime", "CreatorId", "LastModificationTime", "LastModifierId", "IsDeleted", "DeleterId", "DeletionTime", "Name", "Slug", "Description", "Icon", "ParentId", "DisplayOrder", "IsActive", "ThumbnailUrl") FROM stdin;
3a20f3f1-702a-7f07-9196-92139196a7a2	{}	8132499dc21446e8a67c6756496e51eb	2026-05-01 11:20:50.877847	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	2026-05-01 14:16:53.358452	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	f	\N	\N	Việc làm	viec-lam	Việc làm	fa fa-tag	3a20f3f1-702a-7f07-9196-92139196a7a2	1	t	/uploads/articles/1eec5ff34a17720f22f53a20f4927605.png
\.


--
-- Data for Name: ArticleTagMappings; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."ArticleTagMappings" ("Id", "ExtraProperties", "ConcurrencyStamp", "CreationTime", "CreatorId", "LastModificationTime", "LastModifierId", "IsDeleted", "DeleterId", "DeletionTime", "IsPrimary", "Order", "ArticleTagId", "ArticleId") FROM stdin;
3a20f46f-cfcf-9203-ac5a-0d2aaa755ede	{}	a7cbd441fe0f4ac4a3877ca1a1eac923	2026-05-01 13:38:52.89436	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	\N	\N	f	\N	\N	t	1	3a20f46f-9080-b44a-f445-0a29214af6f1	3a20f420-198b-0739-b05c-03c17b1df049
\.


--
-- Data for Name: ArticleTags; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."ArticleTags" ("Id", "ExtraProperties", "ConcurrencyStamp", "CreationTime", "CreatorId", "LastModificationTime", "LastModifierId", "IsDeleted", "DeleterId", "DeletionTime", "Name", "Slug", "Description", "UsageCount") FROM stdin;
3a20f46f-9080-b44a-f445-0a29214af6f1	{}	375ced6b46a54862ab16646ef844fb30	2026-05-01 13:38:36.70155	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	\N	\N	f	\N	\N	Tin tức	tin-tuc	test	1
\.


--
-- Data for Name: ArticleViews; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."ArticleViews" ("Id", "ExtraProperties", "ConcurrencyStamp", "CreationTime", "CreatorId", "LastModificationTime", "LastModifierId", "IsDeleted", "DeleterId", "DeletionTime", "IpAddress", "Device", "Source", "ViewedAt", "Duration", "UserId", "ArticleId") FROM stdin;
\.


--
-- Data for Name: Articles; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Articles" ("Id", "ExtraProperties", "ConcurrencyStamp", "CreationTime", "CreatorId", "LastModificationTime", "LastModifierId", "IsDeleted", "DeleterId", "DeletionTime", "Title", "Slug", "Summary", "Content", "ThumbnailUrl", "CoverImageUrl", "Type", "AuthorName", "Source", "SourceUrl", "Status", "PublishedAt", "IsFeatured", "IsHot", "IsTrending", "ViewCount", "LikeCount", "ShareCount", "CommentCount", "ReadingTime", "SeoTitle", "SeoDescription", "SeoKeywords", "ArticleCategoryId") FROM stdin;
3a20f420-198b-0739-b05c-03c17b1df049	{}	fa2c5f2fbf4d4a70b51d4843cd62fbdc	2026-05-01 12:11:48.921291	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	2026-05-01 12:16:09.115655	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	f	\N	\N	Cần tuyển nhân viên làm việc tại Nha trang	can-tuyen-nhan-vien-lam-viec-tai-nha-trang	Cần tuyển nhân viên làm việc tại Nha trang summary	<p>Cần tuyển nh&acirc;n vi&ecirc;n l&agrave;m việc tại Nha trang</p>	/uploads/articles/b5121e674a0436dac4853a20f41fb7d0.jpeg	/uploads/articles/ef882d01588e9e40fb893a20f41ff2da.png	0	test	\N	\N	2	2026-05-01 00:00:00	t	t	t	0	0	0	0	2	Cần tuyển nhân viên làm việc tại Nha trang	Can tuyen nhan vien lam viec tai Nha trang summary	việc làm | việc làm nha trang	3a20f3f1-702a-7f07-9196-92139196a7a2
3a20f56f-a3a5-1834-f0ee-bb939d7791d2	{}	8de06de371fe465c84d1f79e0bb01953	2026-05-01 18:18:18.798387	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	2026-05-01 18:19:06.149603	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	f	\N	\N	Nước mắm truyền thống – Tinh hoa ẩm thực Việt	nuoc-mam-truyen-thong-tinh-hoa-am-thuc-viet	test	<p><strong>Nước mắm truyền thống từ l&acirc;u đ&atilde; trở th&agrave;nh biểu tượng của ẩm thực Việt Nam. Kh&ocirc;ng chỉ l&agrave; một loại gia vị, nước mắm c&ograve;n mang trong m&igrave;nh n&eacute;t văn h&oacute;a độc đ&aacute;o, thể hiện sự tinh tế trong nghệ thuật chế biến m&oacute;n ăn của người Việt.</strong></p>\n<p><strong>Nước mắm truyền thống l&agrave; g&igrave;?</strong></p>\n<p>Nước mắm truyền thống l&agrave; loại nước mắm được ủ chượp tự nhi&ecirc;n từ&nbsp;<strong>c&aacute; cơm tươi</strong>&nbsp;v&agrave;&nbsp;<strong>muối hạt sạch</strong>, theo phương ph&aacute;p l&ecirc;n men k&eacute;o d&agrave;i từ 12&ndash;24 th&aacute;ng. Th&agrave;nh phẩm cho ra&nbsp;<strong>nước mắm nguy&ecirc;n chất</strong>&nbsp;c&oacute; m&agrave;u n&acirc;u c&aacute;nh gi&aacute;n, hương thơm đặc trưng v&agrave; vị đậm đ&agrave; kh&oacute; qu&ecirc;n.</p>\n<p><strong>V&igrave; sao nước mắm truyền thống vẫn được ưa chuộng ?</strong></p>\n<ul class="wp-block-list">\n<li><strong>Hương vị tự nhi&ecirc;n</strong>: Đậm, thơm, hậu ngọt từ c&aacute;, kh&ocirc;ng phụ gia.</li>\n<li><strong>Gi&agrave;u dinh dưỡng</strong>: Chứa nhiều đạm tự nhi&ecirc;n, axit amin tốt cho sức khỏe.</li>\n<li><strong>Quy tr&igrave;nh truyền thống</strong>: Thủ c&ocirc;ng, lựa chọn nguy&ecirc;n liệu kỹ lưỡng.</li>\n<li><strong>Bản sắc văn h&oacute;a</strong>: L&agrave; biểu tượng của ẩm thực Việt qua h&agrave;ng trăm năm.</li>\n</ul>\n<p><strong>C&aacute;ch Nhận Biết Nước Mắm Truyền Thống Ngon</strong></p>\n<ul class="wp-block-list">\n<li>M&agrave;u n&acirc;u c&aacute;nh gi&aacute;n tự nhi&ecirc;n, kh&ocirc;ng gắt m&ugrave;i.</li>\n<li>Khi lắc chai c&oacute; lớp bọt nhỏ mịn, tan chậm.</li>\n<li>Vị mặn đầu lưỡi, hậu ngọt k&eacute;o d&agrave;i.<br><br><img src="https://nuocmamngoctrang.com/wp-content/uploads/2025/10/EFPI9122-1024x683.jpg" alt=""><img src="/Articles/ArticleMediaFile?name=3a20f56f4886ddb7218cfa4b4c91c308.jpg" alt="" width="2048" height="1365"></li>\n</ul>\n<p><strong>Nước mắm&nbsp;</strong><strong>Ngọc Trang &ndash;&nbsp;</strong><strong>Thương hiệu gia truyền l&acirc;u đời tại kh&aacute;nh ho&agrave;</strong></p>\n<p>Nhắc đến nước mắm truyền thống Kh&aacute;nh Ho&agrave;, kh&ocirc;ng thể bỏ qua Nước mắm Ngọc Trang &ndash; thương hiệu gia đ&igrave;nh c&oacute; lịch sử hơn một thế kỷ, bắt đầu từ những năm 1900. Trải qua nhiều thế hệ giữ nghề, Ngọc Trang vẫn g&igrave;n giữ phương ph&aacute;p sản xuất truyền thống kết hợp c&ocirc;ng nghệ hiện đại, mang đến sản phẩm nước mắm nguy&ecirc;n chất chuẩn vị biển Việt.</p>\n<p>Trong h&agrave;nh tr&igrave;nh hơn 100 năm l&agrave;m nghề,&nbsp;<strong>Nước mắm Ngọc Trang</strong>&nbsp;kh&ocirc;ng chỉ mang đến ch&eacute;n nước mắm thơm ngon chuẩn vị biển m&agrave; c&ograve;n giữ trọn tinh thần truyền thống Việt. Mỗi giọt nước mắm l&agrave; t&acirc;m huyết của nhiều thế hệ, l&agrave; bản sắc ẩm thực Kh&aacute;nh Ho&agrave; v&agrave; niềm tự h&agrave;o của người Việt.</p>\n<p><strong>Nước mắm truyền thống&nbsp;</strong>kh&ocirc;ng chỉ mang trong m&igrave;nh hương vị mộc mạc, m&agrave; c&ograve;n kể c&acirc;u chuyện văn h&oacute;a, nghề biển v&agrave; niềm tự h&agrave;o d&acirc;n tộc. H&atilde;y lựa chọn nước mắm nguy&ecirc;n chất chất lượng để cảm nhận trọn vẹn hương vị Việt trong mỗi bữa ăn.</p>\n<div class="blog-share text-center">&nbsp;</div>	/Articles/ArticleMediaFile?name=3a20f57052c3cc2d1838880a630cbd4c.jpg	/Articles/ArticleMediaFile?name=3a20f56c82c30110688811742deb7dbd.jpeg	0	long	\N	\N	2	2026-05-01 00:00:00	t	t	t	0	0	0	0	2	Nước mắm truyền thống – Tinh hoa ẩm thực Việt	test	nước mắm|nuocmamngoctrang	3a20f3f1-702a-7f07-9196-92139196a7a2
\.


--
-- Data for Name: EntityFiles; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."EntityFiles" ("Id", "ExtraProperties", "ConcurrencyStamp", "CreationTime", "CreatorId", "LastModificationTime", "LastModifierId", "IsDeleted", "DeleterId", "DeletionTime", "EntityType", "EntityId", "Collection", "SortOrder", "IsPrimary", "MediaFileId") FROM stdin;
\.


--
-- Data for Name: MediaFiles; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."MediaFiles" ("Id", "ExtraProperties", "ConcurrencyStamp", "CreationTime", "CreatorId", "LastModificationTime", "LastModifierId", "IsDeleted", "DeleterId", "DeletionTime", "FileName", "OriginalFileName", "Extension", "ContentType", "Size", "StorageProvider", "Bucket", "Folder", "Path", "Url", "Checksum", "Width", "Height", "Duration", "FileType", "Status") FROM stdin;
\.


--
-- Data for Name: PlaceCategories; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."PlaceCategories" ("Id", "ExtraProperties", "ConcurrencyStamp", "CreationTime", "CreatorId", "LastModificationTime", "LastModifierId", "IsDeleted", "DeleterId", "DeletionTime", "Name", "Slug", "Description", "Icon", "Color", "ParentId", "DisplayOrder", "IsActive") FROM stdin;
\.


--
-- Data for Name: PlaceFavorites; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."PlaceFavorites" ("Id", "ExtraProperties", "ConcurrencyStamp", "CreationTime", "CreatorId", "LastModificationTime", "LastModifierId", "IsDeleted", "DeleterId", "DeletionTime", "UserId", "PlaceId") FROM stdin;
\.


--
-- Data for Name: PlaceReviews; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."PlaceReviews" ("Id", "ExtraProperties", "ConcurrencyStamp", "CreationTime", "CreatorId", "LastModificationTime", "LastModifierId", "IsDeleted", "DeleterId", "DeletionTime", "Rating", "Title", "Comment", "LikeCount", "Status", "UserId", "PlaceId") FROM stdin;
\.


--
-- Data for Name: PlaceTagMappings; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."PlaceTagMappings" ("Id", "ExtraProperties", "ConcurrencyStamp", "CreationTime", "CreatorId", "LastModificationTime", "LastModifierId", "IsDeleted", "DeleterId", "DeletionTime", "IsPrimary", "SortOrder", "PlaceTagId", "PlaceId") FROM stdin;
\.


--
-- Data for Name: PlaceTags; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."PlaceTags" ("Id", "ExtraProperties", "ConcurrencyStamp", "CreationTime", "CreatorId", "LastModificationTime", "LastModifierId", "IsDeleted", "DeleterId", "DeletionTime", "Name", "Slug", "Description", "UsageCount") FROM stdin;
\.


--
-- Data for Name: PlaceViews; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."PlaceViews" ("Id", "ExtraProperties", "ConcurrencyStamp", "CreationTime", "CreatorId", "LastModificationTime", "LastModifierId", "IsDeleted", "DeleterId", "DeletionTime", "UserId", "IpAddress", "Device", "ViewedAt", "Duration", "Source", "PlaceId") FROM stdin;
\.


--
-- Data for Name: Places; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Places" ("Id", "ExtraProperties", "ConcurrencyStamp", "CreationTime", "CreatorId", "LastModificationTime", "LastModifierId", "IsDeleted", "DeleterId", "DeletionTime", "Name", "Slug", "ShortDescription", "Description", "ThumbnailUrl", "CoverImageUrl", "Address", "Latitude", "Longituded", "PhoneNumber", "Email", "Website", "OpeningHours", "PriceRange", "GoogleMapUrl", "Status", "ViewCount", "FavoriteCount", "ReviewCount", "RatingAveraged", "RatingTotal", "IsFeatured", "IsHot", "IsVerified", "SeoTitle", "SeoDescription", "SeoKeywords", "PlaceCategoryId", "ProvinceId", "WardId") FROM stdin;
\.


--
-- Data for Name: Provinces; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Provinces" ("Id", "ExtraProperties", "ConcurrencyStamp", "CreationTime", "CreatorId", "LastModificationTime", "LastModifierId", "IsDeleted", "DeleterId", "DeletionTime", "Code", "Name") FROM stdin;
3a20eef8-ee24-1d42-8ea0-c2dfffd00643	{}	ae001fef991f41cd91608815f32d2123	2026-04-30 12:10:55.811432	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	\N	\N	f	\N	\N	056	Khánh Hòa
3a20eef9-9aae-3f2d-4bc7-0a4af4ff3f5f	{}	d6dbcf0f1bb840d2a10bf58a98f0b605	2026-04-30 12:11:39.962599	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	2026-04-30 12:11:43.773874	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	t	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	2026-04-30 12:11:43.769641	059	TPHCM
3a20ef17-12c0-488c-6366-3bf268cbf282	{}	d777ffd0f02249f184d287e8afcb3391	2026-04-30 12:43:51.234235	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	\N	\N	f	\N	\N	021	TP Hồ Chí Minh
\.


--
-- Data for Name: Wards; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Wards" ("Id", "ExtraProperties", "ConcurrencyStamp", "CreationTime", "CreatorId", "LastModificationTime", "LastModifierId", "IsDeleted", "DeleterId", "DeletionTime", "Code", "Name", "ProvinceId") FROM stdin;
3a20ef16-d623-968d-d968-01c767fc522d	{}	62501f81b4b34fc5a03c8cf58933eddd	2026-04-30 12:43:35.729807	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	2026-04-30 12:44:02.731102	3a20e9c3-58f2-bbe9-d4ca-25656eaca802	f	\N	\N	TNT	Tây Nha Trang	3a20eef8-ee24-1d42-8ea0-c2dfffd00643
\.


--
-- Data for Name: __MasterDataService_Migrations; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."__MasterDataService_Migrations" ("MigrationId", "ProductVersion") FROM stdin;
20260430031515_Initial	10.0.2
20260430043138_Added_Province	10.0.2
20260430054054_Added_Ward	10.0.2
20260430110017_Added_ArticleCategory	10.0.2
20260430110247_Added_ArticleTag	10.0.2
20260430110909_Added_Article	10.0.2
20260430111138_Added_ArticleTagMapping	10.0.2
20260430111331_Added_ArticleView	10.0.2
20260501113434_Added_MediaFile	10.0.2
20260501113824_Added_PlaceCategory	10.0.2
20260501113923_Added_PlaceTag	10.0.2
20260501114705_Added_Place	10.0.2
20260501114825_Added_PlaceTagMapping	10.0.2
20260501115006_Added_EntityFile	10.0.2
20260501115217_Added_PlaceReview	10.0.2
20260501115304_Added_PlaceFavorite	10.0.2
20260501115426_Added_PlaceView	10.0.2
\.


--
-- Name: AbpEventInbox PK_AbpEventInbox; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpEventInbox"
    ADD CONSTRAINT "PK_AbpEventInbox" PRIMARY KEY ("Id");


--
-- Name: AbpEventOutbox PK_AbpEventOutbox; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AbpEventOutbox"
    ADD CONSTRAINT "PK_AbpEventOutbox" PRIMARY KEY ("Id");


--
-- Name: ArticleCategories PK_ArticleCategories; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."ArticleCategories"
    ADD CONSTRAINT "PK_ArticleCategories" PRIMARY KEY ("Id");


--
-- Name: ArticleTagMappings PK_ArticleTagMappings; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."ArticleTagMappings"
    ADD CONSTRAINT "PK_ArticleTagMappings" PRIMARY KEY ("Id");


--
-- Name: ArticleTags PK_ArticleTags; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."ArticleTags"
    ADD CONSTRAINT "PK_ArticleTags" PRIMARY KEY ("Id");


--
-- Name: ArticleViews PK_ArticleViews; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."ArticleViews"
    ADD CONSTRAINT "PK_ArticleViews" PRIMARY KEY ("Id");


--
-- Name: Articles PK_Articles; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Articles"
    ADD CONSTRAINT "PK_Articles" PRIMARY KEY ("Id");


--
-- Name: EntityFiles PK_EntityFiles; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."EntityFiles"
    ADD CONSTRAINT "PK_EntityFiles" PRIMARY KEY ("Id");


--
-- Name: MediaFiles PK_MediaFiles; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."MediaFiles"
    ADD CONSTRAINT "PK_MediaFiles" PRIMARY KEY ("Id");


--
-- Name: PlaceCategories PK_PlaceCategories; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."PlaceCategories"
    ADD CONSTRAINT "PK_PlaceCategories" PRIMARY KEY ("Id");


--
-- Name: PlaceFavorites PK_PlaceFavorites; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."PlaceFavorites"
    ADD CONSTRAINT "PK_PlaceFavorites" PRIMARY KEY ("Id");


--
-- Name: PlaceReviews PK_PlaceReviews; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."PlaceReviews"
    ADD CONSTRAINT "PK_PlaceReviews" PRIMARY KEY ("Id");


--
-- Name: PlaceTagMappings PK_PlaceTagMappings; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."PlaceTagMappings"
    ADD CONSTRAINT "PK_PlaceTagMappings" PRIMARY KEY ("Id");


--
-- Name: PlaceTags PK_PlaceTags; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."PlaceTags"
    ADD CONSTRAINT "PK_PlaceTags" PRIMARY KEY ("Id");


--
-- Name: PlaceViews PK_PlaceViews; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."PlaceViews"
    ADD CONSTRAINT "PK_PlaceViews" PRIMARY KEY ("Id");


--
-- Name: Places PK_Places; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Places"
    ADD CONSTRAINT "PK_Places" PRIMARY KEY ("Id");


--
-- Name: Provinces PK_Provinces; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Provinces"
    ADD CONSTRAINT "PK_Provinces" PRIMARY KEY ("Id");


--
-- Name: Wards PK_Wards; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Wards"
    ADD CONSTRAINT "PK_Wards" PRIMARY KEY ("Id");


--
-- Name: __MasterDataService_Migrations PK___MasterDataService_Migrations; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."__MasterDataService_Migrations"
    ADD CONSTRAINT "PK___MasterDataService_Migrations" PRIMARY KEY ("MigrationId");


--
-- Name: IX_AbpEventInbox_MessageId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpEventInbox_MessageId" ON public."AbpEventInbox" USING btree ("MessageId");


--
-- Name: IX_AbpEventInbox_Status_CreationTime; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpEventInbox_Status_CreationTime" ON public."AbpEventInbox" USING btree ("Status", "CreationTime");


--
-- Name: IX_AbpEventOutbox_CreationTime; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AbpEventOutbox_CreationTime" ON public."AbpEventOutbox" USING btree ("CreationTime");


--
-- Name: IX_ArticleTagMappings_ArticleId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_ArticleTagMappings_ArticleId" ON public."ArticleTagMappings" USING btree ("ArticleId");


--
-- Name: IX_ArticleTagMappings_ArticleTagId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_ArticleTagMappings_ArticleTagId" ON public."ArticleTagMappings" USING btree ("ArticleTagId");


--
-- Name: IX_ArticleViews_ArticleId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_ArticleViews_ArticleId" ON public."ArticleViews" USING btree ("ArticleId");


--
-- Name: IX_Articles_ArticleCategoryId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Articles_ArticleCategoryId" ON public."Articles" USING btree ("ArticleCategoryId");


--
-- Name: IX_Articles_CategoryId_PublishedAt; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Articles_CategoryId_PublishedAt" ON public."Articles" USING btree ("ArticleCategoryId", "PublishedAt" DESC);


--
-- Name: IX_Articles_IsFeatured; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Articles_IsFeatured" ON public."Articles" USING btree ("IsFeatured");


--
-- Name: IX_Articles_IsHot; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Articles_IsHot" ON public."Articles" USING btree ("IsHot");


--
-- Name: IX_Articles_IsTrending; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Articles_IsTrending" ON public."Articles" USING btree ("IsTrending");


--
-- Name: IX_Articles_PublishedAt; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Articles_PublishedAt" ON public."Articles" USING btree ("PublishedAt" DESC);


--
-- Name: IX_Articles_Published_Only; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Articles_Published_Only" ON public."Articles" USING btree ("PublishedAt" DESC) WHERE ("Status" = 1);


--
-- Name: IX_Articles_Slug; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "IX_Articles_Slug" ON public."Articles" USING btree ("Slug");


--
-- Name: IX_Articles_Status_PublishedAt; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Articles_Status_PublishedAt" ON public."Articles" USING btree ("Status", "PublishedAt" DESC);

ALTER TABLE public."Articles" CLUSTER ON "IX_Articles_Status_PublishedAt";


--
-- Name: IX_Articles_Type_PublishedAt; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Articles_Type_PublishedAt" ON public."Articles" USING btree ("Type", "PublishedAt" DESC);


--
-- Name: IX_Articles_ViewCount; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Articles_ViewCount" ON public."Articles" USING btree ("ViewCount" DESC);


--
-- Name: IX_EntityFiles_MediaFileId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_EntityFiles_MediaFileId" ON public."EntityFiles" USING btree ("MediaFileId");


--
-- Name: IX_PlaceFavorites_PlaceId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_PlaceFavorites_PlaceId" ON public."PlaceFavorites" USING btree ("PlaceId");


--
-- Name: IX_PlaceReviews_PlaceId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_PlaceReviews_PlaceId" ON public."PlaceReviews" USING btree ("PlaceId");


--
-- Name: IX_PlaceTagMappings_PlaceId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_PlaceTagMappings_PlaceId" ON public."PlaceTagMappings" USING btree ("PlaceId");


--
-- Name: IX_PlaceTagMappings_PlaceTagId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_PlaceTagMappings_PlaceTagId" ON public."PlaceTagMappings" USING btree ("PlaceTagId");


--
-- Name: IX_PlaceViews_PlaceId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_PlaceViews_PlaceId" ON public."PlaceViews" USING btree ("PlaceId");


--
-- Name: IX_Places_PlaceCategoryId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Places_PlaceCategoryId" ON public."Places" USING btree ("PlaceCategoryId");


--
-- Name: IX_Places_ProvinceId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Places_ProvinceId" ON public."Places" USING btree ("ProvinceId");


--
-- Name: IX_Places_WardId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Places_WardId" ON public."Places" USING btree ("WardId");


--
-- Name: IX_Wards_ProvinceId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Wards_ProvinceId" ON public."Wards" USING btree ("ProvinceId");


--
-- Name: ArticleTagMappings FK_ArticleTagMappings_ArticleTags_ArticleTagId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."ArticleTagMappings"
    ADD CONSTRAINT "FK_ArticleTagMappings_ArticleTags_ArticleTagId" FOREIGN KEY ("ArticleTagId") REFERENCES public."ArticleTags"("Id");


--
-- Name: ArticleTagMappings FK_ArticleTagMappings_Articles_ArticleId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."ArticleTagMappings"
    ADD CONSTRAINT "FK_ArticleTagMappings_Articles_ArticleId" FOREIGN KEY ("ArticleId") REFERENCES public."Articles"("Id");


--
-- Name: ArticleViews FK_ArticleViews_Articles_ArticleId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."ArticleViews"
    ADD CONSTRAINT "FK_ArticleViews_Articles_ArticleId" FOREIGN KEY ("ArticleId") REFERENCES public."Articles"("Id");


--
-- Name: Articles FK_Articles_ArticleCategories_ArticleCategoryId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Articles"
    ADD CONSTRAINT "FK_Articles_ArticleCategories_ArticleCategoryId" FOREIGN KEY ("ArticleCategoryId") REFERENCES public."ArticleCategories"("Id");


--
-- Name: EntityFiles FK_EntityFiles_MediaFiles_MediaFileId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."EntityFiles"
    ADD CONSTRAINT "FK_EntityFiles_MediaFiles_MediaFileId" FOREIGN KEY ("MediaFileId") REFERENCES public."MediaFiles"("Id");


--
-- Name: PlaceFavorites FK_PlaceFavorites_Places_PlaceId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."PlaceFavorites"
    ADD CONSTRAINT "FK_PlaceFavorites_Places_PlaceId" FOREIGN KEY ("PlaceId") REFERENCES public."Places"("Id");


--
-- Name: PlaceReviews FK_PlaceReviews_Places_PlaceId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."PlaceReviews"
    ADD CONSTRAINT "FK_PlaceReviews_Places_PlaceId" FOREIGN KEY ("PlaceId") REFERENCES public."Places"("Id");


--
-- Name: PlaceTagMappings FK_PlaceTagMappings_PlaceTags_PlaceTagId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."PlaceTagMappings"
    ADD CONSTRAINT "FK_PlaceTagMappings_PlaceTags_PlaceTagId" FOREIGN KEY ("PlaceTagId") REFERENCES public."PlaceTags"("Id");


--
-- Name: PlaceTagMappings FK_PlaceTagMappings_Places_PlaceId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."PlaceTagMappings"
    ADD CONSTRAINT "FK_PlaceTagMappings_Places_PlaceId" FOREIGN KEY ("PlaceId") REFERENCES public."Places"("Id");


--
-- Name: PlaceViews FK_PlaceViews_Places_PlaceId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."PlaceViews"
    ADD CONSTRAINT "FK_PlaceViews_Places_PlaceId" FOREIGN KEY ("PlaceId") REFERENCES public."Places"("Id");


--
-- Name: Places FK_Places_PlaceCategories_PlaceCategoryId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Places"
    ADD CONSTRAINT "FK_Places_PlaceCategories_PlaceCategoryId" FOREIGN KEY ("PlaceCategoryId") REFERENCES public."PlaceCategories"("Id");


--
-- Name: Places FK_Places_Provinces_ProvinceId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Places"
    ADD CONSTRAINT "FK_Places_Provinces_ProvinceId" FOREIGN KEY ("ProvinceId") REFERENCES public."Provinces"("Id");


--
-- Name: Places FK_Places_Wards_WardId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Places"
    ADD CONSTRAINT "FK_Places_Wards_WardId" FOREIGN KEY ("WardId") REFERENCES public."Wards"("Id");


--
-- Name: Wards FK_Wards_Provinces_ProvinceId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Wards"
    ADD CONSTRAINT "FK_Wards_Provinces_ProvinceId" FOREIGN KEY ("ProvinceId") REFERENCES public."Provinces"("Id");


--
-- PostgreSQL database dump complete
--

\unrestrict c1gRWjaYy0pocMoClFyIguTg1uzc3FQpff2PHzKSfuiYG7yPJ6N2DRYLwA91nOL

--
-- Database "postgres" dump
--

\connect postgres

--
-- PostgreSQL database dump
--

\restrict aGR6jFjoemdlNPL8FoKTnlGgTvVXtKhgTs4XEvD652UcuyQMo3aHmOdGqfngIFS

-- Dumped from database version 14.22
-- Dumped by pg_dump version 14.22

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- PostgreSQL database dump complete
--

\unrestrict aGR6jFjoemdlNPL8FoKTnlGgTvVXtKhgTs4XEvD652UcuyQMo3aHmOdGqfngIFS

--
-- PostgreSQL database cluster dump complete
--

