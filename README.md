# Bulwark.Auth.Admin.Captain
Admin client for Bulwark.Auth.Admin service, is used as 
an api to manage accounts, roles, permissions, and more.

It is important to note that Bulwark.Auth.Admin api should be on a private network
and not exposed to the public internet. This has a lot management features that you would not 
want to expose to the public.

More ways to secure Admin api coming soon.  

# Contributing
- Each contribution will need a issue/ticket created to track the feature or bug fix before it will be considered
- The PR must pass all tests and be reviewed by an official maintainer
- Each PR must be linked to an issue/ticket, once the PR is merged the ticket will be auto closed
- Each feature/bugfix needs to have unit tests
- Each feature must have the code documented inline

There is a docker compose file in the root of the project that `must` be used to run the tests.
This can be started by running `docker-compose up` in the root of the project.
Mocks are not used in `Captain` tests, `Bulwark.Auth.Admin` must be running.

The Reason mocks are not used in most cases is good to verify the integration with `Bulwark.Auth.Admin` is working
with the latest version. It removes the false sense of security that sometimes comes with mocking.

## Usage

Add the package to your project:
https://www.nuget.org/packages/Bulwark.Auth.Admin.Captain


