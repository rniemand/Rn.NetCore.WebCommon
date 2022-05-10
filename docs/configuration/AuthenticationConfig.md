[Home](/README.md) / [Configuration](/docs/configuration/README.md) / AuthenticationConfig

# AuthenticationConfig
More to come...

```json
{
  "Rn.WebCore": {
    "Authentication": {
      "secret": "2QtM...5hg1",
      "sessionLengthMin": 1440
    }
  }
}
```

Details on each option is listed below.

| Property | Type | Required | Default | Notes |
| --- | --- | ---- | ---- | --- |
| `secret` | `string` | required | - | Encryption secret to use. |
| `sessionLengthMin` | `int` | optional | `1440` | Default session length to use. |