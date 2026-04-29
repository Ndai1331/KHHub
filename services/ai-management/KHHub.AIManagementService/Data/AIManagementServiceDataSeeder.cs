using Volo.Abp.DependencyInjection;
using Volo.AIManagement.Workspaces;

namespace KHHub.AIManagementService.Data;

public class AIManagementServiceDataSeeder : ITransientDependency
{
    private readonly ILogger<AIManagementServiceDataSeeder> _logger;
    private readonly IWorkspaceRepository _workspaceRepository;
    private readonly ApplicationWorkspaceManager _workspaceManager;

    public AIManagementServiceDataSeeder(
        ILogger<AIManagementServiceDataSeeder> logger,
        IWorkspaceRepository workspaceRepository,
        ApplicationWorkspaceManager workspaceManager)
    {
        _logger = logger;
        _workspaceRepository = workspaceRepository;
        _workspaceManager = workspaceManager;
    }

    public async Task SeedAsync(Guid? tenantId = null)
    {
        _logger.LogInformation("Seeding data...");

        if (await _workspaceRepository.GetCountAsync() > 0)
        {
            _logger.LogInformation("Data already seeded");
            return;
        }

        var ollamaAssistantWorkspace = await _workspaceManager.CreateAsync(
            name: "OllamaAssistant",
            provider: "Ollama",
            modelName: "llama3.2");
        ollamaAssistantWorkspace.ApiBaseUrl = "http://localhost:11434";
        ollamaAssistantWorkspace.ApiKey = "";
        ollamaAssistantWorkspace.ApplicationName = "KHHub.AIManagementService";

        await _workspaceRepository.InsertAsync(ollamaAssistantWorkspace);

        var openAiAssistantWorkspace = await _workspaceManager.CreateAsync(
            name: "OpenAIAssistant",
            provider: "OpenAI",
            modelName: "gpt-5.4");
        openAiAssistantWorkspace.ApiBaseUrl = "https://api.openai.com/v1";
        openAiAssistantWorkspace.ApiKey = "";
        openAiAssistantWorkspace.ApplicationName = "KHHub.AIManagementService";

        await _workspaceRepository.InsertAsync(openAiAssistantWorkspace);
    }
}
