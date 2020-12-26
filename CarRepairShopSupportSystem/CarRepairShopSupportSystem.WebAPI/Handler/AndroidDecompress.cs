using System;

namespace CarRepairShopSupportSystem.WebAPI.Handler
{
    /// <summary>
    /// Summary description for AndroidDecompress
    /// Source: https://stackoverflow.com/questions/18997163/how-can-i-read-the-manifest-of-an-android-apk-file-using-c-sharp-net
    /// </summary>
    internal class AndroidDecompress
    {
        //Little Endian 32 bit word = 4 byte
        private const int byteCountOfSingleWord = 4;
        private const int stringIndexTableStartOffset = 0x24;

        private const int xmlTagStartSearchingOffset = 3;
        private const int numberOfStringsInStringTableOffset = 4;

        private const int numberOfXmlTagWord = 6;
        private const int numberOfAdditionalXmlStartTagWord = 3;
        private const int numberOfAttributeWord = 5;

        private const int stringIndexOfElementNameXmlTagWord = 5;
        private const int numberOfAttributesXmlTagWord = 7;

        private enum XmlTagCode : int
        {
            UnKnown = 0,
            StartTag = 0x00100102,
            EndTag = 0x00100103,
            EndDocumentTag = 0x00100101
        }

        private enum AttributeWord : int
        {
            StringIndexOfAttributeName = 1,
            StringIndexOfAttributeValue = 2,
            StringIndexOfAttributeValueOrResourceId = 4,
        }

        private readonly byte[] xml;
        private readonly int stringTableStartOffset;

        private int offset;
        private string result = string.Empty;

        public AndroidDecompress(byte[] xml)
        {
            this.xml = xml;
            int numberOfStrings
                = GetValueOfLittleEndian32BitWordAtOffset(numberOfStringsInStringTableOffset * byteCountOfSingleWord);
            stringTableStartOffset = stringIndexTableStartOffset + numberOfStrings * byteCountOfSingleWord;
            SetOffsetOfFirstTag();
        }

        internal string DecompressXML()
        {
            int indent = 0;
            while (offset < xml.Length)
            {
                XmlTagCode currentXmlTagCode = (XmlTagCode)GetValueOfLittleEndian32BitWordAtOffset(offset);
                int nameStringIndex = GetValueOfLittleEndian32BitWordAtOffset(GetOffsetSkippedBy(stringIndexOfElementNameXmlTagWord));

                if (currentXmlTagCode == XmlTagCode.StartTag)
                {
                    int numberOfAttributes 
                        = GetValueOfLittleEndian32BitWordAtOffset(GetOffsetSkippedBy(numberOfAttributesXmlTagWord));
                    SkipXmlStartTagWordOffset();

                    string attributes = string.Empty;
                    for (int ii = 0; ii < numberOfAttributes; ii++)
                    {
                        int attributeNameStringIndex 
                            = GetValueOfLittleEndian32BitWordAtOffset(GetOffsetSkippedBy((int)AttributeWord.StringIndexOfAttributeName));
                        int attributeValueStringIndex 
                            = GetValueOfLittleEndian32BitWordAtOffset(GetOffsetSkippedBy((int)AttributeWord.StringIndexOfAttributeValue));
                        int attributeResourceId 
                            = GetValueOfLittleEndian32BitWordAtOffset(GetOffsetSkippedBy((int)AttributeWord.StringIndexOfAttributeValueOrResourceId));

                        string attributeName = GetXmlStringStoredInStringTableByStringIndex(attributeNameStringIndex);
                        string attributeValue = attributeValueStringIndex != -1
                          ? GetXmlStringStoredInStringTableByStringIndex(attributeValueStringIndex)
                          : attributeResourceId.ToString();
                        attributes += $" {attributeName}=\"{attributeValue}\"";

                        SkipAttributeWordOffset();
                    }
                    PrintIndent(indent);
                    indent++;
                    string tagName = GetXmlStringStoredInStringTableByStringIndex(nameStringIndex);
                    Print($"<{tagName}{attributes}>");
                }
                else if (currentXmlTagCode == XmlTagCode.EndTag)
                {
                    indent--;
                    PrintIndent(indent);
                    string tagName = GetXmlStringStoredInStringTableByStringIndex(nameStringIndex);
                    Print($"</{tagName}>\r\n");

                    SkipXmlEndTagWordOffset();
                }
                else if (currentXmlTagCode == XmlTagCode.EndDocumentTag)
                {
                    break;
                }
                else
                {
                    Print($"  Unrecognized tag code '{currentXmlTagCode:X}' at offset {offset}");
                    break;
                }
            }
            return result;
        }

        internal long GetVersionCode()
        {
            SkipXmlStartTagWordOffset();
            return GetValueOfLittleEndian32BitWordAtOffset(GetOffsetSkippedBy((int)AttributeWord.StringIndexOfAttributeValueOrResourceId));
        }

        private int GetOffsetSkippedBy(int skipWordNumber)
        {
            return offset + skipWordNumber * byteCountOfSingleWord;
        }

        private void SkipXmlStartTagWordOffset()
        {
            offset += (numberOfXmlTagWord + numberOfAdditionalXmlStartTagWord) * byteCountOfSingleWord;
        }

        private void SkipAttributeWordOffset()
        {
            offset += numberOfAttributeWord * byteCountOfSingleWord;
        }

        private void SkipXmlEndTagWordOffset()
        {
            offset += numberOfXmlTagWord * byteCountOfSingleWord;
        }

        private void SetOffsetOfFirstTag()
        {
            for (int offset = GetValueOfLittleEndian32BitWordAtOffset(xmlTagStartSearchingOffset * byteCountOfSingleWord)
                ; offset < xml.Length - byteCountOfSingleWord
                ; offset += byteCountOfSingleWord)
            {
                if ((XmlTagCode)GetValueOfLittleEndian32BitWordAtOffset(offset) == XmlTagCode.StartTag)
                {
                    this.offset = offset;
                    return;
                }
            }
            throw new Exception("Not found first tag");
        }

        private string GetXmlStringStoredInStringTableByStringIndex(int stringIndex)
        {
            if (stringIndex < 0) return null;
            int startOffsetXmlString = GetStartOffsetXmlStringFromStringIndexTable(xml, stringIndex);
            return GetXmlStringStoredInStringTableAt(xml, startOffsetXmlString);
        }

        private int GetStartOffsetXmlStringFromStringIndexTable(byte[] xml, int stringIndex)
        {
            return stringTableStartOffset
                + GetValueOfLittleEndian32BitWordAtOffset(stringIndexTableStartOffset + stringIndex * byteCountOfSingleWord);
        }

        private string GetXmlStringStoredInStringTableAt(byte[] xml, int startOffsetXmlString)
        {
            int stringLength = xml[startOffsetXmlString + 1] << 8 & 0xff00 | xml[startOffsetXmlString] & 0xff;
            byte[] chars = new byte[stringLength];
            for (int ii = 0; ii < stringLength; ii++)
            {
                chars[ii] = xml[startOffsetXmlString + 2 + ii * 2];
            }
            return System.Text.Encoding.UTF8.GetString(chars);
        }

        private int GetValueOfLittleEndian32BitWordAtOffset(int offset)
        {
            byte lastMemoryCellValue = xml[offset + 3];
            byte thirdMemoryCellValue = xml[offset + 2];
            byte secondMemoryCellValue = xml[offset + 1];
            byte firstMemoryCellValue = xml[offset];
            int lastMemoryCellValueAfterLeftShift = lastMemoryCellValue << 24;
            int thirdMemoryCellValueAfterLeftShift = thirdMemoryCellValue << 16;
            int secondMemoryCellValueAfterLeftShift = secondMemoryCellValue << 8;
            long lastMemoryCellValueAfterAnd = lastMemoryCellValueAfterLeftShift & 0xff000000;
            long thirdMemoryCellValueAfterAnd = thirdMemoryCellValueAfterLeftShift & 0xff0000;
            long secondMemoryCellValueAfterAnd = secondMemoryCellValueAfterLeftShift & 0xff00;
            long firstMemoryCellValueAfterAnd = firstMemoryCellValue & 0xFF;
            return (int)(lastMemoryCellValueAfterAnd
                | thirdMemoryCellValueAfterAnd
                | secondMemoryCellValueAfterAnd
                | firstMemoryCellValueAfterAnd);
        }

        private void PrintIndent(int indent)
        {
            for (int i = 0; i < indent; i++)
            {
                Print("\t");
            }
        }

        private void Print(string text)
        {
            result += text;
        }
    }
}