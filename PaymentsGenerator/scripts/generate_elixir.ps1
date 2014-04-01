$fileNo=$args[0]
$recordNo=$args[1]
$filenameArgs=$args[2]
$acntNo=$args[3]
$outPath=$args[4]

Function GenerateElixir ($fileName, $matches, $j)
{
$stream = [system.io.StreamWriter] $fileName
for ($i=0; $i -lt $recordNo;)
{	
	$amount = ($i + 1) * ($j + 1) 
	$content = "112,20120906," + $amount +",11402017,13700001,""85114020170000460201327048"",""" + $matches[$i++%$acntNo].tostring() + """,""NAZWA NADAWCY||UL.NADAWCY 35 M.13|13-100 NIDZICA"",""DANE BENEFICJENTA 35 M 13 BL. 410||92-546 MIASTO"",0,13701082,""TST POPR|TESTY OZYRYS ELX IN rach BM bex FX"","""","""",""56112205099714820"","""""
	$stream.WriteLine($content)
	$content = "112,20120906," + $amount +",11402017,13700001,""85114020170000460201327048"",""" + $matches[$i++%$acntNo].tostring() + """,""NAZWA NADAWCY||UL.NADAWCY 35 M.13|92-546 LODZ"",""DANE BENEFICJENTA 35 M 13 BL. 410||92-546 MIASTO"",0,13701082,""TST POPR|TESTY OZYRYS ELX IN rach BM bex FX"","""","""",""56112205099714820"","""""
	$stream.WriteLine($content)
	$content = "112,20120906," + $amount +",11402017,13700001,""31114020170000490202518645"",""" + $matches[$i++%$acntNo].tostring() + """,""NAZWA NADAWCY||UL.KONOPNICKIEJ 12C M.40|13-100 NIDZICA"",""SLONECZNY DOM|||WARSZAWA"",0,13701082,""TST POPR|2 LINIJKA TYTULU IN BEZ FX"","""","""",""56211305198785132"","""""
	$stream.WriteLine($content)
	$content = "112,20120906," + $amount +",11402017,13700001,""31114020170000490202518645"",""" + $matches[$i++%$acntNo].tostring() + """,""NAZWA NADAWCY||UL.KONOPNICKIEJ 12C M.40|13-100 NIDZICA"",""SLONECZNY DOM|||WARSZAWA"",0,13701082,""TST POPR 1 LINIJKA TYTULU PLATNOSCI|2 LINIJKA TYTULU PLATNOSCI|3 LINIJKA TYTULU PLATNOSCI"","""","""",""56381205198745133"","""""
	$stream.WriteLine($content)
	$content = "114,20120509," + $amount +",12402076,13700001,""80124059181111000049115017"",""" + $matches[$i++%$acntNo].tostring() + """,""SUPERHOBBY MARKET BUDOWLANY SP. Z O|.O.|AL. KRAKOWSKA 102|02-180 WARSZAWA"",""JASKOLSKI JAROSLAW JARMEX POTKANOWS|KA 50|26-600 RADOM Polen"",12405918,13701082,""TST POPR*182.12/SO-188.12/SO-189."","""","""",""56501205099227358"",""4910509981710642"""
	$stream.WriteLine($content)
	$content = "112,20120704," + $amount +",11402017,13700001,""85114020170000460201327048"",""" + $matches[$i++%$acntNo].tostring() + """,""NAZWA NADAWCY||ADRES NADAWCY|MIEJSCOWOSC NADAWCY"",""NAZWA I ADRES BENEFICJENTA||MIEJSCOWOSC BENEFICJENTA"",0,13701082,""TESTY PHUB ELIXIR IN"","""","""",""57002205099784829"","""""
	$stream.WriteLine($content)
	$content = "112,20120906," + $amount +",11402017,13700001,""85114020170000460201327048"",""" + $matches[$i++%$acntNo].tostring() + """,""NAZWA NADAWCY||UL.NADAWCY 35 M.13|92-546 LODZ"",""DANE BENEFICJENTA 35 M 13 BL. 410||92-546 MIASTO"",0,13701082,""TST POPR ELIXIR IN"","""","""",""59402205099714820"","""""
	$stream.WriteLine($content)
	$content = "114,20120509," + $amount +",12402076,13700001,""46124010531111001011846797"",""" + $matches[$i++%$acntNo].tostring() + """,""RUCH SA||WRONIA 23|00-958 WARSZAWA"",""FUNDACJA INSTYTUT SPRAW PUBLICZNYCH||SZPITALNA 5|00-031 WARSZAWA"",12401053,13701037,""TST POPR KLIENTOWSKI|0 PYORD/0000094850 20|/01/12/FVS 11/01/12/FVS"","""","""",""59701205099228012"",""0130509981709503"""
	$stream.WriteLine($content)
}
$stream.close()
}

$matches = @(get-content $filenameArgs | foreach-object { $_.split(";") -match "\d+" } )
for ($j=0; $j -lt $fileNo; $j++)
{
	$fileName = [string]::Format("{1}\elixir_{0}.txt", $j, $outPath)
	GenerateElixir $fileName $matches $j
	$msg = 	[string]::Format("wygenerowano pliki z komunikatami elixir w pliku {0}", $fileName)
	echo  $msg	
	}

