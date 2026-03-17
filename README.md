# StrivoIngestPublish
Reads a file and publish to queue to simulate events.

## Required application settings

Add the following settings to your Azure Function app (Configuration → Application settings) or to `local.settings.json` for local development:

| Setting | Description | Example value |
|---|---|---|
| `BlobServiceUri` | Blob service endpoint of the storage account containing CSV source files | `https://<storage-account-name>.blob.core.windows.net` |
| `QueueServiceUri` | Queue service endpoint of the storage account where messages are published | `https://<storage-account-name>.queue.core.windows.net` |

## Optional application settings

| Setting | Description | Default |
|---|---|---|
| `TimerSchedule` | CRON expression controlling how often the function runs | `0 */5 * * * *` (every 5 minutes) |
| `BlobContainerName` | Name of the blob container to read CSV files from | `datasource` |
| `QueueName` | Name of the storage queue to publish messages to | `consumethis` |

## Managed Identity

The function uses `DefaultAzureCredential` to authenticate against Azure Storage. Ensure the Function App's managed identity has been granted the following RBAC roles on the storage account:

- **Storage Blob Data Reader** – to list and download blobs from the container
- **Storage Queue Data Message Sender** – to enqueue messages
