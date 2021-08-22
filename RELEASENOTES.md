## Breaking change

- The configuration file is not expected in `$XDG_HOME/RepoStatusTable.json` anymore, but instead in `$XDG_HOME/.config/RepoStatusTable/config.json`

## New features

- Added file content provider that attempts to read a specific file path from each repository and adds its content to the output table
- Additional configuration options
  - The look of the table can now be customized via configuration file
  - The content of the column headers can now be customized via configuration
  - Column positions can be set via configuration
  - Added option to either render the table row by row live or in one shot