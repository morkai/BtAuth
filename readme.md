# BtAuth

Bluetooth auto Authentication.

1. Listens for Bluetooth authentication requests on the primary radio.
2. Matches incoming requests by device name. Requests from devices not matching the string
   specified as the first command-line argument are ignored.
3. If the authentication method is 'Legacy', then sets the PIN to the one specified as
   the second command-line argument.
4. Confirms the authentication request.

One might want to uncheck the 'Alert me when a new Bluetooth device wants to connect'
checkbox in the 'Notifications' area of the 'Bluetooth Settings'.

## Requirements

### .NET Framework

  * __Version__: 3.5
  * __Website__: http://www.microsoft.com/net
  * __Download__: http://www.microsoft.com/net/downloads

### 32feet.NET

  * __Version__: 3.5
  * __Website__: https://32feet.codeplex.com/
  * __Download__: https://32feet.codeplex.com/releases/view/88941

## Usage

BtAuth can be run as a console application:

```
BtAuth.exe "<deviceName>" "<pin>"
```

where:

  * `<deviceName>` is a string that should be a part of a name of a device that
    tries to authenticate. Defaults to `*` (handles all devices).
  
  * `<pin>` is an authentication code that should be set in case the authentication
    method is 'Legacy'. Defaults to `1234`.

#### Example

```
BtAuth.exe "Mini300" "1234"
```

## License

This project is released under the
[MIT License](https://raw.github.com/morkai/BtAuth/master/license.md).
