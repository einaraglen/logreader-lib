// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: RangeRequest.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace LogReaderLibrary.Models.Proto {

  /// <summary>Holder for reflection information generated from RangeRequest.proto</summary>
  public static partial class RangeRequestReflection {

    #region Descriptor
    /// <summary>File descriptor for RangeRequest.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static RangeRequestReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChJSYW5nZVJlcXVlc3QucHJvdG8iOQoMUmFuZ2VSZXF1ZXN0Eg8KB3NpZ25h",
            "bHMYASADKAkSDAoEZnJvbRgCIAEoBBIKCgJ0bxgDIAEoBEIgqgIdTG9nUmVh",
            "ZGVyTGlicmFyeS5Nb2RlbHMuUHJvdG9iBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::LogReaderLibrary.Models.Proto.RangeRequest), global::LogReaderLibrary.Models.Proto.RangeRequest.Parser, new[]{ "Signals", "From", "To" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class RangeRequest : pb::IMessage<RangeRequest>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<RangeRequest> _parser = new pb::MessageParser<RangeRequest>(() => new RangeRequest());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<RangeRequest> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::LogReaderLibrary.Models.Proto.RangeRequestReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public RangeRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public RangeRequest(RangeRequest other) : this() {
      signals_ = other.signals_.Clone();
      from_ = other.from_;
      to_ = other.to_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public RangeRequest Clone() {
      return new RangeRequest(this);
    }

    /// <summary>Field number for the "signals" field.</summary>
    public const int SignalsFieldNumber = 1;
    private static readonly pb::FieldCodec<string> _repeated_signals_codec
        = pb::FieldCodec.ForString(10);
    private readonly pbc::RepeatedField<string> signals_ = new pbc::RepeatedField<string>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pbc::RepeatedField<string> Signals {
      get { return signals_; }
    }

    /// <summary>Field number for the "from" field.</summary>
    public const int FromFieldNumber = 2;
    private ulong from_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ulong From {
      get { return from_; }
      set {
        from_ = value;
      }
    }

    /// <summary>Field number for the "to" field.</summary>
    public const int ToFieldNumber = 3;
    private ulong to_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ulong To {
      get { return to_; }
      set {
        to_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as RangeRequest);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(RangeRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!signals_.Equals(other.signals_)) return false;
      if (From != other.From) return false;
      if (To != other.To) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      hash ^= signals_.GetHashCode();
      if (From != 0UL) hash ^= From.GetHashCode();
      if (To != 0UL) hash ^= To.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      signals_.WriteTo(output, _repeated_signals_codec);
      if (From != 0UL) {
        output.WriteRawTag(16);
        output.WriteUInt64(From);
      }
      if (To != 0UL) {
        output.WriteRawTag(24);
        output.WriteUInt64(To);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      signals_.WriteTo(ref output, _repeated_signals_codec);
      if (From != 0UL) {
        output.WriteRawTag(16);
        output.WriteUInt64(From);
      }
      if (To != 0UL) {
        output.WriteRawTag(24);
        output.WriteUInt64(To);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      size += signals_.CalculateSize(_repeated_signals_codec);
      if (From != 0UL) {
        size += 1 + pb::CodedOutputStream.ComputeUInt64Size(From);
      }
      if (To != 0UL) {
        size += 1 + pb::CodedOutputStream.ComputeUInt64Size(To);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(RangeRequest other) {
      if (other == null) {
        return;
      }
      signals_.Add(other.signals_);
      if (other.From != 0UL) {
        From = other.From;
      }
      if (other.To != 0UL) {
        To = other.To;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            signals_.AddEntriesFrom(input, _repeated_signals_codec);
            break;
          }
          case 16: {
            From = input.ReadUInt64();
            break;
          }
          case 24: {
            To = input.ReadUInt64();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            signals_.AddEntriesFrom(ref input, _repeated_signals_codec);
            break;
          }
          case 16: {
            From = input.ReadUInt64();
            break;
          }
          case 24: {
            To = input.ReadUInt64();
            break;
          }
        }
      }
    }
    #endif

  }

  #endregion

}

#endregion Designer generated code
