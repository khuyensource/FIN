document.writeln('<!--[if IE]>');
document.writeln('<script language="VBScript">');
document.writeln('Private i, x, MM_FlashControlVersion');
document.writeln('On Error Resume Next')
;document.writeln('x = null');
document.writeln('MM_FlashControlVersion = 0');
document.writeln('var Flashmode');
document.writeln('FlashMode = False');
document.writeln('var do_dw_var');
document.writeln('var browser_flash_version');
document.writeln('For i = 9 To 1 Step -1');
document.writeln('Set x = CreateObject("ShockwaveFlash.ShockwaveFlash." & i)');
document.writeln('MM_FlashControlInstalled = IsObject(x)');
document.writeln('If MM_FlashControlInstalled Then');
document.writeln('MM_FlashControlVersion = CStr(i)');
document.writeln('Exit For');
document.writeln('End If');
document.writeln('Next');
document.writeln('x = null');
document.writeln('FlashMode = (MM_FlashControlVersion >= 8)');
document.writeln('do_dw_var = FlashMode');
document.writeln('browser_flash_version = MM_FlashControlVersion');
document.writeln('</script>');
document.writeln('<![endif]-->');
document.writeln('<!--[if !IE]>-->');
document.writeln('<script language=javascript>');
document.writeln('FlashMode = 0');
document.writeln('if (navigator.plugins && navigator.plugins.length > 0)');
document.writeln('{');
document.writeln('	if (navigator.plugins["Shockwave Flash"])');
document.writeln('	{');
document.writeln('		var plugin_version = 0');
document.writeln('		var words = navigator.plugins["Shockwave Flash"].description.split(" ")');
document.writeln('		for (var i = 0; i < words.length; ++i)');
document.writeln('		{');
document.writeln('			if (isNaN(parseInt(words[i])))');
document.writeln('			continue');
document.writeln('			plugin_version = words[i]');
document.writeln('		}');
document.writeln('		if (plugin_version >= 8)');
document.writeln('		{');
document.writeln('			var plugin = navigator.plugins["Shockwave Flash"]');
document.writeln('			var numTypes = plugin.length');
document.writeln('			for (j = 0; j < numTypes; j++)');
document.writeln('			{');
document.writeln('				mimetype = plugin[j]');
document.writeln('				if (mimetype)');
document.writeln('				{');
document.writeln('					if (mimetype.enabledPlugin && (mimetype.suffixes.indexOf("swf") != -1))');
document.writeln('						FlashMode = 1');
document.writeln('					if (navigator.mimeTypes["application/x-shockwave-flash"] == null)');
document.writeln('						FlashMode = 0');
document.writeln('				}');
document.writeln('			}');
document.writeln('		}');
document.writeln('	}');
document.writeln('}');
document.writeln('do_dw_var = FlashMode');
document.writeln('browser_flash_version = plugin_version');
document.writeln('</script>');document.writeln('<!-- <![endif]-->');


function WriteObject(FileName, Width, Height)
{
    if(do_dw_var)
    {
        document.writeln('<object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0" width="' + Width + 'px" height="' + Height + 'px">');
        document.writeln('<param name="movie" value="'+ FileName +'" />');
        document.writeln('<param name="quality" value="high" />');
        document.writeln('<param name="menu" value="0" />');
        document.writeln('<param name="wmode" value="transparent" />');
        document.writeln('<embed type="application/x-shockwave-flash" src="'+ FileName +'" quality="high" width="' + Width + '" height="' + Height + '" pluginspage="http://www.macromedia.com/go/getflashplayer" wmode="transparent" ></embed>');
        document.writeln('</object>');
    }
    else
    {
        document.writeln('<div style="text-align: center; font-weight: bolder; background-color: white; border: 3px double red; padding: 5px">Your computer have an old of Macromedia\'s Flash Player.<br> <a href="http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash" target="_blank">Get the latest flash player</a>.</div>');
    }
}