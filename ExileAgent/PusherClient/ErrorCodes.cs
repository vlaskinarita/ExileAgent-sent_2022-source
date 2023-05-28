using System;

namespace PusherClient
{
	public enum ErrorCodes
	{
		Unknown,
		MustConnectOverSSL = 4000,
		ApplicationDoesNotExist,
		ApplicationDisabled = 4003,
		ApplicationOverConnectionQuota,
		PathNotFound,
		ConnectionNotAuthorizedWithinTimeout = 4009,
		ClientOverRateLimit = 4301,
		ClientTimeout = 5000,
		ConnectError,
		DisconnectError,
		SubscriptionError,
		MessageReceivedError,
		ReconnectError,
		WebSocketError,
		EventEmitterActionError = 5100,
		TriggerEventNameInvalidError,
		TriggerEventNotConnectedError,
		TriggerEventNotSubscribedError,
		TriggerEventPublicChannelError,
		TriggerEventPrivateEncryptedChannelError,
		ChannelAuthorizerNotSet = 7500,
		ChannelAuthorizationError,
		ChannelAuthorizationTimeout,
		ChannelUnauthorized,
		PresenceChannelAlreadyDefined,
		ChannelDecryptionFailure,
		ConnectedEventHandlerError = 7601,
		ConnectionStateChangedEventHandlerError,
		DisconnectedEventHandlerError,
		MemberAddedEventHandlerError,
		MemberRemovedEventHandlerError,
		SubscribedEventHandlerError
	}
}
