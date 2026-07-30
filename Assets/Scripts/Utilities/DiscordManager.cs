using Discord.Sdk;
using UnityEngine;

public class DiscordManager : MonoBehaviour
{
    [SerializeField] private ulong clientId;
    [SerializeField] private string state = "Escaping the house";
    [SerializeField] private string details = "Playing Grandpa Escape";

    private static DiscordManager instance;

    private Client client;
    private string codeVerifier;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (clientId == 0)
        {
            Debug.LogError("Discord Client ID is missing. Set it on the DiscordManager object.");
            return;
        }

        client = new Client();
        client.AddLogCallback(OnLog, LoggingSeverity.Error);
        client.SetStatusChangedCallback(OnStatusChanged);

        StartOAuthFlow();
    }

    private void OnLog(string message, LoggingSeverity severity)
    {
        Debug.Log($"Discord SDK: {severity} - {message}");
    }

    private void OnStatusChanged(Client.Status status, Client.Error error, int errorCode)
    {
        Debug.Log($"Discord status changed: {status}");

        if (error != Client.Error.None)
        {
            Debug.LogError($"Discord error: {error}, code: {errorCode}");
            return;
        }

        if (status == Client.Status.Ready)
        {
            SetRichPresence();
        }
    }

    private void StartOAuthFlow()
    {
        var authorizationVerifier = client.CreateAuthorizationCodeVerifier();
        codeVerifier = authorizationVerifier.Verifier();

        var args = new AuthorizationArgs();
        args.SetClientId(clientId);
        args.SetScopes(Client.GetDefaultPresenceScopes());
        args.SetCodeChallenge(authorizationVerifier.Challenge());

        client.Authorize(args, OnAuthorizeResult);
    }

    private void OnAuthorizeResult(ClientResult result, string code, string redirectUri)
    {
        if (!result.Successful())
        {
            Debug.LogError("Discord authorization failed.");
            return;
        }

        client.GetToken(
            clientId,
            code,
            codeVerifier,
            redirectUri,
            (tokenResult, token, refreshToken, tokenType, expiresIn, scope) =>
            {
                if (string.IsNullOrEmpty(token))
                {
                    Debug.LogError("Discord token was empty.");
                    return;
                }

                client.UpdateToken(
                    AuthorizationTokenType.Bearer,
                    token,
                    updateResult => client.Connect()
                );
            }
        );
    }

    private void SetRichPresence()
    {
        Activity activity = new Activity();
        activity.SetType(ActivityTypes.Playing);
        activity.SetState(state);
        activity.SetDetails(details);

        client.UpdateRichPresence(activity, result =>
        {
            if (result.Successful())
            {
                Debug.Log("Discord Rich Presence updated.");
                return;
            }

            Debug.LogError("Failed to update Discord Rich Presence.");
        });
    }
}
