<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet 
    version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    exclude-result-prefixes="msxsl"
    >

    <xsl:output method="xml" indent="yes" omit-xml-declaration="yes"/>
    <xsl:strip-space elements="*"/>

    <xsl:template match="/">
        <xsl:element name="root">
            <xsl:for-each select="//tc[@name='boot']">
                <xsl:call-template name="get-result">
                    <xsl:with-param name="boot-val" select="."/>
                </xsl:call-template>
            </xsl:for-each>
        </xsl:element>
    </xsl:template>

    <xsl:template name="get-result">
        <xsl:param name="boot-val"/>

        <xsl:call-template name="print-result">
            <xsl:with-param name="boot-val" select="$boot-val"/>
        </xsl:call-template>
    </xsl:template>

    <xsl:template name="print-result">
        <xsl:param name="boot-val"/>

        <xsl:element name="{$boot-val}">
            
        </xsl:element>
    </xsl:template>
</xsl:stylesheet>