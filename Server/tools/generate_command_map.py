#!/usr/bin/env python3
"""Generate exact CMDID routing map from the oneof in protocol.protocol."""
import csv,json,os,sys
from pathlib import Path
from google.protobuf import descriptor_pb2,descriptor_pool
if os.environ.get('GITHUB_ACTIONS') != 'true':
    sys.exit('Protocol code generation is restricted to GitHub Actions; use `make codegen` from Server.')
ROOT=Path(__file__).resolve().parents[1]
fds=descriptor_pb2.FileDescriptorSet();fds.ParseFromString((ROOT/'internal/protocol/descriptor.pb').read_bytes())
pool=descriptor_pool.DescriptorPool()
for f in fds.file:pool.AddSerializedFile(f.SerializeToString())
env=pool.FindMessageTypeByName('protocol.protocol')
oneof=env.oneofs_by_name['msg']
rows=[]
for f in oneof.fields:
 name=f.message_type.name
 if 'C2S' in name: direction='c2s'
 elif 'S2C' in name: direction='s2c'
 else: direction='other'
 if f.number>=50000: scope='test'
 elif 5000<=f.number<=9042: scope='client'
 elif 1000<=f.number<=4999 and direction=='s2c':scope='server-notification'
 else:scope='internal'
 rows.append({'cmdId':f.number,'wrapperField':f.name,'message':'protocol.'+name,
              'messageName':name,'direction':direction,'scope':scope,
              'payloadField':f.name})
by_id={r['cmdId']:r for r in rows}
for r in rows:
 nxt=by_id.get(r['cmdId']+1)
 if r['direction']=='c2s' and nxt and nxt['direction']=='s2c':
  r['responseCmdId']=nxt['cmdId'];r['responseMessage']=nxt['message']
 else:
  r['responseCmdId']=None;r['responseMessage']=None
rows.sort(key=lambda x:x['cmdId'])
(ROOT/'internal/config/command-map.json').write_text(json.dumps(rows,ensure_ascii=False,indent=2),encoding='utf8')
with (ROOT/'internal/config/command-map.csv').open('w',encoding='utf8',newline='') as f:
 w=csv.writer(f);w.writerow(['cmd_id','direction','scope','message','response_cmd_id','response_message'])
 for r in rows:w.writerow([r['cmdId'],r['direction'],r['scope'],r['message'],r['responseCmdId'] or '',r['responseMessage'] or ''])
print(f'rows={len(rows)} public_request_pairs={sum(1 for x in rows if x["scope"]=="client" and x["direction"]=="c2s")} server_notifications={sum(1 for x in rows if x["scope"]=="server-notification")}')
