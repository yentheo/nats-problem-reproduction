Start-Process nats-server -ArgumentList "-c", "./nats.conf"
Write-Host "started nats server"
Start-Sleep -s 5
Write-Host "adding stream"
nats stream add `
    --subjects=test.> `
    --discard=old `
    --max-msgs-per-subject=1 `
    --storage=file `
    --retention=limits  `
    --max-msgs=-1 `
    --max-msg-size=-1 `
    --max-bytes=-1 `
    --max-age=-1 `
    --dupe-window="2m" `
    --replicas=1 `
    --no-allow-rollup `
    --no-deny-delete `
    --no-deny-purge `
    test