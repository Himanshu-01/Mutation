"""
    LabelTagDefinitions.py - IDA script to label the tag definitions in IDA.

    Author: Grimdoomer

    Date: 4/8/2017
"""

import idaapi
import idautils
import idc
#from enum import Enum
import ctypes
import os.path


# Global variables.
g_h2langDll = None              # Handle to the h2alang.dll module.
g_user32Dll = None              # Handle to the user32.dll module.
g_BaseAddress = 0               # Base address of the loaded module.
g_TagGroupListAddress = 0       # Address of the list of tag group definitions.
g_TagGroupCount = 120


def MakeAndGetDword(ea, comment=None):
    """
    Creates a dword at the specified address and returns the dword value.
    :param ea: address to make dword at
    :param comment: optional comment to place at the specified address
    :return: the value of the dword at the specified address
    """

    # Make the dword.
    idc.MakeDword(ea)

    # Check if the comment is valid and if so place it at the address.
    if comment is not None:
        idc.MakeComm(ea, comment)

    # Get the dword value.
    return idc.Dword(ea)


def MakeAndGetWord(ea, comment=None):
    """
    Creates a word at the specified address and returns the word value.
    :param ea: address to make word at
    :param comment: optional comment to place at the specified address
    :return: the value of the word at the specified address
    """

    # Make the word value.
    idc.MakeWord(ea)

    # Check if the comment is valid and if so place it at the address.
    if comment is not None:
        idc.MakeComm(ea, comment)

    # Get the word value.
    return idc.Word(ea)


def MakeAndGetByte(ea, comment=None):
    """
    Creates a byte at the specified address and returns the byte value.
    :param ea: address to make byte at
    :param comment: optional comment to place at the specified address
    :return: the value of the byte at the specified address
    """

    # Make the byte value.
    idc.MakeByte(ea)

    # Check if the comment is valid and if so place it at the address.
    if comment is not None:
        idc.MakeComm(ea, comment)

    # Get the byte value.
    return idc.Byte(ea)


def MakeAndGetString(ea, length=-1, comment=None):
    """
    Creates a string at the specified address and returns the string value.
    :param ea: address to make string at
    :param comment: optional comment to place at the specified address
    :return: the value of the string at the specified address
    """

    # Make the string.
    if length is -1:
        idc.MakeStr(ea, idc.BADADDR)
    else:
        idc.MakeStr(ea, ea + length)

    # Check if the comment is valid and if so place it at the address.
    if comment is not None:
        idc.MakeComm(ea, comment)

    # Get the string value.
    return idc.GetString(ea, length)


def ReadString(ea):
    """
    Interprets ea as either an address or an ordinal and loads the string it points to accordingly.
    :param ea: address or ordinal of the string to be read
    :return: a string if the address or ordinal was valid
    """

    # Check if the address is within the module bounds.
    if ea == 0:
        return ""
    elif ea > g_BaseAddress:
        # The string is located in the module, read a null terminated string.
        return idc.GetString(ea)
    else:
        # The string is located in the h2alang.dll module.
        string = ctypes.create_string_buffer(500)
        if g_user32Dll.LoadStringA(g_h2langDll._handle, ea, string, 500) is 0:
            # Failed to load the string from the dll.
            idc.Message("Failed to load string %d from h2lang module!" % ea)

        # Return the string buffer.
        return string.value


class field_type():
    """
        field_type - HEK field type enum.
    """
    _field_string = 0
    _field_long_string = 1
    _field_string_id = 2
    _field_old_string_id = 3
    _field_char_integer = 4
    _field_short_integer = 5
    _field_long_integer = 6
    _field_angle = 7
    _field_tag = 8
    _field_char_enum = 9
    _field_enum = 10
    _field_long_enum = 11
    _field_long_flags = 12
    _field_word_flags = 13
    _field_byte_flags = 14
    _field_point_2d = 15
    _field_rectangle_2d = 16
    _field_rgb_color = 17
    _field_argb_color = 18
    _field_real = 19
    _field_real_fraction = 20
    _field_real_point_2d = 21
    _field_real_point_3d = 22
    _field_real_vector_2d = 23
    _field_real_vector_3d = 24
    _field_real_quaternion = 25
    _field_real_euler_angles_2d = 26
    _field_real_euler_angles_3d = 27
    _field_real_plane_2d = 28
    _field_real_plane_3d = 29
    _field_real_rgb_color = 30
    _field_real_argb_color = 31
    _field_real_hsv_color = 32
    _field_real_ahsv_color = 33
    _field_short_bounds = 34
    _field_angle_bounds = 35
    _field_real_bounds = 36
    _field_real_fraction_bounds = 37
    _field_tag_reference = 38
    _field_block = 39
    _field_long_block_flags = 40
    _field_word_block_flags = 41
    _field_byte_block_flags = 42
    _field_char_block_index1 = 43
    _field_char_block_index2 = 44
    _field_short_block_index1 = 45
    _field_short_block_index2 = 46
    _field_long_block_index1 = 47
    _field_long_block_index2 = 48
    _field_data = 49
    _field_vertex_buffer = 50
    _field_array_start = 51
    _field_array_end = 52
    _field_pad = 53
    _field_useless_pad = 54
    _field_skip = 55
    _field_explanation = 56
    _field_custom = 57
    _field_struct = 58
    _field_terminator = 59
    
    field_types = [
        "string",
        "long_string",
        "string_id",
        "old_string_id",
        "char_integer",
        "short_integer",
        "long_integer",
        "angle",
        "tag",
        "char_enum",
        "enum",
        "long_enum",
        "long_flags",
        "word_flags",
        "byte_flags",
        "point_2d",
        "rectangle_2d",
        "rgb_color",
        "argb_color",
        "real",
        "real_fraction",
        "real_point_2d",
        "real_point_3d",
        "real_vector_2d",
        "real_vector_3d",
        "real_quaternion",
        "real_euler_angles_2d",
        "real_euler_angles_3d",
        "real_plane_2d",
        "real_plane_3d",
        "real_rgb_color",
        "real_argb_color",
        "real_hsv_color",
        "real_ahsv_color",
        "short_bounds",
        "angle_bounds",
        "real_bounds",
        "real_fraction_bounds",
        "tag_reference",
        "block",
        "long_block_flags",
        "word_block_flags",
        "byte_block_flags",
        "char_block_index1",
        "char_block_index2",
        "short_block_index1",
        "short_block_index2",
        "long_block_index1",
        "long_block_index2",
        "data",
        "vertex_buffer",
        "array_start",
        "array_end",
        "pad",
        "useless_pad",
        "skip",
        "explanation",
        "custom",
        "struct",
        "terminator"
    ]


class tag_field:
    kSizeOf = 16

    # Fields.
    type = 0
    # word
    name_address = 0
    definition_address = 0
    group_tag = 0

    name_str = ""

    def readDefinition(self, ea):
        # Read all the fields from the stream.
        self.type = MakeAndGetWord(ea)
        idc.MakeWord(ea + 2)
        self.name_address = MakeAndGetDword(ea + 4)
        self.definition_address = MakeAndGetDword(ea + 8, "definition address")
        self.group_tag = MakeAndGetString(ea + 12, 4, "group tag")

        # Read strings.
        self.name_str = ReadString(self.name_address)

        # Comment the field type.
        idc.MakeComm(ea, field_type.field_types[self.type])

        # Check if we should comment the name string.
        if self.name_address < g_BaseAddress:
            idc.MakeComm(ea + 4, self.name_str)

        # Check the field type and read the definition accordingly.
        if self.type == field_type._field_block:
            # Read the tag block definition.
            tagBlock = tag_block_definition()
            tagBlock.readDefinition(self.definition_address)
        elif self.type == field_type._field_struct:
            # Ghetto hack to read the struct definition struct.
            MakeAndGetDword(self.definition_address)
            MakeAndGetDword(self.definition_address + 4, "group tag")
            MakeAndGetDword(self.definition_address + 8)
            blockAddress = MakeAndGetDword(self.definition_address + 12, "block definition address")

            # Read the tag block definition.
            tagBlock = tag_block_definition()
            tagBlock.readDefinition(blockAddress)


class tag_field_set_version:
    kSizeOf = 20

    # Fields.
    fields_address = 0
    index = 0
    upgrade_proc = 0
    # int
    size_of = 0

    def readDefinition(self, ea):
        # Read all the fields from the stream.
        self.fields_address = MakeAndGetDword(ea, "fields address")
        self.index = MakeAndGetDword(ea + 4, "index")
        self.upgrade_proc = MakeAndGetDword(ea + 8, "upgrade function")
        idc.MakeDword(ea + 12)
        self.size_of = MakeAndGetDword(ea + 16, "size of field set")


class tag_field_set:
    kSizeOf = 76

    # Fields.
    field_set_version = tag_field_set_version()
    size = 0
    alignment = 0
    parent_version_index = 0
    fields_address = 0
    size_string_address = 0

    size_str = ""

    fields = []

    def readDefinition(self, ea):
        # Read all of the fields from the stream.
        self.field_set_version.readDefinition(ea)
        self.size = MakeAndGetDword(ea + 20, "size of field set")
        self.alignment = MakeAndGetDword(ea + 24, "alignment bit")
        self.parent_version_index = MakeAndGetDword(ea + 28, "parent version index")
        self.fields_address = MakeAndGetDword(ea + 32, "fields address")
        self.size_string_address = MakeAndGetDword(ea + 36, "size string address")

        # Read the strings.
        self.size_str = ReadString(self.size_string_address)

        # Check if we should comment the size string.
        if self.size_string_address != 0:
            idc.MakeComm(ea + 36, self.size_str)

        # Loop through the fields until we reach the field_terminator.
        fieldType = 0
        i = 0
        while fieldType != field_type._field_terminator:
            # Read the field definition.
            field = tag_field()
            field.readDefinition(self.fields_address + (i * tag_field.kSizeOf))

            # Add the field to the fields list.
            self.fields.append(field)
            fieldType = field.type
            i += 1


class tag_block_definition:
    kSizeOf = 52

    # Fields.
    address = 0
    display_name_address = 0
    name_address = 0
    flags = 0
    maximum_element_count = 0
    maximum_element_count_string_address = 0
    field_sets_address = 0
    field_set_count = 0
    field_set_latest_address = 0
    #
    postprocess_proc = 0
    format_proc = 0
    generate_default_proc = 0
    dispose_element_proc = 0

    display_name_str = ""
    name_str = ""
    maximum_elements_count_str = ""

    field_sets = []

    def readDefinition(self, ea):
        # Read all the fields from the stream.
        self.address = ea
        self.display_name_address = MakeAndGetDword(ea)
        self.name_address = MakeAndGetDword(ea + 4)
        self.flags = MakeAndGetDword(ea + 8, "flags")
        self.maximum_element_count = MakeAndGetDword(ea + 12, "maximum element count")
        self.maximum_element_count_string_address = MakeAndGetDword(ea + 16, "maximum element count string")
        self.field_sets_address = MakeAndGetDword(ea + 20, "field sets")
        self.field_set_count = MakeAndGetDword(ea + 24, "number of field sets")
        self.field_set_latest_address = MakeAndGetDword(ea + 28, "latest field set")
        idc.MakeDword(ea + 32)
        self.postprocess_proc = MakeAndGetDword(ea + 36, "postprocessing function")
        self.format_proc = MakeAndGetDword(ea + 40, "format function")
        self.generate_default_proc = MakeAndGetDword(ea + 44, "generate default function")
        self.dispose_element_proc = MakeAndGetDword(ea + 48, "dispose element function")

        # Read the strings.
        self.display_name_str = ReadString(self.display_name_address)
        self.name_str = ReadString(self.name_address)
        self.maximum_elements_count_str = ReadString(self.maximum_element_count_string_address)

        # Name the definition address.
        idc.MakeNameEx(self.address, self.name_str, idc.SN_NON_PUBLIC)

        # Check if we should comment the display name.
        if self.display_name_address < g_BaseAddress:
            idc.MakeComm(ea, self.display_name_str)

        # Check if we should comment the name.
        if self.name_address < g_BaseAddress:
            idc.MakeComm(ea + 4, self.name_str)

        # Check if we should comment the maximum elements count.
        if self.maximum_element_count_string_address < g_BaseAddress:
            idc.MakeComm(ea + 12, self.maximum_elements_count_str)

        # Create the postprocess function.
        if self.postprocess_proc != 0:
            idc.MakeNameEx(self.postprocess_proc, "%s_postprocess" % self.name_str, idc.SN_NON_PUBLIC)

        # Create the format proc.
        if self.format_proc != 0:
            idc.MakeNameEx(self.format_proc, "%s_format" % self.name_str, idc.SN_NON_PUBLIC)

        # Create the generate default proc.
        if self.generate_default_proc != 0:
            idc.MakeNameEx(self.generate_default_proc, "%s_generate_default" % self.name_str, idc.SN_NON_PUBLIC)

        # Create the dispose element proc.
        if self.dispose_element_proc != 0:
            idc.MakeNameEx(self.dispose_element_proc, "%s_dispose_element" % self.name_str, idc.SN_NON_PUBLIC)

        # Check for the special case sound_block.
        if self.name_str == "sound_block":
            return

        # Loop through all of the field sets and read each one.
        for i in range(self.field_set_count):
            # Get the address of the current field set.
            fieldSetAddress = self.field_sets_address + (i * tag_field_set.kSizeOf)

            # Read the current field set.
            fieldSet = tag_field_set()
            fieldSet.readDefinition(fieldSetAddress)

            # Check if this is the latest version field set.
            if fieldSetAddress == self.field_set_latest_address:
                idc.MakeNameEx(fieldSetAddress, "%s_latest" % self.name_str, idc.SN_NON_PUBLIC)
            else:
                idc.MakeNameEx(fieldSetAddress, "%s_v%d" % (self.name_str, i), idc.SN_NON_PUBLIC)

            # Add the field set to the field set list.
            self.field_sets.append(fieldSet)


class tag_group_definition:
    kSizeOf = 112

    # Fields.
    address = 0
    name_address = 0
    flags = 0
    group_tag = 0
    parent_group_tag = 0
    version = 0
    initialized = 0
    # byte
    postprocess_proc = 0
    save_postprocess_proc = 0
    postprocess_for_sync_proc = 0
    # int
    definition_address = 0
    child_group_tags = []
    childs_count = 0
    # word
    default_tag_path_address = 0

    name_str = ""
    default_tag_path_str = ""
    definition = tag_block_definition()

    def readDefinition(self, ea):
        # Read all of the fields from the stream.
        self.address = ea
        self.name_address = MakeAndGetDword(ea)
        self.flags = MakeAndGetDword(ea + 4, "flags")
        self.group_tag = MakeAndGetString(ea + 8, 4, "group tag")
        self.parent_group_tag = MakeAndGetString(ea + 12, 4, "parent group tag")
        self.version = MakeAndGetWord(ea + 16, "version")
        self.initialized = MakeAndGetByte(ea + 18, "initialized")
        # byte
        self.postprocess_proc = MakeAndGetDword(ea + 20, "postprocess function")
        self.save_postprocess_proc = MakeAndGetDword(ea + 24, "save postprocess function")
        self.postprocess_for_sync_proc = MakeAndGetDword(ea + 28, "postprocess for sync function")
        # int
        self.definition_address = MakeAndGetDword(ea + 36)
        for i in range(16):
            self.child_group_tags.append(MakeAndGetString(ea + 40 + (i * 4), 4, "child group tag"))
        self.childs_count = MakeAndGetWord(ea + 104, "childs count")
        # word
        self.default_tag_path_address = MakeAndGetDword(ea + 108, "default tag path")

        # Read the strings.
        self.name_str = ReadString(self.name_address)
        self.default_tag_path_str = ReadString(self.default_tag_path_address)

        # Name the definition address.
        idc.MakeNameEx(ea, self.name_str, idc.SN_NON_PUBLIC)

        # Check if we should comment the name.
        if self.name_address < g_BaseAddress:
            idc.MakeComm(ea, self.name_str)

        # Check if we should comment the default tag path address.
        if self.default_tag_path_address < g_BaseAddress:
            idc.MakeComm(ea, self.default_tag_path_str)

        # Check if we should label the postprocess function.
        if self.postprocess_proc != 0:
            idc.MakeNameEx(self.postprocess_proc, "%s_postprocess" % self.name_str, idc.SN_NON_PUBLIC)

        # Check if we should label the save postprocess function.
        if self.save_postprocess_proc != 0:
            idc.MakeNameEx(self.save_postprocess_proc, "%s_save_postprocess" % self.name_str, idc.SN_NON_PUBLIC)

        # Check if we should label the postprocess for sync function.
        if self.postprocess_for_sync_proc != 0:
            idc.MakeNameEx(self.postprocess_for_sync_proc, "%s_postprocess_for_sync" % self.name_str, idc.SN_NON_PUBLIC)

        # Read the tag block definition.
        self.definition.readDefinition(self.definition_address)



def labelTagDefinitions():
    # Label the tag group layout address.
    idc.MakeNameEx(g_TagGroupListAddress, "g_TagGroupDefinitionTable", idc.SN_NON_PUBLIC)

    # Loop through all of the tag definitions and label each one.
    for i in range(g_TagGroupCount):
        # Get the address for the current tag group layout.
        address = idc.Dword(g_TagGroupListAddress + (i * 4))

        # Read and label the tag definition.
        tagGroup = tag_group_definition()
        tagGroup.readDefinition(address)


def initModules():
    global g_h2langDll
    global g_user32Dll
    global g_BaseAddress
    global g_TagGroupListAddress

    # Get the idb folder which will contains the h2alang.dll file.
    modulePath = idautils.GetIdbDir() + "h2alang.dll"

    # Check if the h2alang.dll module exists.
    if os.path.isfile(modulePath) is False:
        # The file does not exist, display a message to the user.
        idaapi.warning("Could not find \"%s\"!" % modulePath)
        return False

    try:
        # Load the h2alang.dll module.
        g_h2langDll = ctypes.cdll.LoadLibrary(modulePath)

        # Try to load the user32.dll module.
        g_user32Dll = ctypes.OleDLL("user32.dll")
    except Exception:
        # Failed to load the h2alang.dll file.
        idaapi.warning("Failed to load \"%s\"!" % modulePath)
        return False

    # Check the input file name to determine which hek tool we have loaded.
    moduleName = idc.GetInputFile()
    if moduleName == "H2Guerilla.exe":
        # Load values for h2 guerilla executable.
        g_BaseAddress = 0x400000
        g_TagGroupListAddress = 0x00901B90
    elif moduleName == "H2Sapien.exe":
        # Load values for h2 sapien executable.
        idaapi.warning("H2Sapien currently not supported!")
        return False
    elif moduleName == "H2Tool.exe":
        # Load values for h2 tool executable.
        idaapi.warning("H2Tool currently not supported!")
        return False
    else:
        # Unknown module.
        idaapi.warning("Unknown HEK module loaded!")
        return False

    # Initialized successfully.
    return True

# Initialize the module.
if initModules() == False:
    quit()

# Label all of the tag group definitions.
labelTagDefinitions()
