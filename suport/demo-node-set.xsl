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

        <xsl:variable name="ns1" select="//tc[@name='boot'][text()=$boot-val]/following-sibling::*"/>
        <xsl:variable name="ns1-next-rev" select="//tc[@name='boot'][text()=$boot-val]/following-sibling::tc[@name='boot'][1]/preceding-sibling::*"/>
        <!-- intersection of two node sets with union -->
        <!--<xsl:variable name="ns2" select="$ns1[count(. | $ns1-next-rev) = count($ns1-next-rev)]"/>-->

        <!-- result tree fragment -->
        <xsl:variable name="ns1-rtf">
            <xsl:value-of select="$ns1"/>
        </xsl:variable>
        <xsl:variable name="ns2" select="msxsl:node-set($ns1-rtf)/tc[@name='boot'][1]/preceding-sibling::*"/>
        <xsl:choose>
            <xsl:when test="count($ns2) &gt; 0">
                <xsl:call-template name="print-result">
                    <xsl:with-param name="boot-val" select="$boot-val"/>
                    <xsl:with-param name="ns" select="$ns2"/>
                </xsl:call-template>
            </xsl:when>
            <xsl:otherwise>
                <xsl:call-template name="print-result">
                    <xsl:with-param name="boot-val" select="$boot-val"/>
                    <xsl:with-param name="ns" select="$ns1"/>
                </xsl:call-template>
            </xsl:otherwise>
        </xsl:choose>

    </xsl:template>

    <xsl:template name="print-result">
        <xsl:param name="boot-val"/>
        <xsl:param name="ns"/>

        <xsl:element name="{$boot-val}">
            <xsl:for-each select="$ns">
                <xsl:variable name="name">
                    <xsl:value-of select="@name"/>
                </xsl:variable>

                <xsl:element name="{$name}">
                    <xsl:value-of select="."/>
                </xsl:element>
            </xsl:for-each>
        </xsl:element>
    </xsl:template>
</xsl:stylesheet>