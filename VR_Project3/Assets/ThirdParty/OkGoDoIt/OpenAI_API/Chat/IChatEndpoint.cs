using OpenAI_API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenAI_API.Chat
{
	/// <summary>
	/// An interface for <see cref="ChatEndpoint"/>, for ease of mock testing, etc
	/// </summary>
	public interface IChatEndpoint
	{
		ChatRequest DefaultChatRequestArgs { get; set; }

		Task<chatResult> CreateChatCompletionAsync(ChatRequest request);
		Task<chatResult> CreateChatCompletionAsync(ChatRequest request, int numOutputs = 5);
		Task<chatResult> CreateChatCompletionAsync(IList<ChatMessage> messages, Model model = null, double? temperature = null, double? top_p = null, int? numOutputs = null, int? max_tokens = null, double? frequencyPenalty = null, double? presencePenalty = null, IReadOnlyDictionary<string, float> logitBias = null, params string[] stopSequences);
		Task<chatResult> CreateChatCompletionAsync(params ChatMessage[] messages);
		Task<chatResult> CreateChatCompletionAsync(params string[] userMessages);
		Conversation CreateConversation();
		Task StreamChatAsync(ChatRequest request, Action<chatResult> resultHandler);
		IAsyncEnumerable<chatResult> StreamChatEnumerableAsync(ChatRequest request);
		IAsyncEnumerable<chatResult> StreamChatEnumerableAsync(IList<ChatMessage> messages, Model model = null, double? temperature = null, double? top_p = null, int? numOutputs = null, int? max_tokens = null, double? frequencyPenalty = null, double? presencePenalty = null, IReadOnlyDictionary<string, float> logitBias = null, params string[] stopSequences);
		Task StreamCompletionAsync(ChatRequest request, Action<int, chatResult> resultHandler);
	}
}