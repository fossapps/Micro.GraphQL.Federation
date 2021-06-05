class SemanticReleaseError extends Error {
    constructor(message, code, details) {
        super(message);
        Error.captureStackTrace(this, this.constructor);
        this.name = 'SemanticReleaseError';
        this.details = details;
        this.code = code;
        this.semanticRelease = true;
    }
}

module.exports = {
    verifyConditions: [
        () => {
            if (!process.env.NUGET_TOKEN) {
                throw new SemanticReleaseError(
                    "No NUGET_TOKEN specified",
                    "ENONUGET_TOKEN",
                    "Please make sure to nuget token in `NUGET_TOKEN` environment variable on your CI environment. The token must be able to push to nuget");
            }
        },
        "@semantic-release/github"
    ],
    "prepare": [
        {
            "path": "@semantic-release/exec",
            "cmd": "dotnet build --configuration Release -p:Version=${nextRelease.version}"
        },
        {
            "path": "@semantic-release/exec",
            "cmd": "dotnet pack --include-symbols -c Release --output ./artifacts -p:PackageVersion=${nextRelease.version}"
        }
    ],
    "publish": [
        "@semantic-release/github",
        {
            "path": "@semantic-release/exec",
            "cmd": "dotnet nuget push ./artifacts/*.nupkg -k ${ process.env.NUGET_TOKEN } -s https://api.nuget.org/v3/index.json"
        }
    ]
}
